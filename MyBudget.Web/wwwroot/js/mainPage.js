$(document).ready(function () {    
    loadTable();

/*    jQuery('#sandbox-container').datepicker({
        format: "MM yyyy",
        minViewMode: 1,
        todayBtn: "linked",
        language: "ru",
        autoclose: true
    }).change(function () {
        var month = jQuery(this).datepicker("getDate").getMonth() + 1;
        var mnthStr = (month < 10) ? '0' + month.toString() : month.toString();
        var year = jQuery(this).datepicker("getDate").getFullYear().toString();
        var dt = mnthStr + year.toString();
        //window.location.href = '/Transactions/MyBudget/' + dt;

        loadTable();
    });

    // Параметры для экспорта в Excel
    jQuery('#ExcelSince').datepicker({
        format: "dd.mm.yyyy",
        minViewMode: 0,
        todayBtn: "linked",
        language: "ru",
        autoclose: true
    });

    jQuery('#ExcelTill').datepicker({
        format: "dd.mm.yyyy",
        minViewMode: 0,
        todayBtn: "linked",
        language: "ru",
        autoclose: true
    });
    */
    $('#btnExportExcel').on('click', function () {
        $('#ExcelSince').val('');
        $('#ExcelTill').val('');
    })

    /*  Высота таблицы  */
    $('#btnPutMoney').on('click', function () {

        amtInp = $('#Amount');
        if (amtInp.val() == '') {
            amtInp.addClass('input-validation-error');
            return false;
        }

    })

    $('#btn-tbl-exp').on("click", function () {
        var span = $(this).find("span");
        if ($("#tbl-w").hasClass("tbl-max") == true) {
            $('#tbl-w').removeClass("tbl-max", 1000);
            span.removeClass("glyphicon-chevron-up");
            span.addClass("glyphicon-chevron-down");
            $("html, body").animate({ scrollTop: 0 }, "slow");
        }
        else {
            $('#tbl-w').addClass("tbl-max", 1000);
            span.removeClass("glyphicon-chevron-down");
            span.addClass("glyphicon-chevron-up");

        }
    });

    /*Пополнить цель/долг*/
    $('.js-pay-goal').on("click", function () {
        $('#Amount').removeClass('input-validation-error')
        $('#putOnId').val($(this).attr("data-goal-id"));
        $('#catType').val($(this).attr("data-catType"));
        $('#putMoneyAmount').val($(this).attr("data-goal-Amount") - $(this).attr("data-goal-CurrentAmount"));

        $('#Amount').val('');
        $('#btnPutMoney').prop('disabled', true);
        $('#wromgAmountMessage').css('display', 'none');
    });

    /* Модальное окно "Удалить цель, долг" */
    $('.js-del-goal').on("click", function () {
        $('#mb-del-goallId').val($(this).attr("data-goal-id"));
    });



    /*Для поля Сумма*/
    var $amt = $('.jq-money');

    if ($amt.val() !== '') {
        var num = parseInt($amt.val(), 10);
        $amt.val(num.toLocaleString("ru-RU"));
    }

    $amt.on("keyup", function (event) {
        var selection = window.getSelection().toString();
        if (selection !== '') { return; }

        if ($.inArray(event.keyCode, [38, 40, 37, 39]) !== -1) { return; }

        var $this = $(this);
        var input = $this.val();

        input = input.replace(/[\D\s\._\-]+/g, "");
        input = input ? parseInt(input, 10) : 0;
        $this.val(function () {
            return (input === 0) ? "" : input.toLocaleString("ru-RU");
        });

        /*Валидация суммы пополнения*/
        var putMoneyAmount = $('#putMoneyAmount').val();

        if (input > putMoneyAmount) {
            $('#btnPutMoney').prop('disabled', true);
            $('#wromgAmountMessage').css('display', '');
            $('#wromgAmountMessage').text('Невозможно пополнить на сумму больше чем ' + amountToString(putMoneyAmount) + ' ' + $(DefCurr).val());
        }
        else {
            $('#btnPutMoney').prop('disabled', false);
            $('#wromgAmountMessage').css('display', 'none');
        }
        if (input == 0) {
            $('#btnPutMoney').prop('disabled', true);
            $('#wromgAmountMessage').css('display', 'none');
        }


    });

    $('form').submit(function () {
        var textValue = $amt.val();
        $amt.val(textValue.replace(/[\D\s\._\-]+/g, ""));
    });
    /*---*/
});

/*jQuery('#sandbox-container').datepicker({
    format: "MM yyyy",
    minViewMode: 1,
    todayBtn: "linked",
    language: "ru",
    autoclose: true,
    defaultViewDate: { year: 2018, month: 01, day: 01 }
});*/

/*function setingDate(r) {
    var dt = jQuery('#sandbox-container').datepicker('getDate');
    var dt1 = new Date();

    if (r == 1) {
        dt1 = new Date(dt.setMonth(dt.getMonth() + 1));
    } else {
        dt1 = new Date(dt.setMonth(dt.getMonth() - 1));
    }

    jQuery('#sandbox-container').datepicker('update', dt1);
};*/


function loadTable() {

    //var month = $('#sandbox-container').datepicker("getDate").getMonth() + 1;
    //var mnthStr = (month < 10) ? '0' + month.toString() : month.toString();
    //var year = $('#sandbox-container').datepicker("getDate").getFullYear().toString();
    //var dt = mnthStr + year.toString();


    /*Высота таблицы*/
    var heightTbl = $(window).height() - 260;
    $('#tbl-w').height(heightTbl + 'px');


    $.ajax({
        url: "/api/transaction?year=" + 2020 + "&month=" + 03,
        method: "GET",
        success: function (result) {
            $('#transactions_table tbody').remove();



            if (result == false) {
                $('#btn-tbl-exp').hide();
            }
            if (result != '') {

                var defCur = $(DefCurr).val();
                $.each(result, function (i, item) {
                    var spendingClass = '';

                    if (item.IsSpending == true) {
                        var opChar = '-'
                        spendingClass = 'text-danger'
                    }
                    else {
                        var opChar = '+'
                        spendingClass = 'text-success'
                    }

                    if (item.Name == null) {
                        item.Name = "---";
                    }
                    if (item.CategoryName == null) {
                        item.CategoryName = "";
                    }


                    var op_class = "";
                    if (item.IsPlaned == true) {
                        op_class = "itm-opacity";
                    }
                    var $td_amt = $('<td class="text-right amt ' + spendingClass + '">').text(opChar + String.fromCharCode(160) + item.Amount.toLocaleString("ru-RU") + ' ' + defCur).on("click", function () {
                        window.location.href = "/Transactions//Edit/" + item.Id;
                    });
                    var $tr = $('<tr data-amt="' + item.Amount + '" data-IsPlaned="' + item.IsPlaned + '" data-tr-id="' + item.Id + '" data-IsSpending="' + item.IsSpending + '">').append(
                        /*Кнопка "запланировано"*/
                        $('<td class="text-center js-switch ' + op_class + '">').append($('<span class="glyphicon glyphicon-ok glyph-btn occured"></span>')),
                        /*Наименование*/
                        $("<td class='td-name'>").text(item.Name).append($('<div class="cat-name">').text(item.CategoryName)).on("click", function () {
                            window.location.href = "/Transactions//Edit/" + item.Id;
                        }),

                        /*$("<td>").text(item.Name).on("click", function () {
                            window.location.href = "/Transactions//Edit/" + item.Id;
                        }),*/
                        /*Сумма*/
                        $td_amt,
                        /*Кнопка "Удалить"*/
                        $('<td class="text-center">').append($('<button type="button" class="btn btn-link glyphicon glyphicon-trash glyph-btn js-del-tr" data-toggle="modal" data-target="#DelTransactionModal">'))
                    );

                    $tr.appendTo('#transactions_table');
                });
                countBalance();

                /*Кнопка свернуть-развернуть*/
                if ($("#transactions_table").height() > heightTbl) {
                    $('#btn-tbl-exp').show();
                } else {
                    $('#btn-tbl-exp').hide();
                }


                /*Нажатие на кнопку "Запланировано"*/
                var $balten = $('#balten');
                $('.js-switch').on('click', function () {
                    var switch_btn = $(this);
                    var $tr = switch_btn.closest('tr');
                    $.ajax({
                        url: "/api/transactions/SwitchPlaned/?Id=" + $tr.attr("data-tr-id"),
                        method: "PUT",
                        success: function () {
                            var $balten = $('#balten');
                            var res = 0;
                            var $td = $('#balten').closest('td');
                            var balance = parseInt($balten.text().replace(String.fromCharCode(160), '').replace(' ', ''));
                            var $amt = parseInt($tr.attr('data-amt'));

                            if ($tr.attr('data-IsSpending') == 'true') {
                                $amt = -$amt;
                            }

                            if ($tr.attr('data-IsPlaned') == 'true') {
                                switch_btn.removeClass('planned');
                                $tr.attr('data-IsPlaned', 'false');
                                res = balance + $amt;
                            }
                            else {
                                switch_btn.addClass('planned');
                                $tr.attr('data-IsPlaned', 'true');
                                res = balance - $amt;
                            }

                            $balten.text(res.toLocaleString("ru-RU"));

                            if (res < 0) {
                                $td.removeClass('text-success');
                                $td.addClass('text-danger');
                                $balten.text($balten.text().replace('-', '- '));
                            } else if (res === 0) {
                                $td.removeClass('text-danger');
                                $td.removeClass('text-success');
                            } else {
                                $td.removeClass('text-danger');
                                $td.addClass('text-success');
                            }

                            if (switch_btn.hasClass("itm-opacity") == true)
                                switch_btn.removeClass("itm-opacity");
                            else
                                switch_btn.addClass("itm-opacity");
                        }

                    })
                });

                /* Модальное окно "Удалить транзакцию" */
                $('.js-del-tr').on("click", function () {
                    $('#mb-del-transId').val($(this).closest('tr').attr("data-tr-id"));
                });
            } else {
                /*Если нет транзакций*/
                $tr = $("<tr class='no-records'>").append($("<td colspan ='4'>").text("В этом месяце ещё нет платежей"));
                $tr.appendTo('#transactions_table');

                $('#balten').CssClass = 'zz';

                var $balten = $('#balten');
                $balten.text('0');
                $balten.closest('td').removeClass('text-success');
                $balten.closest('td').removeClass('text-danger');

                var $balten_pl = $('#balten_pl');
                $balten_pl.closest('td').removeClass('text-success');
                $balten_pl.closest('td').removeClass('text-danger');
                $('#balten_pl').text('0');

            }
        }

    })
};

function countBalance() {
    var bal = 0;
    var bal_pl = 0;

    $("#transactions_table tr").each(function () {
        var $tr = $(this);

        if ($tr.attr("data-IsSpending") == 'true') {


            if ($tr.attr("data-IsPlaned") == 'false') {
                bal -= parseInt($tr.attr("data-amt"));
                bal_pl -= parseInt($tr.attr("data-amt"));
            } else {
                bal_pl -= parseInt($tr.attr("data-amt"));
            }
        } else {

            if ($tr.attr("data-IsPlaned") == 'false') {
                bal += parseInt($tr.attr("data-amt"));
                bal_pl += parseInt($tr.attr("data-amt"));
            } else {
                bal_pl += parseInt($tr.attr("data-amt"));
            }
        }
    });



    var $balten = $('#balten');
    var $td = $balten.closest('td');

    if (bal < 0) {
        $td.removeClass('text-success');
        $td.addClass('text-danger');

    } else {
        $td.removeClass('text-danger');
        $td.addClass('text-success');
    }
    bal = bal.toLocaleString("ru-RU");
    bal = bal.replace('-', '- ');
    $balten.text(bal);

    var $balten_pl = $('#balten_pl');
    var $td_pl = $balten_pl.closest('td');


    if (bal_pl < 0) {
        $td_pl.removeClass('text-success');
        $td_pl.addClass('text-danger');

    } else {
        $td_pl.removeClass('text-danger');
        $td_pl.addClass('text-success');
    }
    bal_pl = bal_pl.toLocaleString("ru-RU");
    bal_pl = bal_pl.replace('-', '- ');
    $balten_pl.text(bal_pl);

};

function amountToString(string) {
    var errorSumm = string;
    errorSumm = parseInt(errorSumm, 10);
    return errorSumm.toLocaleString("ru-RU");
}
