﻿using AutoSerivce.Interfaces;
using AutoSerivce.Interfaces.Implementations;
using AutoSerivce.ViewModels;
using AutoSerivce.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace AutoSerivce
{
    public partial class App : Application
    {
        private static IServiceProvider? _services; /* DI сервис или провайдер сервисов*/

        public static IServiceProvider Services => _services ??= InitializeServices().BuildServiceProvider();

        private static IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<AddEditServiceWindowViewModel>();
            services.AddTransient<AdminWindowViewModel>();
            services.AddSingleton<ClientServiceWindowViewModel>();
            //services.AddSingleton<ServiceClientEntryWindowViewModel>();
            services.AddScoped<ServiceClientEntryWindowViewModel>();

            services.AddSingleton<IUserDialog, UserDialogService>();

            services.AddTransient(
              s =>
              {
                  var model = s.GetRequiredService<MainWindowViewModel>();
                  var window = new MainWindow { DataContext = model, };

                  model.DialogComplete += (_, _) => window.Close();

                  return window;
              });

            services.AddTransient(
               s =>
               {
                   //var scope = s.CreateScope();
                   var model = s/*cope.ServiceProvider*/.GetRequiredService<AddEditServiceWindowViewModel>();
                   var window = new AddEditServiceWindow { DataContext = model };

                   model.DialogComplete += (_, _) => window.Close();
                   //window.Closed += (_, _) => scope.Dispose();


                   return window;
               });

            services.AddTransient(
                s =>
                {
                    //var scope = s.CreateScope();
                    var model = s/*cope.ServiceProvider*/.GetRequiredService<AdminWindowViewModel>();
                    var window = new AdminWindow { DataContext = model };

                    model.DialogComplete += (_, _) => window.Close();
                    //window.Closed += (_, _) => scope.Dispose();

                    return window;
                });

            services.AddTransient(
                s =>
                {
                    var model = s.GetRequiredService<ClientServiceWindowViewModel>();
                    var window = new ClientServiceWindow { DataContext = model };

                    model.DialogComplete += (_, _) => window.Close();

                    return window;
                });

            services.AddTransient(
                s =>
                {
                    //var scope = s.CreateScope();
                    var model = s/*cope.ServiceProvider*/.GetRequiredService<ServiceClientEntryWindowViewModel>();
                    var window = new ServiceClientEntryWindow { DataContext = model };

                    model.DialogComplete += (_, _) => window.Close();
                    //window.Closed += (_, _) => scope.Dispose();

                    return window;
                });

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Региональные параметры(дата)
            var cultureInfo = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);

            //Services.GetRequiredService<IUserDialog>().OpenMainWindow();
            Services.GetRequiredService<IUserDialog>().OpenClientServiceWindow();
            //Services.GetRequiredService<IUserDialog>().OpenServiceClientEntryWindow();

        }
    }
}
