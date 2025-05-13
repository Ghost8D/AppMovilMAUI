using App.ConexionesLogin;

namespace App.Views;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}

	public async void LoginButton_Clicked (object sender, EventArgs e)
	{
        try
        {
            string email = emailEntry.Text?.Trim();
            string password = passwordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Por favor, ingresa el correo y la contrase�a.", "OK");
                return;
            }

            // Conexi�n y autenticaci�n
            ConexionFirebase conexionFirebase = new ConexionFirebase();
            var credenciales = await conexionFirebase.CargarUsuario(email, password);

            if (credenciales?.User == null)
            {
                await DisplayAlert("Error", "No se pudo iniciar sesi�n. Usuario inv�lido.", "OK");
                return;
            }

            string uid = credenciales.User.Uid;

            await DisplayAlert("Bienvenido", $"Usuario: {email}", "OK");

            Application.Current.MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo iniciar sesi�n:\n{ex.Message}", "OK");
        }
    }
}