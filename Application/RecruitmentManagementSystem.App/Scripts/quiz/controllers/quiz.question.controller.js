(function(app) {
    "use strict";

    app.controller("QuestionModalController", [
        "$uibModalInstance", "$http", function($uibModalInstance, $http) {
            var vm = this;

            $http.get("/QuestionCategory/List/").success(function(response) {
                vm.categories = response;
            });

            $http.get("/Question/List/").success(function(response) {
                vm.questions = response;
            });

            vm.save = function() {
                var questions = _.filter(vm.questions, function (x) { return x.selected; });

                $uibModalInstance.close(questions);
            };

            vm.cancel = function() {
                $uibModalInstance.dismiss("cancel");
            };
        }
    ]);
})(angular.module("quiz"));