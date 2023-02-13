using AutoSerivce.Commands;
using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AutoSerivce.ViewModels
{
    public class ClientServiceWindowViewModel : DialogViewModel
    {
        public ClientServiceWindowViewModel()
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentClientServices = db.ClientServices.Include(c => c.Client)
                                                  .Include(s => s.Service)
                                                 /* .OrderBy(d => d.StartTime)*/.ToList(); // по возрастанию, дата которая ближе всего
            }

            SearchLastNameStartTimeCommand = new LambdaCommand(OnSearchLastNameStartTimeCommanExecuted, CanSearchLastNameStartTimeCommanExecute);
        }

        #region Свойства

        private IEnumerable<ClientService> _currentClientServices;
        public IEnumerable<ClientService> CurrentClientServices { get => _currentClientServices; set => Set(ref _currentClientServices, value); }
        private string _title = "Ближайшие записи на услуги";
        public string Title { get => _title; set => Set(ref _title, value); }
        private string _searchLastName;
        public string SearchLastName { get => _searchLastName; set => Set(ref _searchLastName, value); }
        private string _searchStartTime;
        public string SearchStartTime { get => _searchStartTime; set => Set(ref _searchStartTime, value); }

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
                    CurrentClientServices = CurrentClientServices.Where(t => t.Client.LastName.Contains(SearchLastName)).ToList();


                //SearchStartTime


            }
        }

        #endregion
    }
}
