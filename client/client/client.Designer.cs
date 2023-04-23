namespace client
{
    partial class Client
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
            clientStatus = new Label();
            clientStatus1 = new Label();
            showFile = new Button();
            listBox1 = new ListBox();
            downLoad = new Button();
            label1 = new Label();
            downloadFile = new Label();
            check = new Label();
            filecontext = new TextBox();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // clientStatus
            // 
            clientStatus.AutoSize = true;
            clientStatus.Location = new Point(15, 63);
            clientStatus.Margin = new Padding(4, 0, 4, 0);
            clientStatus.Name = "clientStatus";
            clientStatus.Size = new Size(94, 20);
            clientStatus.TabIndex = 0;
            clientStatus.Text = "clientStatus";
            clientStatus.Click += clientStatus_Click;
            // 
            // clientStatus1
            // 
            clientStatus1.AutoSize = true;
            clientStatus1.Location = new Point(15, 43);
            clientStatus1.Margin = new Padding(4, 0, 4, 0);
            clientStatus1.Name = "clientStatus1";
            clientStatus1.Size = new Size(103, 20);
            clientStatus1.TabIndex = 1;
            clientStatus1.Text = "clientStatus1";
            clientStatus1.Click += clientStatus1_Click;
            // 
            // showFile
            // 
            showFile.Location = new Point(15, 87);
            showFile.Margin = new Padding(4, 4, 4, 4);
            showFile.Name = "showFile";
            showFile.Size = new Size(96, 31);
            showFile.TabIndex = 2;
            showFile.Text = "show file";
            showFile.UseVisualStyleBackColor = true;
            showFile.Click += showFile_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 20;
            listBox1.Location = new Point(15, 125);
            listBox1.Margin = new Padding(4, 4, 4, 4);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(265, 144);
            listBox1.TabIndex = 4;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // downLoad
            // 
            downLoad.Location = new Point(15, 299);
            downLoad.Margin = new Padding(4, 4, 4, 4);
            downLoad.Name = "downLoad";
            downLoad.Size = new Size(96, 31);
            downLoad.TabIndex = 5;
            downLoad.Text = "download";
            downLoad.UseVisualStyleBackColor = true;
            downLoad.Click += downLoad_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(120, 97);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(209, 20);
            label1.TabIndex = 6;
            label1.Text = "(select all files in the server)";
            label1.Click += label1_Click;
            // 
            // downloadFile
            // 
            downloadFile.AutoSize = true;
            downloadFile.Location = new Point(15, 275);
            downloadFile.Margin = new Padding(4, 0, 4, 0);
            downloadFile.Name = "downloadFile";
            downloadFile.Size = new Size(561, 20);
            downloadFile.TabIndex = 7;
            downloadFile.Text = "choose a file in above box and click \"download\" button to download the file";
            downloadFile.Click += downloadFile_Click;
            // 
            // check
            // 
            check.AutoSize = true;
            check.Location = new Point(120, 304);
            check.Margin = new Padding(4, 0, 4, 0);
            check.Name = "check";
            check.Size = new Size(51, 20);
            check.TabIndex = 8;
            check.Text = "check";
            // 
            // filecontext
            // 
            filecontext.Location = new Point(15, 337);
            filecontext.Margin = new Padding(4, 4, 4, 4);
            filecontext.Multiline = true;
            filecontext.Name = "filecontext";
            filecontext.ScrollBars = ScrollBars.Vertical;
            filecontext.Size = new Size(1001, 204);
            filecontext.TabIndex = 10;
            // 
            // button1
            // 
            button1.Location = new Point(641, 74);
            button1.Name = "button1";
            button1.Size = new Size(322, 29);
            button1.TabIndex = 11;
            button1.Text = "down load image";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(604, 118);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(399, 206);
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // Client
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 600);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(filecontext);
            Controls.Add(check);
            Controls.Add(downloadFile);
            Controls.Add(label1);
            Controls.Add(downLoad);
            Controls.Add(listBox1);
            Controls.Add(showFile);
            Controls.Add(clientStatus1);
            Controls.Add(clientStatus);
            Margin = new Padding(4, 4, 4, 4);
            Name = "Client";
            Text = "client";
            Load += Client_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label clientStatus;
        private Label clientStatus1;
        private Button showFile;
        private ListBox listBox1;
        private Button downLoad;
        private Label label1;
        private Label downloadFile;
        private Label check;
        private Label jcaioj;
        private TextBox filecontext;
        private Button button1;
        private PictureBox pictureBox1;
    }
}