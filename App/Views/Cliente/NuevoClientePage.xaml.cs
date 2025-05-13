using App.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace App.Views;

[QueryProperty(nameof(Detalle), "Detalle")]
public partial class NuevoClientePage : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://crud-29968-default-rtdb.firebaseio.com/");
    public List<TipoCliente> Tipos { get; set; } = new List<TipoCliente>();
    public Clientes Detalle { get; set; }

    public NuevoClientePage()
	{
		InitializeComponent();
        BindingContext = this;
    }

    // M�todo para cargar los cargos desde Firebase de manera asincr�nica
    private async void CargarCargos()
    {
        var cargos = await client.Child("Tipos").OnceAsync<TipoCliente>();
        Tipos = cargos.Select(x => x.Object).ToList();

        // Asignar los cargos al Picker
        TipoClientePicker.ItemsSource = Tipos;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Cargar los cargos cuando la p�gina est� visible
        CargarCargos();

        // Si estamos editando un empleado, cargar sus datos en los controles
        if (Detalle != null)
        {
            nombreEntry.Text = Detalle.NombreCompleto;
            fechaIngresoPicker.Date = Detalle.FechaDeIngreso;
            TipoClientePicker.SelectedItem = Detalle.TipoCliente;
        }
    }
    // Este m�todo se ejecuta cuando se presiona el bot�n Guardar
    private async void guardarButton_Clicked(object sender, EventArgs e)
    {
        TipoCliente Tipo = TipoClientePicker.SelectedItem as TipoCliente;

        // Si estamos editando un empleado (Detalle no es nulo)
        if (Detalle != null && !string.IsNullOrEmpty(Detalle.ID))
        {
            Detalle.NombreCompleto = nombreEntry.Text;
            Detalle.FechaDeIngreso = fechaIngresoPicker.Date;
            Detalle.TipoCliente = Tipo;

            // Actualizar el empleado en Firebase
            await client
                .Child("Clientes")
                .Child(Detalle.ID)
                .PutAsync(Detalle);

            await DisplayAlert("�xito", "Cliente modificado correctamente", "OK");
        }
        else // Si estamos creando un nuevo empleado
        {
            // Crear un nuevo empleado
            await client.Child("Clientes").PostAsync(new Clientes
            {
                ID = Guid.NewGuid().ToString(), // Crear un nuevo ID
                NombreCompleto = nombreEntry.Text,
                FechaDeIngreso = fechaIngresoPicker.Date,
                TipoCliente = Tipo
            });

            await DisplayAlert("�xito", "Cliente creado correctamente", "OK");
        }

        // Navegar hacia atr�s despu�s de guardar
        await Shell.Current.GoToAsync("..");
    }
}