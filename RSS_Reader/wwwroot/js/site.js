var scheduler;

function getCookie(name) {
    var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) return match[2];
}

function updateFeedItems(feed) {
    $.ajax({
        url: "/Rss/UpdateFeed",
        type: "GET",
        data: {"feed": feed},
        contentType: false,
        dataType: false,
        success: function (items) {
            $(".feed").html(items);
        }
    });
}

$(document).ready(function () {
    const cookieStr = getCookie("feeds")
    let updateTime;
    let cookieJson;
    if (cookieStr) {
        try {
            cookieJson = JSON.parse(decodeURIComponent(cookieStr))
            updateTime = cookieJson[0].SchedulerTime
        } catch (ex) {

        }
    }

    scheduler = setInterval(updateFeedItems, updateTime || 60000, cookieJson[0].Url)
});