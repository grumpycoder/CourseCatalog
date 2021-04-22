//endorsement-course-list.component.js

var module = angular.module("app");

function controller($http) {

    var ctrl = this;

    ctrl.$onInit = function() {
        ctrl.title = "Endorsement Courses";

        fetchEndorsements().then(r => {
            ctrl.endorsementListOptions = {
                dataSource: new DevExpress.data.DataSource({
                    store: ctrl.endorsements,
                    sort: "description"
                }),
                height: 490,
                searchEnabled: true,
                searchExpr: "description",
                searchEditorOptions: {
                    placeholder: "Search endorsements..."
                },
                onItemClick: function(e) {
                    ctrl.selectedEndorsement = e.itemData;
                    getCourses(ctrl.selectedEndorsement.endorsementId);
                }
            };
        });

    };

    function fetchEndorsements() {
        return $http.get("/api/refs/endorsements").then(r => {
            ctrl.endorsements = r.data;
        });
    }

    function getCourses(endorsementId) {
        return $http.get(`/api/courses/endorsements/${endorsementId}`).then(r => {
            ctrl.courses = r.data;
        });
    }

}

module.component("endorsementCourseList",
    {
        bindings: {
        },
        templateUrl: "/src/app/courses/endorsement-course-list.component.html",
        controller: ["$http", controller]
    });