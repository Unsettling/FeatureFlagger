# FeatureFlagger
![Image of Flag](https://github.com/Boggin/FeatureFlagger/raw/master/assets/flag-64x64.png) Yet Another Feature Flag (Feature Toggle / Feature Switch) implementation.

[![Build status](https://ci.appveyor.com/api/projects/status/f3ov0d8fqfy46yuu/branch/master?svg=true)](https://ci.appveyor.com/project/Boggin/featureflagger/branch/master)

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
