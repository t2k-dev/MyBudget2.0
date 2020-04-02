$(document).ready(function () {
    /* Модальное окно "Удалить шаблон" */
    $('.js-del-tmp').on("click", function () {
        $('#mb-del-tempId').val($(this).closest('tr').attr("data-tr-id"));
    });
});