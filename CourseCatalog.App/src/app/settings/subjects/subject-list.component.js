//subject-list.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;

    ctrl.title = "Subjects";

    ctrl.$onInit = function() {
        ctrl.refreshList();
    };

    ctrl.refreshList = function() {
        $http.get("/api/refs/subjects").then(r => {
            ctrl.subjects = r.data;
            ctrl.groupListOptions = {
                dataSource: ctrl.subjects,
                allowItemDeleting: true,
                height: 550,
                searchEnabled: true,
                searchExpr: ["name", "subjectCode"],
                onItemClick: function(e) {
                    ctrl.selected = angular.copy(e.itemData);
                    updateCache();
                },
                onItemDeleting: function(item) {
                    var d = $.Deferred();
                    const url = `/api/refs/subjects/${item.itemData.subjectId}`;
                    $http.delete(url).then(r => {
                        toastr.success("Removed Subject");
                        ctrl.selected = undefined;
                        d.resolve();
                    }).catch(e => {
                        console.error("delete subject error", e);
                        toastr.error(e.data.exceptionMessage);
                        d.reject();
                    });
                    item.cancel = d.promise();
                }
            };

        });
    };

    ctrl.onSubmit = function() {
        const url = "/api/refs/subjects";

        var dto = {
            subjectId: ctrl.selected.subjectId,
            name: ctrl.selected.name,
            subjectCode: ctrl.selected.subjectCode
        };

        if (!ctrl.selected.subjectId) {
            $http.post(url, dto).then(r => {
                toastr.success("Created Subject");
                dto.subjectId = r.data;
                ctrl.subjects.push(dto);
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
            var subject = ctrl.subjects.find(t => t.subjectId === ctrl.selected.subjectId);
            angular.copy(ctrl.selected, subject);
            ctrl.selected = undefined;

            updateCache();
            resetValidation();

            toastr.success("Saved Subject");
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
        ctrl.form.subjectCode.$setValidity("unique", !codeInUse(ctrl.selected.subjectCode));
    };

    function loadCache() {
        ctrl.selected = angular.copy(ctrl.cache);
        resetValidation();
    }

    function resetValidation() {
        //TODO: Should not have to set validity
        ctrl.form.subjectCode.$setValidity("unique", true);
        ctrl.form.$setPristine();
        ctrl.form.$setUntouched();
    }

    function codeInUse(code) {
        //check if code is same as cache code
        if (code === ctrl.cache.subjectCode) return false;
        const inUse = ctrl.subjects.find(t => t.subjectCode.toLowerCase() === code.toLowerCase()) !== undefined;
        return inUse;
    }

}

module.component("subjectList",
    {
        bindings: {},
        templateUrl: "/src/app/settings/subjects/subject-list.component.html",
        controller: ["$http", controller]
    });