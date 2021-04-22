//temp-course-list.component.js 

var module = angular.module("app");

function controller($http) {
    var ctrl = this;

    ctrl.title = "Career Technology Courses";

    ctrl.$onInit = function() {

        definePrograms();

        ctrl.dataGridOptions = {
            dataSource: new DevExpress.data.CustomStore({
                key: "id",
                load: function() {
                    return $http.get("/api/courses/").then(r => { return r.data; });
                },
                update: function(key, values) {
                    ctrl.selectedCourse.cipCode = values;
                    if (values.cipCode === "") values.cipCode = 0;
                    const url = `/api/courses/${ctrl.selectedCourse.id}/cip/${values.cipCode}/`;
                    return $http.put(url).then(r => {
                        toastr.success("Updated Course");
                    }).catch(e => {
                        if (e.status === 404) {
                            toastr.error("Error updating course. Url not found.");
                            return;
                        }
                        toastr.error(e.message);
                    });
                }
            }),
            scrolling: {
                mode: "virtual",
                rowRenderingMode: "virtual"
            },
            paging: {
                pageSize: 200,
                enabled: false
            },
            remoteOperations: {
                filtering: false
            },
            headerFilter: {
                visible: true,
                allowSearch: true
            },
            filterRow: {
                visible: true
            },
            filterPanel: {
                visible: true
            },
            searchPanel: {
                visible: true,
                placeholder: "Search..."
            },
            loadPanel: {
                text: "Loading Courses ..."
            },
            groupPanel: {
                visible: true,
                allowColumnDragging: true,
                emptyPanelText: "Drag a column header here to group by that column"
            },
            grouping: {
                allowCollapsing: true,
                autoExpandAll: false,
                contextMenuEnabled: true,
                expandMode: "rowClick"
            },
            selection: {
                mode: "single"
            },
            editing: {
                mode: "row",
                allowUpdating: true
            },
            hoverStateEnabled: true,
            height: 650,
            allowColumnResizing: true,
            allowColumnReordering: true,
            columnResizingMode: "nextColumn",
            wordWrapEnabled: true,
            showBorders: true,
            columnAutoWidth: true,
            columnMinWidth: 50,
            columns: [
                { dataField: "courseCode", caption: "CourseCode", width: 120, dataType: "string", allowEditing: false },
                { dataField: "title", caption: "Title", dataType: "string", allowEditing: false },
                { dataField: "courseType", caption: "Course Type", dataType: "string", allowEditing: false },
                { dataField: "cipCode", caption: "CIP Code", width: 120, dataType: "string" },
            ],
            onEditingStart: function(e) {
                ctrl.selectedCourse = e.data;
            },
            onRowUpdated: function(e) {
            },
            onSelectionChanged: function(selectedItems) {
                ctrl.selectedCourse = selectedItems.selectedRowsData[0];
            },
            summary: {
                totalItems: [
                    {
                        column: "courseCode",
                        displayFormat: "{0} Course",
                        summaryType: "count",
                        showInGroupFooter: true,
                        showInColumn: "courseCode"
                    }
                ],
                groupItems: [
                    {
                        summaryType: "count",
                        displayFormat: "{0} Courses"
                    }
                ]
            }
        };

    };

    ctrl.removeProgram = function(program) {
        const url = `/api/courses/${ctrl.selectedCourse.id}/programs/${program.programId}`;;

        $http.delete(url).then(r => {
            var idx = ctrl.selectedCourse.programAssignments.indexOf(program);
            ctrl.selectedCourse.programAssignments.splice(idx, 1);
            toastr.success("Removed Program");
        }).catch(e => {
            toastr.error(e.message);
        });
    };

    ctrl.addProgram = function(program) {
        const url = `/api/courses/${ctrl.selectedCourse.id}/programs`;
        const dto = {
            programId: ctrl.selectedProgram.id,
            beginYear: 2019,
            endYear: 2020
        };

        $http.post(url, dto).then(r => {
            ctrl.selectedCourse.programAssignments.push(r.data);
            toastr.success("Assigned Program");
        }).catch(e => {
            toastr.error(e.message);
        });
    };

    function definePrograms() {
        $http.get("/api/programs").then(r => {
            ctrl.availablePrograms = {
                dataSource: new DevExpress.data.DataSource({
                    store: r.data,
                    group: "cluster"
                }),
                showClearButton: true,
                searchExpr: ["name", "programCode", "cluster"],
                searchEnabled: true,
                width: "78%",
                valueExp: "programCode",
                displayExpr: "name",
                grouped: true,
                groupTemplate: function(data) {
                    return $(`<div>Cluster: ${data.key}</div>`);
                },
                itemTemplate: function(data) {
                    return $("<div>").text(data.name);
                },
                onSelectionChanged: function(r) {
                    if (r === undefined) return;
                    ctrl.selectedProgram = angular.copy(r.selectedItem);
                }
            };
        });
    }
}


module.component("ctCourseList",
    {
        bindings: {
        },
        templateUrl: "/src/app/careertech/courses/ct-course-list.component.html",
        controller: ["$http", controller]
    });