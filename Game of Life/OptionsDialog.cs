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
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        public int GetInterval()
        {
            return (int)numericUpDownInterval.Value;
        }

        public void SetInterval(int interval)
        {
            numericUpDownInterval.Value = interval;
        }

        public int GetWidth()
        {
            return (int)numericUpDownWidthOfUniverse.Value;
        }

        public void SetWidth(int width)
        {
            numericUpDownWidthOfUniverse.Value = width;
        }

        public int GetHeight()
        {
            return (int)numericUpDownHeightOfUniverse.Value;
        }

        public void SetHeight(int height)
        {
            numericUpDownHeightOfUniverse.Value = height;
        }
    }
}
