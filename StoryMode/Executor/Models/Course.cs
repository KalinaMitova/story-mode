﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor
{
   public class Course
    {
        private string name;
        private Dictionary<string, Student> studentsByName;

        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask =  100;

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.name = value;
            }         
        }

        public IReadOnlyDictionary <string, Student> StudentsByName
        {
            get {return this.studentsByName;}
        }

        public Course (string name)
        {
            this.Name = name;
            this.studentsByName = new Dictionary<string, Student>();
        }

        public void EnrollInStudent(Student student)
        {
            if (this.studentsByName.ContainsKey(student.UserName))
            {
                throw new DuplicateEntryInStructureException( student.UserName, this.Name);
            }

            this.studentsByName.Add(student.UserName, student);
        }

    }
}
