using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Repository;
using UserManagement.ViewModel;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsControllerLama : ControllerBase
    {
        PersonsRepository personRepository;
        public PersonsControllerLama(PersonsRepository personRepository)
        {

            this.personRepository = personRepository;
        }

        //manggil semua data
        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<Person> persons = personRepository.Get();
            return Ok(persons);
        }


        //manggil data berdasarkan nik
        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            var person = personRepository.Get(NIK);
            if (person==null) 
            {
                return NotFound("Eror!! NIK Salah!! Data Tidak Ditemukan!!");
            }
            return Ok(person);
        
        }

        //insert data
        [HttpPost]
        public ActionResult Post(Person person)
        {
            try
            {
                return Ok(personRepository.insert(person));

            }
            catch (Exception)
            {
                return StatusCode(405, new { status=HttpStatusCode.BadRequest, message = "Objek data yang dimasukan salah!!"});
                throw;
            }
        }

        //update data
        [HttpPut]
        public ActionResult Put(Person person)
        {
            try
            {
                return Ok(personRepository.update(person));
            }
            catch (Exception)
            {
                return StatusCode(400,new {status=HttpStatusCode.BadRequest, message = "Data yang dimasukan belum lengkap"});
            }
        }

        //delete data berdasarkan nik
        [HttpDelete ("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            //dipanggil dulu data dengan nik baru dihapus
           var person = personRepository.Get(NIK);
            if (person!=null)
            {
                return Ok(personRepository.Delete(NIK));
            }
            else 
            {
                return NotFound("Data Tidak Ada");
            }
        }

        [HttpGet("GetFirstName/{NIK}")]
        public ActionResult GetFirstName(string NIK)
        {
            //Person person2 = personRepository.Get(NIK);

            PersonVM persons = personRepository.GetFirstName(NIK);
            if (persons != null)
            {

            }
            return Ok(persons);
        }

        //[HttpGet("GetALL")]
        //public ActionResult GetALL()
        //{
        //    IEnumerable<PersonVM> persons = personRepository.GetALL();
        //    return Ok(persons);
        //}

    }

}
