namespace FeatureFlagger.Example.Console
{
    public class Program
    {
         public static void Main(string[] args)
         {
             var toggle = new ExampleFeatureFlag();

             var isToggleEnabled = toggle.IsEnabled();

             System.Console.WriteLine(
                 isToggleEnabled
                 ? "Enabled."
                 : "Disabled.");

             System.Console.ReadKey();
         }
    }
}