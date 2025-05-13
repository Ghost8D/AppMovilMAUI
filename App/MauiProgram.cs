using Microsoft.Extensions.Logging;
using Firebase.Database;
using Firebase.Database.Query;
using App.Models;

namespace App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            Registrar();
            return builder.Build();
        }

        public static void Registrar()
        {
            FirebaseClient client = new FirebaseClient("https://crud-29968-default-rtdb.firebaseio.com/");
            var cargos = client.Child("Cargos").OnceAsync<Cargo>();
            if (cargos.Result.Count == 0 )
            {
                client.Child("Cargos").PostAsync(new Cargo { Nombre = "Administrador"});
                client.Child("Cargos").PostAsync(new Cargo { Nombre = "Supervisor" });
                client.Child("Cargos").PostAsync(new Cargo { Nombre = "Dependiente" });
            }

            var tipos = client.Child("Tipos").OnceAsync<TipoCliente>();
            if (tipos.Result.Count == 0 )
            {
                client.Child("Tipos").PostAsync(new TipoCliente { NombreTipo = "Empresa" });
                client.Child("Tipos").PostAsync(new TipoCliente { NombreTipo = "Tiendas" });
                client.Child("Tipos").PostAsync(new TipoCliente { NombreTipo = "Persona" });
            }
        }
    }
}
