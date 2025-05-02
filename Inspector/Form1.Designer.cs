namespace Inspector
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            deleteToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            navigateToolStripMenuItem = new ToolStripMenuItem();
            upTreeToolStripMenuItem = new ToolStripMenuItem();
            downTreeToolStripMenuItem = new ToolStripMenuItem();
            bothSideTreeToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            toolStrip1 = new ToolStrip();
            toolStripButton3 = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            dagreToolStripMenuItem = new ToolStripMenuItem();
            toolStripButton1 = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripDropDownButton4 = new ToolStripDropDownButton();
            showVerticalToolStripMenuItem = new ToolStripMenuItem();
            showHorizontalToolStripMenuItem = new ToolStripMenuItem();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.ContextMenuStrip = contextMenuStrip1;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(4, 3);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            tableLayoutPanel1.SetRowSpan(pictureBox1, 2);
            pictureBox1.Size = new Size(925, 572);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.MouseDoubleClick += pictureBox1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem, editToolStripMenuItem, navigateToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(120, 70);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Image = (Image)resources.GetObject("deleteToolStripMenuItem.Image");
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(119, 22);
            deleteToolStripMenuItem.Text = "delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Image = Properties.Resources.pencil;
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(119, 22);
            editToolStripMenuItem.Text = "edit";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // navigateToolStripMenuItem
            // 
            navigateToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { upTreeToolStripMenuItem, downTreeToolStripMenuItem, bothSideTreeToolStripMenuItem });
            navigateToolStripMenuItem.Image = Properties.Resources.magnifier;
            navigateToolStripMenuItem.Name = "navigateToolStripMenuItem";
            navigateToolStripMenuItem.Size = new Size(119, 22);
            navigateToolStripMenuItem.Text = "navigate";
            navigateToolStripMenuItem.Click += navigateToolStripMenuItem_Click;
            // 
            // upTreeToolStripMenuItem
            // 
            upTreeToolStripMenuItem.Name = "upTreeToolStripMenuItem";
            upTreeToolStripMenuItem.Size = new Size(180, 22);
            upTreeToolStripMenuItem.Text = "up tree";
            upTreeToolStripMenuItem.Click += upTreeToolStripMenuItem_Click;
            // 
            // downTreeToolStripMenuItem
            // 
            downTreeToolStripMenuItem.Name = "downTreeToolStripMenuItem";
            downTreeToolStripMenuItem.Size = new Size(180, 22);
            downTreeToolStripMenuItem.Text = "down tree";
            downTreeToolStripMenuItem.Click += downTreeToolStripMenuItem_Click;
            // 
            // bothSideTreeToolStripMenuItem
            // 
            bothSideTreeToolStripMenuItem.Name = "bothSideTreeToolStripMenuItem";
            bothSideTreeToolStripMenuItem.Size = new Size(180, 22);
            bothSideTreeToolStripMenuItem.Text = "both side tree";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 25);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 231F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(933, 578);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton3, toolStripButton4, toolStripDropDownButton2, toolStripButton1, toolStripSeparator3, toolStripDropDownButton4 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(933, 25);
            toolStrip1.TabIndex = 5;
            toolStrip1.Text = "toolStrip1";
            toolStrip1.ItemClicked += toolStrip1_ItemClicked;
            // 
            // toolStripButton3
            // 
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(53, 22);
            toolStripButton3.Text = "fit all";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(63, 22);
            toolStripButton4.Text = "reset view";
            toolStripButton4.Click += toolStripButton4_Click;
            // 
            // toolStripDropDownButton2
            // 
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { dagreToolStripMenuItem });
            toolStripDropDownButton2.Image = (Image)resources.GetObject("toolStripDropDownButton2.Image");
            toolStripDropDownButton2.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            toolStripDropDownButton2.Size = new Size(69, 22);
            toolStripDropDownButton2.Text = "layout";
            // 
            // dagreToolStripMenuItem
            // 
            dagreToolStripMenuItem.Name = "dagreToolStripMenuItem";
            dagreToolStripMenuItem.Size = new Size(104, 22);
            dagreToolStripMenuItem.Text = "dagre";
            dagreToolStripMenuItem.Click += dagreToolStripMenuItem_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(31, 22);
            toolStripButton1.Text = "edit";
            toolStripButton1.Click += toolStripButton1_Click_1;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // toolStripDropDownButton4
            // 
            toolStripDropDownButton4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton4.DropDownItems.AddRange(new ToolStripItem[] { showVerticalToolStripMenuItem, showHorizontalToolStripMenuItem });
            toolStripDropDownButton4.Image = (Image)resources.GetObject("toolStripDropDownButton4.Image");
            toolStripDropDownButton4.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            toolStripDropDownButton4.Size = new Size(48, 22);
            toolStripDropDownButton4.Text = "show";
            // 
            // showVerticalToolStripMenuItem
            // 
            showVerticalToolStripMenuItem.Name = "showVerticalToolStripMenuItem";
            showVerticalToolStripMenuItem.Size = new Size(158, 22);
            showVerticalToolStripMenuItem.Text = "show vertical";
            showVerticalToolStripMenuItem.Click += showVerticalToolStripMenuItem_Click;
            // 
            // showHorizontalToolStripMenuItem
            // 
            showHorizontalToolStripMenuItem.Name = "showHorizontalToolStripMenuItem";
            showHorizontalToolStripMenuItem.Size = new Size(158, 22);
            showHorizontalToolStripMenuItem.Text = "show horizontal";
            showHorizontalToolStripMenuItem.Click += showHorizontalToolStripMenuItem_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 50;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Size = new Size(933, 603);
            DragDrop += Form1_DragDrop;
            DragEnter += Form1_DragEnter;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem dagreToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
        private System.Windows.Forms.ToolStripMenuItem showVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHorizontalToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem navigateToolStripMenuItem;
        private ToolStripMenuItem upTreeToolStripMenuItem;
        private ToolStripMenuItem downTreeToolStripMenuItem;
        private ToolStripMenuItem bothSideTreeToolStripMenuItem;
    }
}

