//teacher-course-list.component.js

var module = angular.module('app');

function controller($http) {

    var ctrl = this;
    ctrl.selectedTeacher = {
        teacherId: null
    }

    ctrl.$onInit = function () {
        ctrl.title = 'Teacher Courses';

        fetchTeachers().then(r => {
            console.log('fetch teachers');
            ctrl.teacherListOptions = {
                dataSource: new DevExpress.data.DataSource({
                    store: DevExpress.data.AspNet.createStore({
                        loadUrl: '/api/certification/certholders'
                    }), 
                    pageSize: 10, 
                    remoteOperations: true, 
                }), 
                height: 490,
                searchTimeout: 1000, 
                searchEnabled: true,
                searchExpr: ['printName'],
                searchEditorOptions: {
                    placeholder: 'Search teachers...', 
                },
                onItemClick: function (e) {
                    ctrl.selectedTeacher = e.itemData;
                    ctrl.selectedTeacherId = e.itemData.certholderId;
                    getCertholderCourses(e.itemData.certholderId);
                }
            }
        });

    };

    function fetchTeachers() {
        return $http.get('/api/refs/endorsements').then(r => {
            ctrl.endorsements = r.data;
        });
    }

    function getCertholderCourses(certholderId) {
        return $http.get('/api/certification/' + ctrl.selectedTeacherId + '/courses').then(r => {
            ctrl.courses = r.data;
            ctrl.courseListOptions = {
                dataSource: ctrl.courses,
                height: 490,
                pageLoadMode: "scrollBottom", 
                searchEnabled: true,
                searchExpr: ['name', 'courseNumber'],
                searchEditorOptions: {
                    placeholder: 'Search courses...'
                }
            }

            $('#courseList').dxList("instance").option('dataSource', ctrl.courses);

        });
    }

}

module.component('teacherCourseList',
    {
        bindings: {
        },
        templateUrl: '/src/app/courses/teacher-course-list.component.html',
        controller: ['$http', controller]
    });

