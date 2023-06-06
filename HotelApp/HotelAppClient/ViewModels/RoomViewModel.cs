using ReactiveUI;
using System.ComponentModel.DataAnnotations;
using System.Reactive;

namespace HotelAppClient.ViewModels;
public class RoomViewModel : ViewModelBase
{
    private uint _id;
    public uint Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    private uint _hotelId;
    [Required]
    public uint HotelId
    {
        get => _hotelId;
        set => this.RaiseAndSetIfChanged(ref _hotelId, value);
    }

    private string _type = null!;
    [Required]
    public string Type
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }

    private uint _amount;
    [Required]
    public uint Amount
    {
        get => _amount;
        set => this.RaiseAndSetIfChanged(ref _amount, value);
    }

    private uint _cost;
    [Required]
    public uint Cost
    {
        get => _cost;
        set => this.RaiseAndSetIfChanged(ref _cost, value);
    }

    public ReactiveCommand<Unit, RoomViewModel> OnSubmitCommand { get; }

    public RoomViewModel()
    {
        OnSubmitCommand = ReactiveCommand.Create(() => this);
    }
}
