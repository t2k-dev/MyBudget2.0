$(document).ready(function () {
    /*var graphTerm = $("#graphTerm");
    loadPie(graphTerm.val());
    
    graphTerm.on("change", function () {
        if ($(this).val() == 1)        
            loadPie($(this).val());
    })*/

    var graphCurrent = $("#loadCurrent");
    graphCurrent.on("click", function () {
        loadPie(1);
        $("#ExcelSince").val('');
        $("#ExcelTill").val('');
        $("#loadCurrent").addClass('btn-default-active');
        $("#btnShowPeriod").removeClass('btn-default-active');
        
    })

    var loadPeriod = $("#loadPeriod");
    loadPeriod.on("click", function () {
        loadPie(2);
        
    })
    

    $('#ExcelSince').datepicker({
        format: "dd-mm-yyyy",
        minViewMode: 0,
        todayBtn: "linked",
        language: "ru",
        autoclose: true
    });

    $('#ExcelTill').datepicker({
        format: "dd-mm-yyyy",
        minViewMode: 0,
        todayBtn: "linked",
        language: "ru",
        autoclose: true
    });

    loadPie(1);
})

function loadPie(term) {
    var urlString = "";
    var userId = $("#UserId").val();

    if (term == 1)
    {        
        urlString = "/api/graph/getSpendingGraphCurrentMonth/" + userId;
    }
    if (term == 2)
    {
        var since = $("#ExcelSince").val();
        var till = $("#ExcelTill").val();

        if (since == '')
            since = '01-01-2010';
        if (till == '')
            till = '01-01-2099';

        urlString = "/api/graph/getSpendingGraph/" + userId + "/" + since + "/" + till;
        $('#graphPeriod').modal('toggle');
        $("#btnShowPeriod").addClass('btn-default-active');
        $("#loadCurrent").removeClass('btn-default-active');
    }

    $.ajax({        
        url: urlString,
        method: "GET",
        success: function (result) {
            $('#no-data-text').hide();
            $('#graphLegend').show();
            $('#canvas-holder').show();

            $('#graphLegend tbody').remove();
            config.data.datasets.splice(0, 1);
            var newDataset = {
                backgroundColor: [],
                data: [],
                label: 'New dataset',
            };
            config.data.labels.splice(0);
            var DefCurrency = $("#DefCurrency").val();
            var $tbody = $('<tbody>').appendTo('#graphLegend');

            $.each(result, function (i, item) {
                newDataset.data.push(item.amount);
                newDataset.backgroundColor.push(item.color);
                config.data.labels.push(item.caption);

                var $tr = $('<tr>').append(
                    $('<td>').append($("<div class='leg-marker' style='background-color:" + item.color + "'>")),
                    $('<td>').text(item.caption),
                    $("<td class='text-right'>").text(item.amount.toLocaleString("ru-Ru") + ' ' + DefCurrency)
                );

                $tr.appendTo($tbody);

            })
            config.data.datasets.push(newDataset);
            window.myPie.update();            
        },
        error: function (data) {
            $('#no-data-text').show();
            $('#graphLegend').hide();
            $('#canvas-holder').hide();            
        }
    })

}