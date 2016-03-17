(function (app) {
    "use strict";

    app.directive("onlyDecimal", function () {
        return {
            require: 'QuestionsController',
            restrict: 'A',
            link: function(scope, element, attr, ctrl) {
                function inputValue(val) {
                    if (val) {
                        var digits = val.replace(/[^0-9.]/g, '');

                        if (digits !== val) {
                            ctrl.$setViewValue(digits);
                            ctrl.$render();
                        }
                        return parseFloat(digits);
                    }
                    return undefined;
                }

                ctrl.$parsers.push(inputValue);
            }
        }
    });
})(angular.module("questions"));