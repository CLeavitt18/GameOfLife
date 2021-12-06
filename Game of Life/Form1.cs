using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Life
{
    public partial class Form1 : Form
    {
        int width = 30;
        int lenght = 30;

        bool ShowNeighborCount = true;
        bool universeState = false;

        bool[,] universe;
        bool[,] scratchPad;

        Color gridColor = Color.Black;
        Color cellColor = Color.LightGray;

        Timer timer = new Timer();

        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            universe = new bool[width, lenght];
            scratchPad = new bool[width, lenght];

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {

            for (int y = 0; y < lenght; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int neighbors;

                    if (universeState == false)
                    {
                        neighbors = CountNeighborsFinite(x, y);

                        if (universe[x,y] == true)
                        {
                            if (neighbors < 2 || neighbors > 3)
                            {
                                scratchPad[x, y] = false;
                            }
                            else
                            {
                                scratchPad[x, y] = true;
                            }
                        }
                        else
                        {
                            if (neighbors == 3)
                            {
                                scratchPad[x, y] = true;
                            }
                            else
                            {
                                scratchPad[x, y] = false;
                            }
                        }
                    }
                    else
                    {
                        neighbors = CountNeighborsToroidal(x, y);

                        if (universe[x, y] == true)
                        {
                            if (neighbors < 2 || neighbors > 3)
                            {
                                scratchPad[x, y] = false;
                            }
                            else
                            {
                                scratchPad[x, y] = true;
                            }
                        }
                        else
                        {
                            if (neighbors == 3)
                            {
                                scratchPad[x, y] = true;
                            }
                            else
                            {
                                scratchPad[x, y] = false;
                            }
                        }
                    }

                }
            }

            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;

            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            graphicsPanel1.Invalidate();
        }

        private int CountNeighborsFinite(int x, int y)
        {
            int count = 0;

            for (int yOffSet = -1; yOffSet <= 1; yOffSet++)
            {
                for (int xOffSet = -1; xOffSet <= 1; xOffSet++)
                {
                    int xCheck = x + xOffSet;
                    int yCheck = y + yOffSet;

                    if (xOffSet == 0 && yOffSet == 0)
                    {
                        continue;
                    }
                    else if (xCheck < 0)
                    {
                        continue;
                    }
                    else if (yCheck < 0)
                    {
                        continue;
                    }
                    else if (xCheck >= width)
                    {
                        continue;
                    }
                    else if (yCheck >= lenght)
                    {
                        continue;
                    }

                    if (universe[xCheck, yCheck] == true)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;

            for (int yOffSet = -1; yOffSet <= 1; yOffSet++)
            {
                for (int xOffSet = -1; xOffSet <= 1; xOffSet++)
                {
                    int xCheck = x + xOffSet;
                    int yCheck = y + yOffSet;

                    if (xOffSet == 0 && yOffSet == 0)
                    {
                        continue;
                    }
                    
                    if (xCheck < 0)
                    {
                        xCheck = width - 1;
                    }
                    
                    if (yCheck < 0)
                    {
                        yCheck = lenght - 1;
                    }
                    
                    if (xCheck >= width)
                    {
                        xCheck = 0;
                    }
                    
                    if (yCheck >= lenght)
                    {
                        yCheck = 0;
                    }

                    if (universe[xCheck, yCheck] == true)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial", 6f * (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0) / 20f);
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);

            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            stringformat.LineAlignment = StringAlignment.Center;
            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);
            Brush countColor;

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int count;

                    if (universeState == false)
                    {
                        count = CountNeighborsFinite(x, y);
                    }
                    else
                    {
                        count = CountNeighborsToroidal(x, y);
                    }

                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    countColor = Brushes.Red;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);

                        if (count == 3 || count == 2)
                        {
                            countColor = Brushes.Green;
                        }
                    }
                    else
                    {
                        if (count == 3)
                        {
                            countColor = Brushes.Green;
                        }
                    }

                    if (count > 0 && ShowNeighborCount)
                    {
                        e.Graphics.DrawString(count.ToString(), font, countColor, cellRect, stringformat);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
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
                float cellWidth = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
                float cellHeight = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = (int)((float)e.X / cellWidth);
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = (int)((float)e.Y / cellHeight);

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Random ran = new Random();

            for (int y = 0; y < lenght; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int chance = ran.Next() % 3;

                    if (chance == 0)
                    {
                        scratchPad[x, y] = true;
                    }
                    else
                    {
                        scratchPad[x, y] = false;
                    }
                }
            }

            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;

            graphicsPanel1.Invalidate();
        }

        private void hUDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNeighborCount = !ShowNeighborCount;

            graphicsPanel1.Invalidate();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            universe = new bool[width, lenght];
            scratchPad = new bool[width, lenght];

            generations = 0;

            graphicsPanel1.Invalidate();
        }

        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            universeState = false;

            graphicsPanel1.Invalidate();
        }

        private void torodialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            universeState = true;

            graphicsPanel1.Invalidate();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;
            dlg.DefaultExt = "cells";

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);

                writer.WriteLine("!This is a cell save file");

                for (int y = 0; y < lenght; y++)
                {
                    string row = string.Empty;

                    for (int x = 0; x < width; x++)
                    {
                        if (universe[x, y] == true)
                        {
                            row += "O";
                        }
                        else
                        {
                            row += ".";
                        }
                    }

                    writer.WriteLine(row);
                }

                writer.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);

                int maxWidth = 0;
                int maxHeight = 0;

                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();

                    if (row[0] == '!')
                    {
                        continue;
                    }

                    maxHeight++;

                    maxWidth = row.Length;
                }

                width = maxWidth;
                lenght = maxHeight;

                universe = new bool[width, lenght];
                scratchPad = new bool[width, lenght];

                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                int y = 0;

                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();
                    
                    if (row[0] == '!')
                    {
                        continue;
                    }

                    for (int x = 0; x < row.Length; x++)
                    {
                        if (row[x] == 'O')
                        {
                            universe[x, y] = true;
                        }
                        else
                        {
                            universe[x, y] = false;
                        }
                    }

                    y++;
                }

                graphicsPanel1.Invalidate();

                reader.Close();
            }

        }
    }
}
