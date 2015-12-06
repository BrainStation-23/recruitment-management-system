(function (app) {
    "use strict";

    app.constant("questionConstants", {
        questionType: { descriptive: "1", multipleChoice: "2", singleChoice: "3" }
    });
    app.value('questionViewModel', 'myModel');

})(angular.module("questions"));