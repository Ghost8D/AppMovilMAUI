using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

namespace App.ConexionesLogin
{
    internal class ConexionFirebase
    {
        public static FirebaseAuthClient ConectarFirebase()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyCZvk00PTBWK5B9LOgiP_bgkDWK_qH9FY8",
                AuthDomain = "crud-29968.web.app",
                Providers = new FirebaseAuthProvider[]
                {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                },
            };
            var client = new FirebaseAuthClient(config);
            return client;
        }

        public async Task<UserCredential>CargarUsuario(string Email, string Password)
        {
            var cliente = ConectarFirebase();

            var userCredential = await cliente.SignInWithEmailAndPasswordAsync(Email, Password);

            return userCredential;
        }
        
        public async Task<UserCredential> CrearUsuario(string Email, string Password, string Nombre)
        {
            var cliente = ConectarFirebase();
            var userCredential = await cliente.CreateUserWithEmailAndPasswordAsync(Email, Password, Nombre);
            return userCredential;
        }
    }
}
