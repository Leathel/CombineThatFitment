namespace CombineFitment
{
    partial class CombineFitment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombineFitment));
            this.readFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.readButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.writeFileTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newFileNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.loadingBar = new System.Windows.Forms.ProgressBar();
            this.estTimeRemainingLabel = new System.Windows.Forms.Label();
            this.timeRemaining = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // readFileTextBox
            // 
            this.readFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.readFileTextBox.Location = new System.Drawing.Point(63, 39);
            this.readFileTextBox.Name = "readFileTextBox";
            this.readFileTextBox.Size = new System.Drawing.Size(178, 20);
            this.readFileTextBox.TabIndex = 1;
            this.readFileTextBox.Text = "C:\\Users\\Ben\\Documents\\ShopEddies\\fitment\\Magento_fitment.csv";
            this.readFileTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File Path";
            // 
            // readButton
            // 
            this.readButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.readButton.Location = new System.Drawing.Point(247, 39);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(112, 23);
            this.readButton.TabIndex = 3;
            this.readButton.Text = "Read";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "File Path";
            // 
            // writeFileTextBox
            // 
            this.writeFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.writeFileTextBox.Location = new System.Drawing.Point(432, 39);
            this.writeFileTextBox.Name = "writeFileTextBox";
            this.writeFileTextBox.Size = new System.Drawing.Size(178, 20);
            this.writeFileTextBox.TabIndex = 5;
            this.writeFileTextBox.Text = "C:\\Users\\Ben\\Documents\\ShopEddies\\fitment\\ebayFITMENT.csv";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(769, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Result";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // newFileNameTextBox
            // 
            this.newFileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newFileNameTextBox.Location = new System.Drawing.Point(812, 36);
            this.newFileNameTextBox.Name = "newFileNameTextBox";
            this.newFileNameTextBox.Size = new System.Drawing.Size(256, 20);
            this.newFileNameTextBox.TabIndex = 11;
            this.newFileNameTextBox.Text = "C:\\Users\\Ben\\Documents\\ShopEddies\\fitment\\testing\\output.csv";
            this.newFileNameTextBox.TextChanged += new System.EventHandler(this.newFileNameTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(890, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "New File Name";
            // 
            // loadingBar
            // 
            this.loadingBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadingBar.BackColor = System.Drawing.Color.DarkRed;
            this.loadingBar.ForeColor = System.Drawing.Color.Aqua;
            this.loadingBar.Location = new System.Drawing.Point(27, 477);
            this.loadingBar.Name = "loadingBar";
            this.loadingBar.Size = new System.Drawing.Size(702, 23);
            this.loadingBar.TabIndex = 13;
            // 
            // estTimeRemainingLabel
            // 
            this.estTimeRemainingLabel.AutoSize = true;
            this.estTimeRemainingLabel.Location = new System.Drawing.Point(769, 477);
            this.estTimeRemainingLabel.Name = "estTimeRemainingLabel";
            this.estTimeRemainingLabel.Size = new System.Drawing.Size(155, 13);
            this.estTimeRemainingLabel.TabIndex = 14;
            this.estTimeRemainingLabel.Text = "Estimated Time of Completion:  ";
            this.estTimeRemainingLabel.Click += new System.EventHandler(this.label5_Click);
            // 
            // timeRemaining
            // 
            this.timeRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.timeRemaining.AutoSize = true;
            this.timeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeRemaining.Location = new System.Drawing.Point(919, 477);
            this.timeRemaining.Name = "timeRemaining";
            this.timeRemaining.Size = new System.Drawing.Size(95, 20);
            this.timeRemaining.TabIndex = 15;
            this.timeRemaining.Text = "0.0 seconds";
            // 
            // CombineFitment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 512);
            this.Controls.Add(this.timeRemaining);
            this.Controls.Add(this.estTimeRemainingLabel);
            this.Controls.Add(this.loadingBar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.newFileNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.writeFileTextBox);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.readFileTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CombineFitment";
            this.Text = "Combine Fitment";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox readFileTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox writeFileTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox newFileNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar loadingBar;
        private System.Windows.Forms.Label estTimeRemainingLabel;
        private System.Windows.Forms.Label timeRemaining;
    }
}

