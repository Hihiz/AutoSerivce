using AutoSerivce.Models;
using AutoSerivce.ViewModels;
using AutoSerivce.Views.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;

namespace AutoSerivce.Interfaces.Implementations
{
    public class UserDialogService : IUserDialog
    {
        private readonly IServiceProvider _services;

        public UserDialogService(IServiceProvider services) => _services = services;

        public void OpenMainWindow()
        {
            MainWindow mainWindow;
            mainWindow = _services.GetRequiredService<MainWindow>();
            mainWindow.Closed += (_, _) => mainWindow = null;
            mainWindow.Show();
        }

        public void OpenMainWindow(Visibility visibility)
        {
            MainWindow mainWindow;
            mainWindow = _services.GetRequiredService<MainWindow>();

            /*visibility = */
            _services.GetRequiredService<MainWindowViewModel>().AdminPanelVisibility = visibility;
            _services.GetRequiredService<MainWindowViewModel>().AdminMenuItemVisibility = visibility;

            mainWindow.Closed += (_, _) => mainWindow = null;
            mainWindow.Show();
        }

        public void OpenAddEditServiceWindow()
        {
            AddEditServiceWindow addEditServiceWindow;
            addEditServiceWindow = _services.GetRequiredService<AddEditServiceWindow>();

            _services.GetRequiredService<AddEditServiceWindowViewModel>().Title = "Добавление новой услуги";
            _services.GetRequiredService<AddEditServiceWindowViewModel>().CurrentService = new Service();
            _services.GetRequiredService<AddEditServiceWindowViewModel>().IdVisibility = Visibility.Collapsed;

            addEditServiceWindow.Closed += (_, _) => addEditServiceWindow = null;
            addEditServiceWindow.ShowDialog();
        }

        public void OpenAddEditServiceWindow(object item)
        {
            //addEditServiceWindow = _services.GetRequiredService<AddEditServiceWindow>(); 

            //AddEditServiceWindowViewModel addEditServiceWindowViewModel = new AddEditServiceWindowViewModel
            //{
            //    Title = $"Редактирование услуги: {((Service)item).Title} | Id: {((Service)item).Id}",
            //    CurrentService = (Service)item,
            //    IdVisibility = Visibility.Visible
            //}; 

            //AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow
            //{
            //    DataContext = addEditServiceWindowViewModel
            //};

            AddEditServiceWindow addEditServiceWindow;
            addEditServiceWindow = _services.GetRequiredService<AddEditServiceWindow>();

            _services.GetRequiredService<AddEditServiceWindowViewModel>().Title = $"Редактирование услуги: {((Service)item).Title} | Id: {((Service)item).Id}"; //
            _services.GetRequiredService<AddEditServiceWindowViewModel>().CurrentService = (Service)item; //
            _services.GetRequiredService<AddEditServiceWindowViewModel>().IdVisibility = Visibility.Visible; //

            addEditServiceWindow.Closed += (_, _) => addEditServiceWindow = null;
            addEditServiceWindow.ShowDialog();
        }

        public void AdminPanelWindow()
        {
            AdminWindow adminWindow;
            adminWindow = _services.GetRequiredService<AdminWindow>();
            adminWindow.Closed += (_, _) => adminWindow = null;
            adminWindow.Show();
        }

        public void OpenClientServiceWindow()
        {
            ClientServiceWindow clientServiceWindow;
            clientServiceWindow = _services.GetRequiredService<ClientServiceWindow>();

            clientServiceWindow.Closed += (_, _) => clientServiceWindow = null;
            clientServiceWindow.Show();
        }

        public void OpenServiceClientEntryWindow()
        {
            ServiceClientEntryWindow serviceClientEntryWindow;
            serviceClientEntryWindow = _services.GetRequiredService<ServiceClientEntryWindow>();

            _services.GetRequiredService<ServiceClientEntryWindowViewModel>().Title = "Регистрация нового клиента";
            _services.GetRequiredService<ServiceClientEntryWindowViewModel>().CurrClientService = new ClientService();
            _services.GetRequiredService<ServiceClientEntryWindowViewModel>().CurrentClientService = new Client();

            serviceClientEntryWindow.Closed += (_, _) => serviceClientEntryWindow = null;
            serviceClientEntryWindow.ShowDialog();
        }

        public void OpenServiceClientEntryWindow(object item, object clientName, object serviceName)
        {
            ServiceClientEntryWindow serviceClientEntryWindow;
            serviceClientEntryWindow = _services.GetRequiredService<ServiceClientEntryWindow>();

            _services.GetRequiredService<ServiceClientEntryWindowViewModel>().Title = $"Редактирование клиента: {((ClientService)item).Client.LastName} {((ClientService)item).Client.FirsName} {((ClientService)item).Client.Patronymic} | Id: {((ClientService)item).Client.Id}";
            _services.GetRequiredService<ServiceClientEntryWindowViewModel>().CurrClientService = (ClientService)item;
            _services.GetRequiredService<ServiceClientEntryWindowViewModel>().CurrentClientService = new Client();

            _services.GetRequiredService<ServiceClientEntryWindowViewModel>().ClientName = (System.Collections.Generic.List<Client>)clientName;
            _services.GetRequiredService<ServiceClientEntryWindowViewModel>().ServiceName = (System.Collections.Generic.List<Service>)serviceName;

            serviceClientEntryWindow.Closed += (_, _) => serviceClientEntryWindow = null;
            serviceClientEntryWindow.ShowDialog();
        }
    }
}
