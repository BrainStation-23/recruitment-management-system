(function(app) {
    "use strict";

    app.controller("QuestionsController", [
        "$http", "fileService", "notifierService", "questionConstants", "$scope", function($http, fileService, notifierService, constants, $scope) {

            var vm = this;

            vm.constants = constants;
            vm.allDocuments = [];

            vm.categories = [];

            var defaultChoices = [
                {
                    text: "",
                    isValid: false
                },
                {
                    text: "",
                    isValid: false
                }
            ];
            vm.choices = angular.copy(defaultChoices);

            vm.create = function() {
                vm.form.submitted = true;

                var model = {
                    text: vm.text,
                    questionType: vm.questionType,
                    choices: parseInt(vm.questionType, 10) === vm.constants.questionType.descriptive ? [] : vm.choices,
                    notes: vm.notes,
                    answer: vm.answer,
                    categoryId: vm.categoryId,
                    files: [],
                    __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                };

                if (vm.allDocuments && vm.allDocuments.length) {
                    model.files = vm.allDocuments;
                }

                if (vm.form.$valid) {
                    fileService.postMultipartForm({
                        url: "/Question/Create",
                        data: model
                    }).progress(function(evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function(data) {
                        location.href = "/Question";
                    }).error(function(response) {
                        notifierService.notifyError(response);
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

            vm.addDocument = function(files) {
                angular.forEach(files, function(file) {
                    if (file) {
                        vm.allDocuments.push(file);
                    }
                });
            }

            vm.discardDocument = function(index) {
                vm.allDocuments.splice(index, 1);
            }

            // Watchers
            $scope.$watch(function() {
                return vm.questionType;
            }, function(newVal, oldVal) {
                if (newVal !== oldVal) {
                    vm.choices = angular.copy(defaultChoices);
                }
            });
        }
    ]);
})(angular.module("questions"));