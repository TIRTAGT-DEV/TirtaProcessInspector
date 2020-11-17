namespace TirtaProccessInspector
{
    partial class TirtaProccessInspector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TirtaProccessInspector));
            this.ProccessGrabber = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.GetAllProc = new System.Windows.Forms.Button();
            this.TreeDisplay1 = new System.Windows.Forms.TreeView();
            this.StartMonitoring = new System.Windows.Forms.Button();
            this.ProcNameInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ProccessGrabber
            // 
            this.ProccessGrabber.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ProccessGrabber_DoWork);
            this.ProccessGrabber.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ProccessGrabber_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 1;
            // 
            // GetAllProc
            // 
            this.GetAllProc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GetAllProc.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GetAllProc.Location = new System.Drawing.Point(17, 69);
            this.GetAllProc.Name = "GetAllProc";
            this.GetAllProc.Size = new System.Drawing.Size(124, 31);
            this.GetAllProc.TabIndex = 2;
            this.GetAllProc.Text = "Get All Proccess Data";
            this.GetAllProc.UseVisualStyleBackColor = true;
            this.GetAllProc.Click += new System.EventHandler(this.GetAllProc_Click);
            // 
            // TreeDisplay1
            // 
            this.TreeDisplay1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.TreeDisplay1.Location = new System.Drawing.Point(17, 106);
            this.TreeDisplay1.Name = "TreeDisplay1";
            this.TreeDisplay1.Size = new System.Drawing.Size(470, 211);
            this.TreeDisplay1.TabIndex = 3;
            this.TreeDisplay1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeDisplay1_NodeMouseDoubleClick);
            // 
            // StartMonitoring
            // 
            this.StartMonitoring.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartMonitoring.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartMonitoring.Location = new System.Drawing.Point(147, 69);
            this.StartMonitoring.Name = "StartMonitoring";
            this.StartMonitoring.Size = new System.Drawing.Size(173, 31);
            this.StartMonitoring.TabIndex = 4;
            this.StartMonitoring.Text = "Start Monitoring This : ";
            this.StartMonitoring.UseVisualStyleBackColor = true;
            this.StartMonitoring.Click += new System.EventHandler(this.StartMonitoring_Click);
            // 
            // ProcNameInput
            // 
            this.ProcNameInput.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcNameInput.Location = new System.Drawing.Point(325, 70);
            this.ProcNameInput.MaxLength = 300;
            this.ProcNameInput.Name = "ProcNameInput";
            this.ProcNameInput.Size = new System.Drawing.Size(161, 29);
            this.ProcNameInput.TabIndex = 5;
            // 
            // TirtaProccessInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 329);
            this.Controls.Add(this.ProcNameInput);
            this.Controls.Add(this.StartMonitoring);
            this.Controls.Add(this.TreeDisplay1);
            this.Controls.Add(this.GetAllProc);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TirtaProccessInspector";
            this.Text = "TirtaProccessInspector ( Internally Broken )";
            this.Load += new System.EventHandler(this.TirtaProccessInspector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker ProccessGrabber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button GetAllProc;
        private System.Windows.Forms.TreeView TreeDisplay1;
        private System.Windows.Forms.Button StartMonitoring;
        private System.Windows.Forms.TextBox ProcNameInput;
    }
}

