(function(app) {
    "use strict";

    app.controller("QuestionsController", [
        "$http", "fileService", "notifierService", "questionConstants", function($http, fileService, notifierService, constants) {

            var vm = this;

            vm.constants = constants;

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
                    choices: vm.questionType === vm.constants.questionType.descriptive ? [] : vm.choices,
                    notes: vm.notes,
                    answer: vm.answer,
                    categoryId: vm.categoryId,
                    __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                };

                if (vm.form.$valid) {

                    fileService.postMultipartForm({
                        url: "/Question/Create",
                        data: model
                    }).progress(function(evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function(data) {
                        location.href = "/Question";
                    }).error(function(response) {
                        var erroMessages = [];

                        if (Object.prototype.toString.call(response) === "[object Array]") {
                            erroMessages = _.map(response, function(error) {
                                return error.errorMessage;
                            });
                        } else {
                            erroMessages.push("Something happened! Please try again.");
                        }

                        notifierService.notifyError(erroMessages);
                    });
                }
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