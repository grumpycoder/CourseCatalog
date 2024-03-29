﻿//course-careertech-edit.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;

    ctrl.$onInit = function() {
        loadRefs();
    };

    function loadRefs() {
        fetchSchoolYears();
        fetchPrograms();
    }

    function fetchSchoolYears() {
        return $http.get("/api/refs/schoolyears").then(function(r) {
            return ctrl.schoolYears = r.data;
        });
    }

    function fetchPrograms() {
        return $http.get("/api/refs/programs").then(r => {
            ctrl.programs = r.data;
            ctrl.programsStore = new DevExpress.data.ArrayStore({
                data: r.data,
                key: "id",
                reshapeOnPush: true
            });
            ctrl.programsListOptions = {
                dataSource: ctrl.programsStore,
                searchEnabled: true,
                searchExpr: "description",
                displayExpr: "name",
                valueExpr: "id"
            };
        });
    }

    ctrl.removeProgram = function(item) {
        var idx = ctrl.course.programAssignments.indexOf(item);
        const url = `/api/courses/${ctrl.course.id}/programs/${item.programId}`;
        $http.delete(url)
            .then(r => {
                toastr.success("Removed Program");
                ctrl.course.programAssignments.splice(idx, 1);
            }).catch(e => {
                console.error(e);
                toastr.error(e.message);
            });
    };

    ctrl.addProgram = function() {
        const data = JSON.stringify(ctrl.programAssignment);
        $http.post(`/api/courses/${ctrl.course.id}/programs`, data)
            .then(r => {
                ctrl.course.programAssignments.push(r.data);
                ctrl.programAssignment.programId = undefined;
                toastr.success("Added Program");
            }).catch(e => {
                console.error(e);
                toastr.error(e.message);
            });
    };
}

module.component("courseCareertechEdit",
    {
        bindings: {
            course: "<"
        },
        templateUrl: "/src/app/courses/course-careertech-edit.html",
        controller: ["$http", controller]
    });