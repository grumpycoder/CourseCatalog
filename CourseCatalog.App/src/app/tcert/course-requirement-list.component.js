//course-endorsement-list.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;
    ctrl.endorsements = [];
    ctrl.visiblePopup = false;

    this.$onInit = function () {
        ctrl.title = 'Endorsement Courses';

        fetchEndorsements().then(r => {
        });
    }

    ctrl.Refresh = function () {
        ctrl.changeEndorsement();
    }

    ctrl.changeEndorsement = function () {
        if (ctrl.endorsement === undefined) {
            ctrl.courses = [];
            $('#gridContainer').dxDataGrid('instance').option('dataSource', ctrl.courses);
        } else {
            fetchCourses(ctrl.endorsement.endorsementId).then(r => {
                ctrl.courses = r;
                $('#gridContainer').dxDataGrid('instance').option('dataSource', ctrl.courses);
            });
        }
    }

    ctrl.dataGridOptions = {
        dataSource: ctrl.courses,
        editing: {
            mode: 'row',
            allowDeleting: true,
            confirmDelete: false,
            texts: {
                deleteRow: "Remove",
                confirmDeleteMessage: null
            }
        },
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
            storageKey: "gridCoursesEndorseFilterStorage"
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
                        .attr('aria-label', 'Course Details ' + options.data.courseNumber)
                        .attr('href', '/courses/' + options.data.courseNumber)
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
                dataField: 'creditHours', dataType: 'decimal', format: {
                    type: "fixedPoint",
                    precision: 2
                }, caption: 'Credit Hours'
            },
            { dataField: 'scedIdentifier', dataType: 'string', caption: 'Sced Category', visible: false },
            { dataField: 'subject', dataType: 'string', caption: 'Subject' },
            { dataField: 'status', dataType: 'string', caption: 'Status', visible: false }
        ],
        summary: {
            totalItems: [
                {
                    column: "courseCode",
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
                },
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
        },
        onRowRemoving: function (e) { 
            var url = '/api/courses/' + e.data.id + '/endorsements/' + ctrl.endorsement.id; 
            return $http.delete(url)
                .then(r => {
                    toastr.success('Removed ' + e.data.name);
                    //TODO: toastr message
                }).catch(err => {
                    console.error('error', err);
                    toastr.error('Error removing ' + e.data.name);
                    e.cancel = true;
                });
        },
        onRowRemoved: function (e) {
        }
    };

    ctrl.content = "The <b>ScrollView</b> allows users to scroll its content vertically. To enable horizontal and vertical scrolling, set the <b>direction</b> option to <i>\"both\"</i>. Horizontal scrolling is available only if the content is wider than the <b>ScrollView</b>. Otherwise, the content adapts to the widget's width.<br/><br/>The <b>ScrollView</b> uses native scrolling on most platforms, except desktops. To use it on all platforms, assign <b>true</b> to the <b>useNative</b> option. If you assign <b>false</b>, scrolling is simulated on all platforms.";

    ctrl.popupOptions = {
        width: 700,
        height: 350,
        visible: false,
        showTitle: false,
        closeOnOutsideClick: true
    };

    ctrl.showInfo = function () {
        

        console.log('popup', ctrl.popupOptions);
        ctrl.popupOptions.visible = true; 
        console.log('popup', ctrl.popupOptions);

        $("#popupContainer").dxPopup("show");

        //ctrl.currentEmployee =
        //{
        //    "ID": 7,
        //    "FirstName": "Sandra",
        //    "LastName": "Johnson",
        //    "Prefix": "Mrs.",
        //    "Position": "Controller",
        //    "Picture": "images/employees/06.png",
        //    "BirthDate": "1974/11/15",
        //    "HireDate": "2005/05/11",
        //    "Notes": "Sandra is a CPA and has been our controller since 2008. She loves to interact with staff so if you've not met her, be certain to say hi.\r\n\r\nSandra has 2 daughters both of whom are accomplished gymnasts.",
        //    "Address": "4600 N Virginia Rd."
        //}
        //console.log('employee', ctrl.currentEmployee);
        //ctrl.visiblePopup = true;
    }

    var url = '/api/courses/active';

    ctrl.dataCourseGridOptions = {
            dataSource: DevExpress.data.AspNet.createStore({
                key: "id",
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
                    dataType: 'string'
                },
                { dataField: 'name', dataType: 'string' },
                { dataField: 'description', dataType: 'string', width: 200, wordWrapEnabled: false, visible: false },
                { dataField: 'beginYear', dataType: 'int', caption: 'Begin Year' },
                { dataField: 'endYear', dataType: 'int', caption: 'End Year' },
                { dataField: 'lowGrade', dataType: 'string', caption: 'Low Grade' },
                { dataField: 'highGrade', dataType: 'string', caption: 'High Grade' },
                { dataField: 'courseLevel', dataType: 'string', caption: 'Course Level' },
                { dataField: 'scedIdentifier', dataType: 'string', caption: 'Sced Category' },
                { dataField: 'subject', dataType: 'string', caption: 'Subject' }
            ],
            onToolbarPreparing: function (e) {
                var dataGrid = e.component;
                e.toolbarOptions.items.unshift(
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
                    }
                   
                );
            }
        };


    function fetchCourses(endorsementId) {
        return $http.get('/api/courses/endorsements/' + endorsementId).then(r => {
            return r.data;
        });
    }

    function fetchEndorsements() {

        return $http.get('/api/refs/endorsements').then(r => {
            ctrl.endorsements = r.data;
            //ctrl.endorsementStore = new DevExpress.data.ArrayStore({
            //    data: r.data,
            //    key: 'id',
            //    //reshapeOnPush: true
            //});
            //ctrl.endorsementListOptions = {
            //    dataSource: ctrl.endorsementStore,
            //    searchEnabled: true,
            //    searchExpr: "description",
            //    displayExpr: 'description',
            //    valueExpr: 'id',
            //    //onValueChanged: function (item) {
            //    //    ctrl.selectedEndorsement = item.value;
            //    //}
            //}
        });
    }

    ctrl.showExpand = function () {
        if (ctrl.isCollapsed) {
            $('#btnExpandAllButton').show();
        }
        else {
            $('#btnExpandAllButton').hide();
        }
    };

}

module.component('courseRequirementList',
    {
        templateUrl: '/src/app/tcert/course-requirement-list.component.html',
        controller: ['$http', controller]
    });
