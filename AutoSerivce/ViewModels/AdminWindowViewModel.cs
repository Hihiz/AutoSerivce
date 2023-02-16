using AutoSerivce.ViewModels.Base;
using System.Windows.Input;
using AutoSerivce.Commands;
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
            BackMainWindowCommand = new LambdaCommand(OnBackMainWindowCommandExecuted, CanBackMainWindowCommandExecute);
        }

        public AdminWindowViewModel(IUserDialog userDialog) : this()
        {
            _userDialog = userDialog;
        }

        #region Свойства

        private string _title = "Доступ к админ панели";
        public string Title { get => _title; set => Set(ref _title, value); }

        private string _passAdmin;
        public string PassAdmin { get => _passAdmin; set => Set(ref _passAdmin, value); }
        #endregion

        #region Команды

        public ICommand AdminPasswordCommand { get; set; }
        private bool CanAdminPasswordCommandExecute(object p)
        {
            if (string.IsNullOrWhiteSpace(PassAdmin))
                return false;

            return true;
        }
        private void OnAdminPasswordCommandExecuted(object p)
        {
            if (PassAdmin == "0000")
            {
                _userDialog.OpenMainWindow(Visibility.Visible);
                
                OnDialogComplete(EventArgs.Empty);
            }
            else
                MessageBox.Show("Пароль введен не верно");
        }

        public ICommand BackMainWindowCommand { get; set; }
        private bool CanBackMainWindowCommandExecute(object p) => true;
        private void OnBackMainWindowCommandExecuted(object p)
        {
            _userDialog.OpenMainWindow(Visibility.Collapsed);

            OnDialogComplete(EventArgs.Empty);
        }

        #endregion
    }
}
