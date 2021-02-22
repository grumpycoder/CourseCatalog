//program-edit.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;
    ctrl.cache = {};

    ctrl.$onInit = function () {
        fetchProgram(ctrl.programcode).then(r => {
            ctrl.title = 'Program: ' + ctrl.program.name + ' (' + ctrl.program.programCode + ')';
        }).finally(f => {
            if (ctrl.program) {
                ctrl.listOptions = {
                    dataSource: ctrl.program.programCredentials,
                    searchEnabled: true,
                    searchExpr: "credential",
                    allowItemDeleting: true,
                    onItemDeleting: function (item) {
                        var d = $.Deferred();
                        var url = '/api/programs/' + item.itemData.programId + '/credentials/' + item.itemData.credentialId;
                        $http.delete(url).then(r => {
                            //TODO: Toastr success message
                            return r;
                        }).catch(e => {
                            //TODO: Toastr error message 
                            console.error(e.data);
                            toastr.error(e.data.exceptionMessage);
                        });
                        data.cancel = d.promise();
                    }
                }
            }
        });
        loadRefs();
    };

    ctrl.$onChanges = function () {
    };

    ctrl.validateTraditional = function (gender) {
        if (gender === 'male') {
            ctrl.program.traditionalForFemales = false;
        }
        if (gender === 'female') {
            ctrl.program.traditionalForMales = false;
        }
    }

    ctrl.create = function () {
        ctrl.program = { id: null };
        resetValidation();
    };

    ctrl.cancel = function () {
        loadCache();
        resetValidation();
    };

    ctrl.onSubmit = function () {
        var url = '/api/programs/' + ctrl.program.id;

        $http.put(url, ctrl.program).then(r => {
            updateCache();
            resetValidation();
            //TODO: toastr message
        }).catch(e => {
            //TODO: toastr message
            console.error(e);
        });
    };

    ctrl.onChangeProgramCode = function () {
        ctrl.form.programCode.$setValidity("unique", !codeInUse(ctrl.program.programCode));
    };
    
    ctrl.createAssignment = function () {
        var url = '/api/programs/' + ctrl.program.id + '/credentials';
        $http.post(url, ctrl.assignment).then(r => {

            ctrl.program.programCredentials.push(r.data);
            $('#programCredentials').dxList('instance').reload();

            ctrl.assignment = {};
            ctrl.showForm = false;
            //TODO: Toastr success message

        }).catch(e => {
            console.error('e', e.data.message);
            toastr.error(e.data.exceptionMessage);
        });

    }

    function fetchProgram(programCode) {
        return $http.get('/api/programs/' + pad(programCode, 3)).then(r => {
            ctrl.program = r.data;
            updateCache();
            return ctrl.program = r.data;

        });
    }

    function loadRefs() {
        fetchSchoolYears();
        fetchProgramTypes();
        fetchClusters();
        fetchPrograms();
        fetchCredentials();
    }

    function fetchSchoolYears() {
        return $http.get('/api/refs/schoolyears').then(function (r) {
            return ctrl.schoolYears = r.data;
        });
    }

    function fetchProgramTypes() {
        return $http.get('/api/refs/programTypes').then(function (r) {
            return ctrl.programTypes = r.data;
        });
    }

    function fetchClusters() {
        return $http.get('/api/clusters').then(function (r) {
            return ctrl.clusters = r.data;
        });
    }

    function fetchPrograms() {
        var url = '/api/programs';
        return $http.get(url).then(function (r) {
            return ctrl.programs = r.data;
        });
    }

    function fetchCredentials() {
        var url = '/api/credentials';
        return $http.get(url).then(function (r) {
            return ctrl.credentials = r.data;
        });
    }

    function updateCache() {
        ctrl.cache = angular.copy(ctrl.program);
    }

    function loadCache() {
        ctrl.program = angular.copy(ctrl.cache);
    }

    function resetValidation() {
        ctrl.form.$setPristine();
        ctrl.form.$setUntouched();
    }

    function codeInUse(code) {
        //check if code is same as cache code
        if (code === ctrl.cache.programCode) return false;
        var inUse = ctrl.programList.find(t => t.programCode === code) !== undefined;
        return inUse;
    }

    function pad(num, size) {
        var s = num + "";
        while (s.length < size) s = "0" + s;
        return s;
    }

}

module.component('programEdit',
    {
        bindings: {
            programcode: '<'
        },
        templateUrl: '/src/app/careertech/programs/program-edit.component.html',
        controller: ['$http', controller]
    });
