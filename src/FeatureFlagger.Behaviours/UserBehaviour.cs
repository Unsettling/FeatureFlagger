namespace Unsettling.FeatureFlagger.Behaviours
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
                    string username =
                        (x.TryGetValue(Constants.Name, out username)
                             ? username
                             : user.UserName())
                        ?? user.UserName();

                    string lookup =
                        x.TryGetValue(Constants.Lookup, out lookup) ? lookup : Constants.Store;

                    if (string.IsNullOrEmpty(lookup) || lookup.Equals(Constants.Store))
                    {
                        return user.UserHasFeature(username, x[Constants.Feature]);
                    }

                    string users =
                        (x.TryGetValue(Constants.Users, out users)
                             ? users
                             : string.Empty)
                        ?? string.Empty;

                    if (lookup.Equals(Constants.Users, StringComparison.OrdinalIgnoreCase))
                    {
                        return
                            users
                                .Split(',')
                                .Select(u => u.Trim())
                                .Contains(username, StringComparer.OrdinalIgnoreCase);
                    }

                    return false;
                };
        }
    }
}
