# FeatureFlagger
![Image of Flag](https://github.com/Boggin/FeatureFlagger/raw/master/assets/flag-64x64.png) Yet Another Feature Flag (Feature Toggle / Feature Switch) implementation.

[![Build status](https://ci.appveyor.com/api/projects/status/f3ov0d8fqfy46yuu/branch/master?svg=true)](https://ci.appveyor.com/project/Boggin/featureflagger/branch/master)

## Introduction

A feature flag allows system functionality to be toggled based on configuration. 

A feature has flags. A basic flag every feature must have is whether or not it 
is enabled. A flag is a behaviour and its parameters. A behaviour is a simple 
piece of functional logic that serves as an activation strategy for the feature. 

    <features>
      <feature name="Example" enabled="true">
        <from date="2018-06-21" />
      </feature>
    </features>

In the above example the feature is called "Example" and its first flag is 
"enabled" which is set to true. The next flag for this feature is a "from" flag. 
This "from" flag will be represented in the code by a "from" behaviour. All 
of a feature's flags must evaluate to true for the feature to be activated.

## Quickstart
1. add to the `<features />` section in your .config the name of your feature and whether it is enabled or not.  

  ```XML
    <features>
      <example enabled="true">
        <from date="2015-03-20" />
      </example>
    </features>
  ```

2. create a class named after your feature that inherits from IFeatureFlag.  

  ```C#
      public class ExampleFeatureFlag : IFeatureFlag { }
  ```

3. where you want to toggle a feature:  
  - instantiate your class (new it up)

  ```C#
      var featureFlag = new ExampleFeatureFlag();
  ```

  - call the 'IsEnabled()' extension method

  ```C#
      var isFeatureFlagEnabled = featureFlag.IsEnabled();
  ```

  - control the flow in your code

  ```C#
      System.Console.WriteLine(
          isFeatureFlagEnabled
          ? "Enabled."
          : "Disabled.");
  ```

## Code

Each feature is a class inheriting from `IFeatureFlag`.

Where the feature is to be toggled the feature flag class is instantiated and 
its `IsEnabled()` method is called.

    if (new ExampleFeatureFlag().IsEnabled())
    {
        doSomething();
    }

Each flag must have a corresponding behaviour. The system loads all the behaviours 
that inherit from an `IBehaviour` interface. All behaviours for the feature are 
evaluated and if they all pass then the feature is said to be enabled.

## Activation Strategies (from, until, etc.)
The activation strategies all implement IBehaviour which means they must implement a `Func<dictionary<string, string>, bool>` method. The behaviours return this method that takes a set of parameters (the dictionary) and tests them truth-ily (the Boolean). You can call as many behaviours as you like for a feature and they each must evaluate to true for the feature flag to be 'on'. Composing your chosen behaviours then becomes your feature's activation strategy.

## Extending the activation strategies
If you want to create a new behaviour you just need to implement IBehaviour, add the MEF2 ExportAttribute and the library will pick it up. [TODO: sample]

## Feature Flag configuration
By default the library will read the settings for the feature flags from the .config of the application. This can be extended, however, by implementing the Read() method from IConfigurationReader. You could then read from your database, a web api or any kind of filesystem. [TODO: sample]

## Conventions
The name of your class must end in "FeatureFlag", i.e. "ExampleFeatureFlag.cs", so the code can read the type, remove the "FeatureFlag" sub-string and use the rest, i.e. "Example", to read from the configuration. If needs be this could be made configurable but convention is easier to deal with.

## Design Notes
Some other feature flaggers will default to disabled if the configuration for a flag isn't found. This library considers that to be exceptional so it will throw if it can't configure a feature flag.

The `<features />` tag in the .config has a stylesheet that ignores the content of the element. This allows for the creation of any type of flag with any kind of properties. Lots of flexibility but you are saved from hanging yourself by the loading of the configuration throwing if it can't set up the feature flag in code.

Instantiation of the feature flags is the correct way to get to .IsEnabled(). Don't add them to your IoC container, even though they inherit from an interface, as they aren't supposed to be part of your application's internal API.

The behaviours are very simple functions that you can compose to get your activation strategies rather than having just a few, large and unwieldy strategies.

## Why another feature flagger?
This feature flagger tries to be both terribly clear to use and very simple to extend. The existing feature flagging utilities all required some extra work on the part of the user that seemed unnecessary (setting up enums or using magic strings). They also weren't as obviously extensible as they could be.

## But what *is* a feature flagger?
There are two main cases for using a feature flag.

The first allows you to share code and give others the option of enabling or disabling your feature. This means, for instance, that you can work on your feature branch and just as you would periodically pull from master you can now push to master without affecting the application's functionality. This practice would prevent the all-to-common 'merge ambush' where you check in your massive change and leave the real pain for others who then need to merge in their work. With feature flagging they would have seen the changes happening as you worked and do smaller merges as they went along. Hey, you could even dispense with feature branches altogether! Personally I think that's the baby going out with the bathwater but maybe your source control tools aren't as great.

The second case allows for releasing code with feature flags enabled. Say you want to get a release out but you don't want a feature to be available just yet. Make your release now and set the date you want the feature to be available from. Perhaps you want to do a gradual rollout of a feature to see how it works for your users. Perhaps you want to do an A/B test with a particular cohort. You can create an activation strategy to allow for each of these cases in production. It could be done in staging so that product owners and stakeholders can review different features without the concomitant risk, should they decide not release a feature this cycle, that you have to remove a bunch of code from master and leave it in a long running feature branch.

## Configuration

Configuration can be done through a config file containing XML _i.e._ a web.config, 
or through a datastore like **Sql Server**. The application can have a setting 
for where the configuration is to be read from and then use the approriate 
configuration reader. The configuration reader implements `IConfigurationReader`.

## User Authorisation Behaviour

This behaviour allows matching users to features, either directly 
through the features configuration or by providing an implementation of 
`UserName()` to find the name of the user and `UserHasFeature()` to check a 
datastore, for example.

There are a number of ways this can be configured:

    <features>
        <feature name="example" enabled="true">
            <from date="2018-06-21" />
            <!-- jenny is in the list of users -->
            <user name="jenny" lookup="users" users="dummy,jenny,john" />
            <!-- dummy (the default) is in the list of users -->
            <user lookup="users" users="dummy,jenny,john" />
            <!-- tom isn't in the list of users -->
            <user name="tom" lookup="users" users="dummy,jenny,john" />
            <!-- user looked up in datastore -->
            <user lookup="store" />
            <!-- jenny looked up in datastore -->
            <user name="jenny" />
            <!-- user looked up in datastore -->
            <user />
        </feature>
    </features>

Obviously only one of these `<user ... />` options can be specified at a time.

If a `name` is provided then the system uses that as the username otherwise 
the `UserName()` method provides the name. The default name is "dummy".

If the `lookup` is "users" then the `users` parameter is checked.

If the `lookup` is "store" then the datastore will be checked using the `UserHasFeature()` method.

Both `UserName()` and `UserHasFeature()` are provided to the user 
behaviour by implementing `IUser`.

### Custom Attribute

It is better to have a custom attribute with which to decorate a class or method 
when the aim is to check user permissions. The `UserFeatureFlagAttribute` wraps 
the `UserBehaviour` to provide this ease of use.

    [UserFeatureFlag("CompanyGet")]
    public async Task<IHttpActionResult> Get(int id)
    {
        ...
    }

As specified above the `IsAuthorized()` method in the attribute will check the 
datastore for a match between the logged in user and the "CompanyGet" feature.

It's also possible to pass the `UserName` which will override whatever the current 
user is. If `Lookup` is "users" then the given `UsersList` will be checked 
rather than the datastore.

### Datastore Schema

Users many-to-many Roles many-to-many Features many-to-many Users

Permissions are synonymous with the UserFeature link table.

### Permissions Management

- Roles can have an enforcement priority
- Roles can be ad hoc
- Permissions are calculated from Features in Roles 
  and Users in Roles using the above rules
- Audit log of where permissions have been granted or revoked

### Access

When a user accesses an action the permission is checked. For example, when a 
user calls CompaniesGet(id) then the system queries the permission. The response 
will contain an HTTP Unauthorised status code if the user doesn't have permission.

Note that claims in a token are not an appropriate place to store permissions. 
A token is for identity only and anyway should be as lightweight as possible.

User authorisation should be stored as close to the application as possible, 
preferably in the same data store.

## A/B Testing

It is easy to do boolean A/B testing with FeatureFlagger. Either the feature is 
enabled and the user sees that functionality or the feature isn't enabled and 
the user sees an alternate function or no functionality at all.

FeatureFlagger can also do multi-variate or ABCD testing by using a simple 
convention. Create features that follow a clear naming pattern, _i.e._ 
"FeatureName_VariantNumber"; "FeatureName_Control". Use these features on the 
branches in your code.

For any A/B or multi-variate test the current user must be passed as a flag to 
the feature (via a **User Flag**). The user is then assigned to a bucket that 
always sees a particular variant.

A **Distribution Flag** with a percentage property is required for each Feature. 
If it's an A/B test then the the percentage would be the number of users who see 
the feature. If it's a multi-variate test then the distribution between the 
variants and the control should add up to 100. If the total isn't 100 then 
you'll get spurious results.

In the case where your users are not identifiable by being logged in then either 
cookies or browser fingerprinting could be employed to try and give anonymous 
users an identity for the test.

### Segments

A test can use a segment (_a.k.a._ cohort) of the user base. The **Segment Flag** 
would give the same segment name on each branch of the ABCD test. If no segment 
is given then the test is across the whole user base.

A **Segment Flag** could be a pointer to a list in a datastore, for instance a 
SQL table, _e.g._  
`<segment name="uk_cohort" table="segments" />`  

The `Segments` table could have fields for `IncludedUsers`, `ExcludedUsers` and 
`Rules` where the rules are preset expressions.

Equally, a **Segment Flag** could carry a rule within it, _e.g._  
`<segment name="uk_cohort" comparator="eq" term="country" variable="UK">`
