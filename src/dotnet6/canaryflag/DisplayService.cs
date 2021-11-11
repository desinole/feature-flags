
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

internal class DisplayService
{
    public async IAsyncEnumerable<string> Display(IFeatureManager _featureManager)
    {
        IEnumerable<User> users = InMemoryUserRepository.Users;
        foreach (var user in users){
            TargetingContext targetingContext = new TargetingContext
            {
                UserId = user.Id,
                Groups = user.Groups
            };
            bool enabled = await 
                _featureManager.IsEnabledAsync
                (nameof(Globals.FeatureFlags.GitMagicFellowPilot), targetingContext);
            await Task.Delay(1000);
            yield return $"         User: { user.Id } belonging to Group(s) { String.Join(',', user.Groups.ToList<string>()) } has access to { nameof(Globals.FeatureFlags.GitMagicFellowPilot) } - { enabled }           ";
        }
    }
}