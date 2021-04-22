//credential-edit.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;
    ctrl.cache = {};

    ctrl.title = "Career Tech Credentials";

    ctrl.$onInit = function() {

        loadRefs();

        if (ctrl.credentialid == -1) {
            ctrl.title = "New Credential";
            return;
        }

        fetchCredential(ctrl.credentialid).then(r => {
            ctrl.title = `Credential: ${ctrl.credential.name} (${ctrl.credential.credentialCode})`;
            ctrl.listOptions = {
                dataSource: {
                    store: ctrl.credential.programs,
                    sort: "programName"
                },
                searchEnabled: true,
                searchExpr: ["programName", "programCode"],
                noDataText: "No Programs Assigned",
                allowItemDeleting: true,
                onItemDeleting: function(item) {
                    var d = $.Deferred();
                    console.log("delete", item);
                    const url = `/api/credentials/${ctrl.credential.credentialId}/programs/${item.itemData.programId}`;
                    $http.delete(url).then(r => {
                        toastr.success("Removed Program");
                        d.resolve();
                    }).catch(e => {
                        console.error("add program error", e);
                        toastr.error(e.data.exceptionMessage);
                        d.reject();
                    });
                    item.cancel = d.promise();
                }
            };
        });

    };

    ctrl.onSubmit = function() {
        const url = "/api/credentials";

        const dto = {
            credentialId: ctrl.credential.credentialId,
            credentialCode: ctrl.credential.credentialCode,
            name: ctrl.credential.name,
            description: ctrl.credential.description,
            beginYear: ctrl.credential.beginYear,
            endYear: ctrl.credential.endYear,
            credentialTypeId: ctrl.credential.credentialTypeId,
            isReimbursable: ctrl.credential.isReimbursable
        };

        if (!ctrl.credential.credentialId) {
            $http.post(url, dto).then(r => {
                toastr.success("Created Credential");
                window.location.href = `/careertech/credentials/${r.data}/edit`;
            }).catch(e => {
                console.error("update error", e.message);
                toastr.error(e.message);
            });
            return;
        }

        $http.put(url, dto).then(r => {
            updateCache();
            resetValidation();
            toastr.success("Saved Cluster");
        }).catch(e => {
            console.error("update error", e.message);
            toastr.error(e.message);
        });
    };

    ctrl.cancel = function() {
        loadCache();
        resetValidation();
    };

    ctrl.onChangeCredentialCode = function() {
        ctrl.form.credentialCode.$setValidity("unique", !codeInUse(ctrl.credential.credentialCode));
    };

    ctrl.createAssignment = function() {
        const dto = {
            credentialId: ctrl.credential.credentialId,
            programId: ctrl.assignment.programId,
            beginYear: ctrl.assignment.beginYear,
            endYear: ctrl.assignment.endYear
        };
        const url = "/api/credentials/programs";
        $http.post(url, dto).then(r => {
            ctrl.credential.programs.push(r.data);
            $("#credentials").dxList("instance").reload();
            ctrl.assignment = {};
            ctrl.showForm = false;
            toastr.success("Saved Credential Assignment");
        }).catch(e => {
            console.error("update error", e);
            toastr.error(e.message);
        });
    };

    ctrl.deleteAssignment = function(item) {
        const url = `/api/programs/${item.programId}/credentials/${item.credentialId}`;
        return $http.delete(url).then(r => {
            toastr.success("Removed Credential Assignment");
            return r;
        }).catch(e => {
            console.error("update error", e);
            toastr.error(e.message);
        });
    };

    function fetchCredential(credentialId) {
        return $http.get(`/api/credentials/${credentialId}`).then(r => {
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
        return $http.get("/api/refs/schoolyears").then(function(r) {
            return ctrl.schoolYears = r.data;
        });
    }

    function fetchCredentialTypes() {
        return $http.get("/api/refs/credentialtypes").then(function(r) {
            return ctrl.credentialTypes = r.data;
        });
    }

    function fetchCredentials() {
        return $http.get("/api/credentials").then(function(r) {
            return ctrl.credentials = r.data;
        });
    }

    function fetchPrograms() {
        const url = "/api/programs";
        return $http.get(url).then(function(r) {
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
        const inUse = ctrl.credentials.find(t => t.credentialCode === code) !== undefined;
        return inUse;
    }

}

module.component("credentialEdit",
    {
        bindings: {
            credentialid: "<"
        },
        templateUrl: "/src/app/careertech/credentials/credential-edit.component.html",
        controller: ["$http", controller]
    });