using GET_POST.Models;
using System.Net.Http;

namespace GET_POST.Services
{
    public interface IContactRepository
    {
        Contact[] GetAllContact();
        HttpResponseMessage SaveContact(Contact contact, HttpRequestMessage ApiPostRequest);
    }
}