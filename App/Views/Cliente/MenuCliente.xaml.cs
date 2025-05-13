using App.Models;
using Firebase.Database;
using System.Collections.ObjectModel;

namespace App.Views.Cliente;

public partial class MenuCliente : ContentPage
{

    FirebaseClient client = new FirebaseClient("https://crud-29968-default-rtdb.firebaseio.com/");
    public ObservableCollection<Clientes> Lista { get; set; } = new ObservableCollection<Clientes>();

    public MenuCliente()
	{
		InitializeComponent();
        BindingContext = this;
    }

    public void CargarLista()
    {
        client.Child("Clientes")
              .OnceAsync<Clientes>()
              .ContinueWith(task =>
              {
                  var clientesFirebase = task.Result;

                  MainThread.BeginInvokeOnMainThread(() =>
                  {
                      Lista.Clear();
                      foreach (var cli in clientesFirebase)
                      {
                          cli.Object.ID = cli.Key;
                          Lista.Add(cli.Object);
                      }
                      listaCollection.ItemsSource = Lista;
                  });
              });
    }

    private async void nuevoButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NuevoClientePage));
    }

    private void filtroEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        string filtro = filtroEntry.Text.ToLower();
        if (filtro.Length > 0)
        {
            listaCollection.ItemsSource = Lista.Where(x => x.NombreCompleto.ToLower().Contains(filtro));
        }
        else
        {
            listaCollection.ItemsSource = Lista;
        }
    }

    private async void listaCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Clientes empleado = e.CurrentSelection.FirstOrDefault() as Clientes;
        var parametro = new Dictionary<string, object>
        {
            ["Detalle"] = empleado
        };
        await Shell.Current.GoToAsync(nameof(VerClientePage), parametro);

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarLista();
    }
}