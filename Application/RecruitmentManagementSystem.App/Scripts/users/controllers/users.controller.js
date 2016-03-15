(function(app) {
    "use strict";

    app.controller("UsersController", [
        "$http", "$uibModal", "$timeout", "fileService", "notifierService", function($http, $uibModal, $timeout, fileService, notifierService) {
            var vm = this;

            vm.user = {};

            vm.find = function(id) {
                $http.get("/Account/Details?userId=" + id).success(function (data) {
                    console.log(data);

                    vm.user = data;
                }).error(function(response) {
                    notifierService.notifyError(response);
                });
            };
        }
    ]);
})(angular.module("users"));