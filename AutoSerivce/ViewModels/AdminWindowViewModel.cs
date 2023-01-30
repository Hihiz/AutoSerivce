using AutoSerivce.ViewModels.Base;
using System.Windows.Input;
using AutoSerivce.Commands;
using AutoSerivce.Views.Windows;
using System.Windows;

namespace AutoSerivce.ViewModels
{
    public class AdminWindowViewModel : DialogViewModel
    {
        public AdminWindowViewModel()
        {
            AdminPasswordCommand = new LambdaCommand(OnAdminPasswordCommandExecuted, CanAdminPasswordCommandExecute);
        }

        private string _passAdmin;
        public string PassAdmin { get => _passAdmin; set => Set(ref _passAdmin, value); }

        //AdminPasswordCommand
        public ICommand AdminPasswordCommand { get; set; }
        private bool CanAdminPasswordCommandExecute(object p) => true;
        private void OnAdminPasswordCommandExecuted(object p)
        {
            AdminWindow adminWindow = new AdminWindow();

            if (PassAdmin == "0000")
            {
                MainWindow mainWindow = new MainWindow();
                MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

                mainWindow.DataContext = mainWindowViewModel;

                mainWindowViewModel.AdminPanelVisibility = Visibility.Visible;

                mainWindow.Show();

                Window window = Application.Current.Windows[0];
                window.Close();

            }
            else
                MessageBox.Show("Пароль введен не верно");
        }
    }
}
