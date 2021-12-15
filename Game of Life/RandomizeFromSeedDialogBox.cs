using System;
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
    public partial class RandomizeFromSeedDialogBox : Form
    {
        public RandomizeFromSeedDialogBox()
        {
            InitializeComponent();
        }

        public int GetSeed()
        {
            return (int)numericUpDown1.Value;
        }

        public void SetSeed(int seed)
        {
            numericUpDown1.Value = seed;
        }
    }
}
