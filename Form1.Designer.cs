namespace Rectanglex
{
    partial class Form1
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.vertexContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stiffAngleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movePolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeHorisontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeRelationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.vertexContextMenuStrip.SuspendLayout();
            this.edgeContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1461, 809);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // vertexContextMenuStrip
            // 
            this.vertexContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteVertexToolStripMenuItem,
            this.stiffAngleToolStripMenuItem});
            this.vertexContextMenuStrip.Name = "vertexContextMenuStrip";
            this.vertexContextMenuStrip.Size = new System.Drawing.Size(143, 48);
            // 
            // deleteVertexToolStripMenuItem
            // 
            this.deleteVertexToolStripMenuItem.Name = "deleteVertexToolStripMenuItem";
            this.deleteVertexToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.deleteVertexToolStripMenuItem.Text = "Delete vertex";
            this.deleteVertexToolStripMenuItem.Click += new System.EventHandler(this.deleteVertexToolStripMenuItem_Click);
            // 
            // stiffAngleToolStripMenuItem
            // 
            this.stiffAngleToolStripMenuItem.Name = "stiffAngleToolStripMenuItem";
            this.stiffAngleToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.stiffAngleToolStripMenuItem.Text = "Stiff angle";
            this.stiffAngleToolStripMenuItem.Click += new System.EventHandler(this.stiffAngleToolStripMenuItem_Click);
            // 
            // edgeContextMenuStrip
            // 
            this.edgeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVertexToolStripMenuItem,
            this.movePolygonToolStripMenuItem,
            this.makeHorisontalToolStripMenuItem,
            this.removeRelationToolStripMenuItem,
            this.makeVerticalToolStripMenuItem});
            this.edgeContextMenuStrip.Name = "edgeContextMenuStrip";
            this.edgeContextMenuStrip.Size = new System.Drawing.Size(161, 114);
            this.edgeContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.edgeContextMenuStrip_Opening);
            // 
            // addVertexToolStripMenuItem
            // 
            this.addVertexToolStripMenuItem.Name = "addVertexToolStripMenuItem";
            this.addVertexToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.addVertexToolStripMenuItem.Text = "Add vertex";
            this.addVertexToolStripMenuItem.Click += new System.EventHandler(this.addVertexToolStripMenuItem_Click);
            // 
            // movePolygonToolStripMenuItem
            // 
            this.movePolygonToolStripMenuItem.Name = "movePolygonToolStripMenuItem";
            this.movePolygonToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.movePolygonToolStripMenuItem.Text = "Move polygon";
            this.movePolygonToolStripMenuItem.Click += new System.EventHandler(this.movePolygonToolStripMenuItem_Click);
            // 
            // makeHorisontalToolStripMenuItem
            // 
            this.makeHorisontalToolStripMenuItem.Name = "makeHorisontalToolStripMenuItem";
            this.makeHorisontalToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.makeHorisontalToolStripMenuItem.Text = "Make horisontal";
            this.makeHorisontalToolStripMenuItem.Click += new System.EventHandler(this.makeHorisontalToolStripMenuItem_Click);
            // 
            // removeRelationToolStripMenuItem
            // 
            this.removeRelationToolStripMenuItem.Name = "removeRelationToolStripMenuItem";
            this.removeRelationToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.removeRelationToolStripMenuItem.Text = "Remove relation";
            this.removeRelationToolStripMenuItem.Visible = false;
            this.removeRelationToolStripMenuItem.Click += new System.EventHandler(this.removeRelationToolStripMenuItem_Click);
            // 
            // makeVerticalToolStripMenuItem
            // 
            this.makeVerticalToolStripMenuItem.Name = "makeVerticalToolStripMenuItem";
            this.makeVerticalToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.makeVerticalToolStripMenuItem.Text = "Make vertical";
            this.makeVerticalToolStripMenuItem.Click += new System.EventHandler(this.makeVerticalToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 809);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rectanglex";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.vertexContextMenuStrip.ResumeLayout(false);
            this.edgeContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ContextMenuStrip vertexContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteVertexToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip edgeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addVertexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem movePolygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stiffAngleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeHorisontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeRelationToolStripMenuItem;
    }
}

