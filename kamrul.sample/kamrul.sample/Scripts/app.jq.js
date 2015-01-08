
function OnBegin() {

    OpenProgressBarDialog();

}

function OnSuccess() {

    var updateTargetIdValue = $("#updateTargetId").html().trim();
    var updateTargetIdValueList = updateTargetIdValue.split("|");

    var statusValue = updateTargetIdValueList[0];
    var messageTextValue = updateTargetIdValueList[1];

    if (statusValue == "True") {

        CloseProgressBarDialog();
        $("#updateTargetId").html("");
        $("#updateTargetId").html(messageTextValue);
        $("#updateTargetId").show();

    }
    else {

        CloseProgressBarDialog();
        $("#updateTargetId").html("");
        $("#updateTargetId").show();
    }

}

function OnComplete() {

    CloseProgressBarDialog();

}

function OpenProgressBarDialog() {

    $(".ui-dialog-titlebar").hide();
    //open Progress Bar
    $("#progressBarDialog").dialog('open');

}

function CloseProgressBarDialog() {

    //close Progress Bar
    $("#progressBarDialog").dialog('close');

}

$(function () {
    //start DataTable Script

    //start Add, Edit, Delete - Dialog, Click Event
    //-------------------------------------------------------

    //For Progress Bar
    $("#progressBarDialog").dialog({
        autoOpen: false,
        minHeight: 'auto',
        height: 'auto',
        width: 'auto',
        minWidth: 'auto',
        resizable: false,
        modal: true,
        open: function () {
            $(this).dialog("widget").find(".ui-dialog-titlebar").hide();
        }
    });

    //For App Dialog
    $("#appDialog").dialog({
        autoOpen: false,
        width: 'auto',
        height: 'auto',
        resizable: false,
        modal: true,
        buttons: {
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('a[name="lnkAppDialog"]').live("click", function (e) {
    //$('body').on('click', '.lnkAppDialog', function(e) {
    //$('body').on('click', 'a[name="lnkAppDialog"]', function (e) {

        var linkObj = $(this);
        var dialogDiv = $('#appDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            dialogDiv.dialog('open');
        });
        return false;

    });

    //end Add, Edit, Delete - Dialog, Click Event
    //-------------------------------------------------------

});