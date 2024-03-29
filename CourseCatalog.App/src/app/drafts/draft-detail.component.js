﻿//draft-detail.component.js

var module = angular.module("app");

function controller($http, userService) {
    var ctrl = this;

    ctrl.$onInit = function() {
        userService.userDetails().then(r => {
            if (r !== null) {
                ctrl.user = r;
                ctrl.isPublisher = ctrl.user.groups.some(g => g.groupName === "Publisher"); 

                ctrl.isAdmin = ctrl.user.groups.some(g => g.groupName === "Admin") ||
                    ctrl.user.groups.some(g => g.groupName === "CourseAdmin");
                ctrl.isCourseAdmin = ctrl.isAdmin;
                ctrl.isTCertAdmin = ctrl.user.groups.some(g => g.groupName === "TeacherCertAdmin") || ctrl.isAdmin;
                ctrl.isCareerTechAdmin = ctrl.user.groups.some(g => g.groupName === "CareerTechAdmin") || ctrl.isAdmin;
            }
        });

        ctrl.loadCourse(ctrl.courseId).then(function() {
            const courseNumber = ctrl.course.courseNumber ? ctrl.course.courseNumber : "No Course Number";
            ctrl.title = ctrl.course.name + " (" + courseNumber + ")";
            ctrl.listOptions = {
                dataSource: ctrl.course.programs,
                searchEnabled: true,
                searchExpr: ["name", "programCode"],
                noDataText: "No Programs Assigned"
            };

        });
    };

    ctrl.$onChanges = function() {
    };

    ctrl.loadCourse = function(courseNumber) {
        return $http.get(`/api/drafts/${courseNumber}`).then(r => {
            ctrl.course = r.data;
            ctrl.programs = r.data.programAssignments;
        }).catch(function(err) {
            console.error(err.message);
        });
    };

    ctrl.publish = function() {
        ctrl.isProcessing = true;
        $http.post(`/api/drafts/publish/${ctrl.course.draftId}`).then(r => {
            toastr.success("Published",
                ctrl.course.courseNumber + " Successfully Published",
                {
                    timeOut: 1000,
                    onHidden: function() {
                        window.location.href = "/drafts";
                    }
                });
        }).catch(e => {
            console.error("error", e);
            ctrl.isProcessing = false;
            toastr.error(e.data.exceptionMessage);
        });
    };

}

module.component("draftDetail",
    {
        bindings: {
            courseId: "@"
        },
        templateUrl: "/src/app/drafts/draft-detail.component.html",
        controller: ["$http", "userService", controller]
    });