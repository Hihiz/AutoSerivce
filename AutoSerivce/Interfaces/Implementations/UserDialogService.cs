using AutoSerivce.Models;
using AutoSerivce.ViewModels;
using AutoSerivce.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace AutoSerivce.Interfaces.Implementations
{
    public class UserDialogService : IUserDialog
    {
        private readonly IServiceProvider _services;

        public UserDialogService(IServiceProvider services) => _services = services;

        public void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow = _services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        public void OpenAddEditServiceWindow()
        {
            AddEditServiceWindowViewModel addEditServiceWindowViewModel = new AddEditServiceWindowViewModel
            {
                Title = "Добавление новой услуги"
            };

            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow()
            {
                DataContext = addEditServiceWindowViewModel
            };

            addEditServiceWindow.ShowDialog();
        }

        public void OpenAddEditServiceWindow(object item)
        {
            AddEditServiceWindowViewModel addEditServiceWindowViewModel = new AddEditServiceWindowViewModel
            {
                Title = $"Редактирование услуги: {((Service)item).Title} | Id: {((Service)item).Id}",
                CurrentService = (Service)item,
                IdVisibility = Visibility.Visible
            };

            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow
            {
                DataContext = addEditServiceWindowViewModel
            };

            addEditServiceWindowViewModel.CurrentService = (Service)item;

            addEditServiceWindow.ShowDialog();
        }

        public void AdminPanelWindow()
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow = _services.GetRequiredService<AdminWindow>();

            adminWindow.Show();

            Window window = Application.Current.Windows[0];
            window.Close();

        }
    }
}
