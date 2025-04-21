using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoLotDALNEW.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;

namespace CarLotMVC.Controllers
{
    public class InventoryController : Controller
    {

        private string _baseUrl = "https://localhost:44348/api/Inventory";
        // GET: Inventory
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var items = JsonConvert.DeserializeObject<List<Inventory>>(
                    await response.Content.ReadAsStringAsync());
                return View(items);
            }
            return HttpNotFound();
            //return View(_repo.GetAll());
        }

        // GET: Inventory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if(response.IsSuccessStatusCode)
            {
                var item = JsonConvert.DeserializeObject<Inventory>(
                    await response.Content.ReadAsStringAsync());
                return View(item);
            }
            return HttpNotFound();
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Price,Make,Color,PetName")] Inventory inventory)
        {
            if(!ModelState.IsValid) return View(inventory);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(inventory);
                var response = await client.PostAsync(_baseUrl,
                    new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to create record: {ex.Message}");
            }
            return View(inventory);
        }

        // GET: Inventory/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if(response.IsSuccessStatusCode)
            {
                var inventory = JsonConvert.DeserializeObject<Inventory>(
                    await response.Content.ReadAsStringAsync());
                return View(inventory);
            }           
            return HttpNotFound();
        }

        // POST: Inventory/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Price,Make,Color,PetName,Timestamp")] Inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(inventory);
                var response = await client.PutAsync($"{_baseUrl}/{inventory.Id}",
                    new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(inventory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, 
                    $@"Unable to save the record. {ex.Message}");
                return View(inventory);
            }
        }

        // GET: Inventory/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var inventory = JsonConvert.DeserializeObject<Inventory>(
                    await response.Content.ReadAsStringAsync());
                return View(inventory);
            }
            return HttpNotFound();
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "Id,Timestamp")] Inventory inventory)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{inventory.Id}")
                { 
                    Content = new StringContent(
                        JsonConvert.SerializeObject(inventory), Encoding.UTF8, "application/json") 
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, 
                    $@"Unable to delete the record. {ex.Message}");
            }
            return View(inventory);
        }
        protected override void Dispose(bool disposing) => base.Dispose(disposing);
    }
}
