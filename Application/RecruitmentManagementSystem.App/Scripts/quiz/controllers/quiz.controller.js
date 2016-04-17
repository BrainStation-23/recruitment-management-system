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
                    angular.forEach(model.quizPages, function(p) {
                        p.quizQuestions = _.map(p.quizQuestions, function (x) {
                            return {
                                questionId: x.id,
                                point: x.point
                            };
                        });
                    });

                    /*for(var i = 0; i< model.quizPages.length; i++)
                    {
                        model.quizPages[i].quizQuestions = [];
                        for (var k = 0; k < model.quizPages[i].questions.length; k++)
                        {
                            model.quizPages[i].quizQuestions[k] = model.quizPages[i].questions[k];
                        }
                    }*/

                    var quizId;
                    $http.post("/quiz/create", model).success(function (data) {
                        console.log(data);
                        quizId = data;
                        //window.location.href = '/Quiz/Edit/' + quizId;
                    });

                    
                }
            };

            vm.edit = function () {
                vm.form.submitted = true;

                var model = {
                    id: vm.id,
                    title: vm.title,
                    courseId: vm.courseId,
                    quizPages: angular.copy(vm.pages),
                    __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                };

                if (vm.form.$valid) {

                    angular.forEach(model.quizPages, function (p) {
                        p.questions = _.map(p.questions, function (x) {
                            return {
                                questionId: x.id,
                                point: x.point
                            };
                        });
                    });
                    for (var i = 0; i < model.quizPages.length; i++) {
                        model.quizPages[i].quizQuestions = [];
                        for (var k = 0; k < model.quizPages[i].questions.length; k++) {
                            model.quizPages[i].quizQuestions[k] = model.quizPages[i].questions[k];
                        }

                    }
                    console.log(model);

                    $http.post("/quiz/edit", model).success(function (data) {
                        alert(data);
                    }).error(function(e){
                        
                    });

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

            vm.findQuiz = function (quizId) {
                vm.id = quizId;
                vm.findCourses();
                $http.get("/Quiz/Edit/"+quizId).success(function (data) {
                    vm.title = data.title;
                    vm.courseId = data.courseId;
                    vm.pages = angular.copy(data.quizPages);
                    console.log(vm);
                });

            };

            vm.openQuestionModal = function(pageIndex) {
                var questionsInAllPages = [];

                vm.pages.forEach(function(p) {
                    questionsInAllPages.push.apply(questionsInAllPages, p.quizQuestions);
                });

                var modalInstance = $uibModal.open({
                    animation: true,
                    backdrop: "static",
                    templateUrl: "/Scripts/quiz/templates/question-modal.html",
                    controller: "QuestionModalController",
                    controllerAs: "modalCtrl",
                    resolve: {
                        questionsInAllPages: function() { return questionsInAllPages; }
                    }
                });

                modalInstance.result.then(function(questions) {
                    var questionsInAllButThisPage = [];
                    for (var i = 0; i < vm.pages.length; i++) {
                        if (i === pageIndex) {
                            continue;
                        }
                        questionsInAllButThisPage.push.apply(questionsInAllButThisPage, vm.pages[i].quizQuestions);
                    }

                    vm.pages[pageIndex].quizQuestions = _.filter(questions, function (q) {
                        return !_.find(questionsInAllButThisPage, { id: q.id });
                    });
                });
            };

            vm.discardQuestion = function(page, index) {
                page.quizQuestions.splice(index, 1);
            };
        }
    ]);
})(angular.module("quiz"));