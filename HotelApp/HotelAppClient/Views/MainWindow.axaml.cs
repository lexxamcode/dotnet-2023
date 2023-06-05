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

        this.WhenActivated(d => d(ViewModel!.ShowClientDialog.RegisterHandler(ShowDialogAsync)));
    }

    private async Task ShowDialogAsync(InteractionContext<ClientViewModel, ClientViewModel?> interaction)
    {
        var dialog = new ClientWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<ClientViewModel?>(this);

        interaction.SetOutput(result);
    }
}