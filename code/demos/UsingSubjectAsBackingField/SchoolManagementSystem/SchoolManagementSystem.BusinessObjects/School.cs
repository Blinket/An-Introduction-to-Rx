using System;
using System.Reactive.Subjects;
using System.Threading;

namespace SchoolManagementSystem
{
    public class School
    {
        private ISubject<Student> _subject = null;
        private int _maxNumberOfSeats;
        private int _numberOfStudentsAdmitted;
        
        public string Name { get; set; }
        
        public School(string name, int maxNumberOfSeats)
        {
            Name = name;

            _maxNumberOfSeats = maxNumberOfSeats;

            _numberOfStudentsAdmitted = 0;

            _subject = new /*Replay*/ Subject<Student>();
        }

        public void AdmitStudent(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentNullException("student");

                if (_numberOfStudentsAdmitted == _maxNumberOfSeats)
                {
                    _subject.OnCompleted();
                    return;
                }

                Interlocked.Increment(ref _numberOfStudentsAdmitted);

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
