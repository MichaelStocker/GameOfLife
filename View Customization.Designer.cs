﻿namespace GOLFinal
{
    partial class View_Customization
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.gridColorButton = new System.Windows.Forms.Button();
            this.cellColorBotton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose a Grid Color:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(205, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Choose a Cell Color:";
            // 
            // gridColorButton
            // 
            this.gridColorButton.BackColor = System.Drawing.Color.White;
            this.gridColorButton.ForeColor = System.Drawing.Color.White;
            this.gridColorButton.Location = new System.Drawing.Point(325, 83);
            this.gridColorButton.Name = "gridColorButton";
            this.gridColorButton.Size = new System.Drawing.Size(33, 17);
            this.gridColorButton.TabIndex = 3;
            this.gridColorButton.UseVisualStyleBackColor = false;
            this.gridColorButton.Click += new System.EventHandler(this.gridColorButton_Click);
            // 
            // cellColorBotton
            // 
            this.cellColorBotton.BackColor = System.Drawing.Color.White;
            this.cellColorBotton.Location = new System.Drawing.Point(325, 118);
            this.cellColorBotton.Name = "cellColorBotton";
            this.cellColorBotton.Size = new System.Drawing.Size(33, 17);
            this.cellColorBotton.TabIndex = 3;
            this.cellColorBotton.UseVisualStyleBackColor = false;
            this.cellColorBotton.Click += new System.EventHandler(this.cellColorBotton_Click);
            // 
            // View_Customization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 246);
            this.Controls.Add(this.cellColorBotton);
            this.Controls.Add(this.gridColorButton);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "View_Customization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View_Customization";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button gridColorButton;
        private System.Windows.Forms.Button cellColorBotton;
    }
}