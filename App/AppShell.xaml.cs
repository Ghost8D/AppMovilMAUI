using App.Views;
using App.Views.Cliente;

namespace App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Rutas de Accesso
            Routing.RegisterRoute(nameof(NuevoEmpleadoPage), typeof(NuevoEmpleadoPage));
            Routing.RegisterRoute(nameof(VerEmpleadoPage), typeof(VerEmpleadoPage));

            Routing.RegisterRoute("NuevoClientePage", typeof(NuevoClientePage));
            Routing.RegisterRoute("VerClientePage", typeof(VerClientePage));
        }
    }
}
