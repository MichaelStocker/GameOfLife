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
    public partial class Form1 : Form
    {

        static int _boxWidth = 192;
        static int _boxHeight = 94;
        int _speed = 100;

        // The universe array
        bool[,] universe = new bool[192, 94];
        bool[,] scratchPad = new bool[192, 94];

        // Drawing colors
        Color gridColor = Color.LightGray;
        Color cellColor = Color.PowderBlue;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = _speed; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    int countResult = CountSwitch(isFinite, i, j);

                    if (universe[i, j] == true && (countResult < 2))
                    {
                        scratchPad[i, j] = false;
                    }
                    else if (universe[i, j] == true && (countResult > 3))
                    {
                        scratchPad[i, j] = false;
                    }
                    else if (universe[i, j] == true && (countResult == 2 || countResult == 3))
                    {
                        scratchPad[i, j] = true;
                    }
                    else if (universe[i, j] == false && countResult == 3)
                    {
                        scratchPad[i, j] = true;
                    }
                }
            }

            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;

            for (int i = 0; i < scratchPad.GetLength(0); i++)
            {
                for (int j = 0; j < scratchPad.GetLength(1); j++)
                {
                    scratchPad[i, j] = false;
                }
            }
            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            int cellLength;
            if (cellWidth < cellHeight)
            {
                cellLength = cellWidth;
            }
            else cellLength = cellHeight;

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellLength;
                    cellRect.Y = y * cellLength;
                    cellRect.Width = cellLength;
                    cellRect.Height = cellLength;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }
                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    if (CountNeighborsFinite(x, y) > 0)
                    {
                        Font font = new Font("Arial", 8f);

                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        int neighbors = CountSwitch(isFinite, x, y);

                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Black, cellRect, stringFormat);
                    }
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                int cellLength;
                if (cellWidth < cellHeight)
                {
                    cellLength = cellWidth;
                }
                else cellLength = cellHeight;

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellLength;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellLength;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }
        private int CountNeighborsFinite(int x, int y)
        {
            int neighborCount = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOff = -1; yOff <= 1; yOff++)
            {
                for (int xOff = -1; xOff <= 1; xOff++)
                {
                    int xChk = x + xOff;
                    int yChk = y + yOff;

                    if (xOff == 0 && yOff == 0)
                    {
                        continue;
                    }
                    if (xChk < 0)
                    {
                        continue;
                    }
                    if (yChk < 0)
                    {
                        continue;
                    }
                    if (xChk >= xLen)
                    {
                        continue;
                    }
                    if (yChk >= yLen)
                    {
                        continue;
                    }
                    if (universe[xChk, yChk] == true)
                    {
                        neighborCount++;
                    }
                }
            }
            return neighborCount;
        }
        private int CountNeighborsToroidal(int x, int y)
        {
            int neighborCount = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);
            for (int yOff = -1; yOff <= 1; yOff++)
            {
                for (int xOff = -1; xOff <= 1; xOff++)
                {
                    int xChk = x + xOff;
                    int yChk = y + yOff;

                    if (xOff == 0 && yOff == 0)
                    {
                        continue;
                    }
                    if (xChk < 0)
                    {
                        xChk = xLen - 1;
                    }
                    if (yChk < 0)
                    {
                        yChk = yLen - 1;
                    }
                    if (xChk >= xLen)
                    {
                        xChk = 0;
                    }
                    if (yChk >= yLen)
                    {
                        yChk = 0;
                    }
                    if (universe[xChk, yChk] == true)
                    {
                        neighborCount++;
                    }
                }
            }
            return neighborCount;
        }
        private void RandomTime()
        {
            if (timer.Enabled == false)
            {
                Random randy = new Random();
                for (int i = 0; i < universe.GetLength(0); i++)
                {
                    for (int j = 0; j < universe.GetLength(1); j++)
                    {
                        int temp = randy.Next(0, 2);
                        if (temp == 0)
                        {
                            universe[i, j] = true;
                        }
                        else universe[i, j] = false;
                    }
                }
                NextGeneration();
            }
        }
        private void RandomSeed()
        {
            if (timer.Enabled == false)
            {
                RandomSeed modal1 = new RandomSeed();
                generations = -1;

                if (DialogResult.OK == modal1.ShowDialog())
                {
                    int seed = modal1.seed;
                    if (timer.Enabled == false)
                    {
                        Random randy = new Random(seed);
                        for (int i = 0; i < universe.GetLength(0); i++)
                        {
                            for (int j = 0; j < universe.GetLength(1); j++)
                            {
                                int temp = randy.Next(0, 2);
                                if (temp == 0)
                                {
                                    universe[i, j] = true;
                                }
                                else universe[i, j] = false;
                            }
                        }
                        NextGeneration();
                    }
                }
                graphicsPanel1.Invalidate();
            }
        }
        int CountSwitch(bool a, int x, int y)
        {
            if (a)
            {
                return CountNeighborsFinite(x, y);
            }
            else return CountNeighborsToroidal(x, y);
        }
        //------------------------------------------------------------------Buttons-------------------------------------------------------------------------------------------------//

        // Toroidal or Finite Switching Variable
        bool isFinite;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Start Button
            timer.Enabled = true;
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Pause Button
            timer.Enabled = false;
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // Step Button
            if (timer.Enabled == false) NextGeneration();
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == false)
            {
                ModalDialog modal1 = new ModalDialog();
                generations = -1;

                modal1.boxWidth = _boxWidth;
                modal1.boxHeight = _boxHeight;
                modal1.boxSpeed = _speed;


                if (DialogResult.OK == modal1.ShowDialog())
                {
                    _boxWidth = modal1.boxWidth;
                    _boxHeight = modal1.boxHeight;
                    timer.Interval = modal1.boxSpeed;
                }

                universe = new bool[_boxWidth, _boxHeight];
                scratchPad = new bool[_boxWidth, _boxHeight];
                graphicsPanel1.Invalidate();
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == false)
            {
                for (int i = 0; i < universe.GetLength(0); i++)
                {
                    for (int j = 0; j < universe.GetLength(1); j++)
                    {
                        universe[i, j] = false;
                    }
                }
                generations = -1;
                graphicsPanel1.Invalidate();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void randomizeByTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomTime();
        }

        private void randomizeBySeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomSeed();
        }

        private void Finite_CheckedChanged(object sender, EventArgs e)
        {
            isFinite = true;
        }

        private void Toroidal_CheckedChanged(object sender, EventArgs e)
        {
            isFinite = false;
        }
    }
}
