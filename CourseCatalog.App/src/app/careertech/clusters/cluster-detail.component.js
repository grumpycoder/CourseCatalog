//cluster-detail.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.$onInit = function () {
        ctrl.isAdmin = (ctrl.isAdmin === 'true');
        ctrl.loadCluster(ctrl.clusterId).then(function () {
            ctrl.title = ctrl.cluster.name + ' (' + ctrl.cluster.clusterCode + ')';
            
            if (ctrl.cluster !== undefined) {

                ctrl.listOptions = {
                    dataSource: ctrl.cluster.programs,
                    searchEnabled: true,
                    searchExpr: "name",
                    height: 310,
                    noDataText: 'No Programs Assigned', 
                    allowItemDeleting: false,
                    onItemDeleting: function (data) {
                    }
                }

            }
        });
    };

    ctrl.$onChanges = function () {

    }

    ctrl.loadCluster = function (clusterId) {
        return $http.get('/api/clusters/' + clusterId).then(r => {
            ctrl.cluster = r.data;
        }).catch(function (err) {
            console.error(err.message);
        });
    };

}

module.component('clusterDetail',
    {
        bindings: {
            clusterId: '@', 
            isAdmin: '@'
        },
        templateUrl: '/src/app/careertech/clusters/cluster-detail.component.html',
        controller: ['$http', controller]
    });