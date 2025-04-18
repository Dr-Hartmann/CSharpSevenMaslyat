using DTOmvp;

namespace MVPv4.Client
{
    public class PersonRepository
    {
        private readonly List<Person> persons = [];

        public PersonRepository()
        {
            persons.Add(new() { Name = "имя"});
            persons.Add(new() { Name = "222" });
        }

        public IEnumerable<Person> GetAll()
        {
            return persons;
        }

        public void Add(Person person)
        {
            persons.Add(person);
        }
    }
}
