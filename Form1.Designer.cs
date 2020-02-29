namespace NhlDownload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelLeagueNumber = new System.Windows.Forms.Label();
            this.textBoxLeagueNumber = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelLastLeagueNumber = new System.Windows.Forms.Label();
            this.textBoxLastLeagueNumber = new System.Windows.Forms.TextBox();
            this.textBoxLastLeagueYear = new System.Windows.Forms.TextBox();
            this.labelLastLeagueYear = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelLeagueNumber
            // 
            this.labelLeagueNumber.AutoSize = true;
            this.labelLeagueNumber.Location = new System.Drawing.Point(13, 13);
            this.labelLeagueNumber.Name = "labelLeagueNumber";
            this.labelLeagueNumber.Size = new System.Drawing.Size(101, 13);
            this.labelLeagueNumber.TabIndex = 1;
            this.labelLeagueNumber.Text = "Current League ID#";
            // 
            // textBoxLeagueNumber
            // 
            this.textBoxLeagueNumber.Location = new System.Drawing.Point(120, 10);
            this.textBoxLeagueNumber.Name = "textBoxLeagueNumber";
            this.textBoxLeagueNumber.Size = new System.Drawing.Size(89, 20);
            this.textBoxLeagueNumber.TabIndex = 0;
            this.textBoxLeagueNumber.TextChanged += new System.EventHandler(this.TextBoxLeagueNumber_Changed);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 93);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(35, 13);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "label1";
            // 
            // buttonDownload
            // 
            this.buttonDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDownload.Location = new System.Drawing.Point(209, 109);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(129, 23);
            this.buttonDownload.TabIndex = 3;
            this.buttonDownload.Text = "Download League Data";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 109);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(171, 23);
            this.progressBar1.Step = 20;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 6;
            // 
            // labelLastLeagueNumber
            // 
            this.labelLastLeagueNumber.AutoSize = true;
            this.labelLastLeagueNumber.Location = new System.Drawing.Point(13, 39);
            this.labelLastLeagueNumber.Name = "labelLastLeagueNumber";
            this.labelLastLeagueNumber.Size = new System.Drawing.Size(87, 13);
            this.labelLastLeagueNumber.TabIndex = 7;
            this.labelLastLeagueNumber.Text = "Last League ID#";
            // 
            // textBoxLastLeagueNumber
            // 
            this.textBoxLastLeagueNumber.Location = new System.Drawing.Point(120, 36);
            this.textBoxLastLeagueNumber.Name = "textBoxLastLeagueNumber";
            this.textBoxLastLeagueNumber.Size = new System.Drawing.Size(89, 20);
            this.textBoxLastLeagueNumber.TabIndex = 1;
            this.textBoxLastLeagueNumber.TextChanged += new System.EventHandler(this.textBoxLastLeagueNumber_TextChanged);
            // 
            // textBoxLastLeagueYear
            // 
            this.textBoxLastLeagueYear.Location = new System.Drawing.Point(120, 62);
            this.textBoxLastLeagueYear.Name = "textBoxLastLeagueYear";
            this.textBoxLastLeagueYear.Size = new System.Drawing.Size(89, 20);
            this.textBoxLastLeagueYear.TabIndex = 2;
            this.textBoxLastLeagueYear.TextChanged += new System.EventHandler(this.textBoxLastLeagueYear_TextChanged);
            // 
            // labelLastLeagueYear
            // 
            this.labelLastLeagueYear.AutoSize = true;
            this.labelLastLeagueYear.Location = new System.Drawing.Point(13, 65);
            this.labelLastLeagueYear.Name = "labelLastLeagueYear";
            this.labelLastLeagueYear.Size = new System.Drawing.Size(91, 13);
            this.labelLastLeagueYear.TabIndex = 10;
            this.labelLastLeagueYear.Text = "Last League Year";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 144);
            this.Controls.Add(this.labelLastLeagueYear);
            this.Controls.Add(this.textBoxLastLeagueYear);
            this.Controls.Add(this.textBoxLastLeagueNumber);
            this.Controls.Add(this.labelLastLeagueNumber);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.textBoxLeagueNumber);
            this.Controls.Add(this.labelLeagueNumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Yahoo! Fantasy NHL Downloader v0.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelLeagueNumber;
        private System.Windows.Forms.TextBox textBoxLeagueNumber;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelLastLeagueNumber;
        private System.Windows.Forms.TextBox textBoxLastLeagueNumber;
        private System.Windows.Forms.TextBox textBoxLastLeagueYear;
        private System.Windows.Forms.Label labelLastLeagueYear;
    }
}

