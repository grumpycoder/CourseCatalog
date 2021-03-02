//credential-edit.component.js


var module = angular.module('app');

function controller($http) {
    var ctrl = this;
    ctrl.cache = {};

    ctrl.title = 'Career Tech Credentials';

    ctrl.$onInit = function () {

        fetchCredential(ctrl.credentialid).then(r => {
            ctrl.title = 'Credential: ' + ctrl.credential.name + ' (' + ctrl.credential.credentialCode + ')';
            ctrl.listOptions = {
                dataSource: ctrl.credential.programs,
                searchEnabled: true,
                searchExpr: ["programName", "programCode"],
                noDataText: 'No Programs Assigned', 
                allowItemDeleting: true,
                onItemDeleting: function (item) {
                    var d = $.Deferred();
                    var url = '/api/credentials/' + ctrl.credential.credentialId + '/programs/' + item.itemData.programId;
                    $http.delete(url).then(r => {
                        toastr.success('Removed Program');
                        d.resolve();
                    }).catch(e => {
                        console.error('add program error', e);
                        toastr.error(e.data.exceptionMessage);
                        d.reject();
                    });
                    item.cancel = d.promise();
                }
            }
        });

        loadRefs();
    };


    ctrl.onSubmit = function () {
        var url = '/api/careertech/credentials';
        if (ctrl.credential.id === null) {
            
            $http.post(url, ctrl.credential).then(r => {

                ctrl.credentialsList.push(r.data);
                initCredentialList(ctrl.credential.credentialCode);

                //HACK: Setting DevExtreme list b/c it doesn't act right when adding new item to list
                var idx = ctrl.credentialsList.findIndex(x => x.credentialCode === ctrl.credential.credentialCode);
                var selectBox = $('#credentials').dxSelectBox("instance");
                selectBox.option('value', ctrl.credentialsList[idx]); 

                updateCache();
                resetValidation();
                //TODO: toastr message
            }).catch(e => {
                //TODO: toastr message
                console.error(e);
            });
        } else {
            $http.put(url, ctrl.credential).then(r => {
                initCredentialList(ctrl.credential.credentialCode);
                updateCache();
                resetValidation();
            }).catch(e => {
                //TODO: toastr message
                console.error(e);
            });
        }
    };

    ctrl.cancel = function () {
        loadCache();
        resetValidation();
    };

    ctrl.create = function () {
        //HACK: Setting DevExtreme list b/c it doesn't act right when adding new item to list
        //TODO: copy course requirements method
        var selectBox = $('#credentials').dxSelectBox("instance");
        selectBox.option('value', null); 

        ctrl.selectedCredential = null; 
        ctrl.credential = {
            id: null
        };
    };

    ctrl.onChangeCredentialCode = function () {
        ctrl.form.credentialCode.$setValidity("unique", !codeInUse(ctrl.credential.credentialCode)); 
    };

    ctrl.createAssignment = function() {
        var dto = {
            credentialId: ctrl.credential.credentialId, 
            programId: ctrl.assignment.programId, 
            beginYear: ctrl.assignment.beginYear, 
            endYar: ctrl.assignment.endYear
        }
        var url = '/api/credentials/programs';
        $http.post(url, dto).then(r => {
            ctrl.credential.programs.push(r.data);
            $('#credentials').dxList('instance').reload();
            ctrl.assignment = {};
            ctrl.showForm = false; 
            toastr.success('Saved Credential Assignment');
        }).catch(e => {
            console.error('update error', e);
            toastr.error(e.data.exceptionMessage);
        });
    }

    ctrl.deleteAssignment = function (item) {
        var url = '/api/programs/' + item.programId + '/credentials/' + item.credentialId;
        return $http.delete(url).then(r => {
            //TODO: Toastr success message
            return r;
        }).catch(e => {
            //TODO: Toastr error message 
            console.log(e.data.message);
        });
    }


    function defineProgramsList() {
        ctrl.store = new DevExpress.data.ArrayStore({
            data: ctrl.credential.programCredentials, 
            key: 'id',
            reshapeOnPush: true
        });


        ctrl.listOptions = {
            dataSource: ctrl.store, 
            searchEnabled: true,
            searchExpr: "credential",
            allowItemDeleting: true,
            height: 310,
            onItemDeleting: function (data) {
                ctrl.deleteAssignment(data.itemData);
            }

        }
    }

    function fetchCredential(credentialid) {
        return $http.get('/api/credentials/' + credentialid).then(r => {
            ctrl.credential = r.data;
            updateCache();
            return ctrl.credential;
        });
    }

    function loadRefs() {
        fetchSchoolYears();
        fetchCredentialTypes();
        fetchCredentials();
        fetchPrograms();
    }

    function fetchSchoolYears() {
        return $http.get('/api/refs/schoolyears').then(function (r) {
            return ctrl.schoolYears = r.data;
        });
    }

    function fetchCredentialTypes()
    {
        return $http.get('/api/refs/credentialtypes').then(function (r) {
            return ctrl.credentialTypes = r.data;
        });
    }

    function fetchCredentials() {
        return $http.get('/api/credentials').then(function (r) {
            return ctrl.credentials = r.data;
        });
    }

    function fetchPrograms() {
        var url = '/api/programs';
        return $http.get(url).then(function (r) {
            return ctrl.programs = r.data;
        });
    }

    function updateCache() {
        ctrl.cache = angular.copy(ctrl.credential);
        if (ctrl.cache === null) ctrl.cache = {}; 
    }

    function loadCache() {
        ctrl.credential = angular.copy(ctrl.cache);
    }

    function resetValidation() {
        ctrl.form.$setPristine();
        ctrl.form.$setUntouched();
    }

    function codeInUse(code) {
        //check if code is same as cache code
        if (code === ctrl.cache.credentialCode) return false;
        var inUse = ctrl.credentials.find(t => t.credentialCode === code) !== undefined;
        return inUse;
    }

    function pad(num, size) {
        var s = num + "";
        while (s.length < size) s = "0" + s;
        return s;
    }
}

module.component('credentialEdit',
    {
        bindings: {
            credentialid: '<'
        },
        templateUrl: '/src/app/careertech/credentials/credential-edit.component.html',
        controller: ['$http', controller]
    });

