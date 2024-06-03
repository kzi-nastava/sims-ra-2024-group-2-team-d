using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.WPF.ViewModels.Owner
{
    public class ForumCommentsViewModel : INotifyPropertyChanged
    {
        private bool _canLeaveComment;
        private string _topic;
        private string _location;
        private string _newComment;
        private ObservableCollection<CommentDTO> _comments;
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IForumCommentService _forumCommentService;
        private readonly IForumIdService _forumIdService;
        private readonly IForumService _forumService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IAccommodationService _accommodationService;
        private readonly IReservationService _reservationService;
        public RelayCommand SubmitCommentCommand { get; set; }
        public RelayCommand ReportCommentCommand { get; set; }
        public ObservableCollection<CommentDTO> Comments
        {
            get => _comments;
            set
            {
                if (value != _comments)
                {
                    _comments = value;
                    OnPropertyChanged("Comments");
                }
            }
        }
        public bool CanLeaveComment
        {
            get => _canLeaveComment;
            set
            {
                if (_canLeaveComment != value)
                {
                    _canLeaveComment = value;
                    OnPropertyChanged(nameof(CanLeaveComment));
                }
            }
        }
        public string Topic
        {
            get => _topic;
            set
            {
                if (_topic != value)
                {
                    _topic = value;
                    OnPropertyChanged(nameof(Topic));
                }
            }
        }
        public string Location
        {
            get => _location;
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }
        public string NewComment
        {
            get => _newComment;
            set
            {
                if (_newComment != value)
                {
                    _newComment = value;
                    OnPropertyChanged(nameof(NewComment));
                }
            }
        }
        public ForumCommentsViewModel()
        {
            _forumService = Injector.Injector.CreateInstance<IForumService>();
            _accommodationService = Injector.Injector.CreateInstance<IAccommodationService>();
            _forumCommentService = Injector.Injector.CreateInstance<IForumCommentService>();
            _userService = Injector.Injector.CreateInstance<IUserService>();
            _commentService = Injector.Injector.CreateInstance<ICommentService>();
            _forumIdService = Injector.Injector.CreateInstance<IForumIdService>();
            _reservationService = Injector.Injector.CreateInstance<IReservationService>();
            Comments = new ObservableCollection<CommentDTO>();
            SubmitCommentCommand = new RelayCommand(SubmitComment);
            ReportCommentCommand = new RelayCommand(ReportComment);
            InitializeForumComments();
            InitializeTopic();
            InitializeLocation();
            InitializeCanLeaveComment();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void InitializeForumComments()
        {
            Comments.Clear();
            List<int> commentIds = _forumCommentService.GetCommentsIdByForumId(_forumIdService.ForumId);
            foreach (int commentId in commentIds)
            {
                Comment com = _commentService.GetByCommentId(commentId);
                User user = _userService.GetById(com.User.Id);
                bool highlight = CheckForHighlight(user);
                CommentDTO commentDTO = new CommentDTO(user.Username, com.Text, com.CreationTime, highlight) { Id = com.Id, UserId = com.User.Id, NumberOfReports = com.NumberOfReports };
                Comments.Add(commentDTO);
            }
            Comments = new ObservableCollection<CommentDTO>(Comments.OrderByDescending(c => c.PostedDate).ToList());
        }
        private void InitializeTopic()
        {
            Topic = _forumService.GetTopic(_forumIdService.ForumId);
        }
        private void InitializeLocation()
        {
            Location location = _forumService.GetLocation(_forumIdService.ForumId);
            Location = location.City + ", " + location.Country;
        }
        private void SubmitComment(object parameter)
        {
            if (!string.IsNullOrEmpty(NewComment))
            {
                //TODO UserId
                Comment newComment = new Comment(DateTime.Now, NewComment, _userService.GetUserId(),0);
                newComment = _commentService.Save(newComment);
                ForumComment forumComment = new ForumComment(_forumIdService.ForumId, newComment.Id);
                _forumCommentService.Save(forumComment);
                NewComment = "";
                InitializeForumComments();
            }
        }

        private void ReportComment(object parameter)
        {
            if (parameter is CommentDTO c)
            {
                c.NumberOfReports += 1;
                _commentService.Update(new Comment()
                {
                    Id = c.Id,
                    CreationTime = c.PostedDate,
                    Text = c.Comment,
                    User = new User() { Id = c.UserId },
                    NumberOfReports = c.NumberOfReports
            });
                InitializeForumComments();
            }
        }

        private bool CheckForHighlight(User user)
        {
            Location location = _forumService.GetLocation(_forumIdService.ForumId);
            return (user.Role == Roles.GUEST && _reservationService.WasOnLocation(user.Id, location)) || (user.Role == Roles.OWNER && _accommodationService.HasAccommodationOnLocation(user.Id, location));
        }
        private void InitializeCanLeaveComment()
        {
            Forum forum = _forumService.GetForumById(_forumIdService.ForumId);
            CanLeaveComment = forum.Status == ForumStatus.Open && _accommodationService.HasAccommodationOnLocation(_userService.GetUserId(),forum.Location);
        }

    }
}

