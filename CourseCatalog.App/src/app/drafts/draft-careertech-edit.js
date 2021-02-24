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
        console.log('item', item);
        var idx = ctrl.course.programs.indexOf(item);
        var url = '/api/drafts/' + ctrl.course.draftId + '/programs/' + item.programId; 
        $http.delete(url)
            .then(r => {
                //TODO: toastr message
                ctrl.course.programs.splice(idx, 1);
            }).catch(e => {
                //TODO: toastr message
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
        
        $http.post('/api/drafts/assignprogram', dto)
            .then(r => {
                console.log('r', r);
                ctrl.course.programs.push(r.data);
                ctrl.programAssignment.programId = undefined; 
                //TODO: toastr message
            }).catch(e => {
                console.error(e);
                //TODO: toastr message
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