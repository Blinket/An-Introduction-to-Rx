using System;
using System.Reactive.Subjects;

namespace UsingSubjectToBuildIObservableImpl
{
    public class School : IObservable<Student>
    {
        private ISubject<Student> _subject = null;

        public School()
        {
            _subject = new Subject<Student>();
        }

        public void AdmitStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    _subject.OnError(new ArgumentNullException("student"));
                }

                _subject.OnNext(student);
            }
            catch(Exception ex)
            {
                _subject.OnError(ex);
            }
        }

        public IDisposable Subscribe(IObserver<Student> observer)
        {
            return _subject?.Subscribe(observer);
        }
    }
}
