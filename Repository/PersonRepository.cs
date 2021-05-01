using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Context;
using UserManagement.Controllers;
using UserManagement.Models;
using UserManagement.Repository.Interface;
using UserManagement.ViewModel;

namespace UserManagement.Repository
{
    public class PersonsRepository : IPersonRepository
    { 
               
       private readonly MyContext conn ;
        public PersonsRepository(MyContext conn) 
        {
            this.conn = conn;
        }
        public IEnumerable<Person> Get()
        {
            IEnumerable<Person> persons = new List<Person>();
            persons = conn.Persons.ToList();
            return persons;
        }
        public Person Get(string NIK)
        {
            var person = conn.Persons.Find(NIK);
            return person;
        }

        public Person insert(Person person)
        {
            conn.Persons.Add(person);
            conn.SaveChanges();
            return person;
        }

        public Person Delete(string NIK)
        {
           var delPerson = conn.Persons.Find(NIK);
           conn.Persons.Remove(delPerson);
           conn.SaveChanges();
           return delPerson;
        }

        public Person update(Person person)
        {
            conn.Persons.Update(person);
            conn.SaveChanges();
            return person;

            //conn.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //var result = conn.SaveChanges();
            //return person;
        }
        public PersonVM GetFirstName(string NIK)
        {
            var result = conn.Persons.Find(NIK);
            PersonVM person = new PersonVM();
            person.FirstName = result.FirstName;
            person.NIK = result.NIK;
            person.Salary = result.Salary;
            return person;
        }

        public IEnumerable<PersonVM> GetALL()
        {
            IEnumerable<Person> persons = new List<Person>();
            persons = conn.Persons.ToList();
            var result = persons.Where(p => p.Salary >= 900000)
                .Select(p => new PersonVM()
                {
                    FirstName = p.FirstName,
                    NIK = p.NIK,
                });
            return result;
        }

    }
}
