using FeatureFlagger.Core;

namespace FeatureFlagger.Example.Console
{
    public class Program
    {
         public static void Main(string[] args)
         {
             var featureFlag = new ExampleFeatureFlag();

             var isFeatureFlagEnabled = featureFlag.IsEnabled();

             System.Console.WriteLine(
                 isFeatureFlagEnabled
                 ? "Enabled."
                 : "Disabled.");

             System.Console.ReadKey();
         }
    }
}