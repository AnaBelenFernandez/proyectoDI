using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ProyectoIntermodular
{
    internal class Negocio
    {
        private readonly HttpClient httpClient = new HttpClient();

        public Negocio()
        {

        }
        public async Task<Profesor> Login(string user, string password)
        {
            string apiUrl = $"https://localhost:8080/api/profesores/login/{user}";
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



        public async Task<List<Profesor>> GetProfesores()
        {
            string apiUrl = "https://localhost:8080/api/profesores/listar";
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

        public async Task<List<Guardia>> GetGuardiasProfesores(Profesor profesor)
        {
            string apiUrl = $"https://localhost:8080/api/profesores/getAllGuardias/{profesor.id}";
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                List<Guardia> guardias = JsonConvert.DeserializeObject<List<Guardia>>(result);
                return guardias;
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new GuardiaException("La solicitud no es válida. Verifica los datos enviados.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new GuardiaException("No se encontraron guardias.");
                }
                else
                {
                    throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
                }
            }
        }


        public async Task<Profesor> ObtenerProfesorID(int id)
        {
            string apiUrl = $"https://localhost:8080/api/profesores/buscar/{id}";
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

        public async Task<Guardia> CrearGuardia(Guardia guardia)
        {
            string apiUrl = "https://localhost:8080/api/guardias/crear";
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


        public async Task CambiarEstadoGuardia(int id, EstadoEnum estado)
        {
            string apiUrl = $"https://localhost:8080/api/guardias/setEstado/{id}";
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
            string apiUrl = $"https://localhost:8080/api/guardias/borrar/{id}";
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
            string apiUrl = $"https://localhost:8080/api/guardias/modificar/{guardia.Id}";
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

        public async Task<List<Guardia>> ObtenerGuardias()
        {
            string apiUrl = "https://localhost:8080/api/guardias/listar";
            var response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new GuardiaException("Ocurrió un error al procesar la solicitud.");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Guardia>>(responseContent);
        }
        public async Task<Guardia> ObtenerGuardiaID(int id)
        {
            string apiUrl = $"https://localhost:8080/api/guardias/buscar/{id}";
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string guardiaJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Guardia>(guardiaJson);
            }
            else
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
    }
}
