﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Executor
{
    public class StudentsRepository
    {
        private Dictionary<string, Course> courses;
        private Dictionary<string, Student> student;
        public bool isDataInitialized;
        private RepositioryFilter filter;
        private RepositorySorter sorter;

        public StudentsRepository(RepositorySorter sorter, RepositioryFilter filter)
        {
            this.filter = filter;
            this.sorter = sorter;
            this.student = new Dictionary<string, Student>();
            this.courses = new Dictionary<string, Course>();
        }


        public void LoadData(string fileName)
        {
            if (this.isDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataAlreadyInitializedException);
            }

            this.student = new Dictionary<string, Student>();
            this.courses = new Dictionary<string, Course>();
            this.ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!this.isDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            this.student = null;
            this.courses = null;
            this.isDataInitialized = false;
           
        }

        private void ReadData(string fileName)
        {
            string path = SessionData.currentPath + "\\" + fileName;
            if (File.Exists(path))
            {
                OutputWriter.WriteMessageOnNewLine("Reading data...");

                string pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                Regex rgx = new Regex(pattern);
                string[] allInputLines = File.ReadAllLines(path);

                for (int line = 0; line < allInputLines.Length; line++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
                    {
                        Match currentMatch = rgx.Match(allInputLines[line]);
                        string courseName = currentMatch.Groups[1].Value;
                        string username = currentMatch.Groups[2].Value;
                        string scoresStr = currentMatch.Groups[3].Value;
                        try
                        {
                            int[] scores = scoresStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).
                               Select(int.Parse).
                               ToArray();

                            if (scores.Any(x => x > 100 || x < 0))
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                                continue;
                            }

                            if (scores.Length > Course.NumberOfTasksOnExam)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            if (!this.student.ContainsKey(username))
                            {
                                this.student.Add(username, new Student(username));
                            }

                            if (!this.courses.ContainsKey(courseName))
                            {
                                this.courses.Add(courseName, new Course(courseName));
                            }

                            Course course = this.courses[courseName];
                            Student student = this.student[username];

                            student.EnrollInCourse(course);
                            student.SetMarksInCourse(courseName, scores);

                            course.EnrollInStudent(student);
                        }
                        catch (FormatException fex)
                        {

                            OutputWriter.DisplayException(fex.Message + $"at line: {line}");
                        }
                    }
                }
                isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                //OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                throw new InvalidPathException();
            }

        }

        public void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossible(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(username, 
                    this.courses[courseName].StudentsByName[username]. MarksByCourseName[courseName]));
            }
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var studentMarksEntry in this.courses[courseName].StudentsByName)
                {
                   this.GetStudentScoresFromCourse(courseName, studentMarksEntry.Key);
                }
            }
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = this.courses[courseName].StudentsByName.
                    ToDictionary(x => x.Key,
                    x => x.Value.MarksByCourseName[courseName]);

                this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
            }
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = this.courses[courseName].StudentsByName.
                    ToDictionary(x => x.Key,
                    x => x.Value.MarksByCourseName[courseName]);

                this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (this.courses.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (this.IsQueryForCoursePossible(courseName) && this.courses[courseName].StudentsByName.ContainsKey(studentUserName))
            {
                return true;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingStudentInDataBase);
            }

            return false;
        }
    }
}

