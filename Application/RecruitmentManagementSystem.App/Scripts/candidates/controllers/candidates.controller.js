angular.module("candidates").controller("CandidatesController", [
    "$http", "$uibModal", "$timeout", "fileService", "notifierService", function($http, $uibModal, $timeout, fileService, notifierService) {
        var vm = this;

        vm.institutions = [];
        vm.educations = [];
        vm.experiences = [];
        vm.projects = [];
        vm.skills = [];

        vm.discardEducation = function(index) {
            vm.educations.splice(index, 1);
        };

        vm.discardExperience = function(index) {
            vm.experiences.splice(index, 1);
        };

        vm.discardProject = function(index) {
            vm.projects.splice(index, 1);
        };

        vm.create = function() {
            vm.form.submitted = true;

            for (var idx = 0; idx < vm.educations.length; idx++) {
                vm.educations[idx].institutionId = vm.educations[idx].institution.Id;
                delete vm.educations[idx].institution;
            }

            var model = {
                firstName: vm.firstName,
                lastName: vm.lastName,
                email: vm.email,
                phoneNumber: vm.phoneNumber,
                educations: vm.educations,
                experiences: vm.experiences,
                projects: vm.projects,
                others: vm.others,
                website: vm.website,
                skills: vm.skills,
                files: []
            };

            if (vm.avatar) {
                model.files.push(vm.avatar);
                model.avatarFileName = vm.avatar.name;
            }

            if (vm.resume) {
                model.files.push(vm.resume);
                model.resumeFileName = vm.resume.name;
            }

            if (vm.form.$valid) {
                fileService.postMultipartForm({
                    url: "/Candidate/Create",
                    data: model
                }).progress(function(evt) {
                    console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                }).success(function(data) {
                    location.href = "/Candidate";
                }).error(function(response) {
                    var erroMessages = _.map(response, function(error) {
                        return error.ErrorMessage;
                    });

                    notifierService.notifyError(erroMessages);
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
                if (row.$$hashKey && _.find(vm.educations, { $$hashKey: row.$$hashKey })) {
                    $timeout(function() {
                        _.assign(_.find(vm.educations, { $$hashKey: row.$$hashKey }), row);
                    });
                } else {
                    vm.educations.push(row);
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
                if (row.$$hashKey && _.find(vm.experiences, { $$hashKey: row.$$hashKey })) {
                    $timeout(function() {
                        _.assign(_.find(vm.experiences, { $$hashKey: row.$$hashKey }), row);
                    });
                } else {
                    vm.experiences.push(row);
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
                if (row.$$hashKey && _.find(vm.projects, { $$hashKey: row.$$hashKey })) {
                    $timeout(function() {
                        _.assign(_.find(vm.projects, { $$hashKey: row.$$hashKey }), row);
                    });
                } else {
                    vm.projects.push(row);
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
                vm.avatar = file;

                if (fileService.fileReaderSupported && file.type.indexOf("image") > -1) {
                    var fileReader = new FileReader();

                    fileReader.readAsDataURL(file);

                    (function(fileReader) {
                        fileReader.onload = function(e) {
                            $timeout(function() {
                                vm.avatarPreview = e.target.result;
                            });
                        };
                    })(fileReader);
                }
            }
        };

        vm.addSkill = function() {
            if (vm.skill && !_.find(vm.skills, { name: vm.skill})) {
                vm.skills.push({
                    name: vm.skill.toLowerCase()
                });
                vm.skill = "";
            }
        };

        vm.discardSkill = function(index) {
            vm.skills.splice(index, 1);
        };
    }
]);