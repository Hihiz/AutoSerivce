using AutoSerivce.Commands;
using AutoSerivce.Interfaces;
using AutoSerivce.Models;
using AutoSerivce.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AutoSerivce.ViewModels
{
    public class ServiceClientEntryWindowViewModel : DialogViewModel
    {
        private readonly IUserDialog _userDialog = null;

        private string _oldImage;
        private string _newImage;
        private string _newImagePath;

        public ServiceClientEntryWindowViewModel()
        {
            CurrentClientService = new Client();

            using (AutoServiceContext db = new AutoServiceContext())
            {
                CurrentClientServices = db.Clients.ToList();
                GenderName = db.Genders.ToList();
            }

            AddClientCommand = new LambdaCommand(OnAddClientCommandExecuted, CanAddClientCommandExecute);
            AddImageClientCommand = new LambdaCommand(OnAddImageClientCommandExecuted, CanAddImageClientCommandExecute);
        }

        public ServiceClientEntryWindowViewModel(IUserDialog userDialog) : this()
        {
            _userDialog = userDialog;
        }

        #region Свойства

        private IEnumerable<Client> _currentClientServices;
        public IEnumerable<Client> CurrentClientServices { get => _currentClientServices; set => Set(ref _currentClientServices, value); }

        private Client _currentClientService;
        public Client CurrentClientService { get => _currentClientService; set => Set(ref _currentClientService, value); }

        private string _title = "Запись клиента на услугу";
        public string Title { get => _title; set => Set(ref _title, value); }

        private List<Gender> _genderName;
        public List<Gender> GenderName { get => _genderName; set => Set(ref _genderName, value); }

        #endregion

        #region Команды

        public ICommand AddClientCommand { get; set; }
        private bool CanAddClientCommandExecute(object p)
        {
            if (string.IsNullOrWhiteSpace(CurrentClientService.LastName))
                return false;

            if (string.IsNullOrWhiteSpace(CurrentClientService.FirsName))
                return false;

            if (string.IsNullOrWhiteSpace(CurrentClientService.Phone))
                return false;

            if (CurrentClientService.GenderCode == null)
                return false;

            return true;
        }
        private void OnAddClientCommandExecuted(object p)
        {
            using (AutoServiceContext db = new AutoServiceContext())
            {
                if (CurrentClientService.Id == 0)
                {
                    Client client = new Client()
                    {
                        LastName = CurrentClientService.LastName,
                        FirsName = CurrentClientService.FirsName,
                        Patronymic = CurrentClientService.Patronymic,
                        Birthday = CurrentClientService.Birthday,
                        RegistrationDate = DateTime.Now/*CurrentClientService.RegistrationDate*/,
                        Email = CurrentClientService.Email,
                        Phone = CurrentClientService.Phone,
                        GenderCodeId = CurrentClientService.GenderCode.Id, // FK
                        PhotoPath = CurrentClientService.PhotoPath,
                    };

                    if (string.IsNullOrEmpty(_newImage))
                    {
                        CurrentClientService.PhotoPath = "picture.png";
                        BitmapImage image = new BitmapImage(new Uri(CurrentClientService.ImagePath));
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        CurrentClientService.ImagePath = image.UriSource.ToString();

                    }
                    else
                    {
                        string newRelativePath = $"{System.DateTime.Now.ToString("HHmmss")}_{_newImage}";
                        client.PhotoPath = newRelativePath;

                        File.Copy(_newImagePath, System.IO.Path.Combine(Environment.CurrentDirectory, $"Фото клиентов/{newRelativePath}"));

                        BitmapImage image = new BitmapImage(new Uri(CurrentClientService.ImagePath));
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        CurrentClientService.ImagePath = image.UriSource.ToString();
                    }

                    try
                    {
                        db.Clients.Add(client);
                        db.SaveChanges();
                        MessageBox.Show($"Клиент {CurrentClientService.FirsName} {CurrentClientService.LastName} {CurrentClientService.Patronymic} зарегистрирован", "Успешно");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                }
                else
                {
                    if (_newImage != null)
                    {
                        string newRelativePath = $"{System.DateTime.Now.ToString("HHmmss")}_{_newImage}";
                        CurrentClientService.PhotoPath = newRelativePath;
                        MessageBox.Show($"Новое фото: {CurrentClientService.PhotoPath} присвоено!");
                        File.Copy(_newImagePath, System.IO.Path.Combine(Environment.CurrentDirectory, $"Фото клиентов/{CurrentClientService.PhotoPath}"));
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
                        db.Clients.Update(CurrentClientService);
                        db.SaveChanges();
                        MessageBox.Show("Данные клиента обновлены", "Успешно");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public ICommand AddImageClientCommand { get; set; }
        private bool CanAddImageClientCommandExecute(object p) => true;
        private void OnAddImageClientCommandExecuted(object p)
        {
            Stream stream;

            if (CurrentClientService.Id != null)
                _oldImage = System.IO.Path.Combine(Environment.CurrentDirectory, $"Услуги автосервиса/{CurrentClientService.PhotoPath}");
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

                    CurrentClientService.PhotoPath = dlg.SafeFileName;

                    CurrentClientService.ImagePath = image.UriSource.ToString();

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
