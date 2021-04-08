//courseLevel-list.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.title = 'Course Levels';

    ctrl.$onInit = function () {
        ctrl.refreshList();
    }

    ctrl.refreshList = function () {
        $http.get('/api/refs/courseLevels').then(r => {
            ctrl.courseLevels = r.data;
            ctrl.groupListOptions = {
                dataSource: ctrl.courseLevels,
                allowItemDeleting: true, 
                height: 550,
                searchEnabled: true,
                searchExpr: ['name', 'courseLevelCode'],
                onItemClick: function (e) {
                    ctrl.selected = angular.copy(e.itemData);
                    updateCache();
                }, 
                onItemDeleting: function (item) {
                    var d = $.Deferred();
                    var url = '/api/refs/courselevels/' + item.itemData.courseLevelId;
                    $http.delete(url).then(r => {
                        toastr.success('Removed Course Level');
                        ctrl.selected = undefined; 
                        d.resolve();
                    }).catch(e => {
                        console.error('delete course level error', e);
                        toastr.error(e.data.exceptionMessage);
                        d.reject();
                    });
                    item.cancel = d.promise();
                }
            }

        });
    }

    ctrl.onSubmit = function () {
        var url = '/api/refs/courselevels';

        var dto = {
            courseLevelId: ctrl.selected.courseLevelId,
            name: ctrl.selected.name,
            courseLevelCode: ctrl.selected.courseLevelCode
        }

        if (!ctrl.selected.courseLevelId) {
            $http.post(url, dto).then(r => {
                toastr.success('Created Course Level');
                dto.courseLevelId = r.data;
                ctrl.courseLevels.push(dto);
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
            var courseLevel = ctrl.courseLevels.find(t => t.courseLevelId === ctrl.selected.courseLevelId);
            angular.copy(ctrl.selected, courseLevel);
            ctrl.selected = undefined;

            updateCache();
            resetValidation();

            toastr.success('Saved Course Level');
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
        ctrl.form.courseLevelCode.$setValidity("unique", !codeInUse(ctrl.selected.courseLevelCode));
    };

    function loadCache() {
        ctrl.selected = angular.copy(ctrl.cache);
        resetValidation();
    }

    function resetValidation() {
        //TODO: Should not have to set validity
        ctrl.form.courseLevelCode.$setValidity("unique", true);
        ctrl.form.$setPristine();
        ctrl.form.$setUntouched();
    }

    function codeInUse(code) {
        //check if code is same as cache code
        if (code === ctrl.cache.courseLevelCode) return false;
        var inUse = ctrl.courseLevels.find(t => t.courseLevelCode.toLowerCase() === code.toLowerCase()) !== undefined;
        return inUse;
    }

}

module.component('courselevelList',
    {
        bindings: {},
        templateUrl: '/src/app/settings/courseLevels/courseLevel-list.component.html',
        controller: ['$http', controller]
    });
