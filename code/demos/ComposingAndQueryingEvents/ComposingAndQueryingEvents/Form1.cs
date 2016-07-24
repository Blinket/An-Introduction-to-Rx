using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace ComposingAndQueryingEvents
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var mouseDown = Observable.FromEventPattern<MouseEventArgs>(this, "MouseDown");
            var mouseMoves = Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove");
            var mouseUp = Observable.FromEventPattern<MouseEventArgs>(this, "MouseUp");

            var query = from md in mouseDown
                        from mm in mouseMoves.TakeUntil(mouseUp)
                        // where mm.EventArgs.Location.X == mm.EventArgs.Location.Y
                        select mm.EventArgs.Location;

            query.Subscribe(location => Text = location.ToString());
        }
    }
}
