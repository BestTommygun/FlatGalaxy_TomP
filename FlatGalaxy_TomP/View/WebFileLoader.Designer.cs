namespace FlatGalaxy_TomP.View
{
    partial class WebFileLoader
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
            this.Description = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Description
            // 
            this.Description.AutoSize = true;
            this.Description.Location = new System.Drawing.Point(13, 13);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(68, 13);
            this.Description.TabIndex = 0;
            this.Description.Text = "Web Adress:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(312, 20);
            this.textBox1.TabIndex = 1;
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(334, 30);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 20);
            this.LoadButton.TabIndex = 2;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // WebFileLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 76);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Description);
            this.Name = "WebFileLoader";
            this.Text = "WebFileLoader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Description;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button LoadButton;
    }
}