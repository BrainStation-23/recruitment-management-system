(function(app) {
    "use strict";

    function usersController($http, $uibModal, $timeout, fileService, notifierService) {
        var vm = this;

        vm.user = {};

        vm.find = function(id) {
            $http.get("/Account/Details?userId=" + id).success(function(data) {
                vm.user = data;
            }).error(function(response) {
                notifierService.notifyError(response);
            });
        };

        vm.save = function () {
            vm.form.submitted = true;

            if (vm.form.$valid) {

                if (vm.user.educations && vm.user.educations.length) {
                    vm.user.educations = _.map(vm.user.educations, function (r) {
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

                console.log(model);

                fileService.postMultipartForm({
                    url: "/Account/Update",
                    data: model
                }).progress(function (evt) {
                    console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                }).success(function () {
                    notifierService.notifySuccess("Profile updated.");
                }).error(function (response) {
                    notifierService.notifyError(response);
                });
            }
        };

        vm.renderAvatarPreview = function (file) {
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

                    (function (fileReader) {
                        fileReader.onload = function (e) {
                            $timeout(function () {
                                vm.user.avatar.relativePath = e.target.result;
                            });
                        };
                    })(fileReader);
                }
            }
        };

        vm.openEducationModal = function (data) {
            var modalInstance = $uibModal.open({
                animation: true,
                backdrop: "static",
                templateUrl: "/Scripts/users/templates/education-modal.html",
                controller: "EducationModalController",
                controllerAs: "education",
                resolve: {
                    data: function () {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (row) {
                vm.user.educations = vm.user.educations || [];

                if (row.$$hashKey && _.find(vm.user.educations, { $$hashKey: row.$$hashKey })) {
                    $timeout(function () {
                        _.assign(_.find(vm.user.educations, { $$hashKey: row.$$hashKey }), row);
                    });
                } else {
                    vm.user.educations.push(row);
                }
            });
        };

        vm.discardEducation = function ($index) {
            if (confirm("Are you sure?")) {
                vm.user.educations.splice($index, 1);
            }
        };
    }

    app.controller("UsersController", usersController);

    usersController.$inject = ["$http", "$uibModal", "$timeout", "fileService", "notifierService"];

})(angular.module("users"));