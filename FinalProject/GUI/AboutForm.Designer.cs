namespace FinalProject.GUI
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.buttonCloseAbout = new System.Windows.Forms.Button();
            this.txtAbout = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonCloseAbout
            // 
            this.buttonCloseAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCloseAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCloseAbout.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonCloseAbout.Image = ((System.Drawing.Image)(resources.GetObject("buttonCloseAbout.Image")));
            this.buttonCloseAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCloseAbout.Location = new System.Drawing.Point(205, 232);
            this.buttonCloseAbout.Name = "buttonCloseAbout";
            this.buttonCloseAbout.Size = new System.Drawing.Size(72, 45);
            this.buttonCloseAbout.TabIndex = 0;
            this.buttonCloseAbout.Text = "Close";
            this.buttonCloseAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCloseAbout.UseVisualStyleBackColor = true;
            this.buttonCloseAbout.Click += new System.EventHandler(this.buttonCloseAbout_Click);
            // 
            // txtAbout
            // 
            this.txtAbout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtAbout.Location = new System.Drawing.Point(11, 80);
            this.txtAbout.Multiline = true;
            this.txtAbout.Name = "txtAbout";
            this.txtAbout.Size = new System.Drawing.Size(266, 146);
            this.txtAbout.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(11, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 65);
            this.panel1.TabIndex = 2;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(289, 284);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtAbout);
            this.Controls.Add(this.buttonCloseAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AboutForm";
            this.Text = "About Us";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCloseAbout;
        private System.Windows.Forms.TextBox txtAbout;
        private System.Windows.Forms.Panel panel1;
    }
}