using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.ViewModel;

namespace UserManagement.Repository.Interface
{
    interface IPersonRepository
    {
        IEnumerable<Person> Get();
        Person Get(string NIK);

        Person insert(Person person);
        Person update(Person person);
        Person Delete(string NIK);

        PersonVM GetFirstName(string NIK);
        IEnumerable<PersonVM> GetALL();


    }
}
