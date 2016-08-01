using System;
using System.Reactive.Subjects;

namespace SchoolManagementSystem
{
    public class School
    {
        private ISubject<Student> _subject = null;
        
        public string Name { get; set; }
        
        public School(string name)
        {
            Name = name;

            _subject = new ReplaySubject<Student>();
        }

        public void AdmitStudent(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentNullException("student");

                _subject?.OnNext(student);
            }
            catch(Exception ex)
            {
                _subject.OnError(ex);
            }
        }

        public IObservable<Student> Students
        {
            get
            {
                return _subject;
            }
        }
    }
}
