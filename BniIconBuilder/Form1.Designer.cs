using BniIconBuilder.Properties;

namespace BniIconBuilder
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
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioTag = new System.Windows.Forms.RadioButton();
            this.radioFlag = new System.Windows.Forms.RadioButton();
            this.lblFlagTag = new System.Windows.Forms.Label();
            this.txtFlag = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWarning = new System.Windows.Forms.Label();
            this.customDrawListBox1 = new Toolset.Controls.CustomDrawListBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBoxIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxIcon.Location = new System.Drawing.Point(203, 32);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(145, 89);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxIcon.TabIndex = 4;
            this.pictureBoxIcon.TabStop = false;
            this.pictureBoxIcon.Click += new System.EventHandler(this.pictureBoxIcon_Click);
            this.pictureBoxIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxIcon_Paint);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Enabled = false;
            this.btnAddItem.Location = new System.Drawing.Point(10, 414);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(85, 27);
            this.btnAddItem.TabIndex = 5;
            this.btnAddItem.Text = "ADD";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Enabled = false;
            this.BtnSave.Location = new System.Drawing.Point(254, 414);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(94, 27);
            this.BtnSave.TabIndex = 6;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Enabled = false;
            this.btnRemoveItem.Location = new System.Drawing.Point(106, 414);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(85, 27);
            this.btnRemoveItem.TabIndex = 7;
            this.btnRemoveItem.Text = "REMOVE";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioTag);
            this.groupBox1.Controls.Add(this.radioFlag);
            this.groupBox1.Location = new System.Drawing.Point(203, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 49);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Icon Type";
            // 
            // radioTag
            // 
            this.radioTag.AutoSize = true;
            this.radioTag.Enabled = false;
            this.radioTag.Location = new System.Drawing.Point(67, 19);
            this.radioTag.Name = "radioTag";
            this.radioTag.Size = new System.Drawing.Size(44, 17);
            this.radioTag.TabIndex = 9;
            this.radioTag.TabStop = true;
            this.radioTag.Text = "Tag";
            this.radioTag.UseVisualStyleBackColor = true;
            this.radioTag.CheckedChanged += new System.EventHandler(this.radioTag_CheckedChanged);
            // 
            // radioFlag
            // 
            this.radioFlag.AutoSize = true;
            this.radioFlag.Enabled = false;
            this.radioFlag.Location = new System.Drawing.Point(16, 19);
            this.radioFlag.Name = "radioFlag";
            this.radioFlag.Size = new System.Drawing.Size(45, 17);
            this.radioFlag.TabIndex = 10;
            this.radioFlag.TabStop = true;
            this.radioFlag.Text = "Flag";
            this.radioFlag.UseVisualStyleBackColor = true;
            this.radioFlag.CheckedChanged += new System.EventHandler(this.radioFlag_CheckedChanged);
            // 
            // lblFlagTag
            // 
            this.lblFlagTag.AutoSize = true;
            this.lblFlagTag.Location = new System.Drawing.Point(203, 201);
            this.lblFlagTag.Name = "lblFlagTag";
            this.lblFlagTag.Size = new System.Drawing.Size(27, 13);
            this.lblFlagTag.TabIndex = 10;
            this.lblFlagTag.Text = "Flag";
            // 
            // txtFlag
            // 
            this.txtFlag.Enabled = false;
            this.txtFlag.Location = new System.Drawing.Point(203, 218);
            this.txtFlag.Name = "txtFlag";
            this.txtFlag.Size = new System.Drawing.Size(143, 20);
            this.txtFlag.TabIndex = 11;
            this.txtFlag.TextChanged += new System.EventHandler(this.txtFlagTag_TextChanged);
            // 
            // txtId
            // 
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(203, 271);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(143, 20);
            this.txtId.TabIndex = 13;
            this.txtId.TextChanged += new System.EventHandler(this.txtId_TextChanged);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(203, 254);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(60, 13);
            this.lblId.TabIndex = 12;
            this.lblId.Text = "Id (unused)";
            // 
            // txtTag
            // 
            this.txtTag.Enabled = false;
            this.txtTag.Location = new System.Drawing.Point(219, 231);
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(143, 20);
            this.txtTag.TabIndex = 11;
            this.txtTag.TextChanged += new System.EventHandler(this.txtFlagTag_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(190, 407);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 2);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(360, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpen,
            this.menuSave,
            this.menuSaveAs,
            this.menuExport,
            this.menuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(152, 22);
            this.menuOpen.Text = "Open...";
            this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(152, 22);
            this.menuSave.Text = "Save";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(152, 22);
            this.menuSaveAs.Text = "Save As...";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // menuExport
            // 
            this.menuExport.Name = "menuExport";
            this.menuExport.Size = new System.Drawing.Size(152, 22);
            this.menuExport.Text = "Export TGA...";
            this.menuExport.Click += new System.EventHandler(this.menuExport_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(152, 22);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(203, 311);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(145, 82);
            this.lblWarning.TabIndex = 16;
            this.lblWarning.Text = "label1";
            // 
            // customDrawListBox1
            // 
            this.customDrawListBox1.AllowDrop = true;
            this.customDrawListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.customDrawListBox1.FormattingEnabled = true;
            this.customDrawListBox1.ItemHeight = 25;
            this.customDrawListBox1.Location = new System.Drawing.Point(10, 32);
            this.customDrawListBox1.Name = "customDrawListBox1";
            this.customDrawListBox1.ScrollAlwaysVisible = true;
            this.customDrawListBox1.Size = new System.Drawing.Size(181, 376);
            this.customDrawListBox1.TabIndex = 3;
            this.customDrawListBox1.SelectedIndexChanged += new System.EventHandler(this.customDrawListBox1_SelectedIndexChanged);
            this.customDrawListBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.customDrawListBox1_DragDrop);
            this.customDrawListBox1.DragOver += new System.Windows.Forms.DragEventHandler(this.customDrawListBox1_DragOver);
            this.customDrawListBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.customDrawListBox1_MouseDown);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(152, 22);
            this.menuAbout.Text = "About...";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(360, 447);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.txtTag);
            this.Controls.Add(this.txtFlag);
            this.Controls.Add(this.lblFlagTag);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.customDrawListBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::BniIconBuilder.Properties.Resources.Ironman;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "icons-WAR3.bni";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Toolset.Controls.CustomDrawListBox customDrawListBox1;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioTag;
        private System.Windows.Forms.RadioButton radioFlag;
        private System.Windows.Forms.Label lblFlagTag;
        private System.Windows.Forms.TextBox txtFlag;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtTag;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuExport;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;

    }
}

