namespace ABC_Car_Traders.GUI.SingleForms
{
    partial class CartItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CartItems));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.conboxclose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.flpcartitems = new System.Windows.Forms.FlowLayoutPanel();
            this.btnorderall = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // conboxclose
            // 
            this.conboxclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.conboxclose.BackColor = System.Drawing.Color.Transparent;
            this.conboxclose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("conboxclose.BackgroundImage")));
            this.conboxclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.conboxclose.FillColor = System.Drawing.Color.Transparent;
            this.conboxclose.IconColor = System.Drawing.Color.Black;
            this.conboxclose.Location = new System.Drawing.Point(456, 12);
            this.conboxclose.Name = "conboxclose";
            this.conboxclose.Size = new System.Drawing.Size(45, 28);
            this.conboxclose.TabIndex = 31;
            // 
            // flpcartitems
            // 
            this.flpcartitems.AutoScroll = true;
            this.flpcartitems.Location = new System.Drawing.Point(0, 46);
            this.flpcartitems.Name = "flpcartitems";
            this.flpcartitems.Size = new System.Drawing.Size(514, 454);
            this.flpcartitems.TabIndex = 32;
            // 
            // btnorderall
            // 
            this.btnorderall.BorderColor = System.Drawing.Color.Transparent;
            this.btnorderall.BorderRadius = 15;
            this.btnorderall.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnorderall.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnorderall.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnorderall.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnorderall.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnorderall.ForeColor = System.Drawing.Color.White;
            this.btnorderall.Location = new System.Drawing.Point(164, 512);
            this.btnorderall.Name = "btnorderall";
            this.btnorderall.Size = new System.Drawing.Size(178, 29);
            this.btnorderall.TabIndex = 33;
            this.btnorderall.Text = "Order All Now";
            this.btnorderall.Click += new System.EventHandler(this.btnorderall_Click);
            // 
            // CartItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 549);
            this.Controls.Add(this.btnorderall);
            this.Controls.Add(this.flpcartitems);
            this.Controls.Add(this.conboxclose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CartItems";
            this.Text = "CartItems";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox conboxclose;
        private System.Windows.Forms.FlowLayoutPanel flpcartitems;
        private Guna.UI2.WinForms.Guna2Button btnorderall;
    }
}