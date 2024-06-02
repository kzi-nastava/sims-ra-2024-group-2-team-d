using BookingApp.Domain.Model;
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
    public class CloseForumWindowViewModel : INotifyPropertyChanged
    {
        private readonly IForumService _forumService;
        private readonly IUserService _userService;
        private int _selectedForumId;
        private bool _canCancelForum;
        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand CloseForumCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public int SelectedForumId
        {
            get => _selectedForumId;
            set
            {
                if (_selectedForumId != value)
                {
                    _selectedForumId = value;
                    CanCancelForum = true;
                    OnPropertyChanged(nameof(SelectedForumId));
                    OnPropertyChanged(nameof(CanCancelForum));
                }
            }
        }
        public bool CanCancelForum
        {
            get => _canCancelForum;
            set
            {
                if (_canCancelForum != value)
                {
                    _canCancelForum = value;
                    OnPropertyChanged(nameof(CanCancelForum));
                }
            }
        }
        public ObservableCollection<KeyValuePair<int, string>> Forums { get; set; }
        public CloseForumWindowViewModel()
        {
            _forumService = Injector.Injector.CreateInstance<IForumService>();
            _userService = Injector.Injector.CreateInstance<IUserService>();
            InitializeUserForums();
            CloseForumCommand = new RelayCommand(CloseForum);
            CancelCommand = new RelayCommand(Cancel);
            CanCancelForum = false;

        }
        private void InitializeUserForums()
        {
            //TODO: user id maybe from userService?
            int userId = 1;
            Forums = new ObservableCollection<KeyValuePair<int, string>>(_forumService.GetForumsByUserKeyValue(userId));
        }
        private void CloseForum(object parameter)
        {
            Forum forum = _forumService.GetForumById(SelectedForumId);
            forum.Status = ForumStatus.Closed;
            _forumService.Update(forum);
            CloseWindow();
        }
        private void Cancel(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }
        private void CloseWindow()
        {
            App.Current.Windows.OfType<CloseForumWindow>().FirstOrDefault().Close();
          //  App.Current.MainWindow.Close();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
