namespace Paint {
  partial class PaintForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaintForm));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileMnu = new System.Windows.Forms.MenuItem();
            this.sep1 = new System.Windows.Forms.MenuItem();
            this.fileExitMnu = new System.Windows.Forms.MenuItem();
            this.imageMnu = new System.Windows.Forms.MenuItem();
            this.imageClearMnu = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.pointPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.pointPanel2 = new System.Windows.Forms.StatusBarPanel();
            this.toolsBar = new System.Windows.Forms.ToolBar();
            this.arrowBtn = new System.Windows.Forms.ToolBarButton();
            this.lineBtn = new System.Windows.Forms.ToolBarButton();
            this.rectangleBtn = new System.Windows.Forms.ToolBarButton();
            this.ellipseBtn = new System.Windows.Forms.ToolBarButton();
            this.imgContainer = new System.Windows.Forms.Panel();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.primColorBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.widthCombo = new System.Windows.Forms.ComboBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pointPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointPanel2)).BeginInit();
            this.imgContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.primColorBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMnu,
            this.imageMnu,
            this.menuItem1});
            // 
            // fileMnu
            // 
            this.fileMnu.Index = 0;
            this.fileMnu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.sep1,
            this.fileExitMnu});
            this.fileMnu.Text = "File";
            // 
            // sep1
            // 
            this.sep1.Index = 0;
            this.sep1.Text = "-";
            // 
            // fileExitMnu
            // 
            this.fileExitMnu.Index = 1;
            this.fileExitMnu.Text = "&Exit";
            this.fileExitMnu.Click += new System.EventHandler(this.fileExitMnu_Click);
            // 
            // imageMnu
            // 
            this.imageMnu.Index = 1;
            this.imageMnu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.imageClearMnu});
            this.imageMnu.Text = "Image";
            // 
            // imageClearMnu
            // 
            this.imageClearMnu.Index = 0;
            this.imageClearMnu.Text = "&Clear";
            this.imageClearMnu.Click += new System.EventHandler(this.imageClearMnu_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5});
            this.menuItem1.Text = "VersionControl";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Commit";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Next Commit";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "Prev Commit";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 3;
            this.menuItem5.Text = "Next Branch";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 605);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.pointPanel1,
            this.pointPanel2});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(739, 22);
            this.statusBar.TabIndex = 1;
            // 
            // pointPanel1
            // 
            this.pointPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.pointPanel1.Name = "pointPanel1";
            this.pointPanel1.Width = 10;
            // 
            // pointPanel2
            // 
            this.pointPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.pointPanel2.Name = "pointPanel2";
            this.pointPanel2.Width = 10;
            // 
            // toolsBar
            // 
            this.toolsBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.arrowBtn,
            this.lineBtn,
            this.rectangleBtn,
            this.ellipseBtn});
            this.toolsBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolsBar.DropDownArrows = true;
            this.toolsBar.Location = new System.Drawing.Point(0, 44);
            this.toolsBar.MinimumSize = new System.Drawing.Size(30, 0);
            this.toolsBar.Name = "toolsBar";
            this.toolsBar.ShowToolTips = true;
            this.toolsBar.Size = new System.Drawing.Size(30, 561);
            this.toolsBar.TabIndex = 2;
            this.toolsBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolsBar_ButtonClick);
            // 
            // arrowBtn
            // 
            this.arrowBtn.ImageIndex = 8;
            this.arrowBtn.Name = "arrowBtn";
            this.arrowBtn.Pushed = true;
            // 
            // lineBtn
            // 
            this.lineBtn.ImageIndex = 0;
            this.lineBtn.Name = "lineBtn";
            // 
            // rectangleBtn
            // 
            this.rectangleBtn.ImageIndex = 1;
            this.rectangleBtn.Name = "rectangleBtn";
            // 
            // ellipseBtn
            // 
            this.ellipseBtn.ImageIndex = 4;
            this.ellipseBtn.Name = "ellipseBtn";
            // 
            // imgContainer
            // 
            this.imgContainer.AutoScroll = true;
            this.imgContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgContainer.Controls.Add(this.imageBox);
            this.imgContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgContainer.Location = new System.Drawing.Point(30, 44);
            this.imgContainer.Name = "imgContainer";
            this.imgContainer.Size = new System.Drawing.Size(709, 561);
            this.imgContainer.TabIndex = 3;
            // 
            // imageBox
            // 
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(705, 557);
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBox_Paint);
            this.imageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseDown);
            this.imageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseMove);
            this.imageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.primColorBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.widthCombo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(739, 44);
            this.panel1.TabIndex = 4;
            // 
            // primColorBox
            // 
            this.primColorBox.BackColor = System.Drawing.Color.DodgerBlue;
            this.primColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.primColorBox.Location = new System.Drawing.Point(12, 12);
            this.primColorBox.Name = "primColorBox";
            this.primColorBox.Size = new System.Drawing.Size(25, 24);
            this.primColorBox.TabIndex = 9;
            this.primColorBox.TabStop = false;
            this.primColorBox.Click += new System.EventHandler(this.ColorBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Width:";
            // 
            // widthCombo
            // 
            this.widthCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.widthCombo.FormattingEnabled = true;
            this.widthCombo.Location = new System.Drawing.Point(87, 12);
            this.widthCombo.Name = "widthCombo";
            this.widthCombo.Size = new System.Drawing.Size(62, 21);
            this.widthCombo.TabIndex = 2;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Red;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            // 
            // PaintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 627);
            this.Controls.Add(this.imgContainer);
            this.Controls.Add(this.toolsBar);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.panel1);
            this.Menu = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(755, 666);
            this.Name = "PaintForm";
            this.Text = "Paint";
            this.Load += new System.EventHandler(this.PaintForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pointPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointPanel2)).EndInit();
            this.imgContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.primColorBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MainMenu mainMenu;
    private System.Windows.Forms.MenuItem fileMnu;
    private System.Windows.Forms.MenuItem imageMnu;
    private System.Windows.Forms.StatusBar statusBar;
    private System.Windows.Forms.ToolBar toolsBar;
    private System.Windows.Forms.Panel imgContainer;
    private System.Windows.Forms.PictureBox imageBox;
    private System.Windows.Forms.StatusBarPanel pointPanel1;
    private System.Windows.Forms.StatusBarPanel pointPanel2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ComboBox widthCombo;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.PictureBox primColorBox;
    private System.Windows.Forms.MenuItem imageClearMnu;
    private System.Windows.Forms.ToolBarButton arrowBtn;
    private System.Windows.Forms.ToolBarButton lineBtn;
    private System.Windows.Forms.ToolBarButton rectangleBtn;
    private System.Windows.Forms.ToolBarButton ellipseBtn;
    private System.Windows.Forms.MenuItem sep1;
    private System.Windows.Forms.MenuItem fileExitMnu;
    private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
    }
}

