using GET_POST.Models;
using GET_POST.Services;
using System.Net.Http;
using System.Web.Http;

namespace GET_POST.Controllers
{
    public class ContactController : ApiController
    {

        private IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Contact[] Get()
        {
            return _contactRepository.GetAllContact();
        }

        public HttpResponseMessage Post(Contact contact)
        {
            return _contactRepository.SaveContact(contact, Request);
        }

    }
}
