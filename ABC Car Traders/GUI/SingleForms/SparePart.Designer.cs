namespace ABC_Car_Traders.GUI.SingleForms
{
    partial class SparePart
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SparePart));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.lblpartdiscription = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnaddtocartpart = new Guna.UI2.WinForms.Guna2CircleButton();
            this.lblpartprice = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnordernowpart = new Guna.UI2.WinForms.Guna2Button();
            this.lblpartname = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.imgimagepart = new Guna.UI2.WinForms.Guna2PictureBox();
            this.conboxclose = new Guna.UI2.WinForms.Guna2ControlBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgimagepart)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // lblpartdiscription
            // 
            this.lblpartdiscription.BackColor = System.Drawing.Color.Transparent;
            this.lblpartdiscription.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpartdiscription.Location = new System.Drawing.Point(16, 483);
            this.lblpartdiscription.Name = "lblpartdiscription";
            this.lblpartdiscription.Size = new System.Drawing.Size(240, 19);
            this.lblpartdiscription.TabIndex = 31;
            this.lblpartdiscription.Text = "engine delivers 621 hp and 738 lb-ft. of...\r\n";
            // 
            // btnaddtocartpart
            // 
            this.btnaddtocartpart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnaddtocartpart.BackColor = System.Drawing.Color.Transparent;
            this.btnaddtocartpart.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnaddtocartpart.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnaddtocartpart.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnaddtocartpart.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnaddtocartpart.FillColor = System.Drawing.Color.WhiteSmoke;
            this.btnaddtocartpart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnaddtocartpart.ForeColor = System.Drawing.Color.White;
            this.btnaddtocartpart.Image = ((System.Drawing.Image)(resources.GetObject("btnaddtocartpart.Image")));
            this.btnaddtocartpart.Location = new System.Drawing.Point(613, 68);
            this.btnaddtocartpart.Name = "btnaddtocartpart";
            this.btnaddtocartpart.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnaddtocartpart.Size = new System.Drawing.Size(41, 41);
            this.btnaddtocartpart.TabIndex = 26;
            this.btnaddtocartpart.UseTransparentBackground = true;
            this.btnaddtocartpart.Click += new System.EventHandler(this.btnaddtocartpart_Click);
            // 
            // lblpartprice
            // 
            this.lblpartprice.BackColor = System.Drawing.Color.Transparent;
            this.lblpartprice.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpartprice.Location = new System.Drawing.Point(582, 454);
            this.lblpartprice.Name = "lblpartprice";
            this.lblpartprice.Size = new System.Drawing.Size(75, 25);
            this.lblpartprice.TabIndex = 29;
            this.lblpartprice.Text = "$42,000";
            // 
            // btnordernowpart
            // 
            this.btnordernowpart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnordernowpart.BackColor = System.Drawing.Color.Transparent;
            this.btnordernowpart.BorderRadius = 15;
            this.btnordernowpart.CustomBorderColor = System.Drawing.Color.Transparent;
            this.btnordernowpart.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnordernowpart.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnordernowpart.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnordernowpart.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnordernowpart.FocusedColor = System.Drawing.Color.Transparent;
            this.btnordernowpart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnordernowpart.ForeColor = System.Drawing.Color.White;
            this.btnordernowpart.Location = new System.Drawing.Point(28, 75);
            this.btnordernowpart.Name = "btnordernowpart";
            this.btnordernowpart.Size = new System.Drawing.Size(103, 31);
            this.btnordernowpart.TabIndex = 27;
            this.btnordernowpart.Text = "Order Now";
            this.btnordernowpart.UseTransparentBackground = true;
            this.btnordernowpart.Click += new System.EventHandler(this.btnordernowpart_Click);
            // 
            // lblpartname
            // 
            this.lblpartname.BackColor = System.Drawing.Color.Transparent;
            this.lblpartname.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpartname.Location = new System.Drawing.Point(15, 456);
            this.lblpartname.Name = "lblpartname";
            this.lblpartname.Size = new System.Drawing.Size(273, 21);
            this.lblpartname.TabIndex = 28;
            this.lblpartname.Text = "Mercedes-AMG G65 final edition";
            // 
            // imgimagepart
            // 
            this.imgimagepart.BackColor = System.Drawing.Color.Transparent;
            this.imgimagepart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imgimagepart.BorderRadius = 15;
            this.imgimagepart.Image = ((System.Drawing.Image)(resources.GetObject("imgimagepart.Image")));
            this.imgimagepart.ImageRotate = 0F;
            this.imgimagepart.Location = new System.Drawing.Point(15, 58);
            this.imgimagepart.Name = "imgimagepart";
            this.imgimagepart.Size = new System.Drawing.Size(648, 377);
            this.imgimagepart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgimagepart.TabIndex = 25;
            this.imgimagepart.TabStop = false;
            this.imgimagepart.UseTransparentBackground = true;
            // 
            // conboxclose
            // 
            this.conboxclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.conboxclose.BackColor = System.Drawing.Color.Transparent;
            this.conboxclose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("conboxclose.BackgroundImage")));
            this.conboxclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.conboxclose.FillColor = System.Drawing.Color.Transparent;
            this.conboxclose.IconColor = System.Drawing.Color.Black;
            this.conboxclose.Location = new System.Drawing.Point(644, 12);
            this.conboxclose.Name = "conboxclose";
            this.conboxclose.Size = new System.Drawing.Size(45, 28);
            this.conboxclose.TabIndex = 30;
            // 
            // SparePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 527);
            this.Controls.Add(this.conboxclose);
            this.Controls.Add(this.lblpartdiscription);
            this.Controls.Add(this.btnaddtocartpart);
            this.Controls.Add(this.lblpartprice);
            this.Controls.Add(this.btnordernowpart);
            this.Controls.Add(this.lblpartname);
            this.Controls.Add(this.imgimagepart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SparePart";
            this.Text = "SpareParts";
            ((System.ComponentModel.ISupportInitialize)(this.imgimagepart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblpartdiscription;
        private Guna.UI2.WinForms.Guna2CircleButton btnaddtocartpart;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblpartprice;
        private Guna.UI2.WinForms.Guna2Button btnordernowpart;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblpartname;
        private Guna.UI2.WinForms.Guna2PictureBox imgimagepart;
        private Guna.UI2.WinForms.Guna2ControlBox conboxclose;
    }
}