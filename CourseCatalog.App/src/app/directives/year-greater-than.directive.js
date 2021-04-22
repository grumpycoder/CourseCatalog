//year-greater-than.directive.js

var module = angular.module("app");

module.directive("numberGreaterThan",
    function() {
        return {
            require: "ngModel",
            link: function(scope, element, attrs, ctrl) {

                var validate = function(viewValue) {
                    const comparisonModel = attrs.numberGreaterThan;

                    if (!viewValue || !comparisonModel) {
                        ctrl.$setValidity("numberGreaterThan", true);
                        return viewValue;
                    }

                    ctrl.$setValidity("numberGreaterThan", parseInt(viewValue) >= comparisonModel);
                    return viewValue;

                };

                ctrl.$parsers.unshift(validate);
                ctrl.$formatters.push(validate);

                attrs.$observe("numberGreaterThan",
                    function(comparisonModel) {
                        // Whenever the comparison model changes we'll re-validate
                        return validate(ctrl.$viewValue);
                    });

            }
        };

    }
);

module.directive("numberLessThan",
    function() {
        return {
            require: "ngModel",
            link: function(scope, element, attrs, ctrl) {

                var validate = function(viewValue) {
                    const comparisonModel = attrs.numberLessThan;

                    if (!viewValue || !comparisonModel) {
                        ctrl.$setValidity("numberLessThan", true);
                        return viewValue;
                    }

                    ctrl.$setValidity("numberLessThan", parseInt(viewValue) <= comparisonModel);
                    return viewValue;

                };

                ctrl.$parsers.unshift(validate);
                ctrl.$formatters.push(validate);

                attrs.$observe("numberLessThan",
                    function(comparisonModel) {
                        // Whenever the comparison model changes we'll re-validate
                        return validate(ctrl.$viewValue);
                    });

            }
        };

    }
);