using Bogus;

internal class InMemoryUserRepository : IUserRepository
{
    internal static List<User> CreateUser()
    {
        List<User> users = new List<User>();
        users.Add(new User
        {
            Id = "Santosh",
            Groups = Enumerable.Empty<string>()
        });
        users.Add(new User
        {
            Id = "Scott",
            Groups = Enumerable.Empty<string>()
        });
        var fakerCanary = new Faker<User>()
        .RuleFor(u => u.Id, f => f.Name.FirstName())
        .RuleFor(u => u.Groups, f => new List<string>() { "Canary" });
        return users;
    }
    internal static readonly IEnumerable<User> Users = new User[]
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
                Id = "Jin",
                Groups = new List<string>()
                {
                    "Ring1"
                }
            }
    };

    Task<User> IUserRepository.GetUser(string id)
    {
        return Task.FromResult(Users.FirstOrDefault(user => user.Id.Equals(id)));
    }
}