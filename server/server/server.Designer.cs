namespace server
{
    partial class Server
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serverStatus1 = new System.Windows.Forms.Label();
            this.serverStatus2 = new System.Windows.Forms.Label();
            this.check = new System.Windows.Forms.Label();
            this.allList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.selectedFile = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serverStatus1
            // 
            this.serverStatus1.AutoSize = true;
            this.serverStatus1.Location = new System.Drawing.Point(7, 6);
            this.serverStatus1.Name = "serverStatus1";
            this.serverStatus1.Size = new System.Drawing.Size(76, 15);
            this.serverStatus1.TabIndex = 0;
            this.serverStatus1.Text = "serverStatus1";
            this.serverStatus1.Click += new System.EventHandler(this.serverStatus1_Click);
            // 
            // serverStatus2
            // 
            this.serverStatus2.AutoSize = true;
            this.serverStatus2.Location = new System.Drawing.Point(7, 21);
            this.serverStatus2.Name = "serverStatus2";
            this.serverStatus2.Size = new System.Drawing.Size(76, 15);
            this.serverStatus2.TabIndex = 1;
            this.serverStatus2.Text = "serverStatus2";
            this.serverStatus2.Click += new System.EventHandler(this.serverStatus2_Click);
            // 
            // check
            // 
            this.check.AutoSize = true;
            this.check.Location = new System.Drawing.Point(527, 85);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(138, 15);
            this.check.TabIndex = 3;
            this.check.Text = "list of downloadable files";
            // 
            // allList
            // 
            this.allList.FormattingEnabled = true;
            this.allList.ItemHeight = 15;
            this.allList.Location = new System.Drawing.Point(20, 103);
            this.allList.Name = "allList";
            this.allList.Size = new System.Drawing.Size(202, 304);
            this.allList.TabIndex = 4;
            this.allList.SelectedIndexChanged += new System.EventHandler(this.allList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "All Files In The Server";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(493, 103);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(210, 304);
            this.listBox1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Available";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // selectedFile
            // 
            this.selectedFile.AutoSize = true;
            this.selectedFile.Location = new System.Drawing.Point(262, 132);
            this.selectedFile.Name = "selectedFile";
            this.selectedFile.Size = new System.Drawing.Size(107, 15);
            this.selectedFile.TabIndex = 8;
            this.selectedFile.Text = "Selected File Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "label3";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectedFile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.allList);
            this.Controls.Add(this.check);
            this.Controls.Add(this.serverStatus2);
            this.Controls.Add(this.serverStatus1);
            this.Name = "Server";
            this.Text = "server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label serverStatus1;
        private Label serverStatus2;
        private Label check;
        private ListBox allList;
        private Label label1;
        private ListBox listBox1;
        private Button button1;
        private Label selectedFile;
        private Label label2;
        private Label label3;
    }
}