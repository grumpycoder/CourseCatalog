//course-program-list.component.js


var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.$onInit = function () {
        if (ctrl.height === undefined) ctrl.height = '100%';
    };

    //ctrl.$onChanges = function () {
    //    if (ctrl.coursecode === undefined || ctrl.coursecode === '') {
    //        return;
    //    }
    //    else {
    //        getCoursePrograms(ctrl.coursecode).then(r => {
    //            ctrl.coursePrograms = {
    //                dataSource: new DevExpress.data.DataSource({
    //                    store: r,
    //                    group: "clusterName"
    //                }),
    //                height: ctrl.height,
    //                grouped: true,
    //                collapsibleGroups: false,
    //                searchEnabled: true,
    //                allowItemDeleting: ctrl.allowEdit,
    //                searchExpr: ['programName', 'programCode', 'clusterName'],

    //                groupTemplate: function (data) {
    //                    return $("<div>Cluster: " + data.key + "</div>");
    //                },
    //                itemTemplate: function (data) {
    //                    return $(`<div>${data.programName} (${data.programCode})</div>`);
    //                },
    //                onItemDeleting: function (data) {
    //                    deleteCourseProgram(data.itemData).then(r => {
    //                        //TODO: add toastr messaging
    //                    });
    //                }
    //            };

    //        }).then(d => {
    //            if (ctrl.allowEdit) {
    //                getPrograms().then(r => {
    //                    ctrl.availablePrograms = {
    //                        dataSource: new DevExpress.data.DataSource({
    //                            store: r,
    //                            group: "clusterName"
    //                        }),
    //                        showClearButton: true,
    //                        width: "78%",
    //                        valueExp: 'programCode',
    //                        displayExpr: 'programName',
    //                        grouped: true,
    //                        groupTemplate: function (data) {
    //                            return $("<div>Cluster: " + data.key + "</div>");
    //                        },
    //                        itemTemplate: function (data) {
    //                            return $("<div>").text(data.programName);
    //                        },
    //                        onSelectionChanged: function (r) {
    //                            if (r === undefined) return;
    //                            ctrl.selectedProgram = r.selectedItem;
    //                        }
    //                    };
    //                });
    //            }
    //        });
    //    }
    //};

    //ctrl.addProgram = function () {
    //    var url = `/api/careertech/programs/${ctrl.selectedProgram.programCode}/course/${ctrl.coursecode}`;

    //    $http.post(url).then(r => {
    //        console.log('r', r);
    //        var obj = {
    //            id: r.data.id,
    //            programCode: r.data.programCode,
    //            programName: r.data.programName,
    //            clusterName: r.data.clusterName,
    //            clusterCode: r.data.clusterCode
    //        };
    //        ctrl.coursePrograms.dataSource.store().insert(obj);
    //        ctrl.coursePrograms.dataSource.reload();
    //        //TODO: add toastr messaging
    //    }).then(d => {
            
    //    });
    //};

    //function getPrograms() {
    //    //TODO: add description to program dto
    //    var url = '/api/ref/programs';
    //    return $http.get(url).then(function (r) {
    //        return r.data;
    //    });
    //}

    //function getCoursePrograms(code) {
    //    var url = `/api/careertech/courses/${code}/programs`;
    //    return $http.get(url).then(function (r) {
    //        return r.data;
    //    });
    //}

    //function deleteCourseProgram(item) {
    //    var url = `/api/careertech/programs/${item.programCode}/course/${ctrl.coursecode}`;
    //    return $http.delete(url).then(r => {
    //        return r;
    //    });
    //}
}

module.component('courseProgramList',
    {
        bindings: {
            coursecode: "@",
            height: '@',
            allowEdit: '@'
        },
        templateUrl: '/src/app/courses/course-program-list.component.html',
        controller: ['$http', controller]
    });
