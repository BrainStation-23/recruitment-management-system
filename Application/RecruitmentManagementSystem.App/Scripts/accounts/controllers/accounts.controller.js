(function(app) {
    "use strict";

    app.controller("AccountsController", [
        "$http", "notifierService", "fileService", "$uibModal", function($http, notifierService, fileService, $uibModal) {

            var vm = this;

            vm.getAccountDetails = function() {
                $http.get("/Manage/AccountDetails/").success(function(response) {
                    vm.profile = response;
                });
            };

            vm.uploadAvatar = function(file) {
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
                    fileService.postMultipartForm({
                        url: "/Manage/UploadAvatar",
                        data: {
                            file: file,
                            __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                        }
                    }).progress(function(evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function(avatar) {
                        vm.profile.avatar = avatar;
                        notifierService.notifySuccess("Avatar updated successfully.");
                    }).error(function(response) {
                        notifierService.notifyError(response);
                    });
                }
            };

            vm.removeAvatar = function() {
                $http.post("/Manage/RemoveAvatar", {
                    __RequestVerificationToken: angular.element(":input:hidden[name*='RequestVerificationToken']").val()
                }).success(function() {
                    vm.applicationUser.avatar = null;
                    notifierService.notifySuccess("Avatar removed successfully.");
                }).error(function(response) {
                    notifierService.notifyError(response);
                });
            };

            vm.openAccountDetailsModal = function(data) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    backdrop: "static",
                    templateUrl: "/Scripts/accounts/templates/account-modal.html",
                    controller: "AccountModalController",
                    controllerAs: "account",
                    resolve: {
                        data: function() {
                            return data;
                        }
                    }
                });

                modalInstance.result.then(function(profile) {
                    vm.profile = profile;
                });
            };
        }
    ]);
})(angular.module("accounts"));