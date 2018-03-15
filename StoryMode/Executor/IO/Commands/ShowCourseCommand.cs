
namespace Executor
{
    class ShowCourseCommand : Command
    {
        public ShowCourseCommand(string input, string[] data, Tester judge, StudentsRepository repository,
                                                              DownloadManager downloadManager, IOManager ioManager)
            : base(input, data, judge, repository, downloadManager, ioManager)
        {
        }

        public override void Executor()
        {
            if (this.Data.Length != 2 && this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);

            }
            if(this.Data.Length == 2)
            {
                string courseName = this.Data[1];
                this.Repository.GetAllStudentsFromCourse(courseName);
            }
            else if (this.Data.Length == 3)
            {
                string courseName = this.Data[1];
                string userName = this.Data[2];
                this.Repository.GetStudentScoresFromCourse(courseName, userName);
            }         
        }
    }
}
