//programType-list.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.title = 'Program Types';

    ctrl.$onInit = function () {
        ctrl.refreshList();
    }

    ctrl.refreshList = function () {
        $http.get('/api/refs/programTypes').then(r => {
            ctrl.programTypes = r.data;
            ctrl.groupListOptions = {
                dataSource: ctrl.programTypes,
                allowItemDeleting: true, 
                height: 550,
                searchEnabled: true,
                searchExpr: ['name', 'programTypeCode'],
                onItemClick: function (e) {
                    ctrl.selected = angular.copy(e.itemData);
                    updateCache();
                }, 
                onItemDeleting: function (item) {
                    var d = $.Deferred();
                    var url = '/api/refs/programTypes/' + item.itemData.programTypeId;
                    $http.delete(url).then(r => {
                        toastr.success('Removed Program Type');
                        ctrl.selected = undefined; 
                        d.resolve();
                    }).catch(e => {
                        console.error('delete program types error', e);
                        toastr.error(e.data.exceptionMessage);
                        d.reject();
                    });
                    item.cancel = d.promise();
                }
            }

        });
    }

    ctrl.onSubmit = function () {
        var url = '/api/refs/programTypes';

        var dto = {
            programTypeId: ctrl.selected.programTypeId,
            name: ctrl.selected.name,
            programTypeCode: ctrl.selected.programTypeCode, 
            description: ctrl.selected.description 
        }

        if (!ctrl.selected.programTypeId) {
            $http.post(url, dto).then(r => {
                toastr.success('Created Program Type');
                dto.programTypeId = r.data;
                ctrl.programTypes.push(dto);
                $('#groupList').dxList('instance').reload();
                resetValidation();
                ctrl.selected = undefined; 
            }).catch(e => {
                console.error('update error', e);
                if(e.data.exceptionMessage) toastr.error(e.data.exceptionMessage);
                if(!e.data.exceptionMessage) toastr.error(e.data.message);
            });
            return;
        }

        $http.put(url, dto).then(r => {
            var programType = ctrl.programTypes.find(t => t.programTypeId === ctrl.selected.programTypeId);
            angular.copy(ctrl.selected, programType);
            ctrl.selected = undefined;

            updateCache();
            resetValidation();

            toastr.success('Saved Program Types');
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
        ctrl.form.programTypeCode.$setValidity("unique", !codeInUse(ctrl.selected.programTypeCode));
    };

    function loadCache() {
        ctrl.selected = angular.copy(ctrl.cache);
        resetValidation();
    }

    function resetValidation() {
        //TODO: Should not have to set validity
        ctrl.form.programTypeCode.$setValidity("unique", true);
        ctrl.form.$setPristine();
        ctrl.form.$setUntouched();
    }

    function codeInUse(code) {
        //check if code is same as cache code
        if (code === ctrl.cache.programTypeCode) return false;
        var inUse = ctrl.programTypes.find(t => t.programTypeCode.toLowerCase() === code.toLowerCase()) !== undefined;
        return inUse;
    }

}

module.component('programtypeList',
    {
        bindings: {},
        templateUrl: '/src/app/settings/programTypes/programType-list.component.html',
        controller: ['$http', controller]
    });
