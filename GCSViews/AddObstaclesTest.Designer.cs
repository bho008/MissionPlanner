﻿namespace MissionPlanner.GCSViews
{
    partial class AddObstaclesTest
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonSDAEnable = new System.Windows.Forms.Button();
            this.testUploadWP = new System.Windows.Forms.Button();
            this.wpUploadStatus = new System.Windows.Forms.TextBox();
            this.drawBufferCheck = new System.Windows.Forms.CheckBox();
            this.buttonEnableEggdrop = new System.Windows.Forms.Button();
            this.drawMissionBounderies = new System.Windows.Forms.Button();
            this.drawLinesCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Change Radius";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "200";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Start/Stop Draw";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonSDAEnable
            // 
            this.buttonSDAEnable.Location = new System.Drawing.Point(12, 344);
            this.buttonSDAEnable.Name = "buttonSDAEnable";
            this.buttonSDAEnable.Size = new System.Drawing.Size(115, 81);
            this.buttonSDAEnable.TabIndex = 3;
            this.buttonSDAEnable.Text = "SDA Enable";
            this.buttonSDAEnable.UseVisualStyleBackColor = true;
            this.buttonSDAEnable.Click += new System.EventHandler(this.buttonSDAEnable_Click);
            // 
            // testUploadWP
            // 
            this.testUploadWP.Location = new System.Drawing.Point(162, 228);
            this.testUploadWP.Name = "testUploadWP";
            this.testUploadWP.Size = new System.Drawing.Size(99, 23);
            this.testUploadWP.TabIndex = 4;
            this.testUploadWP.Text = "Test Upload WP";
            this.testUploadWP.UseVisualStyleBackColor = true;
            this.testUploadWP.Click += new System.EventHandler(this.testUploadWP_Click);
            // 
            // wpUploadStatus
            // 
            this.wpUploadStatus.Location = new System.Drawing.Point(161, 39);
            this.wpUploadStatus.Name = "wpUploadStatus";
            this.wpUploadStatus.Size = new System.Drawing.Size(100, 20);
            this.wpUploadStatus.TabIndex = 5;
            // 
            // drawBufferCheck
            // 
            this.drawBufferCheck.AutoSize = true;
            this.drawBufferCheck.Checked = true;
            this.drawBufferCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawBufferCheck.Location = new System.Drawing.Point(190, 283);
            this.drawBufferCheck.Name = "drawBufferCheck";
            this.drawBufferCheck.Size = new System.Drawing.Size(82, 17);
            this.drawBufferCheck.TabIndex = 6;
            this.drawBufferCheck.Text = "Draw Buffer";
            this.drawBufferCheck.UseVisualStyleBackColor = true;
            this.drawBufferCheck.CheckedChanged += new System.EventHandler(this.drawBufferCheck_CheckedChanged);
            // 
            // buttonEnableEggdrop
            // 
            this.buttonEnableEggdrop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonEnableEggdrop.Location = new System.Drawing.Point(12, 184);
            this.buttonEnableEggdrop.Name = "buttonEnableEggdrop";
            this.buttonEnableEggdrop.Size = new System.Drawing.Size(115, 110);
            this.buttonEnableEggdrop.TabIndex = 7;
            this.buttonEnableEggdrop.Text = "Eggdrop Enable";
            this.buttonEnableEggdrop.UseVisualStyleBackColor = true;
            this.buttonEnableEggdrop.Click += new System.EventHandler(this.buttonEnableEggdrop_Click);
            // 
            // drawMissionBounderies
            // 
            this.drawMissionBounderies.Location = new System.Drawing.Point(162, 85);
            this.drawMissionBounderies.Name = "drawMissionBounderies";
            this.drawMissionBounderies.Size = new System.Drawing.Size(99, 63);
            this.drawMissionBounderies.TabIndex = 8;
            this.drawMissionBounderies.Text = "Draw Mission Boundaries";
            this.drawMissionBounderies.UseVisualStyleBackColor = true;
            this.drawMissionBounderies.Click += new System.EventHandler(this.drawMissionBounderies_Click);
            // 
            // drawLinesCheck
            // 
            this.drawLinesCheck.AutoSize = true;
            this.drawLinesCheck.Checked = true;
            this.drawLinesCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawLinesCheck.Location = new System.Drawing.Point(190, 262);
            this.drawLinesCheck.Name = "drawLinesCheck";
            this.drawLinesCheck.Size = new System.Drawing.Size(79, 17);
            this.drawLinesCheck.TabIndex = 9;
            this.drawLinesCheck.Text = "Draw Lines";
            this.drawLinesCheck.UseVisualStyleBackColor = true;
            this.drawLinesCheck.CheckedChanged += new System.EventHandler(this.drawLinesCheck_CheckedChanged);
            // 
            // AddObstaclesTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 437);
            this.Controls.Add(this.drawLinesCheck);
            this.Controls.Add(this.drawMissionBounderies);
            this.Controls.Add(this.buttonEnableEggdrop);
            this.Controls.Add(this.drawBufferCheck);
            this.Controls.Add(this.wpUploadStatus);
            this.Controls.Add(this.testUploadWP);
            this.Controls.Add(this.buttonSDAEnable);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "AddObstaclesTest";
            this.Text = "UCR UAS Mystery Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonSDAEnable;
        private System.Windows.Forms.Button testUploadWP;
        private System.Windows.Forms.TextBox wpUploadStatus;
        private System.Windows.Forms.CheckBox drawBufferCheck;
        private System.Windows.Forms.Button buttonEnableEggdrop;
        private System.Windows.Forms.Button drawMissionBounderies;
        private System.Windows.Forms.CheckBox drawLinesCheck;
    }
}