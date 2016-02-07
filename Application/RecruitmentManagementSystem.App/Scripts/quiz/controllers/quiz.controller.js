(function(app) {
    "use strict";

    app.controller("QuizController", [
        "$http", "$uibModal", function($http, $uibModal) {

            var vm = this;

            vm.pages = [];

            vm.create = function() {
                vm.form.submitted = true;

                var model = {
                    title: vm.title,
                    courseId: vm.courseId,
                    quizPages: angular.copy(vm.pages),
                    __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                };

                if (vm.form.$valid) {
                    angular.forEach(model.quizPages, function (p) {
                        p.quizQuestions = _.map(p.quizQuestions, function (x) {
                            return {
                                questionId: x.id,
                                point: x.point
                            };
                        });
                    });
                    console.log(model);

                    $http.post("/quiz/create", model);
                }
            };

            vm.addNewPage = function() {
                vm.pages.push({
                    quizQuestions: []
                });
            };

            vm.findCourses = function() {
                $http.get("/Course/List/").success(function(data) {
                    vm.courses = data;
                });
            };

            vm.openQuestionModal = function(page) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    backdrop: "static",
                    templateUrl: "/Scripts/quiz/templates/question-modal.html",
                    controller: "QuestionModalController",
                    controllerAs: "modalCtrl"
                });

                modalInstance.result.then(function(questions) {
                    page.quizQuestions = questions;
                });
            };
        }
    ]);
})(angular.module("quiz"));