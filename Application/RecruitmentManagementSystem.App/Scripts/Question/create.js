(function() {

    var choices = [];

    $(document).on("change", "#drop-down-question-type", function() {
        if ($("#drop-down-question-type").val() === "1") {
            $("#achr-add-choice").show();
        } else {
            $("#achr-add-choice").hide();
        }
    });

    $(document).on("click", "#btn-add-choice", function() {
        var value = $("#choice").val();

        if (value) {
            choices.push(value);
            $("#choice").val("");
        }
    });

    $(document).on("click", "#btn-add-question", function() {
        var data = {
            Title: "some title",
            QuestionType: 2,
            Choices: choices,
            Notes: "some notes",
            CategoryId: 1
        };

        $.ajax({
            url: "/Question/Create",
            method: "POST",
            dataType: "application/json",
            data: data,
            success: function(result) {
                console.log(result);
            }
        });
    });
})();