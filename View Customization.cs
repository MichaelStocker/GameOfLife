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
    public partial class View_Customization : Form
    {
        public View_Customization()
        {
            InitializeComponent();
        }
        public Color userGridColor
        {
            get { return gridColorButton.BackColor; }
            set { gridColorButton.BackColor = value; }
        }
        public Color userCellColor
        {
            get { return cellColorBotton.BackColor; }
            set { cellColorBotton.BackColor = value; }
        }
        public Color panelBackgroundColor
        {
            get { return button2.BackColor; }
            set { button2.BackColor = value; }
        }
        private void gridColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = gridColorButton.BackColor;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                gridColorButton.BackColor = MyDialog.Color;
        }

        private void cellColorBotton_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = cellColorBotton.BackColor;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                cellColorBotton.BackColor = MyDialog.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = button2.BackColor;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                button2.BackColor = MyDialog.Color;
        }
    }
}
