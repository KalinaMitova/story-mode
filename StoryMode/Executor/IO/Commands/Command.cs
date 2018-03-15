using System;

namespace Executor
{
    public abstract class Command
    {
        private string input;
        private string[] data;
        private Tester judge;
        private StudentsRepository repository;
        private DownloadManager downloadManager;
        private IOManager inputOutputManager;

        public Command(string input, string[] data, Tester judge,
                       StudentsRepository repository, DownloadManager downloadManager,
                       IOManager ioManager)
        {
            this.Input = input;
            this.Data = data;
            this.judge = judge;
            this.repository = repository;
            this.downloadManager = downloadManager;
            this.inputOutputManager = ioManager;
        }

        public string Input
        {
            get { return this.input; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.input = value;
            }
        }

        public string[] Data
        {
            get { return this.data; }

            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }

                this.data = value;
            }
        }

        protected StudentsRepository Repository => this.repository;

        protected Tester Judge => this.judge;

        protected IOManager InputOutputManager => this.inputOutputManager;

        protected DownloadManager DownloadManager => this.downloadManager;

        //public void DisplayInvalidCommandMessage(string input)
        //{
        //    throw new InvalidCommandException(input);
        //}

        public abstract void Executor();
    }
}
