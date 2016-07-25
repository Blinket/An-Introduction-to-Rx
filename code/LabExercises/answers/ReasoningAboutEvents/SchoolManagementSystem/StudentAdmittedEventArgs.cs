using System;

namespace SchoolManagementSystem
{
    public class StudentAdmittedEventArgs : EventArgs
    {
        public StudentAdmittedEventArgs(School school, Student student): base()
        {
            School = school;
            Student = student;
        }

        public School School { get; protected set; }
        public Student Student { get; protected set;  }
    }
}