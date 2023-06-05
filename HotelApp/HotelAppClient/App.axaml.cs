using AutoMapper;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HotelAppClient.ViewModels;
using HotelAppClient.Views;
using Splat;

namespace HotelAppClient;
public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientGetDto, ClientViewModel>();
                cfg.CreateMap<ClientViewModel, ClientGetDto>();

                cfg.CreateMap<ClientPostDto, ClientViewModel>();
                cfg.CreateMap<ClientViewModel, ClientPostDto>();
            });

            Locator.CurrentMutable.RegisterConstant(new ApiWrapper());
            Locator.CurrentMutable.RegisterConstant(config.CreateMapper(), typeof(IMapper));

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}