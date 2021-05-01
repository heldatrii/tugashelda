using Limilabs.Client.SMTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UserManagement.Base;
using UserManagement.Context;
using UserManagement.Models;
using UserManagement.Repository.Data;
using UserManagement.ViewModel;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        private readonly MyContext myContext;

        public object[] NIK { get; private set; }

        public AccountsController(AccountRepository accountRepository, MyContext myContext) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.myContext = myContext;
        }

        //insert data from multiple table
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            Person person = new Person();
            person.NIK = registerVM.NIK;
            person.FirstName = registerVM.FirstName;
            person.LastName = registerVM.LastName;
            person.Phone = registerVM.Phone;
            person.BirthDate = registerVM.BirthDate;
            person.Salary = registerVM.Salary;
            person.Email = registerVM.Email;
            myContext.Persons.Add(person);
            myContext.SaveChanges();

            Account account = new Account();
            account.NIK = registerVM.NIK;
            account.Password = registerVM.Password;
            myContext.Accounts.Add(account);
            myContext.SaveChanges();


            Education education = new Education();
            education.Degree = registerVM.Degree;
            education.GPA = registerVM.GPA;
            education.UniversityID = registerVM.UniversityID;
            myContext.Educations.Add(education);
            myContext.SaveChanges();

            Profiling profiling = new Profiling();
            profiling.NIK = registerVM.NIK;
            profiling.EducationID = education.EducationID;
            myContext.Profilings.Add(profiling);
            myContext.SaveChanges();


            return Ok();
        }

        //get all data from multiple table morethan 3
        [HttpGet("UserData")]
        public ActionResult GetAll()
        {
            RegisterVM registerVM = new RegisterVM();
            var viewmodel = (from p in myContext.Persons
                             join a in myContext.Accounts on p.NIK equals a.NIK
                             join f in myContext.Profilings on p.NIK equals f.NIK
                             join e in myContext.Educations on f.EducationID equals e.EducationID
                             select new
                             {
                                 NIK = p.NIK,
                                 FirstName = p.FirstName,
                                 LastName = p.LastName,
                                 Phone = p.Phone,
                                 BirthDate = p.BirthDate,
                                 Salary = p.Salary,
                                 Email = p.Email,
                                 Password = a.Password,
                                 EducationID = f.EducationID,
                                 UniversityID = e.UniversityID,
                                 GPA = e.GPA,
                                 Degree = e.Degree
                             }).ToList();
            return Ok(viewmodel);
        }

        //get data by ID(NIK)

        [HttpGet("Profile/{NIK}")]
        public ActionResult GetById(string NIK)
        {
            //var result = myContext.Persons.Find(NIK);
            //RegisterVM registerVM = new RegisterAllVM();
            var viewmodel = (from p in myContext.Persons
                             join a in myContext.Accounts on p.NIK equals a.NIK
                             join f in myContext.Profilings on p.NIK equals f.NIK
                             join e in myContext.Educations on f.EducationID equals e.EducationID
                             where p.NIK == NIK

                             select new RegisterAllVM
                             {
                                 NIK = p.NIK,
                                 FirstName = p.FirstName,
                                 LastName = p.LastName,
                                 Phone = p.Phone,
                                 BirthDate = p.BirthDate,
                                 Salary = p.Salary,
                                 Email = p.Email,
                                 Password = a.Password,
                                 EducationID = f.EducationID,
                                 UniversityID = e.UniversityID,
                                 GPA = e.GPA,
                                 Degree = e.Degree
                             }).ToList();
            return Ok(viewmodel);
        }


        [HttpPost("Login")]
        public IActionResult Login(LoginVM loginVM)
        {
            var person = myContext.Persons.Where(p => p.Email == loginVM.Email).FirstOrDefault();
            if (person != null)
            {
                var account = myContext.Accounts.Where(a => a.Password == loginVM.Password && a.NIK == person.NIK).FirstOrDefault();
                if (account != null)
                {
                    return GetById(person.NIK);
                }
                else
                {
                    return NotFound("Error UserName/Password is Invalid");
                }
            }
            else
            {
                return NotFound("Error UserName/Password is Invalid");
            }
        }

        [HttpPost("ChangePassword")]
        public ActionResult updatepassword(ChangePasswordVM changePasswordVM)
        {
            var cek = myContext.Accounts.FirstOrDefault(a => a.NIK == changePasswordVM.NIK);

            if (cek != null)
            {
                var resultAccount = myContext.Accounts.Find(changePasswordVM.NIK);
                Account account2 = new Account();
                account2.Password = resultAccount.Password;
                if (account2.Password == changePasswordVM.CurrentPassword)
                {
                    resultAccount.Password = changePasswordVM.NewPassword;
                    myContext.Entry(resultAccount).State = EntityState.Modified;
                    myContext.SaveChanges();
                    return Ok();
                }
                else { return NotFound(); }
            }
            else { return NotFound(); }
        }

        [HttpPost("SendEmail")]
        public IActionResult SendEmail(ForgetPasswordVM forgetPasswordVM)
        {
            var resetpassword = Guid.NewGuid().ToString();

            Person person = myContext.Persons.Where(p => p.Email == forgetPasswordVM.Email).FirstOrDefault();
            if (person != null)
            {
                Account account = myContext.Accounts.Where(a => a.NIK == person.NIK).FirstOrDefault();
                ChangePasswordVM changePasswordVM = new ChangePasswordVM
                {
                    Email = forgetPasswordVM.Email,
                    CurrentPassword = account.Password,
                    NewPassword = resetpassword
                };
                //updatepassword(changePasswordVM);
                var resultAccount = myContext.Accounts.Find(person.NIK);
                resultAccount.Password = resetpassword;
                myContext.Entry(resultAccount).State = EntityState.Modified;
                myContext.SaveChanges();
               
                var user = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("namosayoii@gmail.com", "11ii0399Iii"),
                    EnableSsl = true
                };
                user.Send("namosayoii@gmail.com", forgetPasswordVM.Email, "Reset Password Request", $"Your New Password : {resetpassword}");
                return Ok("request sent");
            }
            else 
            {
                return NotFound("Email Not Found");
            }
        }

        
    }
}
