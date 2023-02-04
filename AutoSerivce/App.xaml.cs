﻿using AutoSerivce.Interfaces;
using AutoSerivce.Interfaces.Implementations;
using AutoSerivce.ViewModels;
using AutoSerivce.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;

namespace AutoSerivce
{
    public partial class App : Application
    {
        private static IServiceProvider? _services;

        public static IServiceProvider Services => _services ??= InitializeServices().BuildServiceProvider();

        private static IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainWindowViewModel>();
            services.AddScoped<AddEditServiceWindowViewModel>();
            services.AddScoped<AdminWindowViewModel>();

            services.AddSingleton<IUserDialog, UserDialogService>();

            services.AddSingleton(
              s =>
              {
                  var model = s.GetRequiredService<MainWindowViewModel>();
                  var window = new MainWindow { DataContext = model };

                  return window;
              });

            services.AddTransient(
               s =>
               {
                   var scope = s.CreateScope();
                   var model = scope.ServiceProvider.GetRequiredService<AddEditServiceWindowViewModel>();
                   var window = new AddEditServiceWindow { DataContext = model };

                   return window;
               });

            services.AddTransient(
                s =>
                {
                    var scope = s.CreateScope();
                    var model = scope.ServiceProvider.GetRequiredService<AdminWindowViewModel>();
                    var window = new AdminWindow { DataContext = model };

                    return window;
                });

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Services.GetRequiredService<IUserDialog>().OpenMainWindow();
        }
    }
}
