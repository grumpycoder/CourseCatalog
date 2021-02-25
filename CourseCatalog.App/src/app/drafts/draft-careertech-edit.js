//draft-careertech-edit.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.$onInit = function () {
        loadRefs(); 
    }
    
    function loadRefs() {
        fetchSchoolYears();
        fetchPrograms();
    }

    function fetchSchoolYears() {
        return $http.get('/api/refs/schoolyears').then(function (r) {
            return ctrl.schoolYears = r.data;
        });
    }

    function fetchPrograms() {
        return $http.get('/api/refs/programs').then(r => {
            ctrl.programs = r.data;
            ctrl.programsStore = new DevExpress.data.ArrayStore({
                data: r.data,
                key: 'programId',
                reshapeOnPush: true
            });
            ctrl.programsListOptions = {
                dataSource: ctrl.programsStore,
                searchEnabled: true,
                searchExpr: "description",
                displayExpr: 'name',
                valueExpr: 'programId'
            }
        });
    }

    ctrl.removeProgram = function(item) {
        var idx = ctrl.course.programs.indexOf(item);
        var url = '/api/drafts/' + ctrl.course.draftId + '/programs/' + item.programId; 
        $http.delete(url)
            .then(r => {
                toastr.success('Removed ' + item.programCode);
                ctrl.course.programs.splice(idx, 1);
            }).catch(e => {
                toastr.error(err.data.message);
                console.error(e);
            }); 
    }

    ctrl.addProgram = function() {
        var dto = {
            draftId: ctrl.course.draftId, 
            programId: ctrl.programAssignment.programId, 
            beginYear: ctrl.programAssignment.beginYear, 
            endYear: ctrl.programAssignment.endYear
        }

        var url = '/api/drafts/assignprogram'; 
        $http.post(url, dto)
            .then(r => {
                ctrl.course.programs.push(r.data);
                ctrl.programAssignment.programId = undefined; 
                toastr.success('Added ' + r.data.programCode);
            }).catch(err => {
                console.error('error', err);
                toastr.error(err.data.message);
            }); 
    }
}

module.component('draftCareertechEdit',
    {
        bindings: {
            course: '<'
        },
        templateUrl: '/src/app/drafts/draft-careertech-edit.html',
        controller: ['$http', controller]
    });