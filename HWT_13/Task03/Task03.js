function SelectAll() {
    var select = $('.list option').appendTo('.selectedItems');
    Deselect('.selectedItems', '.list', '.SelectAll, .SelectItem');
    LockButton();
    UnlockButton('.RemoveAll, .RemoveItem')
}

function SelectItem() {
    var selected = $('.list option:selected').appendTo('.selectedItems');
    Deselect('.selectedItems', '.list', '.SelectAll, .SelectItem');
    LockButton();
    UnlockButton('.RemoveAll, .RemoveItem')
}

function RemoveItem() {
    var selected = $('.selectedItems option:selected').appendTo('.list');
    Deselect('.list', '.selectedItems', '.RemoveAll, .RemoveItem');
    LockButton();
    UnlockButton('.SelectAll, .SelectItem');
}

function RemoveAll() {
    var selected = $('.selectedItems option').appendTo('.list');
    Deselect('.list', '.selectedItems', '.RemoveAll, .RemoveItem');
    LockButton();
    UnlockButton('.SelectAll, .SelectItem')
}

function LockButton() {
    if ($.trim($('.list').text()) == "") {
        $('.SelectAll, .SelectItem').prop("disabled", "true");
    }

    if ($.trim($('.selectedItems').text()) == "") {
        $('.RemoveAll, .RemoveItem').prop("disabled", "true");
    }
}

function UnlockButton(buttons) {
    if ($(buttons).attr("disabled")) {
        $(buttons).removeAttr("disabled");
    }
}

function Deselect(selectFirst, selectSecond, items) {
    var a = $(selectFirst).children('option:selected');
    a.prop("selected", false);
    if ($.trim($(selectSecond).text()) == "") {
        $(items).prop("disabled", "true");
    }
}