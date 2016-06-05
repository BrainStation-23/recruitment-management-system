(function (app) {
    "use strict";

    app.controller("QuestionsController", [
        "$http", "fileService", "notifierService", "questionConstants",
        function ($http, fileService, notifierService, questionConstant) {

            var vm = this;

            vm.constants = questionConstant;

            vm.choices = vm.choices || [{
                text: "",
                isCorrect: false
            }];
            vm.documents = vm.documents || [];

            vm.create = function () {
                vm.form.submitted = true;

                var model = {
                    text: vm.text,
                    questionType: vm.questionType,
                    answer: vm.answer,
                    choices: vm.choices,
                    notes: vm.notes,
                    defaultPoint : vm.defaultPoint,
                    categoryId: vm.categoryId,
                    files: [],
                    __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                };

                if (vm.documents.length) {
                    model.files = vm.documents;
                }

                if (vm.form.$valid) {
                    fileService.postMultipartForm({
                        url: "/Question/Create",
                        data: model
                    }).progress(function (evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function () {
                        location.href = "/Question/List";
                    }).error(function (response) {
                        notifierService.notifyError(response);
                    });
                }
            };

            vm.addChoice = function () {
                vm.choices.push({
                    text: "",
                    isCorrect: false
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
                    vm.documents = vm.files;
                });
            };

            vm.findCategories = function () {
                $http.get("/QuestionCategory/List/").success(function (data) {
                    vm.categories = data;
                });
            };

            vm.addDocument = function (files) {
                vm.documents = vm.documents.concat(files);
            };

            vm.discardDocument = function (index) {
                vm.documents.splice(index, 1);
            };

            vm.getOriginalFileName = function(dbFileName) {
                return fileService.getOriginalFileName(dbFileName);
            };

            vm.resetChoices = function() {
                angular.forEach(vm.choices, function(x) {
                    x.isCorrect = false;
                });
            };

            vm.options = {
                height: 300,
                toolbar: [
                    ["headline", ["style"]],
                    ["style", ["bold", "italic", "underline", "superscript", "subscript", "strikethrough", "clear"]],
                    ["fontface", ["fontname"]],
                    ["textsize", ["fontsize"]],
                    ["fontclr", ["color"]],
                    ["alignment", ["ul", "ol", "paragraph", "lineheight"]],
                    ["height", ["height"]],
                    ["table", ["table"]],
                    ["insert", ["link", "picture", "video", "hr"]],
                    ["view", ["fullscreen", "codeview"]]
                ]
            };
        }
    ]);
})(angular.module("questions"));
