﻿using Newtonsoft.Json;
using Reinco.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reinco.Interfaces.Personal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarPersonal : ContentPage
    {
        public DialogService dialogService;
        public AgregarPersonal()
        {
            InitializeComponent();
            guardar.Clicked += Guardar_Clicked;
            dialogService = new DialogService();

        }
        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dni.Text) || string.IsNullOrEmpty(nombres.Text) || string.IsNullOrEmpty(apellidos.Text) ||
                string.IsNullOrEmpty(usuario.Text) || string.IsNullOrEmpty(contra.Text) || string.IsNullOrEmpty(confirmarContra.Text)
                || string.IsNullOrEmpty(email.Text))
            {
                await dialogService.MostrarMensaje("Iniciar Sessión", "debe rellenar todos los campos");
                return;
            }
            if (contra.Text == confirmarContra.Text)
            {
                using (var cliente = new HttpClient())
                {
                    var result = await cliente.GetAsync("http://192.168.1.37/ServicioUsuario.asmx/AgregarUsuario?dni="+dni.Text + "&nombre=" + nombres.Text
                        + "&apellidos=" + apellidos.Text + "&usuario=" + usuario.Text + "&contrasenia=" + contra.Text 
                        + "&correo=" + email.Text +  "&cip=" + cip.Text);
                    if (result.IsSuccessStatusCode)
                    {
                        await App.Current.MainPage.DisplayAlert("", "Usuario agregado satisfactoriamente", "OK");
                        return;
                    }
                }
            }
        }
        }
}
