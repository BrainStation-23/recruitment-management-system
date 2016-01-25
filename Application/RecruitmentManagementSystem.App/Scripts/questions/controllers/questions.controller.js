(function (app) {
    "use strict";

    app.controller("QuestionsController", [
        "$http", "fileService", "notifierService", "questionConstants", "$scope", function ($http, fileService, notifierService, questionConstant, $scope) {

            var vm = this;

            vm.constants = questionConstant;

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
            if (!vm.choices) {
                vm.choices = angular.copy(defaultChoices);
            }

            vm.create = function () {
                vm.form.submitted = true;

                var model = {
                    text: vm.text,
                    questionType: vm.questionType,
                    choices: vm.questionType === vm.constants.questionType.descriptive ? [] : vm.choices,
                    notes: vm.notes,
                    answer: vm.answer,
                    categoryId: vm.categoryId,
                    files: [],
                    __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                };

                if (vm.allDocuments && vm.allDocuments.length) {
                    model.files = vm.allDocuments;
                }
                console.log(model);

                if (vm.form.$valid) {
                    fileService.postMultipartForm({
                        url: "/Question/Create",
                        data: model
                    }).progress(function (evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function (data) {
                        location.href = "/Question/List";
                    }).error(function (response) {
                        notifierService.notifyError(response);
                    });
                }
            };

            vm.update = function () {
                vm.form.submitted = true;
                var model = {
                    id: vm.id,
                    text: vm.text,
                    questionType: vm.questionType,
                    choices: vm.questionType === vm.constants.questionType.descriptive ? [] : vm.choices,
                    notes: vm.notes,
                    answer: vm.answer,
                    categoryId: vm.categoryId,
                    deletableFile: vm.deletableFile,
                    files: [],
                    __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                };

                if (vm.allDocuments && vm.allDocuments.length) {
                    angular.forEach(vm.allDocuments, function (document) {
                        if (!document.hasOwnProperty("id")) {
                            model.files.push(document);
                        }
                    });
                    model.files = vm.allDocuments;
                }

                if (vm.form.$valid) {
                    fileService.postMultipartForm({
                        url: "/Question/Edit",
                        data: model
                    }).progress(function (evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function (data) {
                        location.href = "/Question/List";
                    }).error(function (response) {
                        notifierService.notifyError(response);
                    });
                }
            };

            vm.addNewChoice = function () {
                vm.choices.push({
                    text: "",
                    isValid: false
                });
            };

            vm.discardChoice = function (index) {
                vm.choices.splice(index, 1);
            };

            vm.find = function (id) {

                $http.get("/Question/Details/" + id).success(function (data) {

                    angular.forEach(data, function (value, index) {
                        vm[index] = value;
                    });

                    vm.questionType = vm.questionType.toString();
                    vm.allDocuments = vm.files;
                    vm.findCategories();
                });
            };

            vm.findCategories = function () {
                $http.get("/QuestionCategory/List/").success(function (data) {
                    vm.categories = data;
                });
            };

            vm.addDocument = function (files) {
                angular.forEach(files, function (file) {
                    if (file) {
                        vm.allDocuments.push(file);
                    }
                });
            };

            vm.discardDocument = function (index) {
                if (vm.allDocuments[index] && vm.allDocuments[index].hasOwnProperty("id")) {
                    vm.deletableFile = vm.deletableFile || [];
                    vm.deletableFile.push(vm.allDocuments[index]);
                }

                vm.allDocuments.splice(index, 1);
            };

            //Watchers
            //$scope.$watch(function () {
            //    return vm.questionType;
            //}, function (newVal, oldVal) {
            //    if (newVal !== oldVal) {
            //        vm.choices = angular.copy(defaultChoices);
            //    }
            //});

        }
    ]);
})(angular.module("questions"));