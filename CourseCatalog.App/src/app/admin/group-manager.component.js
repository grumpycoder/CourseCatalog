//group-manager.component.js

var module = angular.module("app");

function controller($http, $scope) {
    var ctrl = this;
    var membershipUri = "/api/membership";

    ctrl.title = "Group Manager";

    ctrl.$onInit = function() {
        if (ctrl.height === undefined) ctrl.height = "100%";

        fetchGroups().then(r => {
            ctrl.groups = r;

            ctrl.groupListOptions = {
                dataSource: ctrl.groups,
                searchEnabled: true,
                searchExpr: "groupName",
                onItemClick: function(e) {
                    ctrl.selectedGroup = e.itemData;
                }
            };

        });

    };

    ctrl.userListOptions = {
        dataSource: new DevExpress.data.DataSource({
            store: DevExpress.data.AspNet.createStore({
                key: "id",
                loadUrl: "/api/membership/idem"
            })
        }),
        remoteOperations: true,
        scrolling: {
            mode: "virtual",
            rowRenderingMode: "virtual"
        },
        displayExpr: "emailAddress",
        searchEnabled: true,
        searchMode: "contains",
        searchExpr: ["emailAddress", "fullName"],
        itemTemplate: function(data) {
            return $("<div>").text(data.fullName + " (" + data.emailAddress + ")");
        }
    };

    ctrl.addGroupMember = function() {
        $http.post(`/api/membership/groups/${ctrl.selectedGroup.groupId}/user/${ctrl.selectedUser.identityGuid}`).then(
            r => {
                toastr.success(`Added user ${ctrl.selectedUser.fullName}`);
                ctrl.selectedGroup.users.push(r.data);
                ctrl.selectedUser = undefined;
            }).catch(e => {
            console.error("error", e);
            toastr.error(e.data.exceptionMessage);
        });
    };

    ctrl.addGroup = function() {

        $http.post(`/api/membership/groups/${ctrl.groupName}`)
            .then(r => {
                console.log("success", r)
                var group = {
                    groupName: ctrl.groupName, 
                    groupId: r.data
                }
                ctrl.groups.push(group);
                $("#groupList").dxList("instance").reload();

                toastr.success(`Added group ${r.data.name}`);
            }).catch(e => {
                toastr.error(e.data.message);

            }).finally(f => {
                ctrl.groupName = undefined;
            });
    };

    ctrl.deleteUser = function(user) {

        const found = ctrl.selectedGroup.users
            .find(cdt => cdt.identityGuid === user.identityGuid);

        const idx = ctrl.selectedGroup.users.indexOf(found);
        const deleteUri = `${membershipUri}/groups/${ctrl.selectedGroup.groupId}/user/${user.identityGuid}`;

        $http.delete(deleteUri)
            .then(r => {
                ctrl.selectedGroup.users.splice(idx, 1);

                toastr.success(`Deleted user ${found.fullName}`);
            })
            .catch(e => {
                console.error("error", e);
                toastr.error(e.data.exceptionMessage);
            });

    };

    function fetchGroups() {
        return $http.get(`${membershipUri}/groups`).then(r => {
            return r.data;
        });
    }

}

module.component("groupManager",
    {
        bindings: {
            height: "@"
        },
        templateUrl: "/src/app/admin/group-manager.component.html",
        controller: ["$http", "$scope", controller]
    });