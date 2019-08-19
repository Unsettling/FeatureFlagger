namespace FeatureFlagger.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    [Export(typeof(IBehaviour))]
    public class UserBehaviour : IBehaviour
    {
        private readonly IUser user;

        [ImportingConstructor]
        public UserBehaviour([Import(AllowDefault = true)] IUser user)
        {
            this.user = user ?? new User();
        }

        // <user [name="jenny"] [lookup="users|config|store"] [users="tom,dick,harry"] />
        public Func<Dictionary<string, string>, bool> Behaviour()
        {
            return x =>
                {
                    string username = x.TryGetValue("name", out username) ? username : user.Username();
                    string lookup = x.TryGetValue("lookup", out lookup) ? lookup : "store";

                    if (string.IsNullOrEmpty(lookup) || lookup.Equals("store"))
                    {
                        return user.UserHasFeature(username, x["feature"]);
                    }

                    string users = x.TryGetValue("users", out users) ? users : string.Empty;

                    if (lookup.Equals("users"))
                    {
                        return users.Split(',').Contains(username);
                    }

                    return false;
                };
        }
    }
}