//course-requirement-edit.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;
    ctrl.endorsements = [];

    this.$onInit = function() {
        fetchEndorsements().then(r => {
        });
    };

    ctrl.$onChanges = function() {
        if (ctrl.course) {
            ctrl.listOptions = {
                dataSource: ctrl.course.endorsements,
                searchEnabled: true,
                searchExpr: "endorseDescription",
                height: 500,
                allowItemDeleting: true,
                onItemDeleting: function(data) {
                    ctrl.removeEndorsement(data.itemData);
                    //var d = $.Deferred();
                    //console.log('data', data.itemData);
                    //$http.delete('/api/courses/' + ctrl.course.id + '/endorsements/' + data.itemData.endorsementId)
                    //    .then(function (r) {
                    //        //TODO: toastr message
                    //        $('#endorsements').dxList('instance').reload(); 
                    //        d.resolve(d);
                    //    }).catch(err => {
                    //        console.log('error', err);
                    //        //TODO: Toastr error 
                    //        console.log('err', err);
                    //        toastr.error(err.message); 
                    //        d.reject;
                    //    });
                    //data.cancel = d.promise();
                }
            };
        }
    };

    ctrl.removeEndorsement = function(item) {
        var idx = ctrl.course.endorsements.indexOf(item);
        return $http.delete(`/api/courses/${ctrl.course.id}/endorsements/${item.endorsementId}`)
            .then(r => {
                ctrl.course.endorsements.splice(idx, 1);
                toastr.success(`Removed ${item.endorseCode}`);
            }).catch(err => {
                console.error()("error", err);
                toastr.error(err.message);
            });
    };

    ctrl.addEndorsement = function() {
        const idx = ctrl.course.endorsements.find(o => { return o.endorsementId === ctrl.endorsementId });
        if (idx !== undefined) {
            toastr.info("Endorsement already exists");
            return;
        }

        $http.post(`/api/courses/${ctrl.course.id}/endorsements/${ctrl.endorsementId}`)
            .then(r => {
                ctrl.course.endorsements.push(r.data);
                toastr.success(`Added endorsement ${r.data.endorseCode}`);
                $("#endorsements").dxList("instance").reload();
                ctrl.endorsementId = undefined;
            }).catch(e => {
                console.error(e);
                toastr.error(err.message);
            });
    };

    function fetchEndorsements() {

        return $http.get("/api/refs/endorsements").then(r => {
            ctrl.endorsements = r.data;
        });
    }
}

module.component("courseRequirementEdit",
    {
        bindings: {
            course: "<"
        },
        templateUrl: "/src/app/courses/course-requirement-edit.html",
        controller: ["$http", controller]
    });