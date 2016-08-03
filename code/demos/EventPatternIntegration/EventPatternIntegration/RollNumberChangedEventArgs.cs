using System;

namespace EventPatternIntegration
{
    internal class RollNumberChangedEventArgs : EventArgs
    {
        public int NewValue { get; protected set; }
        public int OldValue { get; protected set; }
        public Student Student { get; protected set; }

        public RollNumberChangedEventArgs(Student student, int oldValue, int newValue)
        {
            this.Student = student;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}