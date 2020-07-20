using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

namespace CanaryConsole
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Setup configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            //
            // Setup application services + feature management
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(configuration)
                    .AddFeatureManagement()
                    .AddFeatureFilter<ContextualTargetingFilter>();

            IUserRepository userRepository = new InMemoryUserRepository();

            //
            // Get the feature manager from application services
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                IFeatureManager featureManager = serviceProvider.GetRequiredService<IFeatureManager>();

                //
                // We'll simulate a task to run on behalf of each known user
                // To do this we enumerate all the users in our user repository
                IEnumerable<User> users = InMemoryUserRepository.Users;

                //
                // Mimic work items in a task-driven console application
                foreach (var user in users)
                {
                    const string FeatureName = "Beta";

                    //
                    // Get user
                    //User user = await userRepository.GetUser(userId);

                    //
                    // Check if feature enabled
                    
                    TargetingContext targetingContext = new TargetingContext
                    {
                        UserId = user.Id,
                        Groups = user.Groups
                    };

                    bool enabled = await featureManager.IsEnabledAsync(FeatureName, targetingContext);

                    //
                    // Output results
                    Console.WriteLine($"The {FeatureName} feature is {(enabled ? "enabled" : "disabled")} for the user '{user.Id}' in { string.Join(',',user.Groups.ToArray()) }.");
                }
            }
            Console.WriteLine("done");
        }
    }
}
