using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLFinal
{
    public partial class Form1 : Form
    {
        // Toroidal or Finite Switching Variable
        bool isFinite;
        // Neighbor Count Toggle
        bool isDisplayNums = true;
        // Hud Display Toggle
        bool displayHUD = true;

        // Default Values
        static int _boxWidth = Properties.Settings.Default.width;
        static int _boxHeight = Properties.Settings.Default.height;
        int _speed = Properties.Settings.Default.speed;

        // The universe array
        bool[,] universe = new bool[_boxWidth, _boxHeight];
        bool[,] scratchPad = new bool[_boxWidth, _boxHeight];

        // Drawing colors
        Color gridColor = Properties.Settings.Default.grid;
        Color cellColor = Properties.Settings.Default.cell;
        Color backColor = Properties.Settings.Default.background;
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
            timer.Enabled = false; // start timer as paused
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
            toolStripStatusLivingCells.Text = "Living Cells = " + LivingCells();
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

                    // Neighbor Count Display
                    if (isDisplayNums)
                    {
                        if (CountSwitch(isFinite, x, y) > 0)
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
            }
            // Code for Creating and Displaying the HUD
            string borderType = "Finite";
            Rectangle hud = Rectangle.Empty;
            hud.Y = graphicsPanel1.ClientSize.Height - 95;
            hud.X = graphicsPanel1.ClientSize.Width - (graphicsPanel1.Width - 5);

            if (isFinite)
            {
                borderType = "Finite";
            }
            else
            {
                borderType = "Toroidal";
            }

            if (displayHUD)
            {

                int liveCellDisplay = LivingCells();
                Font font = new Font("Arial", 14f);
                StringFormat stringFormat = new StringFormat();
                e.Graphics.DrawString("Live Cells = " + liveCellDisplay + "\n" +
                    "Generation = " + generations + "\n" +
                    "Border Type = " + borderType + "\n" +
                    "Universe Size (Width,Height) = " + "(" + _boxWidth + "," + _boxHeight + ")", font, Brushes.Black, hud, stringFormat);

            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }
        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            try
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
            // Error Catch
            catch (Exception ex) { }
        }
        // Count neighbors with a finite border
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
        // Count neighbors with an infinite border
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
        // Counts living cells to display Living Cells Count
        public int LivingCells()
        {
            int count = 0;
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    if (universe[i, j] == true) count++;
                }
            }
            return count;
        }
        // Function that randomizes the universe by time
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
        // Function that randomizes the universe by a user's seed
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
        // Function to Toggle between Finite and Toroidal Edge Behavior
        int CountSwitch(bool a, int x, int y)
        {
            if (a)
            {
                return CountNeighborsFinite(x, y);
            }
            else return CountNeighborsToroidal(x, y);
        }
        //------------------------------------------------------------------------Buttons--------------------------------------------------------------------------------------------//
        #region Buttons
        // Start Button
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }
        // Pause Button
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }
        // Step Button
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == false) NextGeneration();
        }
        // Allows user to change cell and grid colors
        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == false)
            {
                View_Customization colorPick = new View_Customization();
                // popup will display current color implimentation
                colorPick.userCellColor = cellColor;
                colorPick.userGridColor = gridColor;
                colorPick.panelBackgroundColor = backColor;

                if (DialogResult.OK == colorPick.ShowDialog())
                {
                    gridColor = colorPick.userGridColor;
                    cellColor = colorPick.userCellColor;
                    backColor = colorPick.panelBackgroundColor;
                    graphicsPanel1.BackColor = backColor;
                }
            }
            graphicsPanel1.Invalidate();
        }
        // Clear the Universe
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
        // Calls the Randomize By Time function
        private void randomizeByTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomTime();
        }
        // Calls the Randomize By User Seed function
        private void randomizeBySeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomSeed();
        }
        // button that toggles the border to Finite
        private void Finite_CheckedChanged(object sender, EventArgs e)
        {
            isFinite = true;
        }
        // button that toggles the border to Toroidal
        private void Toroidal_CheckedChanged(object sender, EventArgs e)
        {
            isFinite = false;
        }
        // Opens a saved .cells file
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            int rowNum;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                for (int i = 0; i < universe.GetLength(0); i++)
                {
                    for (int j = 0; j < universe.GetLength(1); j++)
                    {
                        universe[i, j] = false;
                    }
                }
                generations = -1;

                StreamReader reader = new StreamReader(dlg.FileName);

                // Create a couple variables to calculate the width and height
                // of the data in the file.
                int maxWidth = 0;
                int maxHeight = 0;

                // Iterate through the file once to get its size.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then it is a comment
                    // and should be ignored.
                    if (row[0] == '!') continue;

                    // If the row is not a comment then it is a row of cells.
                    // Increment the maxHeight variable for each row read.
                    if (row[0] != '!')
                    {
                        maxHeight++;
                        // Get the length of the current row string
                        // and adjust the maxWidth variable if necessary.
                        maxWidth = row.Length;
                    }
                }

                // Resize the current universe and scratchPad
                // to the width and height of the file calculated above.
                universe = new bool[maxWidth, maxHeight];
                scratchPad = new bool[maxWidth, maxHeight];

                // Reset the file pointer back to the beginning of the file.
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                rowNum = 0;

                // Iterate through the file again, this time reading in the cells.

                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then
                    // it is a comment and should be ignored.
                    if (row[0] == '!') continue;

                    // If the row is not a comment then 
                    // it is a row of cells and needs to be iterated through.
                    else
                    {
                        for (int xPos = 0; xPos < maxWidth; xPos++)
                        {
                            // If row[xPos] is a 'O' (capital O) then
                            // set the corresponding cell in the universe to alive.
                            if (row[xPos] == 'O') universe[xPos, rowNum] = true;

                            // If row[xPos] is a '.' (period) then
                            // set the corresponding cell in the universe to dead.
                            if (row[xPos] == '.') universe[xPos, rowNum] = false;
                        }
                        rowNum++;
                    }

                }

                // Close the file.
                reader.Close();
                graphicsPanel1.Invalidate();
            }
        }
        // Saves a .cells file
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);

                // Write any comments you want to include first.
                // Prefix all comment strings with an exclamation point.
                // Use WriteLine to write the strings to the file. 
                // It appends a CRLF for you.
                writer.WriteLine("!This is my comment.");

                // Iterate through the universe one row at a time.
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Create a string to represent the current row.
                    String currentRow = string.Empty;

                    // Iterate through the current row one cell at a time.
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        // If the universe[x,y] is alive then append 'O' (capital O)
                        // to the row string.
                        if (universe[x, y] == true) currentRow += 'O';

                        // Else if the universe[x,y] is dead then append '.' (period)
                        // to the row string.
                        else if (universe[x, y] == false) currentRow += '.';
                    }

                    // Once the current row has been read through and the 
                    // string constructed then write it to the file using WriteLine.
                    writer.WriteLine(currentRow);
                }

                // After all rows and columns have been written then close the file.
                writer.Close();
            }
        }
        // Button allows the user to change the size and speed of the universe
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
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
        // Grid Toggle
        private void gridLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == false)
            {
                if (gridLinesToolStripMenuItem.Checked == true)
                {
                    gridLinesToolStripMenuItem.Checked = false;
                    gridColor = Color.Transparent;
                }
                else
                {
                    gridLinesToolStripMenuItem.Checked = true;
                    gridColor = Color.LightGray;
                }
                graphicsPanel1.Invalidate();
            }
        }
        // Neighbor Count Toggle
        private void neighborCountNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == false)
            {
                if (neighborCountNumbersToolStripMenuItem.Checked == true)
                {
                    neighborCountNumbersToolStripMenuItem.Checked = false;
                    isDisplayNums = false;
                }
                else
                {
                    neighborCountNumbersToolStripMenuItem.Checked = true;
                    isDisplayNums = true;
                }
                graphicsPanel1.Invalidate();
            }
        }
        // HUD Display Toggle
        private void displayHUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (displayHUDToolStripMenuItem.Checked == true)
            {
                displayHUDToolStripMenuItem.Checked = false;
                displayHUD = false;
            }
            else
            {
                displayHUDToolStripMenuItem.Checked = true;
                displayHUD = true;
            }
            graphicsPanel1.Invalidate();
        }
        // Reset Settings
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();

            _boxWidth = Properties.Settings.Default.width;
            _boxHeight = Properties.Settings.Default.height;
            _speed = Properties.Settings.Default.speed;
            gridColor = Properties.Settings.Default.grid;
            cellColor = Properties.Settings.Default.cell;
            backColor = Properties.Settings.Default.background;
        }
        // Reload Settings
        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();

            _boxWidth = Properties.Settings.Default.width;
            _boxHeight = Properties.Settings.Default.height;
            _speed = Properties.Settings.Default.speed;
            gridColor = Properties.Settings.Default.grid;
            cellColor = Properties.Settings.Default.cell;
            backColor = Properties.Settings.Default.background;
        }
        #endregion
        // Saves Settings
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.width = _boxWidth;
            Properties.Settings.Default.height = _boxHeight;
            Properties.Settings.Default.speed = _speed;
            Properties.Settings.Default.grid = gridColor;
            Properties.Settings.Default.cell = cellColor;
            Properties.Settings.Default.background = backColor;
            Properties.Settings.Default.Save();
        }

    }
}
