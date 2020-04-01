$(document).ready(function () {
    /* Модальное окно "Удалить шаблон" */
    $('.js-del-rec').on("click", function () {
        $('#mb-del-recId').val($(this).attr("data-id"));
    });
});