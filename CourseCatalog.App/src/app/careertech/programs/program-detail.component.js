//program-detail.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;
    ctrl.cache = {};

    ctrl.$onInit = function () {
        ctrl.isAdmin = (ctrl.isAdmin === 'true');
        fetchProgram(ctrl.programId).then(r => {
            ctrl.title = 'Program: ' + ctrl.program.name + ' (' + ctrl.program.programCode + ')';
        }).finally(f => {
            if (ctrl.program !== undefined) {
                ctrl.listOptions = {
                    dataSource: ctrl.program.credentials,
                    searchEnabled: true,
                    searchExpr: "credential",
                    noDataText: 'No Credentials Assigned'
                }
            }
        });
    };

    ctrl.$onChanges = function () {
    };

    function fetchProgram(programId) {
        return $http.get('/api/programs/' + programId).then(r => {
            ctrl.program = r.data;
            return ctrl.program = r.data;

        });
    }
    
}

module.component('programDetail',
    {
        bindings: {
            programId: '<', 
            isAdmin: '@'
        },
        templateUrl: '/src/app/careertech/programs/program-detail.component.html',
        controller: ['$http', controller]
    });
