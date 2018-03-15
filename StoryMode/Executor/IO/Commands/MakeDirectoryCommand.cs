
namespace Executor
{
    public class MakeDirectoryCommand : Command
    {
        public MakeDirectoryCommand(string input, string[] data, Tester judge, StudentsRepository repository, 
                                                                 DownloadManager downloadManager, IOManager ioManager) 
            : base(input, data, judge, repository, downloadManager, ioManager)
        {
        }

        public override void Executor()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }
            string folderName = this.Data[1];
            this.InputOutputManager.CreateDirectoryInCurrentFolder(folderName);
        }
    }
}
