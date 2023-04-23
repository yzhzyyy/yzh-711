namespace cache
{
    partial class Cache
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
            this.cacheStatus1 = new System.Windows.Forms.Label();
            this.cacheStatus2 = new System.Windows.Forms.Label();
            this.cacheStatus3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.delete = new System.Windows.Forms.Button();
            this.deleteStatus = new System.Windows.Forms.Label();
            this.cacheLog = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cacheStatus1
            // 
            this.cacheStatus1.AutoSize = true;
            this.cacheStatus1.Location = new System.Drawing.Point(12, 7);
            this.cacheStatus1.Name = "cacheStatus1";
            this.cacheStatus1.Size = new System.Drawing.Size(76, 15);
            this.cacheStatus1.TabIndex = 1;
            this.cacheStatus1.Text = "cacheStatus1";
            this.cacheStatus1.Click += new System.EventHandler(this.cacheStatus1_Click);
            // 
            // cacheStatus2
            // 
            this.cacheStatus2.AutoSize = true;
            this.cacheStatus2.Location = new System.Drawing.Point(12, 22);
            this.cacheStatus2.Name = "cacheStatus2";
            this.cacheStatus2.Size = new System.Drawing.Size(76, 15);
            this.cacheStatus2.TabIndex = 2;
            this.cacheStatus2.Text = "cacheStatus2";
            this.cacheStatus2.Click += new System.EventHandler(this.cacheStatus2_Click);
            // 
            // cacheStatus3
            // 
            this.cacheStatus3.AutoSize = true;
            this.cacheStatus3.Location = new System.Drawing.Point(12, 37);
            this.cacheStatus3.Name = "cacheStatus3";
            this.cacheStatus3.Size = new System.Drawing.Size(76, 15);
            this.cacheStatus3.TabIndex = 3;
            this.cacheStatus3.Text = "cacheStatus3";
            this.cacheStatus3.Click += new System.EventHandler(this.cacheStatus3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "file status";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(12, 70);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(122, 23);
            this.delete.TabIndex = 5;
            this.delete.Text = "delete";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // deleteStatus
            // 
            this.deleteStatus.AutoSize = true;
            this.deleteStatus.Location = new System.Drawing.Point(140, 74);
            this.deleteStatus.Name = "deleteStatus";
            this.deleteStatus.Size = new System.Drawing.Size(73, 15);
            this.deleteStatus.TabIndex = 6;
            this.deleteStatus.Text = "delete status";
            // 
            // cacheLog
            // 
            this.cacheLog.AutoSize = true;
            this.cacheLog.Location = new System.Drawing.Point(13, 112);
            this.cacheLog.Name = "cacheLog";
            this.cacheLog.Size = new System.Drawing.Size(63, 15);
            this.cacheLog.TabIndex = 7;
            this.cacheLog.Text = "Cache Log";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 267);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Cache List";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(12, 130);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(695, 124);
            this.listBox1.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "label2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 287);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(692, 136);
            this.textBox1.TabIndex = 12;
            // 
            // Cache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cacheLog);
            this.Controls.Add(this.deleteStatus);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cacheStatus3);
            this.Controls.Add(this.cacheStatus2);
            this.Controls.Add(this.cacheStatus1);
            this.Name = "Cache";
            this.Text = "cache";
            this.Load += new System.EventHandler(this.cache_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label cacheStatus1;
        private Label cacheStatus2;
        private Label cacheStatus3;
        private Label label1;
        private Button delete;
        private Label deleteStatus;
        private Label cacheLog;
        private ListBox logList;
        private Label label3;
        private ListBox listBox1;
        private Label label2;
        private TextBox textBox1;
    }
}