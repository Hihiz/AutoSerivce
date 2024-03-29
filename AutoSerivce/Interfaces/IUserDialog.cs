﻿using System.Windows;

namespace AutoSerivce.Interfaces
{
    public interface IUserDialog
    {
        void OpenMainWindow();
        void OpenMainWindow(Visibility visibility);
        void OpenAddEditServiceWindow();
        void OpenAddEditServiceWindow(object item);
        void AdminPanelWindow();
        void OpenClientServiceWindow();
        void OpenServiceClientEntryWindow();
        void OpenServiceClientEntryWindow(object item, object clientName, object serviceName);
    }
}
