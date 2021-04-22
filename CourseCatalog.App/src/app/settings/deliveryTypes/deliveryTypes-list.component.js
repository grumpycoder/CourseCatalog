//deliveryTypes-list.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;

    ctrl.title = "Delivery Types";

    ctrl.$onInit = function() {
        ctrl.refreshList();
    };

    ctrl.refreshList = function() {
        $http.get("/api/refs/deliveryTypes").then(r => {
            ctrl.deliveryTypes = r.data;
            ctrl.groupListOptions = {
                dataSource: ctrl.deliveryTypes,
                allowItemDeleting: true,
                height: 550,
                searchEnabled: true,
                searchExpr: ["name", "courseLevelCode"],
                onItemClick: function(e) {
                    ctrl.selected = angular.copy(e.itemData);
                    updateCache();
                },
                onItemDeleting: function(item) {
                    var d = $.Deferred();
                    const url = `/api/refs/deliveryTypes/${item.itemData.deliveryTypeId}`;
                    $http.delete(url).then(r => {
                        toastr.success("Removed Delivery Type");
                        ctrl.selected = undefined;
                        d.resolve();
                    }).catch(e => {
                        console.error("delete delivery Type error", e);
                        toastr.error(e.data.exceptionMessage);
                        d.reject();
                    });
                    item.cancel = d.promise();
                }
            };

        });
    };

    ctrl.onSubmit = function() {
        const url = "/api/refs/deliveryTypes";

        var dto = {
            deliveryTypeId: ctrl.selected.deliveryTypeId,
            name: ctrl.selected.name,
            description: ctrl.selected.description
        };

        if (!ctrl.selected.deliveryTypeId) {
            $http.post(url, dto).then(r => {
                toastr.success("Created Delivery Type");
                dto.deliveryTypeId = r.data;
                ctrl.deliveryTypes.push(dto);
                $("#groupList").dxList("instance").reload();
                resetValidation();
                ctrl.selected = undefined;
            }).catch(e => {
                if (e.data.exceptionMessage) toastr.error(e.data.exceptionMessage);
                if (!e.data.exceptionMessage) toastr.error(e.data.message);
            });
            return;
        }

        $http.put(url, dto).then(r => {
            var deliveryType = ctrl.deliveryTypes.find(t => t.deliveryTypeId === ctrl.selected.deliveryTypeId);
            angular.copy(ctrl.selected, deliveryType);
            ctrl.selected = undefined;

            updateCache();
            resetValidation();

            toastr.success("Saved Delivery Type");
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
        ctrl.form.name.$setValidity("unique", !codeInUse(ctrl.selected.name));
    };

    function loadCache() {
        ctrl.selected = angular.copy(ctrl.cache);
        resetValidation();
    }

    function resetValidation() {
        //TODO: Should not have to set validity
        ctrl.form.name.$setValidity("unique", true);
        ctrl.form.$setPristine();
        ctrl.form.$setUntouched();
    }

    function codeInUse(code) {
        //check if code is same as cache code
        if (code === ctrl.cache.name) return false;
        const inUse = ctrl.deliveryTypes.find(t => t.name.toLowerCase() === code.toLowerCase()) !== undefined;
        return inUse;
    }

}

module.component("deliverytypesList",
    {
        bindings: {},
        templateUrl: "/src/app/settings/deliveryTypes/deliveryTypes-list.component.html",
        controller: ["$http", controller]
    });