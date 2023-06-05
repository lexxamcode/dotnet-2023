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
                cfg.CreateMap<ClientGetDto, ClientViewModel>().ReverseMap();
                cfg.CreateMap<ClientPostDto, ClientViewModel>().ReverseMap();

                cfg.CreateMap<HotelGetDto, HotelViewModel>().ReverseMap();
                cfg.CreateMap<HotelPostDto, HotelViewModel>().ReverseMap();

                cfg.CreateMap<RoomGetDto, RoomViewModel>().ReverseMap();
                cfg.CreateMap<RoomPostDto, RoomViewModel>().ReverseMap();

                cfg.CreateMap<BookingGetDto, BookingViewModel>().ReverseMap();
                cfg.CreateMap<BookingPostDto, BookingViewModel>().ReverseMap();
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