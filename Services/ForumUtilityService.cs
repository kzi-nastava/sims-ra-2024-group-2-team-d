using BookingApp.Domain.Model;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.Services
{
    internal class ForumUtilityService : IForumUtilityService
    {
        private readonly IForumCommentService _forumCommentService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IReservationService _accommodationReservationService;
        private readonly IAccommodationService _accommodationService;

        public ForumUtilityService()
        {
            _forumCommentService = Injector.Injector.CreateInstance<IForumCommentService>();
            _commentService = Injector.Injector.CreateInstance<ICommentService>();
            _userService = Injector.Injector.CreateInstance<IUserService>();
            _accommodationReservationService = Injector.Injector.CreateInstance<IReservationService>();
            _accommodationService = Injector.Injector.CreateInstance<IAccommodationService>();
        }
        public string CheckUseful(Forum forum)
        {
            int guestCommentCounter = 0;
            int ownerCommentCounter = 0;
            List<int> commentsIds = _forumCommentService.GetCommentsIdByForumId(forum.ForumId);
            foreach (int commentId in commentsIds)
            {
                Comment comment = _commentService.GetByCommentId(commentId);
               // User user = _userService.GetById(comment.User.Id);
                if (WasOnLocation(comment.User, forum))
                {
                    guestCommentCounter++;

                }
                else if (HasAccommodationOnLocation(comment.User, forum))
                {

                    ownerCommentCounter++;
                }
            }
            if (guestCommentCounter >= 20 && ownerCommentCounter >= 10)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }
        private bool WasOnLocation(User user, Forum forum)
        {
            return  _accommodationReservationService.WasOnLocation(user.Id, forum.Location);
        }
        private bool HasAccommodationOnLocation(User user, Forum forum)
        {
            return  _accommodationService.HasAccommodationOnLocation(user.Id, forum.Location);
        }
    }
}
