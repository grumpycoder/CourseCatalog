//app.module.js

var app = angular.module("app",
        [
            "ngMessages",
            "ngAnimate",
            "dx",
            "extendedSelect"
        ]).factory("userService", ["$http", userService])
    .run(function() {
        //console.log("app run", user);
    });

function userService($http, groups) {
    return {
        userDetails: function() {
            return $http.get("/api/membership/currentUser").then(r => {
                return r.data;
            });
        }
    };
};