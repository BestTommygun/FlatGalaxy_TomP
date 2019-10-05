namespace FlatGalaxy_TomP.View
{
    partial class KeyBindingsConfig
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
            this.FasterLabel = new System.Windows.Forms.Label();
            this.SlowerLabel = new System.Windows.Forms.Label();
            this.PauseLabel = new System.Windows.Forms.Label();
            this.Back5Label = new System.Windows.Forms.Label();
            this.FasterKey = new System.Windows.Forms.Label();
            this.SlowerKey = new System.Windows.Forms.Label();
            this.PauseKey = new System.Windows.Forms.Label();
            this.GoBack5Key = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "All keybindings:";
            // 
            // FasterLabel
            // 
            this.FasterLabel.AutoSize = true;
            this.FasterLabel.Location = new System.Drawing.Point(13, 45);
            this.FasterLabel.Name = "FasterLabel";
            this.FasterLabel.Size = new System.Drawing.Size(55, 13);
            this.FasterLabel.TabIndex = 1;
            this.FasterLabel.Text = "FASTER: ";
            // 
            // SlowerLabel
            // 
            this.SlowerLabel.AutoSize = true;
            this.SlowerLabel.Location = new System.Drawing.Point(13, 76);
            this.SlowerLabel.Name = "SlowerLabel";
            this.SlowerLabel.Size = new System.Drawing.Size(60, 13);
            this.SlowerLabel.TabIndex = 2;
            this.SlowerLabel.Text = "SLOWER: ";
            // 
            // PauseLabel
            // 
            this.PauseLabel.AutoSize = true;
            this.PauseLabel.Location = new System.Drawing.Point(13, 108);
            this.PauseLabel.Name = "PauseLabel";
            this.PauseLabel.Size = new System.Drawing.Size(46, 13);
            this.PauseLabel.TabIndex = 3;
            this.PauseLabel.Text = "PAUSE:";
            // 
            // Back5Label
            // 
            this.Back5Label.AutoSize = true;
            this.Back5Label.Location = new System.Drawing.Point(13, 142);
            this.Back5Label.Name = "Back5Label";
            this.Back5Label.Size = new System.Drawing.Size(121, 13);
            this.Back5Label.TabIndex = 4;
            this.Back5Label.Text = "GO BACK 5 SECONDS:";
            // 
            // FasterKey
            // 
            this.FasterKey.AutoSize = true;
            this.FasterKey.Location = new System.Drawing.Point(155, 45);
            this.FasterKey.Name = "FasterKey";
            this.FasterKey.Size = new System.Drawing.Size(13, 13);
            this.FasterKey.TabIndex = 5;
            this.FasterKey.Text = "+";
            this.FasterKey.Click += new System.EventHandler(this.FasterKey_Click);
            // 
            // SlowerKey
            // 
            this.SlowerKey.AutoSize = true;
            this.SlowerKey.Location = new System.Drawing.Point(155, 76);
            this.SlowerKey.Name = "SlowerKey";
            this.SlowerKey.Size = new System.Drawing.Size(10, 13);
            this.SlowerKey.TabIndex = 6;
            this.SlowerKey.Text = "-";
            this.SlowerKey.Click += new System.EventHandler(this.SlowerKey_Click);
            // 
            // PauseKey
            // 
            this.PauseKey.AutoSize = true;
            this.PauseKey.Location = new System.Drawing.Point(155, 108);
            this.PauseKey.Name = "PauseKey";
            this.PauseKey.Size = new System.Drawing.Size(64, 13);
            this.PauseKey.TabIndex = 7;
            this.PauseKey.Text = "SPACEBAR";
            this.PauseKey.Click += new System.EventHandler(this.PauseKey_Click);
            // 
            // GoBack5Key
            // 
            this.GoBack5Key.AutoSize = true;
            this.GoBack5Key.Location = new System.Drawing.Point(155, 142);
            this.GoBack5Key.Name = "GoBack5Key";
            this.GoBack5Key.Size = new System.Drawing.Size(14, 13);
            this.GoBack5Key.TabIndex = 8;
            this.GoBack5Key.Text = "A";
            this.GoBack5Key.Click += new System.EventHandler(this.GoBack5Key_Click);
            // 
            // KeyBindingsConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GoBack5Key);
            this.Controls.Add(this.PauseKey);
            this.Controls.Add(this.SlowerKey);
            this.Controls.Add(this.FasterKey);
            this.Controls.Add(this.Back5Label);
            this.Controls.Add(this.PauseLabel);
            this.Controls.Add(this.SlowerLabel);
            this.Controls.Add(this.FasterLabel);
            this.Controls.Add(this.label1);
            this.Name = "KeyBindingsConfig";
            this.Text = "KeyBindingsConfig";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label FasterLabel;
        private System.Windows.Forms.Label SlowerLabel;
        private System.Windows.Forms.Label PauseLabel;
        private System.Windows.Forms.Label Back5Label;
        private System.Windows.Forms.Label FasterKey;
        private System.Windows.Forms.Label SlowerKey;
        private System.Windows.Forms.Label PauseKey;
        private System.Windows.Forms.Label GoBack5Key;
    }
}