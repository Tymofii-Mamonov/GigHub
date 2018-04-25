var gigDetailsController = function (followingService) {
    var followingButton;

    var init = function(container) {
        $(container).on("click", ".js-toggle-follow", toggleFollowing);
    };

    var toggleFollowing = function(e) {
        followingButton = $(e.target);

        var artistId = followingButton.attr("data-user-id");

        if (followingButton.hasClass("btn-info"))
            followingService.deleteFollowing(artistId, done, fail);
        else
            followingService.createFollowing(artistId, done, fail);
    };

    var done = function() {
        var text = (followingButton.text() == "Following") ? "Follow" : "Following";

        followingButton.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    var fail = function() {
        alert("Something failed!");
    };

    return {
        init: init
    }
}(FollowingService);