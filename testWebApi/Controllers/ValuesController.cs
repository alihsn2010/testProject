using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testWebApi.Helper;
using testWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Connection Test1", "ConnectionTest2" };
        }
        #region Bank IBAN

        // POST api/<ValuesController>
        [HttpPost]
        public object CheckIBAN(modelBank value)
        {

            bool result;
            modelBank objRespose = new modelBank();
            objRespose.AccountNumber = value.AccountNumber;
            objRespose.Id = value.Id;
            objRespose.IBAN = value.IBAN;
            objRespose.Note = value.Note;
            string IBANNumber = objRespose.IBAN;

            //here we check IBAN is valid or not 
            //
            //test banks 

            //string HBL = "PK19HABB0007867914348601";
            //string Meezan = "PK10MEZN0001850100447086";
            //string bankIslami = "PK22BKIP0106800331460231";
            //string UBL = "PK83UNIL0109000273583624";
            if (ModelState.IsValid)
            {
                result = Helper.IBANChecker.ValidateBankAccount(IBANNumber);

                if (result)
                {
                    objRespose.Note = "Sucess";
                    return GenericMethods.CreateResponce(ObjectResponce.Accepted, objRespose, null, null, "Done");
                }
                else
                {
                    return GenericMethods.CreateResponce(ObjectResponce.Rejected, objRespose, null, null, "Invalid IBAN");
                }
            }
            else
            {
                return GenericMethods.CreateResponce(ObjectResponce.Rejected);
            }    
        }

        #endregion


    }
}
