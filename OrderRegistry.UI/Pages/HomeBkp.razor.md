@page "/"
@using OrderRegistry.Domain.Entities
@using MudBlazor
@inject HttpClient Http

<MudContainer MaxWidth="MaxWidth.Medium" Class="mt-6">

    <MudText Typo="Typo.h3" Align="Align.Center" Class="mb-4">
        Order Registry
    </MudText>

    <!-- FORMULARIO DE CREACIÓN -->
    <MudPaper Class="pa-4 mb-6" Elevation="4">

        <MudText Typo="Typo.h5" Class="mb-3">
            Agregar nueva orden
        </MudText>

        <MudStack Spacing="2">

            <MudTextField @bind-Value="newOrder.Name"
                          Label="Nombre"
                          Variant="Variant.Filled" />

            <MudTextField @bind-Value="newOrder.Type"
                          Label="Tipo"
                          Variant="Variant.Filled" />

            <MudNumericField @bind-Value="newOrder.Quantity"
                             Label="Cantidad"
                             Variant="Variant.Filled" />

            <MudTextField @bind-Value="newOrder.Description"
                          Label="Descripción"
                          Variant="Variant.Filled" />

            <MudButton Color="Color.Primary"
                       Variant="Variant.Filled"
                       OnClick="CreateOrder"
                       StartIcon="@Icons.Material.Filled.Add">
                Agregar
            </MudButton>

        </MudStack>
    </MudPaper>

    <!-- BOTÓN ACTUALIZAR -->
    <MudButton Variant="Variant.Outlined" Color="Color.Secondary" OnClick="LoadOrders">
        Actualizar lista
    </MudButton>

    <MudDivider Class="my-4" />

    <!-- LISTADO ESTILIZADO -->
    @if (orders == null)
    {
        <MudText>Cargando...</MudText>
    }
    else if (!orders.Any())
    {
        <MudText>No hay órdenes registradas.</MudText>
    }
    else
    {
        <MudList T="Order" Dense="true" Class="pa-0">

            @foreach (var order in orders)
            {
                <MudListItem Class="mb-3">

                    <MudPaper Class="pa-3" Elevation="1" Style="background-color:#f9f9f9; border-radius:8px;">

                        <MudText Typo="Typo.h6" Class="mb-1">@order.Name</MudText>

                        <MudText Typo="Typo.body2" Class="mb-1">
                            <b>Tipo:</b> @order.Type | <b>Cantidad:</b> @order.Quantity
                        </MudText>

                        <MudText Typo="Typo.body2" Class="mb-2">
                            <b>Descripción:</b> @order.Description
                        </MudText>

                        <MudStack Direction="Row" Spacing="1">

                            <MudButton Variant="Variant.Outlined"
                                       Color="Color.Primary"
                                       Size="Size.Small"
                                       OnClick="() => StartEdit(order)">
                                Editar
                            </MudButton>

                            <MudButton Variant="Variant.Filled"
                                       Color="Color.Error"
                                       Size="Size.Small"
                                       OnClick="() => DeleteOrder(order.Id)">
                                Eliminar
                            </MudButton>

                        </MudStack>

                        <!-- EDICIÓN INLINE -->
                        @if (editingOrder != null && editingOrder.Id == order.Id)
                        {
                            <MudPaper Class="pa-3 mt-3" Elevation="2" Style="background-color:#eef5fb; border-radius:8px;">

                                <MudStack Spacing="2">

                                    <MudTextField Label="Nombre"
                                                  @bind-Value="editingOrder.Name"
                                                  Variant="Variant.Outlined" />

                                    <MudTextField Label="Tipo"
                                                  @bind-Value="editingOrder.Type"
                                                  Variant="Variant.Outlined" />

                                    <MudNumericField Label="Cantidad"
                                                     @bind-Value="editingOrder.Quantity"
                                                     Variant="Variant.Outlined" />

                                    <MudTextField Label="Descripción"
                                                  @bind-Value="editingOrder.Description"
                                                  Variant="Variant.Outlined" />

                                    <MudStack Direction="Row" Spacing="1">

                                        <MudButton Color="Color.Success"
                                                   Variant="Variant.Filled"
                                                   OnClick="UpdateOrder">
                                            Guardar
                                        </MudButton>

                                        <MudButton Variant="Variant.Outlined"
                                                   OnClick="CancelEdit">
                                            Cancelar
                                        </MudButton>
                                    </MudStack>

                                </MudStack>

                            </MudPaper>
                        }

                    </MudPaper>

                </MudListItem>
            }

        </MudList>
    }

</MudContainer>

@code {

    List<Order> orders = new();
    Order newOrder = new();
    Order? editingOrder = null;

    protected override async Task OnInitializedAsync()
    {
        await LoadOrders();
    }

    private async Task LoadOrders()
    {
        try
        {
            orders = await Http.GetFromJsonAsync<List<Order>>("http://localhost:5016/api/order")
                     ?? new List<Order>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            orders = new List<Order>();
        }
    }

    private async Task CreateOrder()
    {
        var response = await Http.PostAsJsonAsync("http://localhost:5016/api/order", newOrder);

        if (response.IsSuccessStatusCode)
        {
            newOrder = new Order();
            await LoadOrders();
        }
    }

    private void StartEdit(Order order)
    {
        editingOrder = new Order
        {
            Id = order.Id,
            Name = order.Name,
            Type = order.Type,
            Quantity = order.Quantity,
            Description = order.Description
        };
    }

    private void CancelEdit()
    {
        editingOrder = null;
    }

    private async Task UpdateOrder()
    {
        if (editingOrder == null) return;

        var response = await Http.PutAsJsonAsync(
            $"http://localhost:5016/api/order/{editingOrder.Id}",
            editingOrder
        );

        if (response.IsSuccessStatusCode)
        {
            editingOrder = null;
            await LoadOrders();
        }
    }

    private async Task DeleteOrder(int id)
    {
        var response = await Http.DeleteAsync($"http://localhost:5016/api/order/{id}");

        if (response.IsSuccessStatusCode)
        {
            await LoadOrders();
        }
    }
}
