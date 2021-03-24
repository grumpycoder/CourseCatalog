//course-list.component.js

var module = angular.module('app');

function controller($http) {

    var ctrl = this;

    ctrl.$onInit = function () {
        var url = '/api/courses/';
        ctrl.title = 'Course Catalog';
        ctrl.isAdmin = ctrl.isAdmin == 'true';

        if (ctrl.filter === 'active') {
            url += 'active';
            ctrl.title += ' (Active Courses)';
        }

        ctrl.isCollapsed = true;

        ctrl.dataGridOptions = {
            dataSource: DevExpress.data.AspNet.createStore({
                key: "courseId",
                loadUrl: url
            }),
            remoteOperations: true,
            scrolling: {
                mode: "virtual",
                rowRenderingMode: "virtual"
            },
            paging: {
                pageSize: 20
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
                placeholder: 'Search...'
            },
            loadPanel: {
                text: 'Loading Courses...'
            },
            groupPanel: {
                visible: true,
                allowColumnDragging: true,
                emptyPanelText: "Drag a column header here to group by that column",
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
                storageKey: "gridCoursesFilterStorage"
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
                    dataField: 'courseNumber',
                    caption: 'Course Number',
                    dataType: 'string',
                    cellTemplate: function (container, options) {
                        $('<a/>')
                            .text(options.data.courseNumber)
                            .attr('aria-label', 'Course Details ' + options.data.courseId)
                            .attr('href', '/courses/' + options.data.courseId)
                            .appendTo(container);
                    }
                },
                { dataField: 'name', dataType: 'string' },
                { dataField: 'description', dataType: 'string', width: 200, wordWrapEnabled: false, visible: false },
                { dataField: 'beginYear', dataType: 'int', caption: 'Begin Year' },
                { dataField: 'endYear', dataType: 'int', caption: 'End Year' },
                { dataField: 'lowGrade', dataType: 'string', caption: 'Low Grade' },
                { dataField: 'highGrade', dataType: 'string', caption: 'High Grade' },
                { dataField: 'cipCode', dataType: 'string', visible: false },
                { dataField: 'courseLevel', dataType: 'string', caption: 'Course Level' },
                {
                    dataField: 'creditHours',
                    dataType: 'decimal',
                    format: {
                        type: "fixedPoint",
                        precision: 2
                    },
                    caption: 'Credit Hours'
                },
                { dataField: 'scedIdentifier', dataType: 'string', caption: 'Sced Category' },
                { dataField: 'subject', dataType: 'string', caption: 'Subject' },
                { dataField: 'status', dataType: 'string', caption: 'Status' },
                {
                    caption: '',
                    visible: ctrl.isAdmin,
                    cellTemplate: function (container, options) {
                        $('<button>')
                            .append('<i class="fa fa-pencil"></i>')
                            .addClass('btn btn btn-outline-dark btn-sm')
                            .attr('aria-label', 'Create Draft ' + options.data.courseNumber)
                            .attr('title', 'Create Draft ' + options.data.courseNumber)
                            .attr('data-toggle', 'tooltip')
                            .attr('data-placement', 'top')
                            .on('dxclick',
                                function (e) {
                                    $http.post('/api/drafts/' + options.data.courseId + '/create').then(r => {
                                        toastr.success('Created draft ' + options.data.courseNumber);
                                        window.location.href = '/drafts/' + r.data;
                                    }).catch(err => {
                                        if (err.data.exceptionType.includes('EntityFrameworkCore')) {
                                            toastr.error('Database error creating draft');
                                        } else {
                                            toastr.error(err.data.exceptionMessage);
                                        }
                                        console.log('err', err);
                                    });
                                })
                            .appendTo(container);
                    }
                }
            ],
            summary: {
                totalItems: [
                    {
                        column: "courseNumber",
                        displayFormat: '{0} Courses',
                        summaryType: 'count',
                        showInGroupFooter: true,
                        showInColumn: 'CourseCode'
                    }
                ],
                groupItems: [
                    {
                        summaryType: "count",
                        displayFormat: '{0} Courses'
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
        };

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

module.component('courseList',
    {
        bindings: {
            filter: '@',
            isAdmin: '@'
        },
        templateUrl: '/src/app/courses/course-list.component.html',
        controller: ['$http', controller]
    });

