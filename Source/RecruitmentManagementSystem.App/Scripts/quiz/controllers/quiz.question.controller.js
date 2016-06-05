(function(app) {
    "use strict";

    app.controller("QuestionModalController", [
        "$uibModalInstance", "$http", "questionsInAllPages", function ($uibModalInstance, $http, questionsInAllPages) {
            var vm = this;

            $http.get("/QuestionCategory/List/").success(function(response) {
                vm.categories = response;
            });

            $http.get("/Question/List/").success(function(response) {
                vm.questions = response;


                _.map(vm.questions, function(q) {
                    if (_.find(questionsInAllPages, { id: q.id })) {
                        q.selected = true;
                    }
                    return q;
                });
            });

            vm.updateQuestion = function (categoryName) {
                var data = "";
                $http.get("/Question/List/?categoryName=" + categoryName).success(function (response) {
                    vm.questions = response;


                    _.map(vm.questions, function (q) {
                        if (_.find(questionsInAllPages, { id: q.id })) {
                            q.selected = true;
                        }
                        return q;
                    });

                })
            };

            vm.save = function() {
                var questions = _.filter(vm.questions, function(x) { return x.selected; });

                $uibModalInstance.close(questions);
            };

            vm.cancel = function() {
                $uibModalInstance.dismiss("cancel");
            };
        }
    ]);
})(angular.module("quiz"));