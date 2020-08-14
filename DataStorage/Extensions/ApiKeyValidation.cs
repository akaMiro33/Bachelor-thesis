using AspBlog.Data;
using AspNet.Security.ApiKey.Providers.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AspBlog.Extensions
{
    public class ApiKeyValidation 
    {
        public string pomocna;




        public ApiKeyValidation()
        {

        }

        public  void Validacia1(ApiKeyValidatedContext context)
    {

           // foreach (var kluc in this.databaza.ApiKeys) { 

               // if (context.ApiKey == kluc.Key)
           // {
                  context.Principal = new ClaimsPrincipal();

                  context.Success();
           // }
           // else if (context.ApiKey == "789")
           // {
                   throw new NotSupportedException("You must upgrade.");
           // }
        //}
    }

    }
}