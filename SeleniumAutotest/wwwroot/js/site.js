// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function swowError(error) {
    var about = "<br><p style='text-align: right;'><a href='/Info' target='_blank'>О системе</a></p>";
    var content = (error ?? "Во время выполнения запроса произошла ошибка.") + about;

    var wnd = $("<div></div>").kendoDialog({
        width: "50%",
        heigth: "50%",
        title: "Ошибка",
        content: content,
        actions: [            
            {
                text: "Закрыть"
            }]
    }).data("kendoDialog").open();
}