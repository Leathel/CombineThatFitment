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
            this.SuspendLayout();
            // 
            // readFileTextBox
            // 
            this.readFileTextBox.Location = new System.Drawing.Point(125, 50);
            this.readFileTextBox.Name = "readFileTextBox";
            this.readFileTextBox.Size = new System.Drawing.Size(256, 20);
            this.readFileTextBox.TabIndex = 1;
            this.readFileTextBox.Text = "C:\\Users\\Ben\\Documents\\ShopEddies\\fitment\\Magento_fitment.csv";
            this.readFileTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sku and Fitment";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(169, 200);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(114, 23);
            this.readButton.TabIndex = 3;
            this.readButton.Text = "Run";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Vehicle List";
            // 
            // writeFileTextBox
            // 
            this.writeFileTextBox.Location = new System.Drawing.Point(125, 97);
            this.writeFileTextBox.Name = "writeFileTextBox";
            this.writeFileTextBox.Size = new System.Drawing.Size(256, 20);
            this.writeFileTextBox.TabIndex = 5;
            this.writeFileTextBox.Text = "C:\\Users\\Ben\\Documents\\ShopEddies\\fitment\\ebayFITMENT.csv";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Output File Path";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // newFileNameTextBox
            // 
            this.newFileNameTextBox.Location = new System.Drawing.Point(125, 149);
            this.newFileNameTextBox.Name = "newFileNameTextBox";
            this.newFileNameTextBox.Size = new System.Drawing.Size(256, 20);
            this.newFileNameTextBox.TabIndex = 11;
            this.newFileNameTextBox.Text = "C:\\Users\\Ben\\Documents\\ShopEddies\\fitment\\testing\\output.csv";
            this.newFileNameTextBox.TextChanged += new System.EventHandler(this.newFileNameTextBox_TextChanged);
            // 
            // CombineFitment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 289);
            this.Controls.Add(this.newFileNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.writeFileTextBox);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.readFileTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CombineFitment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
    }
}

