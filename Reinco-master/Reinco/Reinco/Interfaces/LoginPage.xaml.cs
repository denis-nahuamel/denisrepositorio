using Newtonsoft.Json;
using Reinco.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reinco.Interfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        string nombres, cargo;
        public DialogService dialogService;
        public LoginPage()
        {
            InitializeComponent();
            enviar.Clicked += Enviar_Clicked;
            dialogService = new DialogService();
            
        }
        
        private async void Enviar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuario.Text))
            {
                await dialogService.MostrarMensaje("Iniciar Sessión", "Debes Ingresar Un Nombre de usuario");
                return;
            }
            if (string.IsNullOrEmpty(password.Text))
            {
                await dialogService.MostrarMensaje("Iniciar Sessión", "Debes Ingresar La Contraseña");
                return;
            }

            var client = new HttpClient();
            //envia por metodo get los datos introducidos en los textbox
            var result = await client.GetAsync("http://192.168.1.37:80/ServicioUsuario.asmx/Login?usuario=" +
                usuario.Text + "&contrasenia=" + password.Text);
            //si no existe el usuario, manda un mensaje de error
            if (!result.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("error al iniciar sesion", "ingrese nuevamente sus datos", "OK");
                return;
            }
            //recoge los datos json y los almacena en la variable resultado
            var resultado = await result.Content.ReadAsStringAsync();
            //si todo es correcto, muestra la pagina que el usuario debe ver
            //List<Usuario> items = JsonConvert.DeserializeObject<List<Usuario>>(resultado);
            //DataTable dtUsuario = new DataTable();
            dynamic array = JsonConvert.DeserializeObject(resultado);
            foreach (var item in array)
            {
                 cargo = item.cargo;
                nombres = item.nombres;
                //dtUsuario.Rows.Add(item);
            }

            App.Current.MainPage = new MainPage();
        }
    }
}
