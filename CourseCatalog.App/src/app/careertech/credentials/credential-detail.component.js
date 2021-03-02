﻿//credential-details.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;
    ctrl.cache = {};

    ctrl.title = 'Career Tech Credentials';

    ctrl.$onInit = function () {
        ctrl.isAdmin = (ctrl.isAdmin === 'true');

        fetchCredential(ctrl.credentialid).then(r => {
            ctrl.title = 'Credential: ' + ctrl.credential.name + ' (' + ctrl.credential.credentialCode + ')';
            ctrl.listOptions = {
                dataSource: ctrl.credential.programs,
                searchEnabled: true,
                searchExpr: ["programName", "programCode"],
                noDataText: 'No Programs Assigned'
            }
        });
    };

    function fetchCredential(credentialid) {
        return $http.get('/api/credentials/' + credentialid).then(r => {
            ctrl.credential = r.data;
            return ctrl.credential;
        });
    }
}

module.component('credentialDetail',
    {
        bindings: {
            credentialid: '<', 
            isAdmin: '@'
        },
        templateUrl: '/src/app/careertech/credentials/credential-detail.component.html',
        controller: ['$http', controller]
    });

