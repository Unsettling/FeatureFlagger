namespace FeatureFlagger.Behaviours
{
    public interface IUser
    {
        string Username();

        bool UserHasFeature(string userName, string featureName);
    }
}
