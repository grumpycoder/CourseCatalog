//cluster-list.component.js

var module = angular.module("app");

function controller($http) {
    var ctrl = this;

    ctrl.title = "CTE Clusters";

    ctrl.$onInit = function() {
        $http.get("/api/clusters").then(r => {
            ctrl.clusters = r.data;
            ctrl.dataGridOptions = {
                dataSource: ctrl.clusters,
                stateStoring: {
                    enabled: true,
                    type: "localStorage",
                    storageKey: "gridClustersFilterStorage"
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
                    text: "Loading Clusters ..."
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
                    {
                        dataField: "clusterCode",
                        caption: "Cluster Code",
                        dataType: "string",
                        width: 120,
                        cellTemplate: function(container, options) {
                            $("<a/>")
                                .text(options.data.clusterCode)
                                .attr("aria-label", `Cluster Details ${options.data.clusterId}`)
                                .attr("href", `/careertech/clusters/${options.data.clusterId}`)
                                .appendTo(container);
                        }
                    },
                    { dataField: "name", caption: "Name", dataType: "string" },
                    { dataField: "description", caption: "Description", dataType: "string" },
                    { dataField: "clusterTypeName", caption: "Cluster Type", width: 120, dataType: "string" },
                    { dataField: "clusterTypeCode", caption: "Cluster Type Code", width: 120, dataType: "string" },
                    { dataField: "beginYear", caption: "Start Year", width: 120, dataType: "int" },
                    { dataField: "endYear", caption: "End Year", width: 120, dataType: "int" },
                    {
                        caption: "",
                        visible: ctrl.isAdmin,
                        width: 75,
                        cssClass: "center-col",
                        cellTemplate: function(container, options) {
                            $("<a/>").addClass("btn btn-sm btn-outline-dark")
                                .text("")
                                .attr("aria-label", `Edit Cluster ${options.data.clusterCode}`)
                                .attr("title", `Edit Cluster ${options.data.clusterCode}`)
                                .attr("data-toggle", "tooltip")
                                .attr("data-placement", "top")
                                .attr("href", `/careertech/clusters/${options.data.clusterId}/edit`)
                                .append('<i class="fa fa-pencil">')
                                .on("dxclick",
                                    function(e) {
                                        $(`<a href="/careertech/clusters/${options.data.clusterId}/edit>${options.data
                                            .clusterCode}</a>`).appendTo(container);
                                    })
                                .appendTo(container);
                        }
                    }
                ],
                summary: {
                    totalItems: [
                        {
                            column: "clusterCode",
                            displayFormat: "{0} Clusters",
                            summaryType: "count",
                            showInGroupFooter: true,
                            showInColumn: "clusterCode"
                        }
                    ],
                    groupItems: [
                        {
                            summaryType: "count",
                            displayFormat: "{0} Clusters"
                        }
                    ]
                },
                onToolbarPreparing: function(e) {
                    var dataGrid = e.component;
                    e.toolbarOptions.items.unshift(
                        {
                            location: "after",
                            widget: "dxButton",
                            options: {
                                text: "Expand All",
                                elementAttr: {
                                    "id": "btnExpandAllButton",
                                    'ng-show': ctrl.isCollapsed,
                                    "data-toggle": "tooltip",
                                    "data-placement": "top"
                                },
                                width: 136,
                                onClick: function(e) {
                                    ctrl.isCollapsed = !dataGrid.option("grouping.autoExpandAll") !== undefined;
                                    e.component.option("text", ctrl.isCollapsed ? "Collapse All" : "Expand All");
                                    dataGrid.option("grouping.autoExpandAll", ctrl.isCollapsed);
                                }
                            }
                        },
                        {
                            location: "after",
                            widget: "dxButton",
                            options: {
                                icon: "refresh",
                                hint: "Refresh",
                                elementAttr: { "data-toggle": "tooltip", "data-placement": "top" },
                                onClick: function() {
                                    dataGrid.refresh();
                                }
                            }
                        },
                        {
                            location: "after",
                            widget: "dxButton",
                            options: {
                                icon: "clearformat",
                                hint: "Clear filters",
                                elementAttr: { "data-toggle": "tooltip", "data-placement": "top" },
                                onClick: function() {
                                    dataGrid.clearFilter();
                                }
                            }
                        },
                        {
                            location: "after",
                            widget: "dxButton",
                            options: {
                                icon: "pulldown",
                                hint: "Reset grid to default",
                                elementAttr: { "data-toggle": "tooltip", "data-placement": "top" },
                                onClick: function() {
                                    dataGrid.state({});
                                }
                            }
                        },
                        {
                            location: "after",
                            widget: "dxButton",
                            options: {
                                icon: "save",
                                hint: "Export",
                                elementAttr: { "data-toggle": "tooltip", "data-placement": "top" },
                                onClick: function() {
                                    dataGrid.exportToExcel(false);
                                }
                            }
                        },
                        {
                            location: "after",
                            widget: "dxButton",

                            options: {
                                icon: "column-chooser",
                                hint: "Column Chooser",
                                elementAttr: { "data-toggle": "tooltip", "data-placement": "top" },
                                onClick: function() {
                                    dataGrid.showColumnChooser();
                                }
                            }
                        }
                    );
                }
            };
        });
    };

}


module.component("clusterList",
    {
        bindings: {
            isAdmin: "<"
        },
        templateUrl: "/src/app/careertech/clusters/cluster-list.component.html",
        controller: ["$http", controller]
    });