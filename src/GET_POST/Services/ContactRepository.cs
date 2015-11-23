using GET_POST.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace GET_POST.Services
{
    public class ContactRepository : IContactRepository
    {
        private const string CacheKey = "ContactStore";
        private HttpContext request;

        public ContactRepository()
        {
            request = HttpContext.Current;

            if (request != null)
            {
                if (request.Cache[CacheKey] == null)
                {
                    var contacts = new Contact[]
                    {
                        new Contact
                        {
                             Id = 1, Name = "George"
                        },
                        new Contact
                        {
                            Id = 2, Name = "Dan"
                        }
                    };

                    request.Cache[CacheKey] = contacts;
                }
            }
        }

        public virtual Contact[] GetAllContact()
        {

            request = HttpContext.Current;

            if (request != null)
            {
                return (Contact[])request.Cache[CacheKey];
            }

            return new Contact[]
                {
            new Contact
            {
                Id = 0,
                Name = "NewName"
            }
                };
        }

        public virtual HttpResponseMessage SaveContact(Contact contact, HttpRequestMessage ApiPostRequest)
        {
            var request = HttpContext.Current;

            if (request != null)
            {
                try
                {
                    var currentData = ((Contact[])request.Cache[CacheKey]).ToList();
                    currentData.Add(contact);
                    request.Cache[CacheKey] = currentData.ToArray();

                    return ApiPostRequest.CreateResponse<Contact>(System.Net.HttpStatusCode.Created, contact); ;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }
            
            return null;
        }
    }
}
