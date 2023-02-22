using AutoSerivce.Commands;
using AutoSerivce.Interfaces;
using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AutoSerivce.ViewModels
{
    public class ServiceClientEntryWindowViewModel : DialogViewModel
    {
        private readonly IUserDialog _userDialog = null;

        public ServiceClientEntryWindowViewModel()
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentClientServices = db.Clients.ToList();
                GenderName = db.Genders.ToList();
            }

            AddClientCommand = new LambdaCommand(OnAddClientCommandExecuted, CanAddClientCommandExecute);
        }

        public ServiceClientEntryWindowViewModel(IUserDialog userDialog) : this()
        {
            _userDialog = userDialog;
        }

        #region Свойства

        private IEnumerable<Client> _currentClientServices;
        public IEnumerable<Client> CurrentClientServices { get => _currentClientServices; set => Set(ref _currentClientServices, value); }

        private Client _currentClientService;
        public Client CurrentClientService { get => _currentClientService; set => Set(ref _currentClientService, value); }

        private string _title = "Запись клиента на услугу";
        public string Title { get => _title; set => Set(ref _title, value); }

        private List<Gender> _genderName;
        public List<Gender> GenderName { get => _genderName; set => Set(ref _genderName, value); }

        #endregion

        #region Команда

        public ICommand AddClientCommand { get; set; }
        private bool CanAddClientCommandExecute(object p) => true;
        private void OnAddClientCommandExecuted(object p)
        {
           
        }

        #endregion
    }
}
