//credential-list.component.js


var module = angular.module('app');

function controller($http) {

    var ctrl = this;

    ctrl.$onChanges = function() {
        ctrl.isAdmin = (ctrl.isAdmin == 'true');
    }

    ctrl.$onInit = function () {
        ctrl.title = 'CTE Credentials';
        
        $http.get('/api/credentials').then(r => {

            ctrl.credentials = r.data;

            ctrl.dataGridOptions = {
                dataSource: ctrl.credentials,
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
                    placeholder: 'Search...'
                },
                loadPanel: {
                    text: 'Loading Programs ...'
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
                stateStoring: {
                    enabled: true,
                    type: "localStorage",
                    storageKey: "gridCredentialFilterStorage"
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
                        dataField: 'credentialCode',
                        caption: 'Credential Code',
                        dataType: 'string',
                        width: 100,
                        cellTemplate: function (container, options) {
                            $('<a/>')
                                .text(options.data.credentialCode)
                                .attr('aria-label', 'Credentials Details ' + options.data.credentialCode)
                                .attr('href', '/careertech/credentials/' + options.data.credentialId)
                                .appendTo(container);
                        }
                    },
                    { dataField: 'name', caption: 'Name', width: 250, dataType: 'string' },
                    { dataField: 'description', caption: 'Description', width: 250, dataType: 'string' },
                    { dataField: 'credentialType', caption: 'Type', width: 110, dataType: 'string' },
                    { dataField: 'credentialTypeCode', caption: 'Code', width: 100, dataType: 'string' },
                    {
                        dataField: 'isReimbursable', caption: 'Reimbursable', dataType: 'boolean',
                        trueText: 'Yes',
                        falseText: 'No',
                        visible: false
                    },
                    { dataField: 'beginYear', caption: 'Begin Year', dataType: 'int' },
                    { dataField: 'endYear', caption: 'End Year', dataType: 'int' },
                    {
                        caption: '',
                        visible: ctrl.isAdmin,
                        width: 75,
                        cssClass: 'center-col',
                        cellTemplate: function (container, options) {
                            $('<a/>').addClass('btn btn-outline-primary')
                                .text('')
                                .attr('aria-label', 'Edit Credential ' + options.data.credentialCode)
                                .attr('title', 'Edit Credential ' + options.data.credentialCode)
                                .attr('data-toggle', 'tooltip')
                                .attr('data-placement', 'top')
                                .attr('href', '/careertech/credentials/' + options.data.credentialCode + '/edit')
                                .append('<i class="fa fa-pencil">')
                                .on('dxclick',
                                    function (e) {
                                        $('<a href="/careertech/programs/' +
                                            options.data.credentialCode +
                                            '/edit>' +
                                            options.data.programCode +
                                            '</a>').appendTo(container);
                                    })
                                .appendTo(container);
                        }
                    }
                ],
                summary: {
                    totalItems: [
                        {
                            column: "credentialCode",
                            displayFormat: '{0} Credentials',
                            summaryType: 'count',
                            showInGroupFooter: true,
                            showInColumn: 'credentialCode'
                        }
                    ],
                    groupItems: [
                        {
                            summaryType: "count",
                            displayFormat: '{0} Credentials'
                        }

                    ]
                },
                onContentReady: function (e) {
                    ctrl.isCollapsed = e.component.columnOption("groupIndex:0") !== undefined;
                    ctrl.showExpand();
                    $('[data-toggle="tooltip"]').tooltip();
                },
                onOptionChanged: function (e) {
                    ctrl.isCollapsed = e.component.columnOption("groupIndex:0") !== undefined;
                    ctrl.showExpand();
                },
                onExporting: function (e) {
                    var time = new Date(),
                        timeStamp =
                            ("0" + time.getMonth().toString()).slice(-2) +
                            ("0" + time.getDay().toString()).slice(-2) +
                            ("0" + time.getFullYear().toString()).slice(-2) +
                            '-' +
                            ("0" + time.getHours().toString()).slice(-2) +
                            ("0" + time.getMinutes().toString()).slice(-2) +
                            ("0" + time.getSeconds().toString()).slice(-2),
                        fileName = "Course-List-" + timeStamp;
                    e.fileName = fileName;
                },
                onToolbarPreparing: function (e) {
                    var dataGrid = e.component;
                    e.toolbarOptions.items.unshift(
                        {
                            location: "after",
                            widget: "dxButton",
                            options: {
                                text: "Expand All",
                                elementAttr: { "id": "btnExpandAllButton", 'ng-show': ctrl.isCollapsed, "data-toggle": "tooltip", "data-placement": "top" },
                                width: 136,
                                onClick: function (e) {
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
                                hint: 'Refresh',
                                elementAttr: { "data-toggle": "tooltip", "data-placement": "top" },
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
                                elementAttr: { "data-toggle": "tooltip", "data-placement": "top" },
                                onClick: function () {
                                    dataGrid.clearFilter();
                                }
                            }
                        },
                        {
                            location: "after",
                            widget: "dxButton",
                            options: {
                                icon: "pulldown",
                                hint: 'Reset grid to default',
                                elementAttr: { "data-toggle": "tooltip", "data-placement": "top" },
                                onClick: function () {
                                    dataGrid.state({});
                                }
                            }
                        }
                        ,
                        {
                            location: "after",
                            widget: "dxButton",
                            options: {
                                icon: "save",
                                hint: 'Export',
                                elementAttr: { "data-toggle": "tooltip", "data-placement": "top" },
                                onClick: function () {
                                    dataGrid.exportToExcel(false);
                                }
                            }
                        },
                        {
                            location: "after",
                            widget: "dxButton",

                            options: {
                                icon: "column-chooser",
                                hint: 'Column Chooser',
                                elementAttr: { "data-toggle": "tooltip", "data-placement": "top" },
                                onClick: function () {
                                    dataGrid.showColumnChooser();
                                }
                            }
                        }
                    );
                }
            }
        });


    };

    ctrl.showExpand = function () {
        if (ctrl.isCollapsed) {
            $('#btnExpandAllButton').show();
        }
        else {
            $('#btnExpandAllButton').hide();
        }
    };
}

module.component('credentialList',
    {
        bindings: {
            isAdmin: '@'
        },
        templateUrl: '/src/app/careertech/credentials/credential-list.component.html',
        controller: ['$http', controller]
    });


