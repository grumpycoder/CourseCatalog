//draft-requirement-edit.component.js

var module = angular.module('app');

function controller($http) {
    var ctrl = this;
    ctrl.endorsements = [];

    this.$onInit = function () {
        fetchEndorsements().then(r => {});
    }

    ctrl.$onChanges = function () {
        if (ctrl.course) {
            ctrl.listOptions = {
                dataSource: ctrl.course.endorsements,
                searchEnabled: true,
                searchExpr: "description",
                height: 500,
                allowItemDeleting: true,
                onItemDeleting: function (data) {
                    ctrl.removeEndorsement(data.itemData);
                }
            }
        }
    }

    ctrl.removeEndorsement = function (item) {
        var idx = ctrl.course.endorsements.indexOf(item);
        var dto = {
            draftId: ctrl.course.draftId,
            endorsementId: item.endorsementId
        }; 
        

        console.log('dto', dto);
        //return $http.delete('/api/drafts/deleterequirement', {deleteRequirementCommand: dto})
        var url = '/api/drafts/' + ctrl.course.draftId + '/endorsements/' + item.endorsementId; 
        console.log('url', url);
        return $http.delete('/api/drafts/' + ctrl.course.draftId + '/endorsements/' + item.endorsementId)
            .then(r => {
                ctrl.course.endorsements.splice(idx, 1);
                toastr.success('Removed ' + item.endorseCode);
            }).catch(err => {
                console.error('error', err);
                toastr.error(err.data.message);
            });
    }

    ctrl.addEndorsement = function () {
        var idx = ctrl.course.endorsements.find(o => { return o.endorsementId === ctrl.endorsementId });
        if (idx !== undefined) {
            toastr.info('Endorsement already exists');
            return;
        }

        //$http.post('/api/drafts/' + ctrl.course.id + '/endorsements/' + ctrl.endorsementId)
        var dto = {
            draftId: ctrl.course.draftId, 
            endorsementId: ctrl.endorsementId
        }
        console.log(dto);
        $http.post('/api/drafts/createrequirement', dto)
            .then(r => {
                ctrl.course.endorsements.push(r.data);
                toastr.success('Added endorsement ' + r.data.endorseCode);
                $('#endorsements').dxList('instance').reload();
                ctrl.endorsementId = undefined;
            }).catch(err => {
                console.error(err);
                toastr.error(err.data.message);
            });
    }

    function fetchEndorsements() {

        return $http.get('/api/refs/endorsements').then(r => {
            ctrl.endorsements = r.data;

            ctrl.endorsementListOptions = {
                dataSource: ctrl.endorsements, 
                displayExpr: 'description', 
                searchEnabled: true, 
                searchExpr: "description", 
                valueExpr: 'endorsementId'
            }
        });
    }
}

module.component('draftRequirementEdit',
    {
        bindings: {
            course: '<'
        },
        templateUrl: '/src/app/drafts/draft-requirement-edit.html',
        controller: ['$http', controller]
    });