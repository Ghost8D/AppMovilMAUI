using App.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace App.Views.Cliente;

[QueryProperty(nameof(Detalle), "Detalle")]
public partial class VerClientePage : ContentPage
{
    private FirebaseClient client = new FirebaseClient("https://crud-29968-default-rtdb.firebaseio.com/");

    Clientes detalle;

    public Clientes Detalle
    {
        get => detalle;
        set
        {
            detalle = value;
            OnPropertyChanged();
        }
    }

    public VerClientePage()
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
            await Shell.Current.GoToAsync(nameof(NuevoClientePage), parametros);
        }
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Eliminar", "¿Deseas eliminar este Cliente?", "Sí", "No");
        if (confirm && !string.IsNullOrEmpty(Detalle.ID))
        {
            await client
                .Child("Clientes")
                .Child(Detalle.ID)
                .DeleteAsync();

            await DisplayAlert("Éxito", "Cliente eliminado correctamente", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}