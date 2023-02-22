using AutoSerivce.Commands;
using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AutoSerivce.ViewModels
{
    public class AddEditServiceWindowViewModel : DialogViewModel
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

        private string _title;
        public string Title { get => _title; set => Set(ref _title, value); }

        private Service _currentService;
        public Service CurrentService { get => _currentService; set => Set(ref _currentService, value); }

        private IEnumerable<Service> _currentServices;
        public IEnumerable<Service> CurrentServices { get => _currentServices; set => Set(ref _currentServices, value); }

        private Visibility _idVisibility = Visibility.Collapsed;
        public Visibility IdVisibility { get => _idVisibility; set => Set(ref _idVisibility, value); }

        #endregion

        #region Команды

        public ICommand SaveServiceCommand { get; set; }
        private bool CanSaveServiceCommandExecute(object p)
        {
            if (string.IsNullOrWhiteSpace(CurrentService.Title))
                return false;

            if (string.IsNullOrWhiteSpace(CurrentService.Cost.ToString())) //
                return false;

            if (string.IsNullOrWhiteSpace(CurrentService.DurationWork))
                return false;

            return true;
        }
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

                    //db.Services.Add(service);

                    if (string.IsNullOrEmpty(_newImage))
                    {
                        CurrentService.MainImagePath = "picture.jpg";
                        BitmapImage image = new BitmapImage(new Uri(CurrentService.ImagePath));
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        CurrentService.ImagePath = image.UriSource.ToString();

                    }
                    else
                    {
                        string newRelativePath = $"{System.DateTime.Now.ToString("HHmmss")}_{_newImage}";
                        service.MainImagePath = newRelativePath;

                        File.Copy(_newImagePath, System.IO.Path.Combine(Environment.CurrentDirectory, $"Услуги автосервиса/{newRelativePath}"));

                        BitmapImage image = new BitmapImage(new Uri(CurrentService.ImagePath));
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        CurrentService.ImagePath = image.UriSource.ToString();
                    }

                    try
                    {
                        db.Services.Add(service);
                        db.SaveChanges();
                        MessageBox.Show("Услуга успешно добавлена");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Услуга '{CurrentService.Title}' существует, введите новое название", "Ошибка ! Название уже существует !");
                    }
                }
                else
                {
                    if (_newImage != null)
                    {
                        string newRelativePath = $"{System.DateTime.Now.ToString("HHmmss")}_{_newImage}";
                        CurrentService.MainImagePath = newRelativePath;
                        MessageBox.Show($"Новое фото: {CurrentService.MainImagePath} присвоено!");
                        File.Copy(_newImagePath, System.IO.Path.Combine(Environment.CurrentDirectory, $"Услуги автосервиса/{CurrentService.MainImagePath}"));
                        _newImage = null;
                    }

                    if (!string.IsNullOrEmpty(_oldImage))
                    {
                        try
                        {
                            File.Delete(_oldImage);
                            MessageBox.Show($"Старое фото: {_oldImage} удалено!");
                            _oldImage = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }

                    try
                    {
                        db.Services.Update(CurrentService);
                        db.SaveChanges();
                        MessageBox.Show("Услуга успешна обновлена");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Услуга '{CurrentService.Title}' существует, введите новое название", "Ошибка ! Название уже существует !");
                    }
                }

                //try
                //{

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Услуга не сохранена, заполните данные и повторите попытку", "Ошибка");
                //}

                CurrentServices = db.Services.ToList();
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
