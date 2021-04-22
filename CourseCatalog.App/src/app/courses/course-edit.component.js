//course-edit.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;

    ctrl.$onInit = function() {
        ctrl.fetchCourse(ctrl.courseNumber).then(function() {
            const courseNumber = ctrl.course.courseNumber ? ctrl.course.courseNumber : "No Course Number";
            ctrl.title = ctrl.course.name + " (" + courseNumber + ")";
        });
    };

    ctrl.fetchCourse = function(courseNumber) {
        return $http.get(`/api/courses/${courseNumber}`).then(r => {
            r.data.creditHours = r.data.creditHours.toFixed(2);
            ctrl.course = r.data;
        }).catch(function(err) {
            toastr.error(err.message);
            console.log("err", err.message);
        });

    };
}

module.component("courseEdit",
    {
        bindings: {
            courseNumber: "@"
        },
        templateUrl: "/src/app/courses/course-edit.component.html",
        controller: ["$http", controller]
    });