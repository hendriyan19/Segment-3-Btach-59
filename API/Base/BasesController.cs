using System.Net;
using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{


    public class BasesController<Entity, Repository, Key> : ControllerBase
        where Entity: class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BasesController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            return Ok(result);
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(Key NIK)
        {
            var result = repository.Get(NIK);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            var result = repository.Insert(entity);
            if (result != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dibuat" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Data tidak berhasil dibuat" });
        }

        [HttpPut]
        public ActionResult<Entity> Update(Entity entity, Key key)
        {

            var UpdateData = repository.Update(entity, key);

            if (UpdateData != 0)
            {
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = repository.Update(entity, key),
                    message = "Data Berhasil Di Update"
                });

            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
                data = "",
                message = "Data Tidak bisa di Update"
            });


        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(Key NIK)
        {
            var result = repository.Delete(NIK);
            return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil di delete" });
        }

    }
}
