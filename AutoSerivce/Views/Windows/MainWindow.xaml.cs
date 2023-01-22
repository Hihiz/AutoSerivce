using AutoSerivce.Models;
using System.Linq;
using System.Windows;

namespace AutoSerivce.Views.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //using (AutoServiceContext db = new AutoServiceContext())
            //{
            //    autoSerivceGrid.ItemsSource = db.Services.ToList();
            //}
        }
    }
}
