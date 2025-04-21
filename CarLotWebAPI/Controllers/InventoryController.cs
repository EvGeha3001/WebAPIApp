using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;
using AutoLotDALNEW.Models;
using AutoLotDALNEW.Repos;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using AutoMapper;


namespace CarLotWebAPI.Controllers
{
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiController
    {
        //[HttpGet, Route("")]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        private readonly InventoryRepo _repo = new InventoryRepo();
        private Mapper _mapper;
        public InventoryController() 
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Inventory, Inventory>()
                    .ForMember(x => x.Orders, opt => opt.Ignore());
            });
            _mapper = new Mapper(config);           
        }

        [HttpGet, Route("")]
        public IEnumerable<Inventory> GetInventory()
        {
            var inventories = _repo.GetAll();
            return _mapper.Map<List<Inventory>, List<Inventory>>(inventories);
        }
        //public HttpResponseMessage Get() 
        //{ 
        //    HttpResponseMessage response 
        //        = Request.CreateResponse(HttpStatusCode.OK, "value");
        //    response.Content = new StringContent("hello", Encoding.Unicode);
        //    response.Headers.CacheControl = new CacheControlHeaderValue()
        //    {
        //        MaxAge = TimeSpan.FromMinutes(20)
        //    };
        //    return response;
        //}

        [HttpGet, Route("{id}", Name = "DisplayRoute")]
        [ResponseType(typeof(Inventory))]
        public async Task<IHttpActionResult> GetInventory(int id)
        {
            Inventory inventory = _repo.GetOne(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Inventory, Inventory>(inventory));
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventory(int id, Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }                
            if (id != inventory.Id)
            {
                return BadRequest();
            }                
            try
            {
                _repo.Save(inventory);
            }
            catch (Exception ex)
            {

                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost, Route("")]
        [ResponseType(typeof(Inventory))]
        public IHttpActionResult PostInventory(Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _repo.Add(inventory);
            }
            catch (Exception ex)
            {
                throw;
            }
            return CreatedAtRoute("DisplayRoute", new { id = inventory.Id }, inventory);
        }

        [HttpDelete, Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteInventory(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }
            try
            { 
                _repo.Delete(inventory); 
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}