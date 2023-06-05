using ReactiveUI;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive;

namespace HotelAppClient.ViewModels;
public class BookingViewModel : ViewModelBase
{
    private uint _id;
    public uint Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    private uint _roomId;
    [Required]
    public uint RoomId
    {
        get => _roomId;
        set => this.RaiseAndSetIfChanged(ref _roomId, value);
    }
    public uint _clientId;
    [Required]
    public uint ClientId
    {
        get => _clientId;
        set => this.RaiseAndSetIfChanged(ref _clientId, value);
    }

    private DateTimeOffset _checkInDate = DateTimeOffset.Now;
    [Required]
    public DateTimeOffset CheckInDate
    {
        get => _checkInDate;
        set => this.RaiseAndSetIfChanged(ref _checkInDate, value);
    }

    private uint _bookingPeriodInDays;
    [Required]
    public uint BookingPeriodInDays
    {
        get => _bookingPeriodInDays;
        set => this.RaiseAndSetIfChanged(ref _bookingPeriodInDays, value);
    }
    
    public DateTimeOffset DepartureDate { get => CheckInDate.AddDays(BookingPeriodInDays); }

    public ReactiveCommand<Unit, BookingViewModel> OnSubmitCommand { get; }

    public BookingViewModel()
    {
        OnSubmitCommand = ReactiveCommand.Create(() => this);
    }
}
