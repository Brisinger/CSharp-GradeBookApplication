using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if(Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }
            else
            {
                switch(CalculatePercentile(averageGrade))
                {
                    case var percentile when percentile >= 80d:
                        return 'A';
                    case var percentile when percentile >= 60d:
                        return 'B';
                    case var percentile when percentile >= 40d:
                        return 'C';
                    case var percentile when percentile >= 20d:
                        return 'D';
                    default:
                        return 'F';
                }
            }
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades " +
                    "in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name)
        {
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades " +
                    "in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
        }

        /// <summary>
        /// Calculates percentile of student's average grade within the class.
        /// </summary>
        /// <param name="averageGrade"></param>
        /// <returns>double percentile value.</returns>
        private double CalculatePercentile(double averageGrade)
        {
            double studentCount = Students.Where(student => student.AverageGrade < averageGrade).Count();
            double percentile = (studentCount / Students.Count) * 100;
            return percentile;
        }
    }
}
