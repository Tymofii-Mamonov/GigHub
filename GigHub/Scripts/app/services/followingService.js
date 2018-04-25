var FollowingService = function () {

    var createFollowing = function (artistId, done, fail) {
        $.post("/api/followings", { followeeId: artistId })
            .done(done)
            .fail(fail);
    };

    var deleteFollowing = function(artistId, done, fail) {
        $.ajax({
                url: "/api/followings/" + artistId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }
}();