//course-teachers-list.component.js

var module = angular.module('app');

function controller($http) {

    var ctrl = this;
    ctrl.selectedCourse = {
        courseId: null 
    }
    
    ctrl.$onInit = function () {
        ctrl.title = 'Courses Teachers'; 

        fetchEndorsements().then(r => {
            ctrl.endorsementListOptions = {
                dataSource: DevExpress.data.AspNet.createStore({
                    key: "courseId",
                    loadUrl: '/api/courses'
                }), 
                height: 490, 
                searchEnabled: true,
                searchExpr: ['name', 'courseNumber'],
                searchEditorOptions: {
                    placeholder: 'Search courses...'
                }, 
                onItemClick: function (e) {
                    ctrl.selectedCourse = e.itemData;
                    ctrl.selectedCourseId = e.itemData.courseId; 

                    getTeachers(e.itemData.courseId);

                }
            }
        });

    };

    function fetchEndorsements() {
        return $http.get('/api/refs/endorsements').then(r => {
            ctrl.endorsements = r.data;
        });
    }

    ctrl.dataSource = DevExpress.data.AspNet.createStore({
        key: 'tchNumber',
        loadUrl: '/api/courses/' + ctrl.selectedCourseId + '/teachers',
        onBeforeSend: function(r, s) {
            s.url = '/api/courses/' + ctrl.selectedCourseId + '/teachers'
        }
    }); 

    function getTeachers(courseId) {
        ctrl.teacherListOptions = {
            dataSource: ctrl.dataSource, 
            height: 490, 
            searchEnabled: true,
            searchExpr: ['fullName', 'tchNumber'],
            searchEditorOptions: {
                placeholder: 'Search teachers...'
            } 
        }

        $('#teacherList').dxList("instance").option('dataSource', ctrl.dataSource);  
    }

}

module.component('courseTeachersList',
    {
        bindings: {
        },
        templateUrl: '/src/app/courses/course-teachers-list.component.html',
        controller: ['$http', controller]
    });

