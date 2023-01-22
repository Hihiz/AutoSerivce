using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using System.Collections.Generic;
using System.Windows;

namespace AutoSerivce.ViewModels
{
    public class AddEditServiceWindowViewModel : ViewModel
    {
        public AddEditServiceWindowViewModel()
        {

        }

        private Service _currentService;
        public Service CurrentService { get => _currentService; set => Set(ref _currentService, value); }

        private IEnumerable<Service> _currentServices;
        public IEnumerable<Service> CurrentServices { get => _currentServices; set => Set(ref _currentServices, value); }

        private Visibility _idVisibility;
        public Visibility IdVisibility { get => _idVisibility; set => Set(ref _idVisibility, value); }

    }
}
