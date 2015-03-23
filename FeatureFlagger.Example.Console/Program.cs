namespace FeatureFlagger.Example.Console
{
    public class Program
    {
         public static void Main(string[] args)
         {
             var toggle = new ExampleFeatureFlag();
             var reader = new AppConfigReader();

             System.Console.WriteLine(
                 toggle.IsEnabled(reader)
                 ? "Enabled."
                 : "Disabled.");

             System.Console.ReadKey();
         }
    }
}