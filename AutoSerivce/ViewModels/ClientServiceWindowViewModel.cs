using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AutoSerivce.ViewModels
{
    public class ClientServiceWindowViewModel : DialogViewModel
    {
        public ClientServiceWindowViewModel()
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                ClientSerivces = db.ClientServices.Include(c => c.Client)
                                                  .Include(s => s.Service)
                                                  .OrderBy(d => d.StartTime).ToList();
            }
        }

        #region Свойства

        private IEnumerable<ClientService> _clientSerivces;
        public IEnumerable<ClientService> ClientSerivces { get => _clientSerivces; set => Set(ref _clientSerivces, value); }
        private string _title = "Ближайшие записи на услуги";
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion
    }
}
