using AutoSerivce.Commands;
using AutoSerivce.Interfaces;
using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AutoSerivce.ViewModels
{
    public class ClientServiceWindowViewModel : DialogViewModel
    {
        private readonly IUserDialog _userDialog = null;

        public ClientServiceWindowViewModel()
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentClientServices = db.ClientServices.Include(c => c.Client)
                                                  .Include(s => s.Service)
                                                 /* .OrderBy(d => d.StartTime)*/.ToList(); // по возрастанию, дата которая ближе всего

                CountClientServices = $"Количество записанных клиентов: {CurrentClientServices.Count()} из {db.ClientServices.Count()}";
            }

            SearchLastNameStartTimeCommand = new LambdaCommand(OnSearchLastNameStartTimeCommanExecuted, CanSearchLastNameStartTimeCommanExecute);
            BackMainWindowCommand = new LambdaCommand(OnBackMainWindowCommandExecuted, CanBackMainWindowCommandExecute);
            EditClientServiceCommand = new LambdaCommand(OnEditClientServiceCommandExecuted, CanEditClientServiceCommandExecute);
            ServiceClientEntryWindowCommand = new LambdaCommand(OnServiceClientEntryWindowCommandExecuted, CanServiceClientEntryWindowCommandExecute);
        }

        public ClientServiceWindowViewModel(IUserDialog userDialog) : this()
        {
            _userDialog = userDialog;
        }

        #region Свойства

        private IEnumerable<ClientService> _currentClientServices;
        public IEnumerable<ClientService> CurrentClientServices { get => _currentClientServices; set => Set(ref _currentClientServices, value); }

        private ClientService _currentClientService;
        public ClientService CurrentClientService { get => _currentClientService; set => Set(ref _currentClientService, value); }

        private Client _currentClient;
        public Client CurrentClient { get => _currentClient; set => Set(ref _currentClient, value); }

        private string _title = "Ближайшие записи на услуги";
        public string Title { get => _title; set => Set(ref _title, value); }

        private string _searchLastName;
        public string SearchLastName { get => _searchLastName; set => Set(ref _searchLastName, value); }

        private string _searchStartTime;
        public string SearchStartTime { get => _searchStartTime; set => Set(ref _searchStartTime, value); }

        private string _countClientServices;
        public string CountClientServices { get => _countClientServices; set => Set(ref _countClientServices, value); }

        #endregion

        #region Команды 

        public ICommand SearchLastNameStartTimeCommand { get; set; }
        private bool CanSearchLastNameStartTimeCommanExecute(object p) => true;
        private void OnSearchLastNameStartTimeCommanExecuted(object p)
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentClientServices = db.ClientServices.Include(c => c.Client)
                                                      .Include(s => s.Service)
                                                     /* .OrderBy(d => d.StartTime)*/.ToList();

                if (SearchLastName != null)
                    CurrentClientServices = CurrentClientServices.Where(c => c.Client.LastName.Contains(SearchLastName)).ToList();

                if (SearchStartTime != null)
                    CurrentClientServices = CurrentClientServices.Where(c => c.StartTime.ToString().Contains(SearchStartTime)).ToList();

                CountClientServices = $"Количество записанных клиентов: {CurrentClientServices.Count()} из {db.ClientServices.Count()}";
            }
        }
        public ICommand BackMainWindowCommand { get; set; }
        private bool CanBackMainWindowCommandExecute(object p) => true;
        private void OnBackMainWindowCommandExecuted(object p)
        {
            _userDialog.OpenMainWindow(Visibility.Visible);

            OnDialogComplete(EventArgs.Empty);
        }

        public ICommand EditClientServiceCommand { get; set; }
        private bool CanEditClientServiceCommandExecute(object p)
        {
            if (((ClientService)p) == null)
                return false;

            return true;
        }
        private void OnEditClientServiceCommandExecuted(object p)
        {
            CurrentClientService = (ClientService)p;

            _userDialog.OpenServiceClientEntryWindow(CurrentClientService);

            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentClientServices = db.ClientServices.Include(c => c.Client)
                                                  .Include(s => s.Service)
                                                 /* .OrderBy(d => d.StartTime)*/.ToList(); // по возрастанию, дата которая ближе всего

                CountClientServices = $"Количество записанных клиентов: {CurrentClientServices.Count()} из {db.ClientServices.Count()}";
            }
        }

        public ICommand ServiceClientEntryWindowCommand { get; set; }
        private bool CanServiceClientEntryWindowCommandExecute(object p) => true;
        private void OnServiceClientEntryWindowCommandExecuted(object p)
        {
            _userDialog.OpenServiceClientEntryWindow();

            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentClientServices = db.ClientServices.Include(c => c.Client)
                                                  .Include(s => s.Service)
                                                 /* .OrderBy(d => d.StartTime)*/.ToList(); // по возрастанию, дата которая ближе всего

                CountClientServices = $"Количество записанных клиентов: {CurrentClientServices.Count()} из {db.ClientServices.Count()}";
            }
        }

        #endregion
    }
}
