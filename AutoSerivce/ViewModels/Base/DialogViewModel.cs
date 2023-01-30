using System;

namespace AutoSerivce.ViewModels.Base
{
    public class DialogViewModel : ViewModel
    {
        //Событие
        public event EventHandler DialogComplete;

        //Метод который генерирует событие
        protected virtual void OnDialogComlete(EventArgs e) => DialogComplete.Invoke(this, e);
    }
}
