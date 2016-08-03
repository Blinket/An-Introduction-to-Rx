using System;

namespace EventPatternIntegration
{
    public class AgeChangedEventArgs : EventArgs
    {
        public AgeChangedEventArgs() : base() { }

        public AgeChangedEventArgs(Student student, 
            int oldValue,
            int newValue) : base()
        {
            Student = student;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public Student Student { get; set; }
        public int OldValue { get; set; }
        public int NewValue { get; set; }
    }
}