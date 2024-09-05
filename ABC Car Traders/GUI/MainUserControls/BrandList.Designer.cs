namespace ABC_Car_Traders.GUI.MainUserControls
{
    partial class BrandList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnelbrands = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.lblbrandname = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.imgbrand = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pnelbrands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgbrand)).BeginInit();
            this.SuspendLayout();
            // 
            // pnelbrands
            // 
            this.pnelbrands.BackColor = System.Drawing.Color.Transparent;
            this.pnelbrands.Controls.Add(this.lblbrandname);
            this.pnelbrands.Controls.Add(this.imgbrand);
            this.pnelbrands.FillColor = System.Drawing.Color.White;
            this.pnelbrands.Location = new System.Drawing.Point(-4, 0);
            this.pnelbrands.Name = "pnelbrands";
            this.pnelbrands.ShadowColor = System.Drawing.Color.Black;
            this.pnelbrands.Size = new System.Drawing.Size(147, 101);
            this.pnelbrands.TabIndex = 0;
            // 
            // lblbrandname
            // 
            this.lblbrandname.BackColor = System.Drawing.Color.Transparent;
            this.lblbrandname.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbrandname.Location = new System.Drawing.Point(56, 69);
            this.lblbrandname.Name = "lblbrandname";
            this.lblbrandname.Size = new System.Drawing.Size(34, 18);
            this.lblbrandname.TabIndex = 3;
            this.lblbrandname.Text = "Audi";
            // 
            // imgbrand
            // 
            this.imgbrand.FillColor = System.Drawing.Color.Transparent;
            this.imgbrand.ImageRotate = 0F;
            this.imgbrand.Location = new System.Drawing.Point(13, 11);
            this.imgbrand.Name = "imgbrand";
            this.imgbrand.Size = new System.Drawing.Size(122, 62);
            this.imgbrand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgbrand.TabIndex = 0;
            this.imgbrand.TabStop = false;
            // 
            // BrandList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnelbrands);
            this.Name = "BrandList";
            this.Size = new System.Drawing.Size(144, 101);
            this.pnelbrands.ResumeLayout(false);
            this.pnelbrands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgbrand)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel pnelbrands;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblbrandname;
        private Guna.UI2.WinForms.Guna2PictureBox imgbrand;
    }
}
