angular.module("accounts").controller("AccountsController", [
    "$http", "notifierService", function ($http, notifierService) {

        var vm = this;
        
        $http.get("/Manage/ApplicationUserInformation/").success(function (response) {
            vm.applicationUser = response;
        });
        
        vm.saveBasicInformation = function () {
            vm.form.submitted = true;

            var model = {
                firstName: vm.applicationUser.FirstName,
                lastName: vm.applicationUser.LastName,
                email: vm.applicationUser.Email,
                phoneNumber: vm.applicationUser.PhoneNumber
            };

            if (vm.form.$valid) {
                $http({
                    method: 'POST',
                    url: '/Manage/EditApplicationUserInformation',
                    data: model
                }).error(function (response) {
                    var erroMessages = _.map(response, function (error) {
                        return error.ErrorMessage;
                    });

                    notifierService.notifyError(erroMessages);
                });
            }
        };
        
        //vm.saveAvatar = function () {
        //    vm.form.submitted = true;

        //    var model = {
        //        avatar: vm.avatar
        //    };

        //    if (vm.form.$valid) {
        //        fileService.postMultipartForm({
        //            url: "/Manage/EditAvatar",
        //            data: model
        //        }).progress(function (evt) {
        //            console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
        //        }).success(function (data) {
        //            location.href = "/Manage";
        //        }).error(function (response) {
        //            var erroMessages = _.map(response, function (error) {
        //                return error.ErrorMessage;
        //            });

        //            notifierService.notifyError(erroMessages);
        //        });
        //    }
        //};
    }
]);