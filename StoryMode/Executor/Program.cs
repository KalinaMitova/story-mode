using System;

namespace Executor
{
    class Program
    {
        static void Main()
        {
            Console.WindowWidth = 150;
            Tester tester = new Tester();
            DownloadManager downloadManager = new DownloadManager();
            IOManager ioManager = new IOManager();
            StudentsRepository repo = new StudentsRepository(new RepositorySorter(), new RepositioryFilter());

            CommandInterpreter currentInterpreter = new CommandInterpreter(tester,repo,downloadManager,ioManager);
            InputReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();       
                              
        }
    }
}