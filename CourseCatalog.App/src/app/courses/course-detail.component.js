//course-detail.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;

    ctrl.$onInit = function () {
        ctrl.isAdmin = ctrl.isAdmin == "true";

        ctrl.loadCourse(ctrl.courseId).then(function () {
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

    ctrl.$onChanges = function () {
        ctrl.isAdmin = ctrl.isAdmin == "true";
    }

    ctrl.createDraft = function () {
        $http.post(`/api/drafts/${ctrl.courseId}/create`).then(r => {
            toastr.success(`Created draft ${ctrl.course.courseNumber}`);
            window.location.href = `/drafts/${r.data}`;
        }).catch(err => {
            toastr.error(err.data.exceptionMessage);
            console.log("err", err);
        });
    };

    ctrl.loadCourse = function (courseNumber) {
        return $http.get(`/api/courses/${courseNumber}`).then(r => {
            ctrl.course = r.data;
            ctrl.programs = r.data.programAssignments;
        }).catch(function (err) {
            console.error(err.message);
        });
    };

}

module.component("courseDetail",
    {
        bindings: {
            courseId: "@",
            isAdmin: "@"
        },
        templateUrl: "/src/app/courses/course-detail.component.html",
        controller: ["$http", controller]
    });