using Firebase.Database;
using App.Models;
using System.Collections.ObjectModel;
using LiteDB;
using System.Threading.Tasks;

namespace App.Views;

public partial class MainPage : ContentPage
{

	FirebaseClient client = new FirebaseClient("https://crud-29968-default-rtdb.firebaseio.com/");
	public ObservableCollection<Empleado> Lista { get; set; } = new ObservableCollection<Empleado>();


    public MainPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

    public void CargarLista()
    {
        client.Child("Empleados")
              .OnceAsync<Empleado>()
              .ContinueWith(task =>
              {
                  var empleadosFirebase = task.Result;

                  MainThread.BeginInvokeOnMainThread(() =>
                  {
                      Lista.Clear();
                      foreach (var emp in empleadosFirebase)
                      {
                          emp.Object.ID = emp.Key; 
                          Lista.Add(emp.Object);
                      }
                      listaCollection.ItemsSource = Lista;
                  });
              });
    }


    private async void nuevoButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(NuevoEmpleadoPage));
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
        Empleado empleado = e.CurrentSelection.FirstOrDefault() as Empleado;
        var parametro = new Dictionary<string, object>
        {
            ["Detalle"] = empleado
        };
        await Shell.Current.GoToAsync (nameof(VerEmpleadoPage), parametro);

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarLista();
    }

}