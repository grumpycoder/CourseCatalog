﻿//perf-viewer.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.$onInit = function () {
        ctrl.title = 'Performance Log Viewer';
    }

    var url = '/api/logs/perf';

    ctrl.dataGridOptions = {
        dataSource: DevExpress.data.AspNet.createStore({
            key: "id",
            loadUrl: url
        }),
        remoteOperations: true,
        allowColumnResizing: true,
        allowColumnReordering: true,
        showBorders: true,
        wordWrapEnabled: false,
        'export': {
            enabled: true,
            fileName: "Log",
            allowExportSelectedData: false,
            icon: 'fa fa-trash'
        },
        stateStoring: {
            enabled: true,
            type: "localStorage",
            storageKey: "gridPerfFilterStorage"
        },
        filterRow: { visible: true },
        headerFilter: { visible: true },
        groupPanel: { visible: true },
        scrolling: { mode: "virtual", rowRenderingMode: "virtual" },
        paging: { pageSize: 20 },
        height: 650,
        columnChooser: { enabled: true },
        columnResizingMode: "nextColumn",
        columnMinWidth: 50,
        columnAutoWidth: true,
        columns: [
            { dataField: 'hostname', caption: 'Host' },
            { dataField: 'layer', caption: 'Layer' },
            { dataField: 'location', caption: 'Location' },
            { dataField: 'userName', caption: 'User' },
            { dataField: 'message', caption: 'Message', width: 200, wordWrapEnabled: false },
            { dataField: 'elapsedMilliseconds', caption: 'Elapsed Milliseconds', visible: false },
            { dataField: 'additionalInfo', caption: 'Additional Info', width: 200, wordWrapEnabled: false },
            { dataField: 'timestamp', caption: 'Time Stamp', dataType: 'datetime' }
        ],
        onToolbarPreparing: function (e) {
            var dataGrid = e.component;

            e.toolbarOptions.items.unshift(
                {
                    location: "after",
                    widget: "dxButton",
                    options: {
                        text: "Collapse All",
                        width: 136,
                        onClick: function (e) {
                            var expanding = e.component.option("text") === "Expand All";
                            dataGrid.option("grouping.autoExpandAll", expanding);
                            e.component.option("text", expanding ? "Collapse All" : "Expand All");
                        }
                    }
                },
                {
                    location: "after",
                    widget: "dxButton",
                    options: {
                        text: "Toggle Wrap",
                        width: 160,
                        onClick: function (e) {
                            dataGrid.option("wordWrapEnabled", !dataGrid.option("wordWrapEnabled"));
                        }
                    }
                },
                {
                    location: "after",
                    widget: "dxButton",

                    options: {
                        icon: "refresh",
                        hint: 'Refresh',
                        onClick: function () {
                            dataGrid.refresh();
                        }
                    }
                },
                {
                    location: "after",
                    widget: "dxButton",
                    options: {
                        icon: "clearformat",
                        hint: 'Clear filters',
                        onClick: function () {
                            dataGrid.clearFilter();
                        }
                    }
                },
                {
                    location: "after",
                    widget: "dxButton",
                    options: {
                        icon: "clearsquare",
                        hint: 'Reset grid to default',
                        onClick: function () {
                            dataGrid.state({});
                        }
                    }
                }

            );
        }
    }


}

module.component('perfViewer',
    {
        templateUrl: '/src/app/admin/perf-viewer.component.html',
        controller: ['$http', controller]
    });
