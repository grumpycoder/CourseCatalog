//program-edit.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;
    ctrl.cache = {};

    ctrl.$onInit = function() {

        loadRefs();

        if (ctrl.programid == -1) {
            ctrl.title = "New Program";
            return;
        }

        fetchProgram(ctrl.programid).then(r => {
            ctrl.title = `Program: ${ctrl.program.name} (${ctrl.program.programCode})`;
        }).finally(f => {
            if (ctrl.program) {
                ctrl.listOptions = {
                    dataSource: ctrl.program.credentials,
                    searchEnabled: true,
                    searchExpr: "credential",
                    allowItemDeleting: true,
                    onItemDeleting: function(item) {
                        var d = $.Deferred();
                        const url = `/api/programs/${ctrl.program.programId}/credentials/${item.itemData.credentialId}`;
                        $http.delete(url).then(r => {
                            toastr.success("Removed Credential");
                            d.resolve();
                        }).catch(e => {
                            console.error("remove credential error", e);
                            toastr.error(e.data.exceptionMessage);
                            d.reject();
                        });
                        item.cancel = d.promise();
                    }
                };
            }
        });

    };

    ctrl.$onChanges = function() {
    };

    ctrl.validateTraditional = function(gender) {
        if (gender === "male") {
            ctrl.program.traditionalForFemales = false;
        }
        if (gender === "female") {
            ctrl.program.traditionalForMales = false;
        }
    };

    ctrl.create = function() {
        ctrl.program = { id: null };
        resetValidation();
    };

    ctrl.cancel = function() {
        loadCache();
        resetValidation();
    };

    ctrl.onSubmit = function() {
        const url = "/api/programs/";
        const dto = {
            programId: ctrl.program.programId,
            programCode: ctrl.program.programCode,
            name: ctrl.program.name,
            description: ctrl.program.programDescription,
            beginYear: ctrl.program.beginYear,
            endYear: ctrl.program.endYear,
            traditionalForMales: ctrl.program.traditionalForMales,
            traditionalForFemales: ctrl.program.traditionalForFemales,
            programTypeId: ctrl.program.programTypeId,
            clusterId: ctrl.program.clusterId
        };

        if (!ctrl.program.programId) {
            $http.post(url, dto).then(r => {
                toastr.success("Created Program");
                window.location.href = `/careertech/programs/${r.data}/edit`;
            }).catch(e => {
                console.error("update error", e.message);
                toastr.error(e.message);
            });
            return;
        }

        $http.put(url, dto).then(r => {
            updateCache();
            resetValidation();
            toastr.success("Saved Program");
        }).catch(e => {
            console.error("update error", e);
            toastr.error(e.data.exceptionMessage);
        });
    };

    ctrl.onChangeProgramCode = function() {
        ctrl.form.programCode.$setValidity("unique", !codeInUse(ctrl.program.programCode));
    };

    ctrl.createAssignment = function() {
        const url = "/api/programs/credentials";
        const dto = {
            programId: ctrl.program.programId,
            credentialId: ctrl.assignment.credentialId,
            beginYear: ctrl.assignment.beginYear,
            endYar: ctrl.assignment.endYear
        };

        $http.post(url, dto).then(r => {
            ctrl.program.credentials.push(r.data);
            $("#credentials").dxList("instance").reload();
            ctrl.assignment = {};
            ctrl.showForm = false;
            toastr.success("Saved Credential Assignment");
        }).catch(e => {
            console.error("update error", e);
            toastr.error(e.data.exceptionMessage);
        });

    };

    function fetchProgram(programid) {
        return $http.get(`/api/programs/${programid}`).then(r => {
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
        return $http.get("/api/refs/schoolyears").then(function(r) {
            return ctrl.schoolYears = r.data;
        });
    }

    function fetchProgramTypes() {
        return $http.get("/api/refs/programTypes").then(function(r) {
            return ctrl.programTypes = r.data;
        });
    }

    function fetchClusters() {
        return $http.get("/api/clusters").then(function(r) {
            return ctrl.clusters = r.data;
        });
    }

    function fetchPrograms() {
        if (!ctrl.programList) {
            const url = "/api/programs";
            return $http.get(url).then(function(r) {
                return ctrl.programList = r.data;
            });
        };
    }

    function fetchCredentials() {
        const url = "/api/credentials";
        return $http.get(url).then(function(r) {
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
        const inUse = ctrl.programList.find(t => t.programCode === code) !== undefined;
        return inUse;
    }

}

module.component("programEdit",
    {
        bindings: {
            programid: "<"
        },
        templateUrl: "/src/app/careertech/programs/program-edit.component.html",
        controller: ["$http", controller]
    });