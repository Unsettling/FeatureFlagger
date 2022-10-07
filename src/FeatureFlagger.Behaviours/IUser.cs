namespace FeatureFlagger.Behaviours
{
    public interface IUser
    {
        string Username { get; set; }

        bool UserHasFeature(string userName, string featureName);
    }
}
