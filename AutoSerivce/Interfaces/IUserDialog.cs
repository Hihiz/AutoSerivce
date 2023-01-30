﻿using AutoSerivce.Models;

namespace AutoSerivce.Interfaces
{
    public interface IUserDialog
    {
        void OpenMainWindow();
        void OpenAddEditServiceWindow();
        void OpenAddEditServiceWindow(Service currentService);
    }
}
