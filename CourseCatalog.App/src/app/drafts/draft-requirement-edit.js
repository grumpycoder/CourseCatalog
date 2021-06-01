//draft-requirement-edit.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;
    ctrl.endorsements = [];

    this.$onInit = function () {
        fetchEndorsements().then(r => { });
    };

    ctrl.$onChanges = function () {
        if (ctrl.course) {
            var store = new DevExpress.data.ArrayStore(ctrl.course.endorsements);
            var dataSource = new DevExpress.data.DataSource({
                sort: "description",
                pageSize: 25,
                store: store
            });

            ctrl.listOptions = {
                dataSource: dataSource,
                searchEnabled: true,
                searchExpr: "description",
                sortBy: 'description',
                height: 500,
                allowItemDeleting: ctrl.isAdmin,
                onItemDeleting: function (data) {
                    ctrl.removeEndorsement(data.itemData);
                }
            };
        }
    };

    ctrl.removeEndorsement = function (item) {
        var idx = ctrl.course.endorsements.indexOf(item);
        const url = `/api/drafts/${ctrl.course.draftId}/endorsements/${item.endorsementId}`;
        return $http.delete(url)
            .then(r => {
                ctrl.course.endorsements.splice(idx, 1);
                toastr.success(`Removed ${item.endorseCode}`);
            }).catch(err => {
                console.error("error", err);
                toastr.error(err.data.message);
            });
    };

    var endorsementsToAdd = [];
    var addedEndorsements = [];
    var skippedEndorsements = [];
    var failedToAdd = [];

    ctrl.addEndorsement = function () {
      
        ctrl.endorsementsToAdd.forEach(x => {
            var idx = ctrl.course.endorsements.find(e => {
                return e.endorsementId === x.endorsementId;
            });
            if (idx !== undefined) skippedEndorsements.push(x);
            if (idx === undefined) endorsementsToAdd.push(x);
        });

        endorsementsToAdd.forEach(a => {
            var dto = {
                draftId: ctrl.course.draftId,
                endorsementId: a.endorsementId
            };
            $http.post("/api/drafts/CreateEndorsement", dto)
                .then(r => {
                    addedEndorsements.push(r.data);
                    ctrl.course.endorsements.push(r.data);
                    toastr.success(`Added ${r.data.endorseCode}`);
                    $("#endorsements").dxList("instance").reload();
                }).catch(err => {
                    console.error(err);
                    toastr.error(err.data.exceptionMessage);
                    failedToAdd.push(a);
                });
        });

        skippedEndorsements.forEach(a => {
            toastr.warning(`Already assigned ${a.endorseCode}`);
        });

        ctrl.endorsementsToAdd = []; 
        endorsementsToAdd = [];
        addedEndorsements = [];
        skippedEndorsements = [];
        failedToAdd = [];

        return; 
    };

    function fetchEndorsements() {

        return $http.get("/api/refs/endorsements").then(r => {
            ctrl.endorsements = r.data;
        });
    }
}

module.component("draftRequirementEdit",
    {
        bindings: {
            course: "<",
            isAdmin: "<"
        },
        templateUrl: "/src/app/drafts/draft-requirement-edit.html",
        controller: ["$http", controller]
    });