using SchoolManagementSystem;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;
using System.Reactive;
using System.Diagnostics;

namespace ReasoningAboutEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        static void ObserveStudentsJoiningWithin(TimeSpan timeSpan)
        {
            var school = new School("School 1");

            var admissionObservable =
                Observable.FromEventPattern<StudentAdmittedEventArgs>(school, "StudentAdmitted");

            var observable = admissionObservable.TimeInterval()
                .Scan<TimeInterval<EventPattern<StudentAdmittedEventArgs>>, StudentPair>(null, (previousPair, current) =>
                {
                    Debug.Print(string.Format($"Student joined after {current.Interval.TotalSeconds} seconds, timeSpan = {timeSpan.TotalSeconds} seconds"));

                    var pair = new StudentPair();

                    if (previousPair == null)
                    {
                        pair.FirstStudent = null;
                        pair.SecondStudent = current.Value.EventArgs.Student;
                        pair.IntervalBetweenJoining = current.Interval;
                        pair.School = current.Value.EventArgs.School;

                        return pair;
                    }

                    if (current.Interval <= timeSpan)
                    {
                        pair.FirstStudent = previousPair.SecondStudent;
                        pair.SecondStudent = current.Value.EventArgs.Student;
                        pair.IntervalBetweenJoining = current.Interval;
                        pair.School = current.Value.EventArgs.School;

                        return pair;
                    }
                    else
                    {
                        return default(StudentPair);
                    }
                })
                .Where(p => (p != default(StudentPair)) && (p.FirstStudent != null));

            var subscription = observable.Subscribe(StudentPairValueHandler);

            school.FillWithStudents(4, TimeSpan.FromSeconds(1));
            school.FillWithStudents(2, TimeSpan.FromSeconds(10));
            school.FillWithStudents(3, TimeSpan.FromSeconds(2));
            school.FillWithStudents(2, TimeSpan.FromSeconds(5));
            school.FillWithStudents(5, TimeSpan.FromSeconds(0.6));

            Console.WriteLine("Press any key to exit the program");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void StudentPairValueHandler(StudentPair pair)
        {
            if (pair != null && pair.FirstStudent != null)
            {
                Console.WriteLine($"{pair.SecondStudent.Name} joined {pair.School.Name} {Math.Round(pair.IntervalBetweenJoining.TotalSeconds, 2)} seconds after {pair.FirstStudent.Name}.");
            }
        }

        static void ObserveStudentsAndJoiningGap(TimeSpan timeSpan)
        {
            var school = new School("School 1");

            var admissionObservable =
                Observable.FromEventPattern<StudentAdmittedEventArgs>(school, "StudentAdmitted");

            var observable = admissionObservable.TimeInterval()
                .Where(t => t.Interval <= timeSpan);

            var subscription = observable.Subscribe(TimeIntervalValueHandler);

            school.FillWithStudentsAsync(10, TimeSpan.FromSeconds(3));
            school.FillWithStudentsAsync(3, TimeSpan.FromSeconds(10));
            school.FillWithStudentsAsync(2, TimeSpan.FromSeconds(5));

            Console.WriteLine("Press any key to exit the program");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void TimeIntervalValueHandler(TimeInterval<EventPattern<StudentAdmittedEventArgs>> t)
        {
            if (t == null) return;

            var student = t.Value.EventArgs.Student.Name;
            var school = t.Value.EventArgs.School.Name;
            var seconds = t.Interval.TotalSeconds;

            if (seconds < 0.5)
            {
                Console.WriteLine($"{student} joined {school} almost immediately after the previous student.");
            }
            else
            {
                Console.WriteLine($"{student} joined {school} in {seconds} seconds of the previous one.");
            }
        }

        static void ObserveAdmissionsPeriodically()
        {
            var school = new School("School 1");

            var admissionObservable =
                Observable.FromEventPattern<StudentAdmittedEventArgs>(school, "StudentAdmitted");

            var observables = admissionObservable.Buffer(TimeSpan.FromSeconds(5))
                .Select(lst => lst.Select(ep => ep.EventArgs));

            var subscription = observables.Subscribe(StudentAdmittedValueHandler);

            school.FillWithStudents(10);
            school.FillWithStudentsAsync(3, TimeSpan.FromSeconds(10));
            school.FillWithStudentsAsync(2, TimeSpan.FromSeconds(5));

            Console.WriteLine("Press any key to exit the program");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void StudentAdmittedValueHandler(IEnumerable<StudentAdmittedEventArgs> args)
        {
            if (args != null)
            {
                var list = args.ToList();

                var count = list.Count;

                if (count > 0)
                {
                    Console.WriteLine($"New batch of {count} students received:");

                    list.ForEach(arg =>
                    {
                        Console.WriteLine($"{arg.Student} joined {arg.School.Name}");
                    });

                    Console.WriteLine();
                }
            }
        }
    }
}
