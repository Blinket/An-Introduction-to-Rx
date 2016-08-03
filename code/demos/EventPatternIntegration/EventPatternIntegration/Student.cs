using System;

namespace EventPatternIntegration
{
    public class Student
    {
        private string _name;
        private int _rollNumber;
        private int _age;
        private string _class;
        
        public event NameChangedHandler NameChanged;
        public event EventHandler RollNumberChanged;
        public event EventHandler<AgeChangedEventArgs> AgeChanged;
        public event Action<Tuple<string, string>> ClassChanged;
        
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

                OnNameChanged(oldValue, _name);
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

                OnAgeChanged(oldValue, _age);
            }
        }

        public int RollNumber
        {
            get
            {
                return _rollNumber;
            }
            set
            {
                var oldValue = _rollNumber;
                _rollNumber = value;

                OnRollNumberChanged(oldValue, _rollNumber);
            }
        }

        public string Class
        {
            get
            {
                return _class;
            }
            set
            {
                var oldValue = _class;
                _class = value;

                OnClassChanged(oldValue, _class);
            }
        }

        protected virtual void OnNameChanged(string oldValue, string newValue)
        {
            NameChanged?.Invoke(this, new NameChangedEventArgs(this, oldValue, newValue));
        }

        private void OnRollNumberChanged(int oldValue, int newValue)
        {
            RollNumberChanged?.Invoke(this, new RollNumberChangedEventArgs(this, oldValue, newValue));
        }

        protected virtual void OnAgeChanged(int oldValue, int newValue)
        {
            AgeChanged?.Invoke(this, new AgeChangedEventArgs(this, oldValue, newValue));
        }

        protected virtual void OnClassChanged(string oldValue, string newValue)
        {
            ClassChanged?.Invoke(new Tuple<string, string>(oldValue, newValue));
        }
    }
}