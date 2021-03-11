//draft-detail-edit.component.js

var module = angular.module('app').value('user', {});

function detailController($http) {
    var ctrl = this;
    ctrl.selectedDeliveryTypes = [];
    ctrl.cacheSelectedDeliveryTypes = [];
    ctrl.course = {
        tags: []
    };

    this.$onChanges = function () {
        //path for new course
        if (!ctrl.course) {
            ctrl.course = {};
            ctrl.scedCategoryCode = '##';
            ctrl.courseLevelCode = '##';
            ctrl.stateAttribute1 = '##';
            ctrl.stateAttribute2 = '##';
            ctrl.scedCourseNumber = '##';
            ctrl.updateCourseNumber();
            ctrl.course.status = 'NewCourse';
            loadRefs();
        }
        //path for existing course
        if (ctrl.course) {
            fetchScedCategories();
            if (ctrl.course.deliveryTypes !== undefined)
                ctrl.course.deliveryTypes.forEach(e => {
                    ctrl.selectedDeliveryTypes.push(e.deliveryTypeId);
                });

            if (ctrl.course.tags !== undefined) {
                ctrl.selectedTags = JSON.parse(JSON.stringify(ctrl.course.tags));
                ctrl.selectedCreditTypeTags = JSON.parse(JSON.stringify(ctrl.course.creditTypes));
            }
            ctrl.canEditCourseNumber = ctrl.course.status === 'NewCourse';
        }
    };

    ctrl.$onInit = function () {
        loadRefs();
    }

    ctrl.onSubmit = function () {
        var url = '/api/drafts/';

        ctrl.course.tags = ctrl.selectedTags;
        ctrl.course.creditTypes = ctrl.selectedCreditTypeTags;
        
        if (!ctrl.course.draftId) {
            $http.post(url + '/create', ctrl.course).then(r => {
                toastr.success('Created Course Draft');
                window.location.href = '/drafts/' + r.data + '/edit';
            }).catch(e => {
                console.error('update error', e);
                toastr.error(e.data.message);
            });
            return;
        }

        $http.post(url, ctrl.course).then(r => {
            updateCache();
            toastr.success('Saved Course Draft');
        }).catch(e => {
            console.error('update error', e.message);
            toastr.error(e.message);
        });
    };

    ctrl.cancel = function () {
        loadCache();
    };

    ctrl.updateCourseNumber = function () {
        if (!ctrl.course.stateAttribute1) {
            ctrl.stateAttribute1 = '##';
        } else {
            ctrl.stateAttribute1 = ctrl.course.stateAttribute1;
        }
        if (!ctrl.course.stateAttribute2) {
            ctrl.stateAttribute2 = '##';
        } else {
            ctrl.stateAttribute2 = ctrl.course.stateAttribute2;
        }

        if (ctrl.course.scedCourseNumber) {
            ctrl.scedCourseNumber = ctrl.course.scedCourseNumber;
        }

        if (ctrl.course.scedCategoryId) {
            ctrl.scedCategoryCode = ctrl.scedCategories.find(s => s.scedCategoryId === ctrl.course.scedCategoryId).code;
        }
        if (ctrl.course.courseLevelId) {
            ctrl.courseLevelCode = ctrl.courseLevels.find(s => s.courseLevelId === ctrl.course.courseLevelId).courseLevelCode;
        }

        ctrl.course.courseNumber = ctrl.scedCategoryCode + ctrl.scedCourseNumber + ctrl.courseLevelCode + ctrl.stateAttribute1 + ctrl.stateAttribute2;
    }

    function loadRefs() {
        fetchSchoolYears();
        fetchGrades();
        fetchGradeScales();
        fetchSubjects();
        fetchTags();
        fetchDeliveryTypes();
        fetchCourseLevels();
        fetchScedCategories();
    }

    function fetchSchoolYears() {
        return $http.get('/api/refs/schoolyears').then(function (r) {
            return ctrl.schoolYears = r.data;
        });
    }

    function fetchCourseLevels() {
        return $http.get('/api/refs/courseLevels').then(function (r) {

            ctrl.courseLevelSelect = {
                dataSource: r.data,
                displayExpr: 'name',
                valueExpr: 'courseLevelId',
                onValueChanged: function (e) {
                    ctrl.updateCourseNumber();
                }
            };

            return ctrl.courseLevels = r.data;
        });
    }

    function fetchGradeScales() {
        return $http.get('/api/refs/gradeScales').then(function (r) {
            return ctrl.gradeScales = r.data;
        });
    }

    function fetchGrades() {
        if (ctrl.grades !== undefined) {
            return true;
        }

        return $http.get('/api/refs/grades').then(function (r) {
            ctrl.grades = r.data;

            var store = new DevExpress.data.DataSource({
                store: ctrl.grades
            });

            ctrl.gradeListOptions = {
                dataSource: store,
                searchEnabled: true,
                searchMode: 'contains',
                searchExpr: ['description', 'name'],
                displayExpr: function (item) {
                    return item && item.name + ' - ' + item.description;
                },
                valueExpr: 'id',
                itemTemplate: function (data) {
                    return $("<div>").text(data.name + ' - ' + data.description);
                },
                onSelectionChanged: function (e) {
                    if (e.element[0].id === 'highGradeId') {
                        ctrl.selectedHighGrade = e.selectedItem.name;
                    } else {
                        ctrl.selectedLowGrade = e.selectedItem.name;
                    }
                    ctrl.validateCourseCode();
                }
            }
            return ctrl.grades = r.data;
        });
    }

    function fetchScedCategories() {
        return $http.get('/api/refs/scedcategories').then(function (r) {
            ctrl.scedCategories = r.data;

            ctrl.scedSelect = {
                dataSource: r.data,
                displayExpr: 'identifier',
                valueExpr: 'scedCategoryId',
                onValueChanged: function (e) {
                    console.log('change', e);
                    console.log(ctrl.course);
                    ctrl.updateCourseNumber();
                }
            };

            return ctrl.scedCategories;
        });
    }

    function fetchSubjects() {
        return $http.get('/api/refs/subjects').then(function (r) {
            ctrl.subjects = r.data;
            return ctrl.subjects;
        });
    }

    function fetchTags() {
        return $http.get('/api/refs/tags').then(function (r) {

            ctrl.creditTypeTags = r.data.filter(c => { return c.groupName === 'CreditType' });
            ctrl.generalTags = r.data.filter(c => { return c.groupName === 'General' });

            ctrl.creditTypesTags = {
                dataSource: ctrl.creditTypeTags,
                displayExpr: 'description',
                valueExpr: 'name',
                searchEnabled: true,
                hideSelectedItems: true,
                multiline: true,
                showSelectionControls: true,
                applyValueMode: 'useButtons'
            };

            ctrl.tags = {
                dataSource: ctrl.generalTags,
                displayExpr: 'description',
                valueExpr: 'name',
                searchEnabled: true,
                hideSelectedItems: true,
                multiline: true,
                showSelectionControls: true,
                applyValueMode: 'useButtons'
            };

            return ctrl.creditTypeTags;
        });
    }

    function fetchDeliveryTypes() {
        return $http.get('/api/refs/deliveryTypes').then(function (r) {
            ctrl.deliveryTypes = r.data;

            var source = new DevExpress.data.DataSource({
                store: new DevExpress.data.ArrayStore({
                    key: 'deliveryTypeId',
                    data: ctrl.deliveryTypes
                })
            });


            ctrl.deliveryTypeOptions = {
                dataSource: source,
                height: 120,
                showSelectionControls: true,
                selectionMode: 'multiple',
                bindingOptions: {
                    selectedItemKeys: '$ctrl.selectedDeliveryTypes'
                },
                displayExpr: function (item) {
                    return item.name;
                },
                onSelectionChanged: function (e) {
                    if (!ctrl.course.deliveryTypes) ctrl.course.deliveryTypes = [];

                    e.addedItems.forEach(item => {
                        addDeliveryType(item);
                        e.model.$ctrl.form.$setDirty();
                    });

                    e.removedItems.forEach(item => {
                        removeDeliveryType(item);
                        e.model.$ctrl.form.$setDirty();
                    });
                }
            };
            return ctrl.deliveryTypes;
        });
    };

    function addDeliveryType(item) {
        var deliveryType = {
            draftDeliveryTypeId: 0, 
            deliveryTypeId: item.deliveryTypeId, 
            draftId: ctrl.course.draftId 

        }
        if (!ctrl.course.deliveryTypes.find(c => c.deliveryTypeId === deliveryType.deliveryTypeId)) {
            ctrl.course.deliveryTypes.push(deliveryType);
        };
    }

    function removeDeliveryType(item) {
        let found = ctrl.course.deliveryTypes
            .find(cdt => cdt.deliveryTypeId === item.deliveryTypeId);

        if (found) {
            let idx = ctrl.course.deliveryTypes.indexOf(found);
            ctrl.course.deliveryTypes.splice(idx, 1);
        }
    }

    function updateCache() {
        ctrl.cache = angular.copy(ctrl.course);

        if (ctrl.course.deliveryTypes !== undefined) {
            ctrl.course.deliveryTypes.forEach(e => {
                ctrl.cacheSelectedDeliveryTypes.push(e.deliveryTypeId);
            });
        }
        resetValidation();
    }

    function loadCache() {
        ctrl.selectedDeliveryTypes = [];
        ctrl.selectedTags = [];

        ctrl.course = angular.copy(ctrl.cache);
        ctrl.selectedTags = ctrl.course.tags;
        ctrl.selectedCreditTypes = ctrl.course.creditTypes;
        ctrl.cacheSelectedDeliveryTypes.forEach(e => {
            ctrl.selectedDeliveryTypes.push(e);
        });
        resetValidation();
    }

    function resetValidation() {
        if (ctrl.form !== undefined) {
            ctrl.form.$setPristine();
            ctrl.form.$setUntouched();
        }
    }

}

module.component('draftDetailEdit',
    {
        bindings: {
            course: '<', 
            isAdmin: '<'
        },
        templateUrl: '/src/app/drafts/draft-detail-edit.component.html',
        controller: ['$http', detailController]
    });

