using AutoMapper;
using ReactiveUI;
using Splat;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace HotelAppClient.ViewModels;
internal class ClientsWindowViewModel : ViewModelBase
{
    private readonly ApiWrapper _apiClient;
    private readonly IMapper _mapper;
    public ObservableCollection<ClientViewModel> Clients { get; } = new();

    private ClientViewModel? _selectedClient;
    public ClientViewModel? SelectedClient
    {
        get => _selectedClient;
        set => this.RaiseAndSetIfChanged(ref _selectedClient, value);
    }

    public ReactiveCommand<Unit, Unit> OnAddCommand { get; set; }
    public ReactiveCommand<Unit, Unit> OnChangeCommand { get; set; }
    public ReactiveCommand<Unit, Unit> OnDeleteCommand { get; set; }

    public Interaction<ClientViewModel, ClientViewModel?> ShowClientDialog { get; }

    public ClientsWindowViewModel()
    {
        _apiClient = Locator.Current.GetService<ApiWrapper>();
        _mapper = Locator.Current.GetService<IMapper>();

        ShowClientDialog = new Interaction<ClientViewModel, ClientViewModel?>();

        OnAddCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var clientViewModel = await ShowClientDialog.Handle(new ClientViewModel());

            if (clientViewModel != null)
            {
                var newClient = _mapper.Map<ClientPostDto>(clientViewModel);

                await _apiClient.AddClientAsync(newClient);
                Clients.Add(clientViewModel);
                Clients.Clear();
                LoadClientsAsync();
            }
        });

        OnChangeCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var clientViewModel = await ShowClientDialog.Handle(SelectedClient!);

            if (clientViewModel != null)
            {
                var newClient = _mapper.Map<ClientPostDto>(clientViewModel);

                await _apiClient.UpdateClientAsync((int)SelectedClient!.Id, _mapper.Map<ClientPostDto>(newClient));
                _mapper.Map(clientViewModel, SelectedClient);
            }
        }, this.WhenAnyValue(vm => vm.SelectedClient).Select(SelectedClient => SelectedClient != null));

        OnDeleteCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _apiClient.DeleteClientAsync((int)SelectedClient!.Id);
            Clients.Remove(SelectedClient);
        }, this.WhenAnyValue(vm => vm.SelectedClient).Select(selectedCustomer => selectedCustomer != null));

        RxApp.MainThreadScheduler.Schedule(LoadClientsAsync);
    }

    private async void LoadClientsAsync()
    {
        var clients = await _apiClient.GetClientsAsync();
        foreach (var client in clients)
        {
            Clients.Add(_mapper.Map<ClientViewModel>(client));
        }
    }
}
