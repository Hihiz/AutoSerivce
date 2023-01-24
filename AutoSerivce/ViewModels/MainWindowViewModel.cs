using AutoSerivce.Commands;
using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using AutoSerivce.Views.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AutoSerivce.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {

        public MainWindowViewModel()
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentServices = db.Services.ToList();
                CountServices = $"Количество {CurrentServices.Count()} из {db.Services.Count()}";
            }

            AddServiceCommand = new LambdaCommand(OnAddServiceCommandExecuted, CanAddServiceCommandExecute);
            EditServiceCommand = new LambdaCommand(OnEditServiceCommandExecuted, CanEditServiceCommandExecute);
            DeleteServiceCommand = new LambdaCommand(OnDeleteServiceCommandExecuted, CanDeleteServiceCommandExecute);
            AdminPanelCommand = new LambdaCommand(OnAdminPanelCommandExecuted, CanAdminPanelCommandExecute);
        }

        #region Свойства

        private string _title = "Услуги автосервиса";
        public string Title { get => _title; set => Set(ref _title, value); }

        private Service _currentService;
        public Service CurrentService { get => _currentService; set => Set(ref _currentService, value); }

        private IEnumerable<Service> _currentServices;
        public IEnumerable<Service> CurrentServices { get => _currentServices; set => Set(ref _currentServices, value); }

        private Visibility _adminPanelVisibility = Visibility.Collapsed;
        public Visibility AdminPanelVisibility { get => _adminPanelVisibility; set => Set(ref _adminPanelVisibility, value); }

        private string _countServices;
        public string CountServices { get => _countServices; set => Set(ref _countServices, value); }

        #endregion

        #region Комманды

        public ICommand AddServiceCommand { get; set; }
        private bool CanAddServiceCommandExecute(object p) => true;
        public void OnAddServiceCommandExecuted(object p)
        {
            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow();
            AddEditServiceWindowViewModel addEditServiceWindowViewModel = new AddEditServiceWindowViewModel();

            CurrentService = new Service();

            addEditServiceWindow.DataContext = addEditServiceWindowViewModel;

            addEditServiceWindowViewModel.IdVisibility = Visibility.Collapsed;

            addEditServiceWindow.Title = "Добавление новой услуги";
            addEditServiceWindow.ShowDialog();

            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentServices = db.Services.ToList();
                CountServices = $"Количество {CurrentServices.Count()} из {db.Services.Count()}";
            }
        }

        public ICommand EditServiceCommand { get; set; }
        private bool CanEditServiceCommandExecute(object p)
        {
            if ((Service)p != null)
                return true;

            return false;
        }
        public void OnEditServiceCommandExecuted(object p)
        {
            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow();
            AddEditServiceWindowViewModel addEditServiceWindowViewModel = new AddEditServiceWindowViewModel();

            addEditServiceWindow.DataContext = addEditServiceWindowViewModel;

            CurrentService = (Service)p;

            addEditServiceWindowViewModel.CurrentService = CurrentService;
            addEditServiceWindowViewModel.IdVisibility = Visibility.Visible;

            addEditServiceWindow.Title = $"Редактирование услуги: {((Service)p).Title} | Id: {((Service)p).Id}";
            addEditServiceWindow.ShowDialog();

            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentServices = db.Services.ToList();
                CountServices = $"Количество {CurrentServices.Count()} из {db.Services.Count()}";
            }
        }

        public ICommand DeleteServiceCommand { get; set; }
        private bool CanDeleteServiceCommandExecute(object p)
        {
            if ((Service)p != null)
                return true;

            return false;
        }
        private void OnDeleteServiceCommandExecuted(object p)
        {
            if (MessageBox.Show($"Вы точно хотите удалить {((Service)p).Title}", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (AutoServiceContext db = new AutoServiceContext())
                {
                    db.Services.Remove((Service)p);
                    db.SaveChanges();

                    MessageBox.Show($"Услуга {((Service)p).Title} удалена !");

                    CurrentServices = db.Services.ToList();
                    CountServices = $"Количество {CurrentServices.Count()} из {db.Services.Count()}";
                }
            }
        }

        public ICommand AdminPanelCommand { get; set; }
        private bool CanAdminPanelCommandExecute(object p) => true;
        private void OnAdminPanelCommandExecuted(object p)
        {
            AdminWindow adminWindow = new AdminWindow();
            AdminWindowViewModel adminWindowViewModel = new AdminWindowViewModel();

            adminWindow.DataContext = adminWindowViewModel;

            adminWindow.Show();

            Window window = Application.Current.Windows[0];
            window.Close();
        }


        #endregion

    }
}
