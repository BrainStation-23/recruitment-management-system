angular.module("candidates").controller("CandidatesController", [
    "$http", "$uibModal", "$timeout", "fileService", "notifierService", function($http, $uibModal, $timeout, fileService, notifierService) {
        var vm = this;

        vm.institutions = [];

        vm.educations = [];

        vm.experiences = [];

        vm.discardEducation = function(index) {
            vm.educations.splice(index, 1);
        };
        
        vm.discardExperience = function (index) {
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
                website: vm.website
            };

            var uploadConfig = {
                url: "/Candidate/Create",
                file: vm.avatar,
                data: model
            };

            if (vm.form.$valid) {

                fileService.postMultipartForm(uploadConfig).progress(function(evt) {
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
                templateUrl: "EducationModalContent.html",
                controller: "EducationModalInstanceController",
                controllerAs: "education"
            });

            modalInstance.result.then(function(row) {
                vm.educations.push(row);
            });
        };
        
        vm.openExperienceModal = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                backdrop: "static",
                templateUrl: "ExperienceModalContent.html",
                controller: "ExperienceModalInstanceController",
                controllerAs: "experience"
            });

            modalInstance.result.then(function (row) {
                vm.experiences.push(row);
            });
        };

        vm.attachPhoto = function(file) {
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

        vm.attachResume = function(file) {
            if (!file) {
                return;
            }

            vm.resume = file;
        };
    }
]);

angular.module("candidates").controller("EducationModalInstanceController", [
    "$http", "$uibModalInstance", function($http, $uibModalInstance) {
        var vm = this;

        vm.institutions = [];

        $http.get("/Institution/").success(function(data) {
            vm.institutions = data;
        });

        vm.add = function() {
            vm.form.submitted = true;

            if (vm.form.$valid) {
                var education = {
                    degree: vm.degree,
                    fieldOfStudy: vm.fieldOfStudy,
                    grade: vm.grade,
                    institution: vm.institution,
                    firstYear: vm.firstYear,
                    lastYear: vm.lastYear,
                    activites: vm.activites,
                    description: vm.description
                };
                $uibModalInstance.close(education);
            }
        };

        vm.cancel = function() {
            $uibModalInstance.dismiss("cancel");
        };
    }
]);

angular.module("candidates").controller("ExperienceModalInstanceController", [
    "$http", "$uibModalInstance", function ($http, $uibModalInstance) {
        var vm = this;

        vm.add = function () {
            vm.form.submitted = true;

            if (vm.form.$valid) {
                var experience = {
                    organization: vm.organization,
                    jobTitle: vm.jobTitle,
                    from: vm.from,
                    to: vm.to,
                    stillWorking: vm.stillWorking,
                    description: vm.description
                };
                $uibModalInstance.close(experience);
            }
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss("cancel");
        };
    }
]);