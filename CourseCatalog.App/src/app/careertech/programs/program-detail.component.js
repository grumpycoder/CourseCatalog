//program-detail.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;
    ctrl.cache = {};

    ctrl.$onInit = function () {

        fetchProgram(ctrl.programcode).then(r => {
            ctrl.title = 'Program: ' + ctrl.program.name + ' (' + ctrl.program.programCode + ')';
        }).finally(f => {
            if (ctrl.program !== undefined) {
                ctrl.listOptions = {
                    dataSource: ctrl.program.programCredentials,
                    searchEnabled: true,
                    searchExpr: "credential",
                    noDataText: 'No Credentials Assigned'
                }
            }
        });
    };

    ctrl.$onChanges = function () {
    };

    function fetchProgram(programCode) {
        return $http.get('/api/programs/' + pad(programCode, 3)).then(r => {
            ctrl.program = r.data;
            return ctrl.program = r.data;

        });
    }
    
    function pad(num, size) {
        var s = num + "";
        while (s.length < size) s = "0" + s;
        return s;
    }
}

module.component('programDetail',
    {
        bindings: {
            programcode: '<'
        },
        templateUrl: '/src/app/careertech/programs/program-detail.component.html',
        controller: ['$http', controller]
    });
