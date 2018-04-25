function task02() {
    var str = document.getElementById("subtask2").value;
    var digitArr = String(str).match(/[0-9]+(\.[0-9]+)*/g);
    var operArr = String(str).match(/[+-]|[*/]|=/g);
    

    digitArr = Array.from(digitArr.filter(element => element !== ""));
    operArr = Array.from(operArr.filter(element => element !== ""));
    var count = 1 * digitArr[0];

    for (var i = 0; i < digitArr.length; i++) {
        if (operArr[i] == '=') {
            break;
        }
        count = operation(count, digitArr[i+1], operArr[i]);
    }

    document.getElementById("result1").innerHTML = count;
}

function operation(count, value, op) {
    switch (op) {
        case '+':
            count += value * 1;
            break;
        case '-':
            count -= value * 1;
            break;
        case '*':
            count *= value;
            break;
        case '/':
            count /= value;
            break;
        default:
            break;
    }

    return count;
}