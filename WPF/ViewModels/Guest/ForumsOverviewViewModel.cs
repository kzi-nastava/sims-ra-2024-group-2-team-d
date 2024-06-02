using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Services.IServices;
using BookingApp.WPF.Views.Guest1;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.WPF.ViewModels.Guest
{
    public class ForumsOverviewViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ForumOverviewDTO> _myForums;
        private ObservableCollection<ForumOverviewDTO> _allForums;
        private ForumOverviewDTO _selectedForum;
        private readonly IForumService _forumService;
        private readonly IForumCommentService _forumCommentService;
        private readonly IUserService _userService;
        private readonly IForumIdService _forumIdService;
        private readonly IForumUtilityService _forumUtilityService;
        public RelayCommand CreateForumCommand { get; set; }
        public RelayCommand CloseForumCommand { get; set; }
        public RelayCommand OpenForumCommand { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<ForumOverviewDTO> MyForums
        {
            get => _myForums;
            set
            {
                if (value != _myForums)
                {
                    _myForums = value;
                    OnPropertyChanged("MyForums");
                }
            }
        }
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
        public ForumsOverviewViewModel()
        {
            _forumService = Injector.Injector.CreateInstance<IForumService>();
            _forumCommentService = Injector.Injector.CreateInstance<IForumCommentService>();
            _userService = Injector.Injector.CreateInstance<IUserService>();
            _forumIdService = Injector.Injector.CreateInstance<IForumIdService>();
            _forumUtilityService = Injector.Injector.CreateInstance<IForumUtilityService>();
            CreateForumCommand = new RelayCommand(OpenCreateForumForm);
            CloseForumCommand = new RelayCommand(CloseForum);
            OpenForumCommand = new RelayCommand(OpenForum);
            MyForums = new ObservableCollection<ForumOverviewDTO>();
            AllForums = new ObservableCollection<ForumOverviewDTO>();
            InitializeMyForums();
            InitializeAllForums();
        }
        private void OpenCreateForumForm(object parameter)
        {
            CreatingForumForm creatingForumForm = new CreatingForumForm();
            creatingForumForm.Owner = App.Current.Windows.OfType<ForumsOverviewWindow>().FirstOrDefault();
            creatingForumForm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            creatingForumForm.ShowDialog();
            InitializeAllForums();
            InitializeMyForums();
        }
        private void InitializeMyForums()
        {
            //TODO FIND LOGGED IN USER
            List<Forum> myForums = _forumService.GetForumsByCreatorId(1);
            string useful;
            MyForums.Clear();
            foreach (Forum myForum in myForums)
            {
                useful = CheckUseful(myForum);
                int commentsNumber = _forumCommentService.GetCommentsNumberByForum(myForum.ForumId);
                ForumOverviewDTO forum = new(myForum.ForumId, myForum.ForumTopic, myForum.Location, myForum.DateCreated, commentsNumber, myForum.Status, useful);
                MyForums.Add(forum);
            }
            OnPropertyChanged(nameof(MyForums));
        }
        private void InitializeAllForums()
        {
            List<Forum> forums = _forumService.GetAll();
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
        private void CloseForum(object parameter)
        {
            CloseForumWindow closeForumWindow = new CloseForumWindow();
            SetWindowsProperties(closeForumWindow);
            closeForumWindow.ShowDialog();
            InitializeAllForums();
            InitializeMyForums();
        }
        private void OpenForum(object parameter)
        {
            if (SelectedForum != null)
            {
                _forumIdService.ForumId = SelectedForum.ForumId;

            }
            ForumCommentsOverview forumCommentsOverview = new ForumCommentsOverview();
            SetWindowsProperties(forumCommentsOverview);
            forumCommentsOverview.ShowDialog();
            InitializeAllForums();
            InitializeMyForums();

        }
        private void SetWindowsProperties(Window window)
        {
            window.Owner = App.Current.Windows.OfType<ForumsOverviewWindow>().FirstOrDefault();
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
