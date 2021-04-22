//dashboard-home.component.js

var module = angular.module("app");

function controller($http) {

    var ctrl = this;

    ctrl.$onInit = function() {

        ctrl.title = "Course Catalog";

        fetchCareertechSummary().then(r => {
            ctrl.activeProgramsCount = r.activeProgramsCount;
            ctrl.activeCredentialsCount = r.activeCredentialsCount;
        });

        fetchCoursesSummary().then(r => {
            ctrl.activeCourseCount = r.activeCourseCount;
            ctrl.draftCount = r.draftCount;
        });

    };

    function fetchCareertechSummary() {
        return $http.get("/api/programs/summary").then(r => {
            return r.data;
        });
    }

    function fetchCoursesSummary() {
        return $http.get("/api/courses/summary").then(r => {
            return r.data;
        });
    }

}

module.component("dashboard",
    {
        templateUrl: "/src/app/dashboard/dashboard-home.component.html",
        controller: ["$http", controller]
    });