(function(app) {
    "use strict";

    app.controller("UsersController", [
        "$http", "$uibModal", "$timeout", "fileService", "notifierService", function($http, $uibModal, $timeout, fileService, notifierService) {
            var vm = this;

            vm.user = {};

            vm.find = function(id) {
                $http.get("/Account/Details?userId=" + id).success(function(data) {
                    vm.user = data;
                }).error(function(response) {
                    notifierService.notifyError(response);
                });
            };

            vm.save = function() {
                vm.form.submitted = true;

                if (vm.form.$valid) {

                    if (vm.user.educations && vm.user.educations.length) {
                        vm.user.educations = _.map(vm.user.educations, function(r) {
                            r.institutionId = r.institution.id;
                            return r;
                        });
                    }

                    var model = {
                        id: vm.user.id,
                        firstName: vm.user.firstName,
                        lastName: vm.user.lastName,
                        email: vm.user.email,
                        phoneNumber: vm.user.phoneNumber,
                        website: vm.user.website,
                        address: vm.user.address,
                        others: vm.user.others,
                        educations: vm.user.educations,
                        experiences: vm.user.experiences,
                        projects: vm.user.projects,
                        skills: vm.user.skills,
                        __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                    };

                    if (vm.user.avatar) {
                        model.avatar = vm.user.avatar;
                        model.avatarFileName = vm.user.avatar.name;
                    }

                    if (vm.user.resume) {
                        model.resume = vm.user.resume;
                        model.resumeFileName = vm.user.resume.name;
                    }

                    fileService.postMultipartForm({
                        url: "/Account/Update",
                        data: model
                    }).progress(function(evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function() {
                        notifierService.notifySuccess("Profile updated.");
                    }).error(function(response) {
                        notifierService.notifyError(response);
                    });
                }
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
                    vm.user.avatar = file;

                    if (file.type.indexOf("image") > -1) {
                        var fileReader = new FileReader();

                        fileReader.readAsDataURL(file);

                        (function(fileReader) {
                            fileReader.onload = function(e) {
                                $timeout(function() {
                                    vm.user.avatar.relativePath = e.target.result;
                                });
                            };
                        })(fileReader);
                    }
                }
            };

            vm.openEducationModal = function(data) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    backdrop: "static",
                    templateUrl: "/Scripts/users/templates/education-modal.html",
                    controller: "EducationModalController",
                    controllerAs: "education",
                    resolve: {
                        data: function() {
                            return data;
                        }
                    }
                });

                modalInstance.result.then(function(row) {
                    vm.user.educations = vm.user.educations || [];

                    if (row.$$hashKey && _.find(vm.user.educations, { $$hashKey: row.$$hashKey })) {
                        $timeout(function() {
                            _.assign(_.find(vm.user.educations, { $$hashKey: row.$$hashKey }), row);
                        });
                    } else {
                        vm.user.educations.push(row);
                    }
                });
            };

            vm.discardEducation = function($index) {
                if (confirm("Are you sure?")) {
                    vm.user.educations.splice($index, 1);
                }
            };

            vm.openExperienceModal = function(data) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    backdrop: "static",
                    templateUrl: "/Scripts/users/templates/experience-modal.html",
                    controller: "ExperienceModalController",
                    controllerAs: "experience",
                    resolve: {
                        data: function() {
                            return data;
                        }
                    }
                });

                modalInstance.result.then(function(row) {
                    vm.user.experiences = vm.user.experiences || [];

                    if (row.$$hashKey && _.find(vm.user.experiences, { $$hashKey: row.$$hashKey })) {
                        $timeout(function() {
                            _.assign(_.find(vm.user.experiences, { $$hashKey: row.$$hashKey }), row);
                        });
                    } else {
                        vm.user.experiences.push(row);
                    }
                });
            };

            vm.discardExperience = function(index) {
                if (confirm("Are you sure?")) {
                    vm.user.experiences.splice(index, 1);
                }
            };

            vm.openProjectModal = function(data) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    backdrop: "static",
                    templateUrl: "/Scripts/users/templates/project-modal.html",
                    controller: "ProjectModalController",
                    controllerAs: "project",
                    resolve: {
                        data: function() {
                            return data;
                        }
                    }
                });

                modalInstance.result.then(function(row) {
                    vm.user.projects = vm.user.projects || [];

                    if (row.$$hashKey && _.find(vm.user.projects, { $$hashKey: row.$$hashKey })) {
                        $timeout(function() {
                            _.assign(_.find(vm.user.projects, { $$hashKey: row.$$hashKey }), row);
                        });
                    } else {
                        vm.user.projects.push(row);
                    }
                });
            };

            vm.discardProject = function(index) {
                if (confirm("Are you sure?")) {
                    vm.user.projects.splice(index, 1);
                }
            };

            vm.addSkill = function() {
                vm.user.skills = vm.user.skills || [];

                if (_.find(vm.user.skills, { name: vm.skill })) {
                    notifierService.notifyWarning("Skill already added!");

                    return;
                }

                if (vm.skill && vm.skill.length > 1) {
                    vm.user.skills.push({
                        name: vm.skill.toLowerCase()
                    });
                    vm.skill = "";
                }
            };

            vm.discardSkill = function(index) {
                vm.user.skills.splice(index, 1);
            };
        }
    ]);
})(angular.module("users"));