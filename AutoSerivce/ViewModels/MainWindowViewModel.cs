using AutoSerivce.Commands;
using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using AutoSerivce.Views.Windows;
using System.Collections.Generic;
using System.Linq;
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
            }

            AddServiceCommand = new LambdaCommand(OnAddServiceCommandExecuted, CanAddServiceCommandExecute);
        }

        #region Свойства

        private string _title = "Услуги автосервиса";
        public string Title { get => _title; set => Set(ref _title, value); }

        private Service _currentService;
        public Service CurrentService { get => _currentService; set => Set(ref _currentService, value); }

        private IEnumerable<Service> _currentServices;
        public IEnumerable<Service> CurrentServices { get => _currentServices; set => Set(ref _currentServices, value); }

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

            addEditServiceWindow.Title = "Добавление новой услуги";
            addEditServiceWindow.ShowDialog();

        }

        #endregion

    }
}
