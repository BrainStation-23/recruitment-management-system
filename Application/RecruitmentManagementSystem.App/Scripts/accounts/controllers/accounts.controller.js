angular.module("accounts").controller("AccountsController", [
    "$http", "notifierService", "fileService", function($http, notifierService, fileService) {

        var vm = this;

        vm.applicationUserInformations = function() {
            $http.get("/Manage/ApplicationUserInformation/").success(function(response) {
                vm.applicationUser = response;
            });
        };

        vm.saveBasicInformation = function() {
            vm.form.submitted = true;

            var model = {
                firstName: vm.applicationUser.FirstName,
                lastName: vm.applicationUser.LastName,
                email: vm.applicationUser.Email,
                phoneNumber: vm.applicationUser.PhoneNumber
            };

            if (vm.form.$valid) {
                $http({
                    method: "POST",
                    url: "/Manage/EditApplicationUserInformation",
                    data: model
                }).success(function() {
                    notifierService.notifySuccess("Basic Informations updated successfully.");
                }).error(function(response) {
                    var erroMessages = _.map(response, function(error) {
                        return error.ErrorMessage;
                    });

                    notifierService.notifyError(erroMessages);
                });
            }
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
                    url: "/Manage/EditAvatar",
                    data: {
                        file: file
                    }
                }).progress(function(evt) {
                    console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                }).success(function(avatar) {
                    vm.applicationUser.Avatar = avatar;
                    notifierService.notifySuccess("Avatar updated successfully.");
                }).error(function(response) {
                    var erroMessages = _.map(response, function(error) {
                        return error.ErrorMessage;
                    });

                    notifierService.notifyError(erroMessages);
                });
            }
        };

        vm.removeAvatar = function() {
            $http.post("/Manage/RemoveAvatar").success(function() {
                //vm.applicationUser.Avatar = null;
                notifierService.notifySuccess("Avatar removed successfully.");
            }).error(function(response) {
                var erroMessages = _.map(response, function(error) {
                    return error.ErrorMessage;
                });

                notifierService.notifyError(erroMessages);
            });
        };
    }
]);