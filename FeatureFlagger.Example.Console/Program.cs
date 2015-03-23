namespace FeatureFlagger.Example.Console
{
    public class Program
    {
         public static void Main(string[] args)
         {
             var toggle = new ExampleFeatureFlag();

             System.Console.WriteLine(
                 toggle.IsEnabled()
                 ? "Enabled."
                 : "Disabled.");

             System.Console.ReadKey();
         }
    }
}