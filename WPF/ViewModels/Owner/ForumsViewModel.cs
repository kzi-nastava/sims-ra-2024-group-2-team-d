using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Services.IServices;
using BookingApp.WPF.Views.Guest1;
using BookingApp.WPF.Views.Owner;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.WPF.ViewModels.Owner
{
    public class ForumsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ForumOverviewDTO> _myForums;
        private ObservableCollection<ForumOverviewDTO> _allForums;
        private ForumOverviewDTO _selectedForum;
        private readonly IForumService _forumService;
        private readonly IForumCommentService _forumCommentService;
        private readonly IUserService _userService;
        private readonly IForumIdService _forumIdService;
        private readonly IForumUtilityService _forumUtilityService;
        public RelayCommand OpenForumCommand { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<ForumOverviewDTO> AllForums
        {
            get => _allForums;
            set
            {
                if (value != _allForums)
                {
                    _allForums = value;
                    OnPropertyChanged("AllForums");
                }
            }
        }
        public ForumOverviewDTO SelectedForum
        {
            get => _selectedForum;
            set
            {
                if (value != _selectedForum)
                {
                    _selectedForum = value;
                    OnPropertyChanged("SelectedForum");
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ForumsViewModel()
        {
            _forumService = Injector.Injector.CreateInstance<IForumService>();
            _forumCommentService = Injector.Injector.CreateInstance<IForumCommentService>();
            _userService = Injector.Injector.CreateInstance<IUserService>();
            _forumIdService = Injector.Injector.CreateInstance<IForumIdService>();
            _forumUtilityService = Injector.Injector.CreateInstance<IForumUtilityService>();
            OpenForumCommand = new RelayCommand(OpenForum);
            AllForums = new ObservableCollection<ForumOverviewDTO>();
            InitializeAllForums();
        }

        private void InitializeAllForums()
        {
            List<Forum> forums = _forumUtilityService.GetForumsWhereOwnerHasAccommodation();
            string useful;
            AllForums.Clear();
            foreach (Forum f in forums)
            {
                useful = CheckUseful(f);
                int commentsNumber = _forumCommentService.GetCommentsNumberByForum(f.ForumId);
                ForumOverviewDTO forum = new ForumOverviewDTO(f.ForumId, f.ForumTopic, f.Location, f.DateCreated, commentsNumber, f.Status, useful);
                AllForums.Add(forum);
            }
            OnPropertyChanged(nameof(AllForums));
        }
        private string CheckUseful(Forum forum)
        {
            return _forumUtilityService.CheckUseful(forum);
        }

        private void SetWindowsProperties(Window window)
        {
            window.Owner = App.Current.Windows.OfType<ForumsWindow>().FirstOrDefault();
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void OpenForum(object parameter)
        {
            if (SelectedForum != null)
            {
                _forumIdService.ForumId = SelectedForum.ForumId;

            }
            ForumCommentsWindow forumCommentsOverview = new ForumCommentsWindow();
            SetWindowsProperties(forumCommentsOverview);
            forumCommentsOverview.ShowDialog();
            InitializeAllForums();

        }
    }
}
