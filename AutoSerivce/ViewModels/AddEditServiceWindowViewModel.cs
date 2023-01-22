using AutoSerivce.Commands;
using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AutoSerivce.ViewModels
{
    public class AddEditServiceWindowViewModel : ViewModel
    {
        private string _oldImage;
        private string _newImage;
        private string _newImagePath;

        public AddEditServiceWindowViewModel()
        {
            CurrentService = new Service();

            SaveServiceCommand = new LambdaCommand(OnSaveServiceCommandExecuted, CanSaveServiceCommandExecute);
            AddMainImageService = new LambdaCommand(OnAddImageCommandExecuted, CanAddMainImageServiceExecute);
        }

        #region Свойства

        private Service _currentService;
        public Service CurrentService { get => _currentService; set => Set(ref _currentService, value); }

        private IEnumerable<Service> _currentServices;
        public IEnumerable<Service> CurrentServices { get => _currentServices; set => Set(ref _currentServices, value); }

        private Visibility _idVisibility;


        public Visibility IdVisibility { get => _idVisibility; set => Set(ref _idVisibility, value); }

        #endregion

        #region Команды

        public ICommand SaveServiceCommand { get; set; }
        private bool CanSaveServiceCommandExecute(object p) => true;
        private void OnSaveServiceCommandExecuted(object p)
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                if (CurrentService.Id == 0)
                {
                    Service service = new Service()
                    {
                        Title = CurrentService.Title,
                        Cost = CurrentService.Cost,
                        DurationWork = CurrentService.DurationWork,
                        Description = CurrentService.Description,
                        Discount = CurrentService.Discount,
                        MainImagePath = CurrentService.MainImagePath
                    };

                    if (CurrentService.Cost < 0)
                    {
                        MessageBox.Show("Цена на услугу не может быть отрицательной");
                        return;
                    }

                    db.Services.Add(service);

                    if (String.IsNullOrEmpty(_newImage))
                    {
                        CurrentService.MainImagePath = "picture.jpg";
                        BitmapImage image = new BitmapImage(new Uri(CurrentService.ImagePath));
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        CurrentService.ImagePath = image.UriSource.ToString();

                    }
                    else // если выбрано фото
                    {
                        string newRelativePath = $"{System.DateTime.Now.ToString("HHmmss")}_{_newImage}";
                        service.MainImagePath = newRelativePath;

                        File.Copy(_newImagePath, System.IO.Path.Combine(Environment.CurrentDirectory, $"Услуги автосервиса/{newRelativePath}"));

                        BitmapImage image = new BitmapImage(new Uri(CurrentService.ImagePath));
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        CurrentService.ImagePath = image.UriSource.ToString();
                    }

                    MessageBox.Show("Услуга успешно добавлена");
                }
                else
                {
                    db.Services.Update(CurrentService);
                    MessageBox.Show("Услуга успешна обновлена");
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Услуга не сохранена, заполните данные и повторите попытку", "Ошибка");
                }
            }
        }

        public ICommand AddMainImageService { get; set; }
        private bool CanAddMainImageServiceExecute(object p) => true;
        private void OnAddImageCommandExecuted(object p)
        {
            Stream stream;

            if (CurrentService.Id != null)
                _oldImage = System.IO.Path.Combine(Environment.CurrentDirectory, $"Услуги автосервиса/{CurrentService.MainImagePath}");
            else
                _oldImage = null;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                if ((stream = dlg.OpenFile()) != null)
                {
                    dlg.DefaultExt = ".png";
                    dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                    dlg.Title = "Open Image";
                    dlg.InitialDirectory = "./";

                    // Предпросмотр изображения
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.UriSource = new Uri(dlg.FileName);

                    image.DecodePixelHeight = 300;
                    image.DecodePixelWidth = 200;

                    CurrentService.MainImagePath = dlg.SafeFileName;

                    CurrentService.ImagePath = image.UriSource.ToString();

                    image.EndInit();

                    try
                    {
                        _newImage = dlg.SafeFileName;
                        _newImagePath = dlg.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                stream.Dispose();
            }
        }

        #endregion
    }
}
