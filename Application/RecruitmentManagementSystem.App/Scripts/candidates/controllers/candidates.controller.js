(function(app) {
    "use strict";

    app.controller("CandidatesController", [
        "$http", "$uibModal", "$timeout", "fileService", "notifierService", function($http, $uibModal, $timeout, fileService, notifierService) {
            var vm = this;

            vm.candidate = {};
            vm.candidate.skills = [];

            vm.discardEducation = function($index) {
                if (confirm("Are you sure?")) {
                    vm.candidate.educations.splice($index, 1);
                }
            };

            vm.discardExperience = function(index) {
                if (confirm("Are you sure?")) {
                    vm.candidate.experiences.splice(index, 1);
                }
            };

            vm.discardProject = function(index) {
                if (confirm("Are you sure?")) {
                    vm.candidate.projects.splice(index, 1);
                }
            };

            vm.getJobPositions = function() {
                $http.get("/JobPosition/List").success(function(data) {
                    vm.positions = data;
                }).error(function() {
                    notifierService.notifyError("Oops! Something happened.");
                });
            };

            var prepareFormData = function() {
                if (vm.candidate.educations && vm.candidate.educations.length) {
                    vm.candidate.educations = _.map(vm.candidate.educations, function(r) {
                        r.institutionId = r.institution.id;
                        return r;
                    });
                }

                vm.candidate.__RequestVerificationToken = angular.element(":input:hidden[name*='RequestVerificationToken']").val();
                vm.candidate.files = [];

                if (vm.candidate.avatar) {
                    vm.candidate.files.push(vm.candidate.avatar);
                    vm.candidate.avatarFileName = vm.candidate.avatar.name;
                }

                if (vm.candidate.resume) {
                    vm.candidate.files.push(vm.candidate.resume);
                    vm.candidate.resumeFileName = vm.candidate.resume.name;
                }
            };

            vm.create = function() {
                vm.form.submitted = true;

                if (vm.form.$valid) {
                    prepareFormData();

                    fileService.postMultipartForm({
                        url: "/Candidate/Create",
                        data: vm.candidate
                    }).progress(function(evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function(data) {
                        location.href = "/Candidate";
                    }).error(function(response) {
                        notifierService.notifyError(response);
                    });
                }
            };

            vm.openEducationModal = function(data) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    backdrop: "static",
                    templateUrl: "/Scripts/candidates/templates/education-modal.html",
                    controller: "EducationModalController",
                    controllerAs: "education",
                    resolve: {
                        data: function() {
                            return data;
                        }
                    }
                });

                modalInstance.result.then(function(row) {
                    vm.candidate.educations = vm.candidate.educations || [];

                    if (row.$$hashKey && _.find(vm.candidate.educations, { $$hashKey: row.$$hashKey })) {
                        $timeout(function() {
                            _.assign(_.find(vm.candidate.educations, { $$hashKey: row.$$hashKey }), row);
                        });
                    } else {
                        vm.candidate.educations.push(row);
                    }
                });
            };

            vm.openExperienceModal = function(data) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    backdrop: "static",
                    templateUrl: "/Scripts/candidates/templates/experience-modal.html",
                    controller: "ExperienceModalController",
                    controllerAs: "experience",
                    resolve: {
                        data: function() {
                            return data;
                        }
                    }
                });

                modalInstance.result.then(function(row) {
                    vm.candidate.experiences = vm.candidate.experiences || [];

                    if (row.$$hashKey && _.find(vm.candidate.experiences, { $$hashKey: row.$$hashKey })) {
                        $timeout(function() {
                            _.assign(_.find(vm.candidate.experiences, { $$hashKey: row.$$hashKey }), row);
                        });
                    } else {
                        vm.candidate.experiences.push(row);
                    }
                });
            };

            vm.openProjectModal = function(data) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    backdrop: "static",
                    templateUrl: "/Scripts/candidates/templates/project-modal.html",
                    controller: "ProjectModalController",
                    controllerAs: "project",
                    resolve: {
                        data: function() {
                            return data;
                        }
                    }
                });

                modalInstance.result.then(function(row) {
                    vm.candidate.projects = vm.candidate.projects || [];

                    if (row.$$hashKey && _.find(vm.candidate.projects, { $$hashKey: row.$$hashKey })) {
                        $timeout(function() {
                            _.assign(_.find(vm.candidate.projects, { $$hashKey: row.$$hashKey }), row);
                        });
                    } else {
                        vm.candidate.projects.push(row);
                    }
                });
            };

            vm.renderAvatarPreview = function(file) {
                if (!file) {
                    return;
                }

                var errorMessages = [];

                if (file.size > 1024 * 1024 * 2) {
                    errorMessages.push("File size is too large. Max upload size is 2MB.");
                }

                if (errorMessages.length) {
                    notifierService.notifyInfo(errorMessages);
                } else {
                    vm.candidate.avatar = file;

                    if (file.type.indexOf("image") > -1) {
                        var fileReader = new FileReader();

                        fileReader.readAsDataURL(file);

                        (function(fileReader) {
                            fileReader.onload = function(e) {
                                $timeout(function() {
                                    vm.candidate.avatarSource = e.target.result;
                                });
                            };
                        })(fileReader);
                    }
                }
            };

            vm.addSkill = function() {
                vm.candidate.skills = vm.candidate.skills || [];

                if (_.find(vm.candidate.skills, { name: vm.skill })) {
                    notifierService.notifyWarning("Skill already added!");

                    return;
                }

                if (vm.skill && vm.skill.length > 1) {
                    vm.candidate.skills.push({
                        name: vm.skill.toLowerCase()
                    });
                    vm.skill = "";
                }
            };

            vm.discardSkill = function(index) {
                vm.candidate.skills.splice(index, 1);
            };

            vm.find = function() {
                $http.get("/candidates/6").success(function(data) {
                    vm.candidate = data;
                });
            };

            vm.update = function() {
                vm.form.submitted = true;

                if (vm.form.$valid) {
                    prepareFormData();

                    fileService.postMultipartForm({
                        url: "/candidates/4",
                        data: vm.candidate,
                        method: "PUT"
                    }).progress(function(evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function(data) {
                        location.href = "/Candidate";
                    }).error(function(response) {
                        notifierService.notifyError(response);
                    });
                }
            };
        }
    ]);
})(angular.module("candidates"));