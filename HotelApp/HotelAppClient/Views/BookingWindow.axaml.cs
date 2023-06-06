using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using HotelAppClient.ViewModels;
using ReactiveUI;
using System;

namespace HotelAppClient.Views;
public partial class BookingWindow : ReactiveWindow<BookingViewModel>
{
    public BookingWindow()
    {
        InitializeComponent();

        this.WhenActivated(d => d(ViewModel!.OnSubmitCommand.Subscribe(Close)));
    }

    public void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
