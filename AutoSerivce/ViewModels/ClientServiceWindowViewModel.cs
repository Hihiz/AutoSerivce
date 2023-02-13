using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
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
                ClientSerivces = db.ClientServices.ToList();
            }
        }

        #region Свойства

        private IEnumerable<ClientService> _clientSerivces;
        public IEnumerable<ClientService> ClientSerivces { get => _clientSerivces; set => Set(ref _clientSerivces, value); }

        #endregion
    }
}
