namespace Vurdalakov.Bencode
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var application = new Application(args);
            application.Run();
        }
    }
}
