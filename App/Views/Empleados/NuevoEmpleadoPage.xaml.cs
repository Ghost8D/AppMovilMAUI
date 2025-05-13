using Firebase.Database;
using Firebase.Database.Query;
using App.Models;
using System.Linq;

namespace App.Views;

[QueryProperty(nameof(Detalle), "Detalle")]
public partial class NuevoEmpleadoPage : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://crud-29968-default-rtdb.firebaseio.com/");
    public List<Cargo> Cargos { get; set; } = new List<Cargo>();
    public Empleado Detalle { get; set; }

    public NuevoEmpleadoPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    // Método para cargar los cargos desde Firebase de manera asincrónica
    private async void CargarCargos()
    {
        var cargos = await client.Child("Cargos").OnceAsync<Cargo>();
        Cargos = cargos.Select(x => x.Object).ToList();

        // Asignar los cargos al Picker
        cargoPicker.ItemsSource = Cargos;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Cargar los cargos cuando la página esté visible
        CargarCargos();

        // Si estamos editando un empleado, cargar sus datos en los controles
        if (Detalle != null)
        {
            nombreEntry.Text = Detalle.NombreCompleto;
            fechaInicioPicker.Date = Detalle.FechaInicio;
            salarioEntry.Text = Detalle.Salario.ToString();
            cargoPicker.SelectedItem = Detalle.Cargo;
        }
    }

    // Este método se ejecuta cuando se presiona el botón Guardar
    private async void guardarButton_Clicked(object sender, EventArgs e)
    {
        Cargo cargo = cargoPicker.SelectedItem as Cargo;

        // Si estamos editando un empleado (Detalle no es nulo)
        if (Detalle != null && !string.IsNullOrEmpty(Detalle.ID))
        {
            Detalle.NombreCompleto = nombreEntry.Text;
            Detalle.FechaInicio = fechaInicioPicker.Date;
            Detalle.Salario = double.Parse(salarioEntry.Text);
            Detalle.Cargo = cargo;

            // Actualizar el empleado en Firebase
            await client
                .Child("Empleados")
                .Child(Detalle.ID)
                .PutAsync(Detalle);

            await DisplayAlert("Éxito", "Empleado modificado correctamente", "OK");
        }
        else // Si estamos creando un nuevo empleado
        {
            // Crear un nuevo empleado
            await client.Child("Empleados").PostAsync(new Empleado
            {
                ID = Guid.NewGuid().ToString(), // Crear un nuevo ID
                NombreCompleto = nombreEntry.Text,
                FechaInicio = fechaInicioPicker.Date,
                Salario = double.Parse(salarioEntry.Text),
                Cargo = cargo
            });

            await DisplayAlert("Éxito", "Empleado creado correctamente", "OK");
        }

        // Navegar hacia atrás después de guardar
        await Shell.Current.GoToAsync("..");
    }
}
