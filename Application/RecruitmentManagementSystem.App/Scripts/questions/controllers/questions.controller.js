(function(app) {
    "use strict";

    app.controller("QuestionsController", [
        "$http", function($http) {
            var vm = this;

            vm.categories = [];
            vm.choices = [
                {
                    text: "",
                    isValid: false
                },
                {
                    text: "",
                    isValid: false
                }
            ];

            vm.create = function() {
                vm.form.submitted = true;

                var model = {
                    text: vm.text,
                    questionType: vm.questionType,
                    choices: vm.choices,
                    notes: vm.notes,
                    answer: vm.answer,
                    categoryId: vm.categoryId,
                    __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                };

                $http.post("/Question/Create", model).success(function() {
                    location.href = "/Question";
                });
            };

            vm.update = function() {
                vm.form.submitted = true;
            };

            vm.addNewChoice = function() {
                vm.choices.push({
                    text: "",
                    isValid: false
                });
            };

            vm.discardChoice = function(index) {
                vm.choices.splice(index, 1);
            };

            vm.find = function() {
                var id = location.pathname.replace("/Question/Edit/", "");

                $http.get("/Question/Details/" + id).success(function(data) {
                    vm.question = data;
                });
            };

            vm.findCategories = function() {
                $http.get("/QuestionCategory/").success(function(data) {
                    vm.categories = data;
                });
            };
        }
    ]);
})(angular.module("questions"));