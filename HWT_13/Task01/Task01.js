function task01() {
    var str = document.getElementById("string").value;
    var separators = ["\t", ",", ".", "!", "?", ";", ":"];
    var wordsArray = str.split(' ');
    var letters = {};
    var lettersArray = str.split('');
    var result = [];

    var tmpArr = [];
    separators.forEach(function (separator) {
        wordsArray.forEach(function (word) {
            tmpArr = tmpArr.concat((word + '').split(separator));
        })
        wordsArray = tmpArr;
        tmpArr = [];
    })

    wordsArray = Array.from(wordsArray.filter(element => element !== ""));
    wordsArray.forEach(function (word) {
        word.split('').forEach(function (letter, i) {
            if (!letters[letter] && -1 != word.indexOf(letter, i + 1)) {
                letters[letter] = 1;
            }
        })
    })

    result = result.concat(lettersArray.filter(function (element) {
        return !letters[element];
    }))

    document.getElementById("result1").innerHTML = result.join('');
}