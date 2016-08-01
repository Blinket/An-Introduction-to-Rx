using System;
using System.Reactive.Subjects;

namespace UsingSubjectToWatchProperty
{
    class Program
    {
        static void Main(string[] args)
        {
            var student = new Student { Name = "Sathyaish", Age = 39 };
            student.PropertyChanges.Subscribe(Console.WriteLine);
            student.Age++;

            Console.ReadKey();
        }
    }

    class Student
    {
        private ISubject<IPropertyChangedNotification<Student, object>> _subject = 
            new Subject<IPropertyChangedNotification<Student, object>>();

        private string _name = null;
        private int _age;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                
                var oldValue = _name;
                _name = value;

                OnPropertyChanged<string>("Name", oldValue, _name);
            }
        }

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                var oldValue = _age;
                _age = value;

                OnPropertyChanged<int>("Age", oldValue, _age);
            }
        }

        protected virtual void OnPropertyChanged<T>(string propertyName, T oldValue, T newValue)
        {
            try
            {
                var notification = new PropertyChangedNotification<Student, object>(this,
                    propertyName, oldValue, newValue);

                _subject.OnNext(notification);
            }
            catch(Exception ex)
            {
                _subject.OnError(ex);
            }
        }

        public IObservable<IPropertyChangedNotification<Student, object>> PropertyChanges
        {
            get
            {
                return _subject;
            }
        }
    }

    public class PropertyChangedNotification<TDeclaringType, TPropertyType>
        : IPropertyChangedNotification<TDeclaringType, TPropertyType>
    {
        public PropertyChangedNotification(TDeclaringType declaringObject, 
            string propertyName, 
            TPropertyType oldValue, 
            TPropertyType newValue)
        {
            DeclaringObject = declaringObject;
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }
        
        public TDeclaringType DeclaringObject { get; set; }
        public string PropertyName { get; set; }
        
        public TPropertyType OldValue { get; protected set; }
        public TPropertyType NewValue { get; protected set; }

        public override string ToString()
        {
            var msg = string.Format($"The value of {typeof(TDeclaringType).Name}.{PropertyName} changed from {OldValue} to {NewValue}.");

            return msg;
        }
    }

    public interface IPropertyChangedNotification<TDeclaringType, out TPropertyType>
    {
        TDeclaringType DeclaringObject { get; set; }
        string PropertyName { get; set; }

        TPropertyType OldValue { get; }
        TPropertyType NewValue { get; }
    }
}
