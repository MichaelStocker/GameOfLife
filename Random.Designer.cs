﻿namespace GOLFinal
{
    partial class RandomSeed
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
            this.Seed = new System.Windows.Forms.NumericUpDown();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Seed)).BeginInit();
            this.SuspendLayout();
            // 
            // Seed
            // 
            this.Seed.Location = new System.Drawing.Point(224, 104);
            this.Seed.Name = "Seed";
            this.Seed.Size = new System.Drawing.Size(120, 20);
            this.Seed.TabIndex = 0;
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(205, 211);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(286, 211);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Random
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 246);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Seed);
            this.MaximizeBox = false;
            this.Name = "Random";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Randomize By Seed";
            ((System.ComponentModel.ISupportInitialize)(this.Seed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown Seed;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
    }
}