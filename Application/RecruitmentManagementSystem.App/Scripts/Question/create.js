(function() {

    var choices = [];

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
            QuestionType: 1,
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