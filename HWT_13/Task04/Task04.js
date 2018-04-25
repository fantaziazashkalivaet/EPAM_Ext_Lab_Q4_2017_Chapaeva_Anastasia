var Timer;
var IsPause = false;
var IsStart = false;
var EndPage = 4;
var StartPage = 1;
var timeout = setTimeout(action, 10);

function start() {
    Timer = 400 * 1;
    action();
}

function action() {
    if (!IsStart) {
        IsStart = true;
        start();
    }

    Timer--;
    var pageName = location.href;
    pageName = pageName.substr(pageName.lastIndexOf("/") + 1);
    var numberNextPage = pageName.match(/[0-9]+/) * 1 + 1;

    if (Timer < 1) {        
        if (numberNextPage - 1 != EndPage) {
            NextPage(SearchNameNextPage);
        }
        else {
            return;
        }
    }

    if (numberNextPage <= EndPage) {
        document.getElementById('time').innerHTML = Timer / 100;
        if (!IsPause) {
            timeout = setTimeout(action, 10);
        }       
    }
}

function SearchNameNextPage() {
    var pageName = location.href;
    pageName = pageName.substr(pageName.lastIndexOf("/") + 1);
    var numberNextPage = pageName.match(/[0-9]+/) * 1 + 1;
    var mainPageName = pageName.substr(0, pageName.lastIndexOf(numberNextPage - 1));
    if (numberNextPage > EndPage) {
        return "./" + mainPageName + StartPage + ".html";
    }
    
    return "./" + mainPageName + (numberNextPage) + ".html";
}

function SearchPreviousPage() {
    var pageName = location.href;
    pageName = pageName.substr(pageName.lastIndexOf("/") + 1);
    var numberPreviousPage = pageName.match(/[0-9]+/) * 1 - 1;
    if (numberPreviousPage < StartPage) {
        return "./" + mainPageName + EndPage + ".html";
    }
    var mainPageName = pageName.substr(0, pageName.lastIndexOf(numberNextPage - 1));
    return "./" + mainPageName + (numberPreviousPage) + ".html";
}

function NextPage() {
    location.assign(SearchNameNextPage());
}


function Back() {
    history.back(SearchPreviousPage);
}

function Pause() {
    if (!IsPause) {
        clearTimeout(timeout);
    }
    else {
        timeout = setTimeout(action, 10);
    }
    IsPause = !IsPause;
}