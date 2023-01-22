using AutoSerivce.Commands;
using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using AutoSerivce.Views.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace AutoSerivce.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentServices = db.Services.ToList();
            }

            AddServiceCommand = new LambdaCommand(OnAddServiceCommandExecuted, CanAddServiceCommandExecute);
            EditServiceCommand = new LambdaCommand(OnEditServiceCommandExecuted, CanEditServiceCommandExecute);
        }

        #region Свойства

        private string _title = "Услуги автосервиса";
        public string Title { get => _title; set => Set(ref _title, value); }

        private Service _currentService;
        public Service CurrentService { get => _currentService; set => Set(ref _currentService, value); }

        private IEnumerable<Service> _currentServices;
        public IEnumerable<Service> CurrentServices { get => _currentServices; set => Set(ref _currentServices, value); }

        //private Visibility _idVisibility;
        //public Visibility IdVisibility { get => _idVisibility; set => Set(ref _idVisibility, value); }

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
        }


        #endregion

    }
}
