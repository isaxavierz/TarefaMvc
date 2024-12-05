using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TarefasMvc.Models;

namespace TarefasMvc.Controllers
{
    public class TarefaController : Controller
    {
        public string uriBase = "http://isabellyxavier.somee.com/TarefasApi/Tarefas/"; // Tem que fazer a API e Colocar no Somee antes de testar

         [HttpGet]
        public  async Task<IActionResult> IndexAsync()
        {
           try
           {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessinTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<TarefaViewModel> listaTarefas = await Task.Run(() => JsonConvert.DeserializeObject<List<TarefaViewModel>>(serialized));
                    return View(listaTarefas);
                }
                else
                    throw new System.Exception(serialized);

            }
            catch(System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


        [HttpPost]
        public async Task<ActionResult> CreateAsync(TarefaViewModel t)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessinTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(t));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = String.Format("Tarefa {0}, Id {1} salva com sucesso!", t.Titulo, serialized);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);

            }
             catch(System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create");
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpGet]
        public  async Task<IActionResult> DetailAsync(int? id)
        {
           try
           {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TarefaViewModel p = await Task.Run(() => JsonConvert.DeserializeObject<TarefaViewModel>(serialized));
                    return View(p);
                }
                else
                    throw new System.Exception(serialized);

            }
            catch(System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        
        }

          [HttpGet]
        public  async Task<IActionResult> EditAsync(int? id)
        {
           try
           {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TarefaViewModel p = await Task.Run(() => JsonConvert.DeserializeObject<TarefaViewModel>(serialized));
                    return View(p);
                }
                else
                    throw new System.Exception(serialized);

            }
            catch(System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        
        }

           [HttpPost]
        public  async Task<IActionResult> EditAsync(TarefaViewModel t)
        {
           try
           {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(JsonConvert.SerializeObject(t));
                content.Headers.ContentType = new MediaTypeHeaderValue("aplication/json");

                HttpResponseMessage response = await httpClient.PutAsync(uriBase , content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                   TempData["Mensagem"] = String.Format("Tarefa {0}, atualizada com sucesso!", t.Titulo);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);

            }
            catch(System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        
        }

            [HttpGet]
        public  async Task<IActionResult> DeleteAsync(int id)
        {
           try
           {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                     TempData["Mensagem"] = String.Format("Tarefa Id {0} removida com sucesso!", id);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);

            }
            catch(System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        
        }
    }
}