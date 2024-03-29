﻿//cluster-edit.component.js


var module = angular.module("app");

function controller($http) {
    var ctrl = this;

    ctrl.cache = {};

    ctrl.$onInit = function() {

        fetchSchoolYears().then(r => { ctrl.schoolYears = r; });
        fetchClusterTypes().then(r => { ctrl.clusterTypes = r; });
        fetchClusters().then(r => { ctrl.clusters = r });

        if (ctrl.clusterid == -1) {
            ctrl.title = "New Cluster";
            return;
        }

        fetchCluster(ctrl.clusterid).then(r => {
            ctrl.title = `Cluster: ${ctrl.cluster.name} (${ctrl.cluster.clusterCode})`;
        }).finally(d => {
            if (ctrl.cluster !== undefined) {
                ctrl.listOptions = {
                    dataSource: ctrl.cluster.programs,
                    searchEnabled: true,
                    searchExpr: "name",
                    height: 310,
                    allowItemDeleting: false,
                    noDataText: "No Programs Assigned",
                    onItemDeleting: function(data) {
                    }
                };
            }
        });

    };

    ctrl.onSubmit = function() {
        const url = "/api/clusters/";

        const dto = {
            clusterId: ctrl.cluster.clusterId,
            name: ctrl.cluster.name,
            description: ctrl.cluster.description,
            edFactsClusterValue: ctrl.cluster.edFactsClusterValue,
            clusterCode: ctrl.cluster.clusterCode,
            beginYear: ctrl.cluster.beginYear,
            endYear: ctrl.cluster.endYear,
            clusterTypeId: ctrl.cluster.clusterTypeId
        };

        if (!ctrl.cluster.clusterId) {
            $http.post(url, dto).then(r => {
                toastr.success("Created Cluster");
                window.location.href = `/careertech/clusters/${r.data}/edit`;
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
    };

    ctrl.create = function() {
        const selectBox = $("#clusters").dxSelectBox("instance");
        selectBox.option("value", null);

        ctrl.selectedCluster = null;

        ctrl.cluster = { id: null };
        ctrl.cache = angular.copy(ctrl.cluster);
        resetValidation();
    };

    ctrl.delete = function() {
        //TODO: Delete cluster??
    };

    ctrl.onChangeClusterCode = function() {
        ctrl.form.clusterCode.$setValidity("unique", !codeInUse(ctrl.cluster.clusterCode));
    };

    function fetchCluster(clusterId) {
        return $http.get(`/api/clusters/${clusterId}`).then(r => {
            ctrl.cluster = r.data;
            updateCache();
            return ctrl.cluster = r.data;

        });
    }

    function fetchClusters() {
        return $http.get("/api/clusters").then(r => {
            return r.data;
        });
    }

    function fetchSchoolYears() {
        return $http.get("/api/refs/schoolYears").then(function(r) {
            return r.data;
        });
    }

    function fetchClusterTypes() {
        return $http.get("/api/refs/clusterTypes").then(function(r) {
            return r.data;
        });
    }

    function updateCache() {
        ctrl.cache = angular.copy(ctrl.cluster);
    }

    function loadCache() {
        ctrl.cluster = angular.copy(ctrl.cache);
        resetValidation();
    }

    function codeInUse(code) {
        //check if code is same as cache code
        if (code === ctrl.cache.clusterCode) return false;
        const inUse = ctrl.clusters.find(t => t.clusterCode === code) !== undefined;
        return inUse;
    }

    function resetValidation() {
        //TODO: Should not have to set validity
        ctrl.form.clusterCode.$setValidity("unique", true);
        ctrl.form.$setPristine();
        ctrl.form.$setUntouched();

    }

    function pad(num, size) {
        var s = num + "";
        while (s.length < size) s = `0${s}`;
        return s;
    }

}

module.component("clusterEdit",
    {
        bindings: {
            clusterid: "<"
        },
        templateUrl: "/src/app/careertech/clusters/cluster-edit.component.html",
        controller: ["$http", controller]
    });