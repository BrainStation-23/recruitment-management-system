(function(app) {
    "use strict";

    app.factory("fileService", [
        "Upload", function($upload) {

            var postMultipartForm = function(config) {
                return $upload.upload({
                    url: config.url,
                    method: config.method || "POST",
                    data: config.data
                });
            };

            return {
                postMultipartForm: postMultipartForm
            };
        }
    ]);
})(angular.module("core"));