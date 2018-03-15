
namespace Executor
{
    public class ChangeRelativePathCommand : Command
    {
        public ChangeRelativePathCommand(string input, string[] data, Tester judge, StudentsRepository repository, 
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
            string relPath = this.Data[1];
            this.InputOutputManager.ChangeCurrentDirectoryRelative(relPath);
        }
    }
}
