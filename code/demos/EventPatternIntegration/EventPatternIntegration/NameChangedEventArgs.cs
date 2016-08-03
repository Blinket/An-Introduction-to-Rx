using System;

namespace EventPatternIntegration
{
    public class NameChangedEventArgs : EventArgs
    {
        public NameChangedEventArgs() : base() { }

        public NameChangedEventArgs(Student student, 
            string oldName, 
            string newName) : base()
        {
            Student = student;
            OldName = oldName;
            NewName = newName;
        }

        public Student Student { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}