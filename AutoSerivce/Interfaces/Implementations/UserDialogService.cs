﻿using AutoSerivce.Models;
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

            //addEditServiceWindow = _services.GetRequiredService<AddEditServiceWindow>();

            addEditServiceWindow.ShowDialog();
        }

        public void OpenAddEditServiceWindow(Service currentService)
        {
            AddEditServiceWindowViewModel addEditServiceWindowViewModel = new AddEditServiceWindowViewModel
            {
                Title = $"Редактирование услуги: {((Service)currentService).Title} | Id: {((Service)currentService).Id}",
                CurrentService = currentService,
                IdVisibility = Visibility.Visible
            };

            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow
            {
                DataContext = addEditServiceWindowViewModel
            };

            //_services.GetRequiredService<AddEditServiceWindowViewModel>().CurrentService = currentService;
            //_services.GetRequiredService<AddEditServiceWindowViewModel>().IdVisibility = Visibility.Visible;

            addEditServiceWindowViewModel.CurrentService = currentService;

            addEditServiceWindow.ShowDialog();
        }

        public void AdminPanelWindow()
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow = _services.GetRequiredService<AdminWindow>();

            //Window window = Application.Current.Windows[0];
            //window.Close();

            adminWindow.Show();

            Window window = Application.Current.Windows[0];
            window.Close();

            //Window window = Application.Current.Windows[0];
            //window.Close();
        }
    }
}