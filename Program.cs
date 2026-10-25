using PicoGK;

class Program
{
    [STAThread]
    static void Main()
    {
        try
        {
            Library.Go(0.5f, ModulatedRing.Task);
        }
        catch (Exception e)
        {
            Console.WriteLine("There has been an Error: ");
            Console.WriteLine(e.ToString());
        }
    }
}