using System.Collections.Generic;

namespace CanaryConsole
{
    public class User
    {
        public string Id { get; set; }

        public IEnumerable<string> Groups { get; set; }

    }

}