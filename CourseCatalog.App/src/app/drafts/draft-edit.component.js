﻿//draft-edit.component.js

var module = angular.module("app");

function controller($http, userService) {
    var ctrl = this;

    ctrl.$onInit = function() {
        userService.userDetails().then(r => {
            ctrl.user = r;
            ctrl.isAdmin = ctrl.user.groups.some(g => g.groupName === "Admin") ||
                ctrl.user.groups.some(g => g.groupName === "CourseAdmin");
            ctrl.isCourseAdmin = ctrl.isAdmin;
            ctrl.isTCertAdmin = ctrl.user.groups.some(g => g.groupName === "TeacherCertAdmin") || ctrl.isAdmin;
            ctrl.isCareerTechAdmin = ctrl.user.groups.some(g => g.groupName === "CareerTechAdmin") || ctrl.isAdmin;
        });

        if (ctrl.courseId == -1) {
            ctrl.title = "New Draft";
            return;
        }

        ctrl.fetchCourse(ctrl.courseId).then(function() {
            if (!ctrl.course) {
                ctrl.title = "No Draft Found";
                return;
            }

            const courseNumber = ctrl.course.courseNumber ? ctrl.course.courseNumber : "No Course Number";
            ctrl.title = ctrl.course.name + " (" + courseNumber + ")";
        });
    };

    ctrl.fetchCourse = function(courseId) {
        return $http.get(`/api/drafts/${courseId}`).then(r => {
            r.data.creditHours = r.data.creditHours.toFixed(2);
            ctrl.course = r.data;
        }).catch(function(err) {
            if (err.status == "404") {
                toastr.error("Draft Not Found");
            } else {
                toastr.error(err.data.message);
                ctrl.errorMessage = err.data.message;
                console.error("error", err);
            }
        });

    };
}

module.component("draftEdit",
    {
        bindings: {
            courseId: "@"
        },
        templateUrl: "/src/app/drafts/draft-edit.component.html",
        controller: ["$http", "userService", controller]
    });