using System;
using System.Windows;

namespace AutoSerivce.Commands.Base
{
    public class CloseApplicationCommand : Command
    {
        public event EventHandler? CanExecuteChanged;

        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter) => Application.Current.Shutdown();

    }
}
