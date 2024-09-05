namespace ABC_Car_Traders.GUI
{
    partial class ReportViwer
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
            this.btngeneraetreport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btngeneraetreport
            // 
            this.btngeneraetreport.Location = new System.Drawing.Point(163, 198);
            this.btngeneraetreport.Name = "btngeneraetreport";
            this.btngeneraetreport.Size = new System.Drawing.Size(284, 89);
            this.btngeneraetreport.TabIndex = 0;
            this.btngeneraetreport.Text = "Generate";
            this.btngeneraetreport.UseVisualStyleBackColor = true;
            this.btngeneraetreport.Click += new System.EventHandler(this.btngeneraetreport_Click);
            // 
            // ReportViwer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 504);
            this.Controls.Add(this.btngeneraetreport);
            this.Name = "ReportViwer";
            this.Text = "ReportViwer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btngeneraetreport;
    }
}