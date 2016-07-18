using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PassingEventsAround
{
    public partial class Form1 : Form
    {
        private IEnumerable<string> strings = new List<string>
        {
            "Sasha Goldshtein",
            "Bart de Smet", 
            "Wes Dyer", 
            "Jeffrey Richter", 
            "Charles Petzold", 
            "Eric Lippert", 
            "Jon Skeet", 
            "Matt Warren", 
            "Erik Meijer", 
            "Anders Hejlsberg"
        };
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var observable = strings.ToObservable();
            
            PopulateList(listBox1, observable);
            PopulateList(listBox2, observable);
        }

        private void PopulateList(ListBox lst, IObservable<string> observable)
        {
            lst.Items.Clear();
            
            observable.ObserveOn(SynchronizationContext.Current).Subscribe(s => lst.Items.Add(s));
        }
    }
}