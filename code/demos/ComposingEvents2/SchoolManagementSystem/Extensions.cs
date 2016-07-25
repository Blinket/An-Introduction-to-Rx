using System;
using System.Threading;

namespace SchoolManagementSystem
{
    public static class Extensions
    {
        public static void FillWithStudents(this School school, int howMany)
        {
            FillWithStudents(school, howMany, TimeSpan.Zero);
        }

        public static void FillWithStudents(this School school, int howMany, TimeSpan gapBetweenEachAdmission)
        {
            if (school == null)
                throw new ArgumentNullException("school");

            if (howMany < 0)
                throw new ArgumentOutOfRangeException("howMany");

            if (howMany == 1)
            {
                school.AdmitStudent(Student.CreateRandom());
                return;
            }

            for (int i = 0; i < howMany; i++)
            {
                Thread.Sleep(gapBetweenEachAdmission);

                school.AdmitStudent(Student.CreateRandom());
            }
        }
    }
}