using ReactiveUI;
using System.ComponentModel.DataAnnotations;
using System.Reactive;

namespace HotelAppClient.ViewModels;
public class HotelViewModel : ViewModelBase
{
    private uint _id;
    public uint Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    private string _name = string.Empty;
    [Required]
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string _city = string.Empty;
    [Required]
    public string City
    {
        get => _city;
        set => this.RaiseAndSetIfChanged(ref _city, value);
    }

    private string _address = string.Empty;
    [Required]
    public string Address
    {
        get => _address;
        set => this.RaiseAndSetIfChanged(ref _address, value);
    }

    public ReactiveCommand<Unit, HotelViewModel> OnSubmitCommand { get; }

    public HotelViewModel()
    {
        OnSubmitCommand = ReactiveCommand.Create(() => this);
    }
}
