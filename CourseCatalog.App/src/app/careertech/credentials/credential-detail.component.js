//credential-details.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;
    ctrl.cache = {};

    ctrl.title = 'Career Tech Credentials';

    ctrl.$onInit = function () {
        fetchCredential(ctrl.credentialcode).then(r => {
            ctrl.title = 'Credential: ' + ctrl.credential.name + ' (' + ctrl.credential.credentialCode + ')';
            ctrl.listOptions = {
                dataSource: ctrl.credential.programCredentials,
                searchEnabled: true,
                searchExpr: ["program", "programCode"],
                noDataText: 'No Credentials Assigned'
            }
        });
    };


    function fetchCredential(credentialCode) {
        return $http.get('/api/credentials/' + pad(credentialCode, 4)).then(r => {
            
            ctrl.credential = r.data;
            //updateCache();
            return ctrl.credential;
        });
    }

    function pad(num, size) {
        var s = num + "";
        while (s.length < size) s = "0" + s;
        return s;
    }
}

module.component('credentialDetail',
    {
        bindings: {
            credentialcode: '<'
        },
        templateUrl: '/src/app/careertech/credentials/credential-detail.component.html',
        controller: ['$http', controller]
    });

