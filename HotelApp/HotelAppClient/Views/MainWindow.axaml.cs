using Avalonia.Controls;
using Avalonia.ReactiveUI;
using HotelAppClient.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace HotelAppClient.Views;
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(d => d(ViewModel!.ShowClientDialog.RegisterHandler(ShowClientDialogAsync)));
        this.WhenActivated(d => d(ViewModel!.ShowHotelDialog.RegisterHandler(ShowHotelDialogAsync)));
        this.WhenActivated(d => d(ViewModel!.ShowRoomDialog.RegisterHandler(ShowRoomDialogAsync)));
        this.WhenActivated(d => d(ViewModel!.ShowBookingDialog.RegisterHandler(ShowBookingDialogAsync)));
    }

    private async Task ShowClientDialogAsync(InteractionContext<ClientViewModel, ClientViewModel?> interaction)
    {
        var dialog = new ClientWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<ClientViewModel?>(this);

        interaction.SetOutput(result);
    }

    private async Task ShowHotelDialogAsync(InteractionContext<HotelViewModel, HotelViewModel?> interaction)
    {
        var dialog = new HotelWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<HotelViewModel?>(this);

        interaction.SetOutput(result);
    }

    private async Task ShowRoomDialogAsync(InteractionContext<RoomViewModel, RoomViewModel?> interaction)
    {
        var dialog = new RoomWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<RoomViewModel?>(this);

        interaction.SetOutput(result);
    }

    private async Task ShowBookingDialogAsync(InteractionContext<BookingViewModel, BookingViewModel?> interaction)
    {
        var dialog = new BookingWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<BookingViewModel?>(this);

        interaction.SetOutput(result);
    }

}