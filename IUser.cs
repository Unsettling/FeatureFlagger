namespace RoyalLondon.IntermediaryManagement.Api.FeatureFlagger.Behaviours
{
    public interface IUser
    {
        string UserName();

        bool UserHasFeature(string userName, string featureName);
    }
}