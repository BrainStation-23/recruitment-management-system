(function(app) {
    "use strict";

    app.factory("fileService", [
        "Upload", function($upload) {

            var fileReaderSupported = window.FileReader != null && (window.FileAPI == null || FileAPI.html5 != false);

            var postMultipartForm = function(config) {
                return $upload.upload({
                    url: config.url,
                    method: config.method || "POST",
                    data: config.data
                });
            };

            return {
                postMultipartForm: postMultipartForm,
                fileReaderSupported: fileReaderSupported
            };
        }
    ]);
})(angular.module("core"));