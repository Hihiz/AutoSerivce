using AutoSerivce.ViewModels.Base;
using System.Windows.Input;
using AutoSerivce.Commands;
using AutoSerivce.Views.Windows;
using System.Windows;
using AutoSerivce.Interfaces;
using System;

namespace AutoSerivce.ViewModels
{
    public class AdminWindowViewModel : DialogViewModel
    {
        private readonly IUserDialog _userDialog = null;

        public AdminWindowViewModel()
        {
            AdminPasswordCommand = new LambdaCommand(OnAdminPasswordCommandExecuted, CanAdminPasswordCommandExecute);
        }

        public AdminWindowViewModel(IUserDialog userDialog) : this()
        {
            _userDialog = userDialog;
        }

        private string _title = "Доступ к админ панели";
        public string Title { get => _title; set => Set(ref _title, value); }

        private string _passAdmin;
        public string PassAdmin { get => _passAdmin; set => Set(ref _passAdmin, value); }

        public ICommand AdminPasswordCommand { get; set; }
        private bool CanAdminPasswordCommandExecute(object p)
        {
            if (string.IsNullOrWhiteSpace(PassAdmin))
                return false;

            return true;
        }
        private void OnAdminPasswordCommandExecuted(object p)
        {
            AdminWindow adminWindow = new AdminWindow();

            if (PassAdmin == "0000")
            {
                //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel
                //{
                //    AdminPanelVisibility = Visibility.Visible
                //};

                //MainWindow mainWindow = new MainWindow()
                //{
                //    DataContext = mainWindowViewModel
                //};

                //mainWindow.Show();

                _userDialog.OpenMainWindow();

                OnDialogComlete(EventArgs.Empty);
            }
            else
                MessageBox.Show("Пароль введен не верно");
        }
    }
}
