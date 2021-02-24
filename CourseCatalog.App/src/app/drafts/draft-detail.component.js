//draft-detail.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.$onInit = function () {
        //ctrl.processing = true; 
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
        return $http.get('/api/drafts/' + courseNumber).then(r => {
            ctrl.course = r.data;
            ctrl.programs = r.data.programAssignments;

            ctrl.canPublish = ctrl.course.courseStatus !== 'Published';
            ctrl.canEdit = (ctrl.course.canEdit === true) && (ctrl.canPublish === true);

            if (!ctrl.canPublish) ctrl.publishMessage = 'Course Published';
        }).catch(function (err) {
            console.error(err.message);
        });
    };

    ctrl.publish = function () {
        ctrl.processing = true; 
        $http.post('/api/drafts/publish/' + ctrl.course.draftId).then(r => {
            //TODO: Disable publish and edit button
            toastr.success('Published', ctrl.course.courseNumber + ' Successfully Published',
                {
                    timeOut: 1000,
                    onHidden: function () {
                        window.location.href = '/drafts';
                    }
                });
        }).catch(e => {
            console.error('error', e);
            toastr.error(e.data.message);
        });
    }

}

module.component('draftDetail',
    {
        bindings: {
            courseId: '@'
        },
        templateUrl: '/src/app/drafts/draft-detail.component.html',
        controller: ['$http', controller]
    });