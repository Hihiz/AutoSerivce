using System;

namespace AutoSerivce.ViewModels.Base
{
    public class DialogViewModel : ViewModel
    {
        //Событие
        public event EventHandler DialogComplete;

        //Метод которое событие генерирует
        protected virtual void OnDialogComplete(EventArgs e) => DialogComplete.Invoke(this, e);
    }
}
