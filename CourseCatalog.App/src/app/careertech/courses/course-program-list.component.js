//course-program-list.component.js 

var module = angular.module('app');

function controller($http) {
    var ctrl = this;

    ctrl.title = 'Career Technology Programs';

    ctrl.$onInit = function () {
        $http.get('/api/programs').then(r => {
            ctrl.programs = r.data;
            console.log('programs', ctrl.programs);
        });
    }

    ctrl.dataGridOptions = {
        dataSource: DevExpress.data.AspNet.createStore({
            key: "id",
            loadUrl: "/api/programs"
        }),
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
        loadPanel: {
            text: 'Loading Courses ...'
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
            { dataField: 'programCode', caption: 'Program Code', width: 120, dataType: 'string' },
            { dataField: 'name', caption: 'Name', dataType: 'string' },
            { dataField: 'description', caption: 'Description', dataType: 'string' },
            { dataField: 'programType', caption: 'Program Type', width: 120, dataType: 'string' },
            { dataField: 'clusterCode', caption: 'Cluster Code', width: 120, dataType: 'string' },
            { dataField: 'clusterType', caption: 'Cluster Type', width: 120, dataType: 'string' },
            { dataField: 'beginYear', caption: 'Valid Start', width: 120, dataType: 'string' },
            { dataField: 'endYear', caption: 'Valid End', width: 120, dataType: 'string' }, 
            {
                caption: '',
                width: 75,
                cssClass: 'center-col',
                cellTemplate: function (container, options) {
                    $('<a/>').addClass('btn btn-outline-primary')
                        .text('')
                        .attr('aria-label', 'Edit Cluster ' + options.data.clusterCode)
                        .attr('title', 'Edit Cluster ' + options.data.clusterCode)
                        .attr('data-toggle', 'tooltip')
                        .attr('data-placement', 'top')
                        .attr('href', '/careertech/clusters/' + options.data.clusterCode + '/edit')
                        .append('<i class="fa fa-pencil">')
                        .on('dxclick',
                            function (e) {
                                $('<a href="/careertech/clusters/' + options.data.clusterCode + '/edit>' + options.data.clusterCode + '</a>').appendTo(container);
                            })
                        .appendTo(container);
                }
            }
        ],
        summary: {
            totalItems: [
                {
                    column: "clusterCode",
                    displayFormat: '{0} Clusters',
                    summaryType: 'count',
                    showInGroupFooter: true,
                    showInColumn: 'clusterCode'
                }
            ],
            groupItems: [
                {
                    summaryType: "count",
                    displayFormat: '{0} Clusters'
                }

            ]
        }
    }
}


module.component('courseProgramList',
    {
        bindings: {
        },
        templateUrl: '/src/app/careertech/courses/course-program-list.component.html',
        controller: ['$http', controller]
    });

