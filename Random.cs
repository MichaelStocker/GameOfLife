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
    public partial class RandomSeed : Form
    {
        public RandomSeed()
        {
            InitializeComponent();
        }
        public int seed
        {
            get { return (int)Seed.Value; }
            set { Seed.Value = value; }
        }
    }
}
