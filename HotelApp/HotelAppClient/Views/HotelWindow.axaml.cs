using Avalonia.ReactiveUI;
using Avalonia.Interactivity;
using HotelAppClient.ViewModels;
using System;
using ReactiveUI;

namespace HotelAppClient.Views;
public partial class HotelWindow : ReactiveWindow<HotelViewModel>
{
    public HotelWindow()
    {
        InitializeComponent();

        this.WhenActivated(d => d(ViewModel!.OnSubmitCommand.Subscribe(Close)));
    }

    public void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
