using AutoSerivce.Models;
using System.Windows;

namespace AutoSerivce.Interfaces
{
    public interface IUserDialog
    {
        void OpenMainWindow();
        void OpenMainWindow(Visibility visibility);
        void OpenAddEditServiceWindow();
        void OpenAddEditServiceWindow(object item);
        void AdminPanelWindow();
    }
}
