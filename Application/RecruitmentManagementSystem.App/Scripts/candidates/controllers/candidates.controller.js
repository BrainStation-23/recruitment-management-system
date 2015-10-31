angular.module("candidates").controller("CandidatesController", [
    "$http", "$uibModal", "$timeout", "fileService", "notifierService", function($http, $uibModal, $timeout, fileService, notifierService) {
        var vm = this;

        vm.institutions = [];

        vm.educations = [];

        vm.experiences = [];

        vm.discardEducation = function(index) {
            vm.educations.splice(index, 1);
        };

        vm.discardExperience = function(index) {
            vm.experiences.splice(index, 1);
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
                others: vm.others,
                website: vm.website,
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
                });
            }
        };

        vm.openEducationModal = function() {
            var modalInstance = $uibModal.open({
                animation: true,
                backdrop: "static",
                templateUrl: "/Scripts/candidates/templates/education-modal.html",
                controller: "EducationModalInstanceController",
                controllerAs: "education"
            });

            modalInstance.result.then(function(row) {
                vm.educations.push(row);
            });
        };

        vm.openExperienceModal = function() {
            var modalInstance = $uibModal.open({
                animation: true,
                backdrop: "static",
                templateUrl: "/Scripts/candidates/templates/experience-modal.html",
                controller: "ExperienceModalInstanceController",
                controllerAs: "experience"
            });

            modalInstance.result.then(function(row) {
                vm.experiences.push(row);
            });
        };

        vm.renderPreview = function(file) {
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
    }
]);