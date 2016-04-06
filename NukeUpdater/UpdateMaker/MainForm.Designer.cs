namespace UpdateMaker
{
    partial class MainForm
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
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_Found = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnMake = new System.Windows.Forms.Button();
            this.txtProj = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(12, 32);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(419, 29);
            this.txtFolder.TabIndex = 0;
            this.txtFolder.Text = "C:\\nuke";
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.Location = new System.Drawing.Point(437, 32);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(96, 29);
            this.btn_Search.TabIndex = 1;
            this.btn_Search.Text = "...";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Root Folder Path";
            // 
            // label_Found
            // 
            this.label_Found.AutoSize = true;
            this.label_Found.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label_Found.Location = new System.Drawing.Point(12, 64);
            this.label_Found.Name = "label_Found";
            this.label_Found.Size = new System.Drawing.Size(161, 19);
            this.label_Found.TabIndex = 3;
            this.label_Found.Text = "Waiting for project folder";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(12, 86);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(521, 410);
            this.listBox1.TabIndex = 4;
            // 
            // btnMake
            // 
            this.btnMake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMake.Enabled = false;
            this.btnMake.Location = new System.Drawing.Point(416, 502);
            this.btnMake.Name = "btnMake";
            this.btnMake.Size = new System.Drawing.Size(117, 36);
            this.btnMake.TabIndex = 5;
            this.btnMake.Text = "Make Update";
            this.btnMake.UseVisualStyleBackColor = true;
            this.btnMake.Click += new System.EventHandler(this.btnMake_Click);
            // 
            // txtProj
            // 
            this.txtProj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProj.Location = new System.Drawing.Point(12, 507);
            this.txtProj.Name = "txtProj";
            this.txtProj.Size = new System.Drawing.Size(398, 29);
            this.txtProj.TabIndex = 6;
            this.txtProj.Text = "Project Name";
            this.txtProj.TextChanged += new System.EventHandler(this.txtProj_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 550);
            this.Controls.Add(this.txtProj);
            this.Controls.Add(this.btnMake);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label_Found);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.txtFolder);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Nuke Updater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_Found;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnMake;
        private System.Windows.Forms.TextBox txtProj;
    }
}

