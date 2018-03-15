
namespace Executor
{
    public class TraverseFoldersCommand : Command
    {
        public TraverseFoldersCommand(string input, string[] data, Tester judge, StudentsRepository repository, 
                                                                   DownloadManager downloadManager, IOManager ioManager) 
            : base(input, data, judge, repository, downloadManager, ioManager)
        {
        }

        public override void Executor()
        {
            if (this.Data.Length != 1 && this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }
            if (this.Data.Length == 1)
            {
                this.InputOutputManager.TraverseDirectory(0);
            }
            else if (this.Data.Length == 2)
            {
                int depth;
                bool hasParsed = int.TryParse(this.Data[1], out depth);
                if (!hasParsed)
                {
                    throw new InvalidCommandException(this.Input);
                }

                this.InputOutputManager.TraverseDirectory(depth);
            }
            
        }
    }
}
