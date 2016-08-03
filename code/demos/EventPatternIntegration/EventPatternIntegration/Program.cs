using System;
using System.Reactive.Linq;

namespace EventPatternIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            var student = CreateStudent();

            DemoClassChanged(student);
        }

        private static Student CreateStudent()
        {
            return new Student
            {
                Name = "Sathyaish",
                Age = 6,
                Class = "2nd"
            };
        }

        private static void DemoNameChangedWithEventObjectAndName(Student student)
        {
            var observable = Observable
                .FromEventPattern<NameChangedEventArgs>(student, "NameChanged")
                .Select(ep => ep.EventArgs);

            var subscription = observable.Subscribe(
                args => Print<Student, string>("Name", args.OldName, args.NewName));

            student.Name = "Sathyaish Chakravarthy";

            Console.WriteLine("Press any key to unscubribe.");
            Console.ReadKey();
            subscription.Dispose();
        }

        private static void DemoAgeChanged(Student student)
        {
            var observable = Observable
                .FromEventPattern<AgeChangedEventArgs>(
                handler => student.AgeChanged += handler,
                handler => student.AgeChanged -= handler)
                .Select(ep => ep.EventArgs);

            var subscription = observable.Subscribe(
                args => Print<Student, int>("Age", args.OldValue, args.NewValue));

            student.Age++;

            Console.WriteLine("Press any key to unscubribe.");
            Console.ReadKey();
            subscription.Dispose();
        }

        private static void DemoRollNumberChanged(Student student)
        {
            var observable = Observable
                .FromEventPattern<EventHandler, RollNumberChangedEventArgs>(
                handler => student.RollNumberChanged += handler,
                handler => student.RollNumberChanged -= handler)
                .Select(ep => ep.EventArgs);

            var subscription = observable.Subscribe(
                args => Print<Student, int>("RollNumber", args.OldValue, args.NewValue));

            student.Age++;

            Console.WriteLine("Press any key to unscubribe.");
            Console.ReadKey();
            subscription.Dispose();
        }

        private static void DemoClassChanged(Student student)
        {
            var observable = Observable
                .FromEvent<Tuple<string, string>>(
                handler => student.ClassChanged += handler,
                handler => student.ClassChanged -= handler);

            var subscription = observable.Subscribe(
                tuple => Print<Student, string>("Class", tuple.Item1, tuple.Item2));

            student.Class = "12th";

            Console.WriteLine("Press any key to unscubribe.");
            Console.ReadKey();
            subscription.Dispose();
        }

        private static void DemoNameChangedWithAddAndRemoveHandler(Student student)
        {
            var observable = Observable
                .FromEventPattern<NameChangedHandler, NameChangedEventArgs>(
                /*epHandler => ((object o, NameChangedEventArgs args) => epHandler(o, args)),*/
                handler => student.NameChanged += handler,
                handler => student.NameChanged -= handler)
                .Select(ep => ep.EventArgs);

            var subscription = observable.Subscribe(
                args => Print<Student, string>("Name", args.OldName, args.NewName));

            student.Name = "Sathyaish Chakravarthy";

            Console.WriteLine("Press any key to unscubribe.");
            Console.ReadKey();
            subscription.Dispose();
        }

        private static void Print<TDeclaringType, TPropertyType>(string propertyName, 
            TPropertyType oldValue, 
            TPropertyType newValue)
        {
            Console.WriteLine($"{typeof(TDeclaringType).Name}'s {propertyName} changed from {oldValue.ToString()} to {newValue.ToString()}.");
        }
    }
}
