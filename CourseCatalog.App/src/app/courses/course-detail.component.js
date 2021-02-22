//course-detail.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.$onInit = function () {

        ctrl.loadCourse(ctrl.courseId).then(function () {

            var courseNumber = ctrl.course.courseNumber ? ctrl.course.courseNumber : 'No Course Number'; 
            ctrl.title = ctrl.course.name + ' (' + courseNumber + ')';

            ctrl.listOptions = {
                dataSource: ctrl.course.programs,
                searchEnabled: true,
                searchExpr: ['name', 'programCode'],
                noDataText: 'No Programs Assigned'
            }

        });
    };

    ctrl.$onChanges = function () {

    }

    ctrl.loadCourse = function (courseNumber) {
        return $http.get('/api/courses/' + courseNumber).then(r => {
            ctrl.course = r.data;
            ctrl.programs = r.data.programAssignments;
        }).catch(function (err) {
            console.error(err.message);
        });
    };

}

module.component('courseDetail',
    {
        bindings: {
            courseId: '@'
        },
        templateUrl: '/src/app/courses/course-detail.component.html',
        controller: ['$http', controller]
    });