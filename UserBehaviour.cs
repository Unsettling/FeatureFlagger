namespace RoyalLondon.IntermediaryManagement.Api.FeatureFlagger.Behaviours
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Castle.Core.Internal;

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
                    string username = x.TryGetValue("name", out username) ? username : user.UserName();
                    string lookup = x.TryGetValue("lookup", out lookup) ? lookup : "store";

                    if (lookup.IsNullOrEmpty() || lookup.Equals("store"))
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