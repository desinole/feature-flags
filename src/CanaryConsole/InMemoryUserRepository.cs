using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanaryConsole
{
    public class InMemoryUserRepository : IUserRepository
    {
        public static readonly IEnumerable<User> Users = new User[]
        {
            new User
            {
                Id = "Satya",
                Groups = Enumerable.Empty<string>()
            },
            new User
            {
                Id = "Scott",
                Groups = Enumerable.Empty<string>()
            },
            new User
            {
                Id = "BillG",
                Groups = Enumerable.Empty<string>()
            },
            new User
            {
                Id = "Damian",
                Groups = new List<string>()
                {
                    "Ring0"
                }
            },
            new User
            {
                Id = "Judd",
                Groups = new List<string>()
                {
                    "Ring0"
                }
            },
            new User
            {
                Id = "Vicente",
                Groups = new List<string>()
                {
                    "Ring0"
                }
            },
            new User
            {
                Id = "Tatiana",
                Groups = new List<string>()
                {
                    "Ring0"
                }
            },
            new User
            {
                Id = "Ananya",
                Groups = new List<string>()
                {
                    "Ring0"
                }
            },
            new User
            {
                Id = "Tim",
                Groups = new List<string>()
                {
                    "Ring1"
                }
            },
            new User
            {
                Id = "Tanya",
                Groups = new List<string>()
                {
                    "Ring1"
                }
            },
            new User
            {
                Id = "Alec",
                Groups = new List<string>()
                {
                    "Ring1"
                }
            },
            new User
            {
                Id = "Betty",
                Groups = new List<string>()
                {
                    "Ring1"
                }
            },
            new User
            {
                Id = "Mohammed",
                Groups = new List<string>()
                {
                    "Ring1"
                }
            },
            new User
            {
                Id = "Jamal",
                Groups = new List<string>()
                {
                    "Ring1"
                }
            },
            new User
            {
                Id = "Nitya",
                Groups = new List<string>()
                {
                    "Ring1"
                }
            },
            new User
            {
                Id = "Jin Yang",
                Groups = new List<string>()
                {
                    "Ring1"
                }
            }




        };

        public Task<User> GetUser(string id)
        {
            return Task.FromResult(Users.FirstOrDefault(user => user.Id.Equals(id)));
        }
    }
}