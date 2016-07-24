using System;
using System.Collections;
using System.Collections.Generic;

namespace SchoolManagementSystem
{
    public class School: IEnumerable<Student>
    {
        private List<Student> _students;

        public event StudentAdmitted StudentAdmitted;

        public string Name { get; set; }
        
        public School(string name)
        {
            Name = name;
            _students = new List<Student>();
        }

        public void AdmitStudent(Student student)
        {
            if (!_students.Contains(student))
            {
                _students.Add(student);

                OnStudentAdmitted(this, student);
            }
        }

        protected virtual void OnStudentAdmitted(School school, Student student)
        {
            var args = new StudentAdmittedEventArgs(school, student);

            StudentAdmitted?.Invoke(this, args);
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return _students.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int NumberOfStudents
        {
            get
            {
                return _students.Count;
            }
        }
    }
}
