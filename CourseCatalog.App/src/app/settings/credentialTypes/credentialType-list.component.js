//credentialType-list.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;

    ctrl.title = "Credential Types";

    ctrl.$onInit = function() {
        ctrl.refreshList();
    };

    ctrl.refreshList = function() {
        $http.get("/api/refs/credentialTypes").then(r => {
            ctrl.credentialTypes = r.data;
            ctrl.groupListOptions = {
                dataSource: ctrl.credentialTypes,
                allowItemDeleting: true,
                height: 550,
                searchEnabled: true,
                searchExpr: ["name", "credentialTypeCode"],
                onItemClick: function(e) {
                    ctrl.selected = angular.copy(e.itemData);
                    updateCache();
                },
                onItemDeleting: function(item) {
                    var d = $.Deferred();
                    const url = `/api/refs/credentialTypes/${item.itemData.credentialTypeId}`;
                    $http.delete(url).then(r => {
                        toastr.success("Removed Credential Type");
                        ctrl.selected = undefined;
                        d.resolve();
                    }).catch(e => {
                        console.error("delete credential types error", e);
                        toastr.error(e.data.exceptionMessage);
                        d.reject();
                    });
                    item.cancel = d.promise();
                }
            };

        });
    };

    ctrl.onSubmit = function() {
        const url = "/api/refs/credentialTypes";

        var dto = {
            credentialTypeId: ctrl.selected.credentialTypeId,
            name: ctrl.selected.name,
            credentialTypeCode: ctrl.selected.credentialTypeCode,
            description: ctrl.selected.description
        };

        if (!ctrl.selected.credentialTypeId) {
            $http.post(url, dto).then(r => {
                toastr.success("Created Credential Type");
                dto.credentialTypeId = r.data;
                ctrl.credentialTypes.push(dto);
                $("#groupList").dxList("instance").reload();
                resetValidation();
                ctrl.selected = undefined;
            }).catch(e => {
                console.error("update error", e);
                if (e.data.exceptionMessage) toastr.error(e.data.exceptionMessage);
                if (!e.data.exceptionMessage) toastr.error(e.data.message);
            });
            return;
        }

        $http.put(url, dto).then(r => {
            var credentialType = ctrl.credentialTypes.find(t => t.credentialTypeId === ctrl.selected.credentialTypeId);
            angular.copy(ctrl.selected, credentialType);
            ctrl.selected = undefined;

            updateCache();
            resetValidation();

            toastr.success("Saved Credential Types");
        }).catch(e => {
            console.error("update error", e);
            if (e.data.exceptionMessage) toastr.error(e.data.exceptionMessage);
            if (!e.data.exceptionMessage) toastr.error(e.data.message);
        });
    };

    ctrl.cancel = function() {
        loadCache();
    };

    ctrl.create = function() {
        ctrl.selected = {};
        ctrl.cache = angular.copy(ctrl.selected);
    };

    function updateCache() {
        ctrl.cache = angular.copy(ctrl.selected);
    }

    ctrl.onChangeCode = function() {
        ctrl.form.credentialTypeCode.$setValidity("unique", !codeInUse(ctrl.selected.credentialTypeCode));
    };

    function loadCache() {
        ctrl.selected = angular.copy(ctrl.cache);
        resetValidation();
    }

    function resetValidation() {
        //TODO: Should not have to set validity
        ctrl.form.credentialTypeCode.$setValidity("unique", true);
        ctrl.form.$setPristine();
        ctrl.form.$setUntouched();
    }

    function codeInUse(code) {
        //check if code is same as cache code
        if (code === ctrl.cache.credentialTypeCode) return false;
        const inUse = ctrl.credentialTypes.find(t => t.credentialTypeCode.toLowerCase() === code.toLowerCase()) !==
            undefined;
        return inUse;
    }

}

module.component("credentialtypeList",
    {
        bindings: {},
        templateUrl: "/src/app/settings/credentialTypes/credentialType-list.component.html",
        controller: ["$http", controller]
    });