//clusterType-list.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.title = 'Cluster Types';

    ctrl.$onInit = function () {
        ctrl.refreshList();
    }

    ctrl.refreshList = function () {
        $http.get('/api/refs/clusterTypes').then(r => {
            ctrl.clusterTypes = r.data;
            ctrl.groupListOptions = {
                dataSource: ctrl.clusterTypes,
                allowItemDeleting: true, 
                height: 550,
                searchEnabled: true,
                searchExpr: ['name'],
                onItemClick: function (e) {
                    ctrl.selected = angular.copy(e.itemData);
                    updateCache();
                }, 
                onItemDeleting: function (item) {
                    var d = $.Deferred();
                    var url = '/api/refs/clusterTypes/' + item.itemData.clusterTypeId;
                    $http.delete(url).then(r => {
                        toastr.success('Removed Delivery Type');
                        ctrl.selected = undefined; 
                        d.resolve();
                    }).catch(e => {
                        console.error('delete delivery Type error', e);
                        toastr.error(e.data.exceptionMessage);
                        d.reject();
                    });
                    item.cancel = d.promise();
                }
            }

        });
    }

    ctrl.onSubmit = function () {
        var url = '/api/refs/clusterTypes';

        var dto = {
            clusterTypeId: ctrl.selected.clusterTypeId,
            name: ctrl.selected.name,
            description: ctrl.selected.description
        }

        if (!ctrl.selected.clusterTypeId) {
            $http.post(url, dto).then(r => {
                toastr.success('Created Delivery Type');
                dto.clusterTypeId = r.data;
                ctrl.clusterTypes.push(dto);
                $('#groupList').dxList('instance').reload();
                ctrl.selected = undefined;
                resetValidation(); 
            }).catch(e => {
                if(e.data.exceptionMessage) toastr.error(e.data.exceptionMessage);
                if(!e.data.exceptionMessage) toastr.error(e.data.message);
            });
            return;
        }

        $http.put(url, dto).then(r => {
            var clusterType = ctrl.clusterTypes.find(t => t.clusterTypeId === ctrl.selected.clusterTypeId);
            angular.copy(ctrl.selected, clusterType);
            ctrl.selected = undefined;

            updateCache();
            resetValidation();

            toastr.success('Saved Delivery Type');
        }).catch(e => {
            console.error('update error', e);
            if(e.data.exceptionMessage) toastr.error(e.data.exceptionMessage);
            if(!e.data.exceptionMessage) toastr.error(e.data.message);
        });
    };

    ctrl.cancel = function () {
        loadCache();
    };

    ctrl.create = function () {
        ctrl.selected = {};
        ctrl.cache = angular.copy(ctrl.selected);
    }

    function updateCache() {
        ctrl.cache = angular.copy(ctrl.selected);
    }

    ctrl.onChangeCode = function () {
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
        var inUse = ctrl.clusterTypes.find(t => t.name.toLowerCase() === code.toLowerCase()) !== undefined;
        return inUse;
    }

}

module.component('clustertypeList',
    {
        bindings: {},
        templateUrl: '/src/app/settings/clusterTypes/clusterType-list.component.html',
        controller: ['$http', controller]
    });
