using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLFinal
{
    public partial class ModalDialog : Form
    {
        public ModalDialog()
        {
            InitializeComponent();
        }
        public int boxWidth
        {
            get { return (int)Width.Value; }
            set { Width.Value = value; }
        }
        public int boxHeight
        {
            get { return (int)Height.Value; }
            set { Height.Value = value; }
        }
        public int boxSpeed
        {
            get { return (int)Speed.Value; }
            set { Speed.Value = value; }
        }
    }

}
