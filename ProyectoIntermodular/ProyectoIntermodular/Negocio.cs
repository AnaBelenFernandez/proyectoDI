using Newtonsoft.Json;
using ProyectoIntermodular.modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIntermodular
{
    public class Negocio
    {
        private readonly HttpClient httpClient = new HttpClient();

        public Negocio()
        {
            
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="GuardiaException"></exception>
        public async Task<Profesor> Login(string user, string password)
        {
            string apiUrl = $"https://localhost:3306/profesores/login";
            var content = new StringContent(JsonConvert.SerializeObject(new { user, password }), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new GuardiaException("La solicitud no es válida. Verifica los datos enviados.");
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new GuardiaException("Las credenciales son incorrectas. Verifica el usuario y contraseña.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }
            var profesor = JsonConvert.DeserializeObject<Profesor>(await response.Content.ReadAsStringAsync());
           
            return profesor;
        }


        /// <summary>
        /// obtener la lista de profesores
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GuardiaException"></exception>
        public async Task<List<Profesor>> GetProfesores()
        {
            string apiUrl = "https://localhost:3306/profesores";
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                List<Profesor> profesores = JsonConvert.DeserializeObject<List<Profesor>>(result);
                return profesores;
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new GuardiaException("La solicitud no es válida. Verifica los datos enviados.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new GuardiaException("No se encontraron profesores.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }

        }
        /// <summary>
        /// obtener un profesor por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="GuardiaException"></exception>
        public async Task<Profesor> ObtenerProfesorID(int id)
        {
            string apiUrl = $"https://localhost:3306/profesores/{id}";
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string profesorJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Profesor>(profesorJson);
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new GuardiaException("La solicitud no es válida. Verifica el ID del profesor.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new GuardiaException("No se encontró el profesor con el ID especificado.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }
        }
        /// <summary>
        /// Borrar profesor por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="GuardiaException"></exception>
        public async Task BorrarProfesorId(int id)
        {
            string apiUrl = $"https://localhost:3306/profesores/{id}";
            var response = await httpClient.DeleteAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new GuardiaException("La solicitud no es válida. Verifica el ID del profesor.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new GuardiaException("No se encontró el profesor con el ID especificado.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }
        }
        /// <summary>
        /// Crear un profesor pasándole un profesor
        /// </summary>
        /// <param name="profesor"></param>
        /// <returns></returns>
        /// <exception cref="GuardiaException"></exception>
        public async Task<Profesor> CrearProfesor(Profesor profesor)
        {
            string apiUrl = "https://localhost:3306/profesores";
            var content = new StringContent(JsonConvert.SerializeObject(profesor), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new GuardiaException("La solicitud no es válida. Verifica los datos del profesor.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Profesor>(responseContent);
        }
        /// <summary>
        /// crear una guardia en la api pasándole un objeto Guardia
        /// </summary>
        /// <param name="guardia"></param>
        /// <returns></returns>
        /// <exception cref="GuardiaException"></exception>
        public async Task<Guardia> CrearGuardia(Guardia guardia)
        {
            string apiUrl = "https://localhost:3306/guardias";
            var content = new StringContent(JsonConvert.SerializeObject(guardia), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new GuardiaException("La solicitud no es válida. Verifica los datos de la guardia.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Guardia>(responseContent);
        }


        public async Task CambiarEstadoGuardia(int id, Estado estado)
        {
            string apiUrl = $"https://localhost:3306/guardias/{id}";
            var content = new StringContent(JsonConvert.SerializeObject(new { estado }), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new GuardiaException("La solicitud no es válida. Verifica los datos enviados.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new GuardiaException("No se encontró la guardia con el ID especificado.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }
        }

        public async Task BorrarGuardia(int id)
        {
            string apiUrl = $"https://localhost:3306/guardias/{id}";
            var response = await httpClient.DeleteAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new GuardiaException("La solicitud no es válida. Verifica el ID de la guardia.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new GuardiaException("No se encontró la guardia con el ID especificado.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }
        }
        public async Task ActualizarGuardia(Guardia guardia)
        {
            string apiUrl = $"https://localhost:3306:/guardias/{guardia.id}";
            var content = new StringContent(JsonConvert.SerializeObject(guardia), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new GuardiaException("La solicitud no es válida. Verifica los datos enviados.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new GuardiaException("No se encontró la guardia con el ID especificado.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }
        }


        public async Task<Guardia> ActualizarGuardia(int id)
        {
            string apiUrl = $"https://localhost:3306/guardias/{id}";
            var response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new GuardiaException("No se encontró la guardia con el ID especificado.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Guardia>(responseContent);
        }
        public async Task<List<Guardia>> ObtenerGuardias()
        {
            string apiUrl = "https://localhost:3306/guardias";
            var response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Guardia>>(responseContent);
        }


       

       

        

       

      

        
    }
}
