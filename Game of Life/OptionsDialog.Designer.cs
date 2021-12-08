
namespace Game_of_Life
{
    partial class OptionsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelInteral = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWidthOfUniverse = new System.Windows.Forms.NumericUpDown();
            this.labelUniverseWidth = new System.Windows.Forms.Label();
            this.numericUpDownHeightOfUniverse = new System.Windows.Forms.NumericUpDown();
            this.labelHeightOfUniverse = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidthOfUniverse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeightOfUniverse)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(12, 278);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(158, 278);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelInteral
            // 
            this.labelInteral.AutoSize = true;
            this.labelInteral.Location = new System.Drawing.Point(9, 65);
            this.labelInteral.Name = "labelInteral";
            this.labelInteral.Size = new System.Drawing.Size(140, 13);
            this.labelInteral.TabIndex = 2;
            this.labelInteral.Text = "Timer interval in milliseconds";
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Location = new System.Drawing.Point(169, 63);
            this.numericUpDownInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownInterval.TabIndex = 3;
            // 
            // numericUpDownWidthOfUniverse
            // 
            this.numericUpDownWidthOfUniverse.Location = new System.Drawing.Point(169, 97);
            this.numericUpDownWidthOfUniverse.Name = "numericUpDownWidthOfUniverse";
            this.numericUpDownWidthOfUniverse.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownWidthOfUniverse.TabIndex = 4;
            // 
            // labelUniverseWidth
            // 
            this.labelUniverseWidth.AutoSize = true;
            this.labelUniverseWidth.Location = new System.Drawing.Point(9, 99);
            this.labelUniverseWidth.Name = "labelUniverseWidth";
            this.labelUniverseWidth.Size = new System.Drawing.Size(145, 13);
            this.labelUniverseWidth.TabIndex = 5;
            this.labelUniverseWidth.Text = "Width of the Universe in cells";
            // 
            // numericUpDownHeightOfUniverse
            // 
            this.numericUpDownHeightOfUniverse.Location = new System.Drawing.Point(169, 131);
            this.numericUpDownHeightOfUniverse.Name = "numericUpDownHeightOfUniverse";
            this.numericUpDownHeightOfUniverse.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownHeightOfUniverse.TabIndex = 6;
            // 
            // labelHeightOfUniverse
            // 
            this.labelHeightOfUniverse.AutoSize = true;
            this.labelHeightOfUniverse.Location = new System.Drawing.Point(9, 133);
            this.labelHeightOfUniverse.Name = "labelHeightOfUniverse";
            this.labelHeightOfUniverse.Size = new System.Drawing.Size(148, 13);
            this.labelHeightOfUniverse.TabIndex = 7;
            this.labelHeightOfUniverse.Text = "Height of the Universe in cells";
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(245, 313);
            this.Controls.Add(this.labelHeightOfUniverse);
            this.Controls.Add(this.numericUpDownHeightOfUniverse);
            this.Controls.Add(this.labelUniverseWidth);
            this.Controls.Add(this.numericUpDownWidthOfUniverse);
            this.Controls.Add(this.numericUpDownInterval);
            this.Controls.Add(this.labelInteral);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidthOfUniverse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeightOfUniverse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelInteral;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownWidthOfUniverse;
        private System.Windows.Forms.Label labelUniverseWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeightOfUniverse;
        private System.Windows.Forms.Label labelHeightOfUniverse;
    }
}