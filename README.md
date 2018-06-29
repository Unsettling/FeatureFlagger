# FeatureFlagger

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

## User Behaviour

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