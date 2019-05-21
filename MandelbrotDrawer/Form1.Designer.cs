using System.Drawing;

namespace MandelbrotServer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.button_go = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.server_info_textBox = new System.Windows.Forms.TextBox();
            this.button_server = new System.Windows.Forms.Button();
            this.numericUpDownServers = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.animation_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownFrames = new System.Windows.Forms.NumericUpDown();
            this.showAnimationButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrames)).BeginInit();
            this.SuspendLayout();
            // 
            // button_go
            // 
            this.button_go.Location = new System.Drawing.Point(1118, 12);
            this.button_go.Name = "button_go";
            this.button_go.Size = new System.Drawing.Size(75, 23);
            this.button_go.TabIndex = 18;
            this.button_go.Text = "Go!";
            this.button_go.UseVisualStyleBackColor = false;
            this.button_go.Click += new System.EventHandler(this.button_go_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox.Location = new System.Drawing.Point(12, 9);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1100, 660);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 20;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.panel1.Location = new System.Drawing.Point(249, 197);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 23;
            this.panel1.Visible = false;
            // 
            // server_info_textBox
            // 
            this.server_info_textBox.Enabled = false;
            this.server_info_textBox.Location = new System.Drawing.Point(1118, 64);
            this.server_info_textBox.Name = "server_info_textBox";
            this.server_info_textBox.Size = new System.Drawing.Size(133, 20);
            this.server_info_textBox.TabIndex = 24;
            this.server_info_textBox.TextChanged += new System.EventHandler(this.server_info_textBox_TextChanged);
            // 
            // button_server
            // 
            this.button_server.Location = new System.Drawing.Point(1118, 90);
            this.button_server.Name = "button_server";
            this.button_server.Size = new System.Drawing.Size(89, 23);
            this.button_server.TabIndex = 25;
            this.button_server.Text = "Check Servers";
            this.button_server.UseVisualStyleBackColor = true;
            this.button_server.Click += new System.EventHandler(this.button_server_Click);
            // 
            // numericUpDownServers
            // 
            this.numericUpDownServers.Location = new System.Drawing.Point(1119, 140);
            this.numericUpDownServers.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownServers.Name = "numericUpDownServers";
            this.numericUpDownServers.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownServers.TabIndex = 26;
            this.numericUpDownServers.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1175, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "frames/server";
            // 
            // animation_button
            // 
            this.animation_button.Location = new System.Drawing.Point(1119, 234);
            this.animation_button.Name = "animation_button";
            this.animation_button.Size = new System.Drawing.Size(75, 23);
            this.animation_button.TabIndex = 28;
            this.animation_button.Text = "Animate";
            this.animation_button.UseVisualStyleBackColor = true;
            this.animation_button.Click += new System.EventHandler(this.animation_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1175, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "frames/zoom";
            // 
            // numericUpDownFrames
            // 
            this.numericUpDownFrames.Location = new System.Drawing.Point(1119, 183);
            this.numericUpDownFrames.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownFrames.Name = "numericUpDownFrames";
            this.numericUpDownFrames.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownFrames.TabIndex = 29;
            this.numericUpDownFrames.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // showAnimationButton
            // 
            this.showAnimationButton.Enabled = false;
            this.showAnimationButton.Location = new System.Drawing.Point(1200, 234);
            this.showAnimationButton.Name = "showAnimationButton";
            this.showAnimationButton.Size = new System.Drawing.Size(62, 23);
            this.showAnimationButton.TabIndex = 31;
            this.showAnimationButton.Text = "Start";
            this.showAnimationButton.UseVisualStyleBackColor = true;
            this.showAnimationButton.Click += new System.EventHandler(this.showAnimationButton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.showAnimationButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownFrames);
            this.Controls.Add(this.animation_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownServers);
            this.Controls.Add(this.button_server);
            this.Controls.Add(this.server_info_textBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.button_go);
            this.Name = "Form1";
            this.Text = "Mandelbrot set";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrames)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_go;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox server_info_textBox;
        private System.Windows.Forms.Button button_server;
        private System.Windows.Forms.NumericUpDown numericUpDownServers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button animation_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownFrames;
        private System.Windows.Forms.Button showAnimationButton;
        private System.Windows.Forms.Timer timer1;
    }
}

