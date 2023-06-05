using AutoMapper;
using ReactiveUI;
using Splat;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive;
using System.Reactive.Concurrency;

namespace HotelAppClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ApiWrapper _apiClient;
    private readonly IMapper _mapper;
    public ObservableCollection<ClientViewModel> Clients { get; } = new();
    public ObservableCollection<HotelViewModel> Hotels { get; } = new();
    public ObservableCollection<RoomViewModel> Rooms { get; } = new();
    public ObservableCollection<BookingViewModel> Bookings { get; } = new(); 

    private ClientViewModel? _selectedClient;
    public ClientViewModel? SelectedClient
    {
        get => _selectedClient;
        set => this.RaiseAndSetIfChanged(ref _selectedClient, value); 
    }

    private HotelViewModel? _selectedHotel;
    public HotelViewModel? SelectedHotel
    {
        get => _selectedHotel;
        set => this.RaiseAndSetIfChanged(ref _selectedHotel, value);
    }

    private RoomViewModel? _selectedRoom;
    public RoomViewModel? SelectedRoom
    {
        get => _selectedRoom;
        set => this.RaiseAndSetIfChanged(ref _selectedRoom, value);
    }

    private BookingViewModel? _selectedBooking;
    public BookingViewModel? SelectedBooking
    {
        get => _selectedBooking;
        set => this.RaiseAndSetIfChanged(ref _selectedBooking, value);
    }

    public ReactiveCommand<Unit, Unit> OnAddClientCommand { get; set; }
    public ReactiveCommand<Unit, Unit> OnChangeClientCommand { get; set; }
    public ReactiveCommand<Unit, Unit> OnDeleteClientCommand { get; set; }

    public ReactiveCommand<Unit, Unit> OnAddHotelCommand { get; set; }
    public ReactiveCommand<Unit, Unit> OnChangeHotelCommand { get; set; }
    public ReactiveCommand<Unit, Unit> OnDeleteHotelCommand { get; set; }

    public ReactiveCommand<Unit, Unit> OnAddRoomCommand { get; set; }
    public ReactiveCommand<Unit, Unit> OnChangeRoomCommand { get; set; }
    public ReactiveCommand<Unit, Unit> OnDeleteRoomCommand { get; set; }

    public ReactiveCommand<Unit, Unit> OnAddBookingCommand { get; set; }
    public ReactiveCommand<Unit, Unit> OnChangeBookingCommand { get; set; }
    public ReactiveCommand<Unit, Unit> OnDeleteBookingCommand { get; set; }

    public Interaction<ClientViewModel, ClientViewModel?> ShowClientDialog { get; }
    public Interaction<HotelViewModel, HotelViewModel?> ShowHotelDialog { get; }
    public Interaction<RoomViewModel, RoomViewModel?> ShowRoomDialog { get; }
    public Interaction<BookingViewModel, BookingViewModel?> ShowBookingDialog { get; }

    public MainWindowViewModel()
    {
        _apiClient = Locator.Current.GetService<ApiWrapper>();
        _mapper = Locator.Current.GetService<IMapper>();

        ShowClientDialog = new Interaction<ClientViewModel, ClientViewModel?>();
        ShowHotelDialog = new Interaction<HotelViewModel, HotelViewModel?>();
        ShowRoomDialog = new Interaction<RoomViewModel, RoomViewModel?>();
        ShowBookingDialog = new Interaction<BookingViewModel, BookingViewModel?>();

        OnAddClientCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var clientViewModel = await ShowClientDialog.Handle(new ClientViewModel());

            if (clientViewModel != null)
            {
                var newClient = _mapper.Map<ClientPostDto>(clientViewModel);

                await _apiClient.AddClientAsync(newClient);
                Clients.Add(clientViewModel);
                Clients.Clear();
                Hotels.Clear();
                Rooms.Clear();
                Bookings.Clear();
                LoadAsync();
            }
        });

        OnAddHotelCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var hotelViewModel = await ShowHotelDialog.Handle(new HotelViewModel());

            if (hotelViewModel != null)
            {
                var newHotel = _mapper.Map<HotelPostDto>(hotelViewModel);

                await _apiClient.AddHotelAsync(newHotel);
                Hotels.Add(hotelViewModel);
                Hotels.Clear();
                Clients.Clear();
                Rooms.Clear();
                Bookings.Clear();
                LoadAsync();
            }
        });

        OnAddRoomCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var roomViewModel = await ShowRoomDialog.Handle(new RoomViewModel());

            if (roomViewModel != null)
            {
                var newRoom = _mapper.Map<RoomPostDto>(roomViewModel);

                await _apiClient.AddRoomAsync(newRoom);
                Rooms.Add(roomViewModel);
                Rooms.Clear();
                Clients.Clear();
                Hotels.Clear();
                Bookings.Clear();
                LoadAsync();
            }
        });

        OnAddBookingCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var bookingViewModel = await ShowBookingDialog.Handle(new BookingViewModel());

            if (bookingViewModel != null)
            {
                var newBooking = _mapper.Map<BookingPostDto>(bookingViewModel);

                await _apiClient.AddBookingAsync(newBooking);
                Bookings.Add(bookingViewModel);
                Bookings.Clear();
                Clients.Clear();
                Hotels.Clear();
                Rooms.Clear();
                LoadAsync();
            }
        });

        OnChangeClientCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var clientViewModel = await ShowClientDialog.Handle(SelectedClient!);

            if (clientViewModel != null)
            {
                var newClient = _mapper.Map<ClientPostDto>(clientViewModel);

                await _apiClient.UpdateClientAsync((int)SelectedClient!.Id, _mapper.Map<ClientPostDto>(newClient));
                _mapper.Map(clientViewModel, SelectedClient);
            }
        }, this.WhenAnyValue(vm => vm.SelectedClient).Select(SelectedClient => SelectedClient != null));

        OnChangeHotelCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var hotelViewModel = await ShowHotelDialog.Handle(SelectedHotel!);

            if (hotelViewModel != null)
            {
                var newHotel = _mapper.Map<HotelPostDto>(hotelViewModel);

                await _apiClient.UpdateHotelAsync((int)SelectedHotel!.Id, _mapper.Map<HotelPostDto>(newHotel));
                _mapper.Map(hotelViewModel, SelectedHotel);
            }
        }, this.WhenAnyValue(vm => vm.SelectedHotel).Select(SelectedHotel => SelectedHotel != null));

        OnChangeRoomCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var roomViewModel = await ShowRoomDialog.Handle(SelectedRoom!);

            if (roomViewModel != null)
            {
                var newRoom = _mapper.Map<RoomPostDto>(roomViewModel);

                await _apiClient.UpdateRoomAsync((int)SelectedRoom!.Id, _mapper.Map<RoomPostDto>(newRoom));
                _mapper.Map(roomViewModel, SelectedRoom);
            }
        }, this.WhenAnyValue(vm => vm.SelectedRoom).Select(SelectedRoom => SelectedRoom != null));

        OnChangeBookingCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var bookingViewModel = await ShowBookingDialog.Handle(SelectedBooking!);

            if (bookingViewModel != null)
            {
                var newBooking = _mapper.Map<BookingPostDto>(bookingViewModel);

                await _apiClient.UpdateBookingAsync((int)SelectedBooking!.Id, _mapper.Map<BookingPostDto>(newBooking));
                _mapper.Map(bookingViewModel, SelectedBooking);
            }
        }, this.WhenAnyValue(vm => vm.SelectedBooking).Select(SelectedBooking => SelectedBooking != null));

        OnDeleteClientCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _apiClient.DeleteClientAsync((int)SelectedClient!.Id);
            Clients.Remove(SelectedClient);
        }, this.WhenAnyValue(vm => vm.SelectedClient).Select(selectedClient => selectedClient != null));

        OnDeleteHotelCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _apiClient.DeleteHotelAsync((int)SelectedHotel!.Id);
            Hotels.Remove(SelectedHotel);
        }, this.WhenAnyValue(vm => vm.SelectedHotel).Select(selectedHotel=> selectedHotel != null));

        OnDeleteRoomCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _apiClient.DeleteRoomAsync((int)SelectedRoom!.Id);
            Rooms.Remove(SelectedRoom);
        }, this.WhenAnyValue(vm => vm.SelectedRoom).Select(selectedRoom => selectedRoom != null));

        OnDeleteBookingCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _apiClient.DeleteBookingAsync((int)SelectedBooking!.Id);
            Bookings.Remove(SelectedBooking);
        }, this.WhenAnyValue(vm => vm.SelectedBooking).Select(selectedBooking => selectedBooking != null));

        RxApp.MainThreadScheduler.Schedule(LoadAsync);
    }

    private async void LoadAsync()
    {
        var clients = await _apiClient.GetClientsAsync();
        foreach (var client in clients)
        {
            Clients.Add(_mapper.Map<ClientViewModel>(client));
        }

        var hotels = await _apiClient.GetHotelsAsync(); 
        foreach (var hotel in hotels)
        {
            Hotels.Add(_mapper.Map<HotelViewModel>(hotel));
        }

        var rooms = await _apiClient.GetRoomsAsync();
        foreach (var room in rooms)
        {
            Rooms.Add(_mapper.Map<RoomViewModel>(room)); 
        }

        var bookings = await _apiClient.GetBookingsAsync();
        foreach (var booking in bookings)
        {
            Bookings.Add(_mapper.Map<BookingViewModel>(booking));
        }
    }
}   
