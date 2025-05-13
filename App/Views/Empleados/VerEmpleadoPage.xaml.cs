using App.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace App.Views;

[QueryProperty(nameof(Detalle), "Detalle")]
public partial class VerEmpleadoPage : ContentPage
{
    private FirebaseClient client = new FirebaseClient("https://crud-29968-default-rtdb.firebaseio.com/");

    Empleado detalle;
	public Empleado Detalle
	{
		get => detalle;
		set
		{
			detalle = value;
			OnPropertyChanged();
		}

    }

	public VerEmpleadoPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        if (Detalle != null)
        {
            var parametros = new Dictionary<string, object>
        {
            { "Detalle", Detalle }
        };
            await Shell.Current.GoToAsync(nameof(NuevoEmpleadoPage), parametros);
        }
    }


    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Eliminar", "¿Deseas eliminar este empleado?", "Sí", "No");
        if (confirm && !string.IsNullOrEmpty(Detalle.ID))
        {
            await client
                .Child("Empleados")
                .Child(Detalle.ID)
                .DeleteAsync();

            await DisplayAlert("Éxito", "Empleado eliminado correctamente", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}