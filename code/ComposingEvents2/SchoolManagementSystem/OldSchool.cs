using System.Collections;
using System.Collections.Generic;

namespace SchoolManagementSystem
{
    public class OldSchool : IEnumerable<Student>
    {
        private List<Student> _students;

        public string Name { get; set; }

        public OldSchool(string name)
        {
            Name = name;
            _students = new List<Student>();
        }

        public void AdmitStudent(Student student)
        {
            if (!_students.Contains(student))
                _students.Add(student);
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
