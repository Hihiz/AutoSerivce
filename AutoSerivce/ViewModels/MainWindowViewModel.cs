using AutoSerivce.Commands;
using AutoSerivce.Interfaces;
using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AutoSerivce.ViewModels
{
    public class MainWindowViewModel : DialogViewModel
    {
        private readonly IUserDialog _userDialog = null;

        public MainWindowViewModel()
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentServices = db.Services.ToList();
                CountServices = $"Количество {CurrentServices.Count()} из {db.Services.Count()}";
            }

            SortServicePrice = new List<string>() { "По возрастанию цены", "По убыванию цены" };

            FilterServiceDiscount = new List<string>() { "от 0 до 5%", "от 5 до 15%", "от 15 до 30%", "от 30 до 70%", "от 70 до 100%" };
            FilterServiceDiscount.Insert(0, "Все");

            AddServiceCommand = new LambdaCommand(OnAddServiceCommandExecuted, CanAddServiceCommandExecute);
            EditServiceCommand = new LambdaCommand(OnEditServiceCommandExecuted, CanEditServiceCommandExecute);
            DeleteServiceCommand = new LambdaCommand(OnDeleteServiceCommandExecuted, CanDeleteServiceCommandExecute);
            AdminPanelCommand = new LambdaCommand(OnAdminPanelCommandExecuted, CanAdminPanelCommandExecute);
            SortFilterSearchServiceCommand = new LambdaCommand(OnSortFilterSearchServiceCommandExecuted, CanSortFilterSearchServiceCommandExecute);
            ClearCommand = new LambdaCommand(OnClearCommandExecuted, CanClearCommandExecute);
        }

        public MainWindowViewModel(IUserDialog userDialog) : this()
        {
            _userDialog = userDialog;
        }

        #region Свойства

        private string _title = "Услуги автосервиса";
        public string Title { get => _title; set => Set(ref _title, value); }

        private Service _currentService;
        public Service CurrentService { get => _currentService; set => Set(ref _currentService, value); }

        private IEnumerable<Service> _currentServices;
        public IEnumerable<Service> CurrentServices { get => _currentServices; set => Set(ref _currentServices, value); }

        private Visibility _adminPanelVisibility = Visibility.Collapsed; //
        public Visibility AdminPanelVisibility { get => _adminPanelVisibility; set => Set(ref _adminPanelVisibility, value); }

        private string _countServices;
        public string CountServices { get => _countServices; set => Set(ref _countServices, value); }

        private List<string> _sortServicePrice;
        public List<string> SortServicePrice { get => _sortServicePrice; set => Set(ref _sortServicePrice, value); }

        private int _sortSelectedIndex;
        public int SortSelectedIndex { get => _sortSelectedIndex; set => Set(ref _sortSelectedIndex, value); }

        private string _sortSelectedValue;
        public string SortSelectedValue { get => _sortSelectedValue; set => Set(ref _sortSelectedValue, value); }

        private List<string> _filterDiscount;
        public List<string> FilterServiceDiscount { get => _filterDiscount; set => Set(ref _filterDiscount, value); }

        private int _filterSelectedIndex;
        public int FilterSelectedIndex { get => _filterSelectedIndex; set => Set(ref _filterSelectedIndex, value); }

        private string _filterSelectedValue;
        public string FilterSelectedValue { get => _filterSelectedValue; set => Set(ref _filterSelectedValue, value); }

        private string _searchText;
        public string SearchText { get => _searchText; set => Set(ref _searchText, value); }

        #endregion

        #region Комманды

        public ICommand AddServiceCommand { get; set; }
        private bool CanAddServiceCommandExecute(object p) => true;
        public void OnAddServiceCommandExecuted(object p)
        {
            _userDialog.OpenAddEditServiceWindow();

            using (AutoServiceContext db = new AutoServiceContext())
            {
                //CurrentServices = db.Services.ToList();
                CountServices = $"Количество {CurrentServices.Count()} из {db.Services.Count()}";
            }
        }

        public ICommand EditServiceCommand { get; set; }
        private bool CanEditServiceCommandExecute(object p)
        {
            if (AdminPanelVisibility == Visibility.Collapsed)
                return false;

            return p is Service;
        }
        public void OnEditServiceCommandExecuted(object p)
        {
            CurrentService = (Service)p;

            _userDialog.OpenAddEditServiceWindow(CurrentService);

            using (AutoServiceContext db = new AutoServiceContext())
            {
                //CurrentServices = db.Services.ToList();
                CountServices = $"Количество {CurrentServices.Count()} из {db.Services.Count()}";
            }
        }

        public ICommand DeleteServiceCommand { get; set; }
        private bool CanDeleteServiceCommandExecute(object p) => p is Service;
        private void OnDeleteServiceCommandExecuted(object p)
        {
            if (MessageBox.Show($"Вы точно хотите удалить {((Service)p).Title}", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (AutoServiceContext db = new AutoServiceContext())
                {
                    db.Services.Remove((Service)p);
                    db.SaveChanges();

                    MessageBox.Show($"Услуга {((Service)p).Title} удалена !");

                    CurrentServices = db.Services.ToList();
                    CountServices = $"Количество {CurrentServices.Count()} из {db.Services.Count()}";
                }
            }
        }

        public ICommand AdminPanelCommand { get; set; }
        private bool CanAdminPanelCommandExecute(object p) => true;
        private void OnAdminPanelCommandExecuted(object p)
        {
            _userDialog.AdminPanelWindow();

            OnDialogComplete(EventArgs.Empty);
        }

        public ICommand SortFilterSearchServiceCommand { get; set; }
        private bool CanSortFilterSearchServiceCommandExecute(object p) => true;
        private void OnSortFilterSearchServiceCommandExecuted(object p)
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentServices = db.Services.ToList();

                if (SortSelectedIndex != -1)
                {
                    if (SortSelectedValue.Contains("По возрастанию цены"))
                        CurrentServices = CurrentServices.OrderBy(p => p.Cost);

                    if (SortSelectedValue.Contains("По убыванию цены"))
                        CurrentServices = CurrentServices.OrderByDescending(p => p.Cost);
                }
                if (FilterSelectedIndex != -1)
                {
                    if (FilterSelectedValue.Contains("от 0 до 5%"))
                        CurrentServices = CurrentServices.Where(d => d.Discount >= 0 && d.Discount <= 5);

                    if (FilterSelectedValue.Contains("от 5 до 15%"))
                        CurrentServices = CurrentServices.Where(d => d.Discount >= 5 && d.Discount <= 15);

                    if (FilterSelectedValue.Contains("от 15 до 30%"))
                        CurrentServices = CurrentServices.Where(d => d.Discount >= 15 && d.Discount <= 30);

                    if (FilterSelectedValue.Contains("от 30 до 70%"))
                        CurrentServices = CurrentServices.Where(d => d.Discount >= 30 && d.Discount <= 70);

                    if (FilterSelectedValue.Contains("от 70 до 100%"))
                        CurrentServices = CurrentServices.Where(d => d.Discount >= 70 && d.Discount <= 100);

                }
                if (SearchText != null)
                    CurrentServices = CurrentServices.Where(t => t.Title.Contains(SearchText) /*|| t.Description.Contains(SearchText)*/).ToList();

                CountServices = $"Количество {CurrentServices.Count()} из {db.Services.Count()}";
            }
        }

        public ICommand ClearCommand { get; set; }
        private bool CanClearCommandExecute(object p) => true;
        private void OnClearCommandExecuted(object p)
        {
            SortSelectedIndex = -1;
            FilterSelectedIndex = -1;
            SearchText = "";
        }

        #endregion
    }
}
