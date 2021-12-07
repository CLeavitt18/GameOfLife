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
        //universe x legnth
        int width = 30;
        //universe y legnth
        int lenght = 30;

        bool showHUD = true;
        bool ShowNeighborCount = true;
        bool showGrid;

        //Finite mode if false toroidal if true;
        bool boundaryType = false;


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
            //interate thuoght the universe height
            for (int y = 0; y < lenght; y++)
            {
                //interate thuoght the universe width
                for (int x = 0; x < width; x++)
                {
                    //temp int to hold the amount of lving neighbors
                    int neighbors;

                    //check to see whitch CountNeightbors to call
                    //if false finite is called
                    //if true toroidal is called
                    if (boundaryType == false)
                    {
                        //finite count neighbors
                        neighbors = CountNeighborsFinite(x, y);
                    }
                    else
                    {
                        //toroidal count neighbors
                        neighbors = CountNeighborsToroidal(x, y);
                    }

                    //Check to see if cell is dead or alive
                    if (universe[x, y] == true)
                    {
                        //cell is alive
                        if (neighbors < 2 || neighbors > 3)
                        {
                            //cell is set to dead
                            //if there are to few or to many living neighbors
                            scratchPad[x, y] = false;
                        }
                        else
                        {
                            //cell lives to the next generation
                            //the cell has 2 or 3 living neioghbors
                            scratchPad[x, y] = true;
                        }
                    }
                    else
                    {
                        //cell is dead
                        //if cell has 3 living neighbors
                        //then the cell becomes a living cell
                        //if the cell doesn't have excalty 3 living neighbors it stays dead
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

            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;

            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            
            //make graphics panel redraw
            graphicsPanel1.Invalidate();
        }

        private int CountNeighborsFinite(int x, int y)
        {
            int count = 0;

            //loop around the current cell 
            //setting the offset to -1 for the y postion
            //then adding 1 to wrap around the current cell in the y postion
            for (int yOffSet = -1; yOffSet <= 1; yOffSet++)
            {
                //setting the offset to -1 for the x postion
                //then adding 1 to wrap around the current cell in the x postion
                for (int xOffSet = -1; xOffSet <= 1; xOffSet++)
                {
                    //add the current offsets to the current cells loction
                    //store this in a new int as to not modify the current location
                    int xCheck = x + xOffSet;
                    int yCheck = y + yOffSet;

                    //this is the current cell we ignore this
                    if (xOffSet == 0 && yOffSet == 0)
                    {
                        continue;
                    }
                    else if (xCheck < 0)
                    {
                        //check to see if the current cell being checked
                        //is inside the array not -1 on the x
                        //if x is -1 we ignore this cell
                        continue;
                    }
                    else if (yCheck < 0)
                    {
                        //check to see if the current cell being checked
                        //is inside the array not -1 on the y
                        //if y is -1 we ignore this cell
                        continue;
                    }
                    else if (xCheck >= width)
                    {
                        //check to see if the current cell being checked
                        //is inside the array not greater than the size of the universe
                        //if x is greater than the size of the array then we ignore this cell
                        continue;
                    }
                    else if (yCheck >= lenght)
                    {
                        //check to see if the current cell being checked
                        //is inside the array not greater than the size of the universe
                        //if y is greater than the size of the array then we ignore this cell
                        continue;
                    }

                    if (universe[xCheck, yCheck] == true)
                    {
                        //if the cell is alive then we increament the count variable
                        count++;
                    }
                }
            }

            //after all the neighbors are checked and counted we return the number of living neighbors
            return count;
        }

        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;

            //loop around the current cell 
            //setting the offset to -1 for the y postion
            //then adding 1 to wrap around the current cell in the y postion
            for (int yOffSet = -1; yOffSet <= 1; yOffSet++)
            {
                //setting the offset to -1 for the x postion
                //then adding 1 to wrap around the current cell in the x postion
                for (int xOffSet = -1; xOffSet <= 1; xOffSet++)
                {
                    //add the current offsets to the current cells loction
                    //store this in a new int as to not modify the current location
                    int xCheck = x + xOffSet;
                    int yCheck = y + yOffSet;

                    //this is the current cell we ignore this
                    if (xOffSet == 0 && yOffSet == 0)
                    {
                        continue;
                    }
                    
                    //check to see if x is less then 0 
                    // if x = -1
                    if (xCheck < 0)
                    {
                        //set x = to the max width of the universe 
                        //so we can wrap around to the other side
                        xCheck = width - 1;
                    }

                    //check to see if y is less then 0 
                    // if y = -1
                    if (yCheck < 0)
                    {
                        //set y = to the max width of the universe 
                        //so we can wrap around to the other side
                        yCheck = lenght - 1;
                    }
                    
                    //check to see if x is greater than the universe size 
                    if (xCheck >= width)
                    {
                        //set x to 0 so we can wrap around to the other side to check that cell
                        xCheck = 0;
                    }

                    //check to see if y is greater than the universe size 
                    if (yCheck >= lenght)
                    {
                        //set y to 0 so we can wrap around to the other side to check that cell
                        yCheck = 0;
                    }

                    //once we do of all these checks we can see if that cell is alive or dead
                    if (universe[xCheck, yCheck] == true)
                    {
                        //increament the amount of living neighbors count if the cell is alive
                        count++;
                    }
                }
            }

            //return the number of living neighbors
            return count;
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            int aliveCells = 0;
            //set the font type and size
            //for the cells neighbor count
            Font font = new Font("Arial", 6f * (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0) / 20f);
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);

            //create a string formatter so the number of neightbors will be centered in the cell
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            stringformat.LineAlignment = StringAlignment.Center;
            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);
            //A bush for the color of the neighbor count
            Brush countColor;

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int count;

                    //check to see witch version of count neighbors to call
                    //if false we call finite
                    //if true we call toroidal
                    if (boundaryType == false)
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

                    //Set the neighbors count vrush color to red
                    countColor = Brushes.Red;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        aliveCells++;

                        e.Graphics.FillRectangle(cellBrush, cellRect);

                        //Set neighbors brush to green if the cell will live to the next generation
                        if (count == 3 || count == 2)
                        {
                            countColor = Brushes.Green;
                        }
                    }
                    else
                    {
                        //Set brush color to green if the cell will becoem alive in the next generation
                        if (count == 3)
                        {
                            countColor = Brushes.Green;
                        }
                    }
                    //draws the number of neighbors if the cell has any living neighbors
                    if (count > 0 && ShowNeighborCount)
                    {
                        e.Graphics.DrawString(count.ToString(), font, countColor, cellRect, stringformat);
                    }

                    if (showGrid)
                    {
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    }
                    // Outline the cell with a pen
                }
            }

            if (showHUD)
            {
                PointF point = new Point(0, graphicsPanel1.ClientSize.Height - ((int)font.Size + 15));
                e.Graphics.DrawString("Universe Size: width = " + width + " Height = " + lenght, font, Brushes.Green, point);

                if (boundaryType)
                {
                    point.Y -= font.Size + 8;
                    e.Graphics.DrawString("Boundary Type: Toroidal", font, Brushes.Green, point);
                }
                else
                {
                    point.Y -= font.Size + 8;
                    e.Graphics.DrawString("Boundary Type: Finite", font, Brushes.Green, point);
                }

                point.Y -= font.Size + 8;
                e.Graphics.DrawString("Cell Count: " + aliveCells, font, Brushes.Green, point);

                point.Y -= font.Size + 8;
                e.Graphics.DrawString("Generations: " + generations, font, Brushes.Green, point);
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
            //Starts the timer after the run button is clicked
            timer.Enabled = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Starts to calculate the next generation when the next button is clicked
            NextGeneration();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //Sets the universe to a random state
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
            showHUD = !showHUD;

            graphicsPanel1.Invalidate();
        }

        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Toggels if the nieghbor count will be drawn on the cell
            ShowNeighborCount = !ShowNeighborCount;

            graphicsPanel1.Invalidate();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //clears the universe
            universe = new bool[width, lenght];
            scratchPad = new bool[width, lenght];

            generations = 0;

            graphicsPanel1.Invalidate();
        }

        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sets the count neighbors call to finite
            boundaryType = false;

            graphicsPanel1.Invalidate();
        }

        private void torodialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sets the count neighbors call to toroidal
            boundaryType = true;

            graphicsPanel1.Invalidate();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //pause the games time when the pause btton is clicked
            timer.Enabled = false;
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //starts the timer when the run tool option is clicked
            timer.Enabled = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Saves the current universe to a cell file

            //Create a new save file dialog box
            //set the default state of the new save file dialog box
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;
            dlg.DefaultExt = "cells";

            //check to mkae the sure the dialog opened properly
            if (DialogResult.OK == dlg.ShowDialog())
            {
                //Make a new stream writer using the file name the user gave us
                StreamWriter writer = new StreamWriter(dlg.FileName);

                //Writes a comment to skip while loading
                writer.WriteLine("!This is a cell save file");

                //Interate thuoght the universe on the y postion
                for (int y = 0; y < lenght; y++)
                {
                    //make a new string that is empty by default
                    string row = string.Empty;

                    //Interate thuoght the universe on the y postion
                    for (int x = 0; x < width; x++)
                    {
                        //If cell is alive then we add a O to indicate that the cell is alive
                        if (universe[x, y] == true)
                        {
                            row += "O";
                        }
                        else
                        {
                            //If cell is dead then we add a . to indicate that the cell is dead
                            row += ".";
                        }
                    }

                    //write the new string we just made to a new line in our save file
                    writer.WriteLine(row);
                }

                //Close the file when we aer done writing to the file
                writer.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Craete a new Open file dialog box
            //set the default state of the new open file dialog box
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                //Create a new stream reader using eh file name the user gave us
                StreamReader reader = new StreamReader(dlg.FileName);

                //varablies to keep track of the size of the universe from the file
                int maxWidth = 0;
                int maxHeight = 0;

                while (!reader.EndOfStream)
                {
                    //Make a string that is set the current line the reader is on for the file
                    string row = reader.ReadLine();

                    //if the string starts with a ! the it is a comment nad we skip this row
                    if (row[0] == '!')
                    {
                        continue;
                    }

                    //Increment the height varable so we can count the lenght of the universe height 
                    // from teh amount of rows we read
                    maxHeight++;

                    //Set the width of the unverse to the lenght of the string
                    maxWidth = row.Length;
                }

                //Set out universe width and height to the files universe width and height
                width = maxWidth;
                lenght = maxHeight;

                //Create a new universe and scratch pad to new bool 2d array
                //using the new width and lenght
                universe = new bool[width, lenght];
                scratchPad = new bool[width, lenght];
                
                //Set the readers location to the beginig of the file
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                //An int varable to keep track of what row we are on for the universe
                int y = 0;

                //Loop though and read the file until we are at the end of the file
                while (!reader.EndOfStream)
                {
                    //read the current row we are on then move the reader to the next line
                    string row = reader.ReadLine();
                    
                    //if the first element of the string is a ! then the row is a comment
                    //and we ignore the row
                    if (row[0] == '!')
                    {
                        continue;
                    }

                    //loop though each element of the string
                    for (int x = 0; x < row.Length; x++)
                    {
                        //if the char in the string is a O then we set the corresponding cell 
                        //in the universe to alive/true
                        //if the char is a . then we set the corresponding cell to dead/false
                        if (row[x] == 'O')
                        {
                            universe[x, y] = true;
                        }
                        else
                        {
                            
                            universe[x, y] = false;
                        }
                    }

                    //then we increament y so we can move to the next row in the universe
                    y++;
                }

                generations = 0;

                //Make the graphics panel redraw it's self
                graphicsPanel1.Invalidate();

                //Close the reader once we have read all the rows
                reader.Close();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clears the universe
            universe = new bool[width, lenght];
            scratchPad = new bool[width, lenght];

            timer.Enabled = false;
            generations = 0;

            graphicsPanel1.Invalidate();

        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showGrid = !showGrid;

            graphicsPanel1.Invalidate();
        }
    }
}
