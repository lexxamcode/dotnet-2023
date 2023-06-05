using ReactiveUI;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive;

namespace HotelAppClient.ViewModels;
public class ClientViewModel : ViewModelBase
{
    private uint _id;
    public uint Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    private string _firstName = string.Empty;
    [Required]
    public string FirstName
    {
        get => _firstName;
        set => this.RaiseAndSetIfChanged(ref _firstName, value);
    }

    private string _lastName = string.Empty;
    [Required]
    public string LastName
    {
        get => _lastName;
        set => this.RaiseAndSetIfChanged(ref _lastName, value);
    }

    private string _surname = null!;
    public string Surname
    {
        get => _surname;
        set => this.RaiseAndSetIfChanged(ref _surname, value);
    }

    private string _passport = string.Empty;
    [Required]
    public string Passport
    {
        get => _passport;
        set => this.RaiseAndSetIfChanged(ref _passport, value);
    }

    private DateTimeOffset _birthDate = DateTimeOffset.Now;
    [Required]
    public DateTimeOffset BirthDate 
    {
        get => _birthDate;
        set => this.RaiseAndSetIfChanged(ref _birthDate, value);
    }

    public ReactiveCommand<Unit, ClientViewModel> OnSubmitCommand { get; }

    public ClientViewModel()
    {
        OnSubmitCommand = ReactiveCommand.Create(() => this);
    }
}
