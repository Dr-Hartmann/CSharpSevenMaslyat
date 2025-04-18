using DTOmvp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVPv4.Client;

[ApiController]
[Route("input/[controller]/[action]")]
public class PersonController : Controller
{
    private readonly PersonRepository personRepository;

    public PersonController(PersonRepository personRepository)
    {
        this.personRepository = personRepository;
    }

    [HttpGet]
    public IEnumerable<Person> GetAllPersons()
    {
        return personRepository.GetAll();
    }

    [HttpPost]
    public void Post(Person? person)
    {
        personRepository.Add(person);
    }
}
