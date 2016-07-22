using System;

namespace SchoolManagementSystem
{
    public static class Extensions
    {
        public static void FillWithStudents(this School school, int howMany)
        {
            if (school == null)
                throw new ArgumentNullException("school");

            if (howMany < 0)
                throw new ArgumentOutOfRangeException("howMany");

            for (int i = 0; i < howMany; i++)
                school.AdmitStudent(Student.CreateRandom());
        }
    }
}