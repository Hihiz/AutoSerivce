using AutoSerivce.Interfaces;
using AutoSerivce.Interfaces.Implementations;
using AutoSerivce.ViewModels;
using AutoSerivce.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            services.AddSingleton<AddEditServiceWindowViewModel>();
            services.AddTransient<AdminWindowViewModel>();
            services.AddSingleton<ClientServiceWindowViewModel>();
            services.AddSingleton<ServiceClientEntryWindowViewModel>();

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
                    var model = s.GetRequiredService<ServiceClientEntryWindowViewModel>();
                    var window = new ServiceClientEntryWindow { DataContext = model };

                    model.DialogComplete += (_, _) => window.Close();

                    return window;
                });

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Services.GetRequiredService<IUserDialog>().OpenMainWindow();
            //Services.GetRequiredService<IUserDialog>().OpenClientServiceWindow();
            Services.GetRequiredService<IUserDialog>().OpenServiceClientEntryWindow();

        }
    }
}
