using Bogus;

internal class InMemoryUserRepository : IUserRepository
{
    internal static List<User> CreateUsers()
    {
        List<User> users = new List<User>();
        users.Add(new User
        {
            Id = "Santosh",
            Groups = new List<string>() { "Ring10" }
        });
        users.Add(new User
        {
            Id = "Zhong",
            Groups = new List<string>() { "Ring8" }
        });
        var fakerCanary = new Faker<User>()
        .RuleFor(u => u.Id, f => f.Name.FirstName())
        .RuleFor(u => u.Groups, f => new List<string>() { "Ring0", "Ring1", "Ring2" })
        .Generate(100);
        return users;
    }
    internal static readonly IEnumerable<User> Users = new User[]
    {
            new User
            {
                Id = "Santosh",
                Groups = new List<string>() { "Ring10" }
            },
            new User
            {
                Id = "Edgar",
                Groups = new List<string>() { "Ring3" }
            },
            new User
            {
                Id = "Mike",
                Groups = Enumerable.Empty<string>()
            },
            new User
            {
                Id = "Zhong",
                Groups = new List<string>()
                {
                    "Ring8"
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