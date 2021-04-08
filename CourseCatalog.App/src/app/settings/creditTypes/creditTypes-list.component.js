//creditTypes-list.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.title = 'Credit Types';

    ctrl.$onInit = function () {
        ctrl.refreshList();
    }

    ctrl.refreshList = function () {
        $http.get('/api/refs/creditTypes').then(r => {
            ctrl.creditTypes = r.data;
            ctrl.groupListOptions = {
                dataSource: ctrl.creditTypes,
                allowItemDeleting: true, 
                height: 550,
                searchEnabled: true,
                searchExpr: ['name', 'description'],
                onItemClick: function (e) {
                    ctrl.selected = angular.copy(e.itemData);
                    updateCache();
                }, 
                onItemDeleting: function (item) {
                    var d = $.Deferred();
                    var url = '/api/refs/creditTypes/' + item.itemData.tagId;
                    $http.delete(url).then(r => {
                        toastr.success('Removed Credit Type');
                        ctrl.selected = undefined; 
                        d.resolve();
                    }).catch(e => {
                        console.error('delete Credit Type error', e);
                        toastr.error(e.data.exceptionMessage);
                        d.reject();
                    });
                    item.cancel = d.promise();
                }
            }

        });
    }

    ctrl.onSubmit = function () {
        var url = '/api/refs/creditTypes';

        var dto = {
            tagId: ctrl.selected.tagId,
            name: ctrl.selected.name,
            description: ctrl.selected.description
        }

        if (!ctrl.selected.tagId) {
            $http.post(url, dto).then(r => {
                toastr.success('Created Credit Type');
                dto.tagId = r.data;
                ctrl.creditTypes.push(dto);
                $('#groupList').dxList('instance').reload();
                resetValidation();
                ctrl.selected = undefined; 
            }).catch(e => {
                if(e.data.exceptionMessage) toastr.error(e.data.exceptionMessage);
                if(!e.data.exceptionMessage) toastr.error(e.data.message);
            });
            return;
        }

        $http.put(url, dto).then(r => {
            var creditType = ctrl.creditTypes.find(t => t.tagId === ctrl.selected.tagId);
            angular.copy(ctrl.selected, creditType);

            updateCache();
            resetValidation();

            ctrl.selected = undefined;
            toastr.success('Saved Credit Type');
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
        var inUse = ctrl.creditTypes.find(t => t.name.toLowerCase() === code.toLowerCase()) !== undefined;
        return inUse;
    }

}

module.component('credittypesList',
    {
        bindings: {},
        templateUrl: '/src/app/settings/creditTypes/creditTypes-list.component.html',
        controller: ['$http', controller]
    });
