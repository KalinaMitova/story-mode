using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor
{
    public class DownloadFileCommand : Command
    {
        public DownloadFileCommand(string input, string[] data, Tester judge, StudentsRepository repository, 
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

            string url = this.Data[1];
            this.DownloadManager.Download(url);
        }
    }
}
