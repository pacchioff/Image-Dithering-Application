namespace ImageDithering
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            文件ToolStripMenuItem = new ToolStripMenuItem();
            打开ToolStripMenuItem = new ToolStripMenuItem();
            导出ToolStripMenuItem = new ToolStripMenuItem();
            退出ToolStripMenuItem1 = new ToolStripMenuItem();
            关于ToolStripMenuItem = new ToolStripMenuItem();
            帮助ToolStripMenuItem = new ToolStripMenuItem();
            pictureBox = new PictureBox();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            grayScaleCheckBox = new CheckBox();
            reset = new Button();
            bayer2Button = new Button();
            bayer3Button = new Button();
            bayer4Button = new Button();
            bayer8Button = new Button();
            bayer16Button = new Button();
            floydSteinbergButton = new Button();
            sierraLiteButton = new Button();
            sierraButton = new Button();
            atkinsonButton = new Button();
            bayer3bppButton = new Button();
            bayer6bppButton = new Button();
            bayer9bppButton = new Button();
            bayer12bppButton = new Button();
            bayer15bppButton = new Button();
            bayer18bppButton = new Button();
            lblBlackAndWhite = new Label();
            lblCtrlI = new Label();
            DUpDownBayer = new DomainUpDown();
            dUpDownFS = new DomainUpDown();
            dUpDownSierra = new DomainUpDown();
            radioBayer = new RadioButton();
            radioFS = new RadioButton();
            radioSerra = new RadioButton();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 文件ToolStripMenuItem, 关于ToolStripMenuItem, 帮助ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1044, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            文件ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 打开ToolStripMenuItem, 导出ToolStripMenuItem, 退出ToolStripMenuItem1 });
            文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            文件ToolStripMenuItem.Size = new Size(53, 24);
            文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            打开ToolStripMenuItem.Size = new Size(122, 26);
            打开ToolStripMenuItem.Text = "打开";
            打开ToolStripMenuItem.Click += 打开ToolStripMenuItem_Click;
            // 
            // 导出ToolStripMenuItem
            // 
            导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            导出ToolStripMenuItem.Size = new Size(122, 26);
            导出ToolStripMenuItem.Text = "导出";
            导出ToolStripMenuItem.Click += 导出ToolStripMenuItem_Click;
            // 
            // 退出ToolStripMenuItem1
            // 
            退出ToolStripMenuItem1.Name = "退出ToolStripMenuItem1";
            退出ToolStripMenuItem1.Size = new Size(122, 26);
            退出ToolStripMenuItem1.Text = "退出";
            退出ToolStripMenuItem1.Click += 退出ToolStripMenuItem1_Click;
            // 
            // 关于ToolStripMenuItem
            // 
            关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            关于ToolStripMenuItem.Size = new Size(53, 24);
            关于ToolStripMenuItem.Text = "关于";
            关于ToolStripMenuItem.Click += 关于ToolStripMenuItem_Click;
            // 
            // 帮助ToolStripMenuItem
            // 
            帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            帮助ToolStripMenuItem.Size = new Size(53, 24);
            帮助ToolStripMenuItem.Text = "帮助";
            帮助ToolStripMenuItem.Click += 帮助ToolStripMenuItem_Click;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(12, 31);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(800, 600);
            pictureBox.TabIndex = 1;
            pictureBox.TabStop = false;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 631);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1044, 26);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(24, 20);
            toolStripStatusLabel.Text = "无";
            // 
            // grayScaleCheckBox
            // 
            grayScaleCheckBox.AutoSize = true;
            grayScaleCheckBox.Checked = true;
            grayScaleCheckBox.CheckState = CheckState.Checked;
            grayScaleCheckBox.Location = new Point(818, 607);
            grayScaleCheckBox.Name = "grayScaleCheckBox";
            grayScaleCheckBox.Size = new Size(61, 24);
            grayScaleCheckBox.TabIndex = 3;
            grayScaleCheckBox.Text = "灰度";
            grayScaleCheckBox.UseVisualStyleBackColor = true;
            grayScaleCheckBox.CheckedChanged += grayScaleCheckBox_CheckedChanged;
            // 
            // reset
            // 
            reset.Location = new Point(818, 572);
            reset.Name = "reset";
            reset.Size = new Size(106, 29);
            reset.TabIndex = 4;
            reset.Text = "重置";
            reset.UseVisualStyleBackColor = true;
            reset.Click += reset_Click;
            // 
            // bayer2Button
            // 
            bayer2Button.Location = new Point(818, 54);
            bayer2Button.Name = "bayer2Button";
            bayer2Button.Size = new Size(106, 29);
            bayer2Button.TabIndex = 5;
            bayer2Button.Text = "bayer2";
            bayer2Button.UseVisualStyleBackColor = true;
            bayer2Button.Click += bayer2Button_Click;
            // 
            // bayer3Button
            // 
            bayer3Button.Location = new Point(818, 89);
            bayer3Button.Name = "bayer3Button";
            bayer3Button.Size = new Size(106, 29);
            bayer3Button.TabIndex = 6;
            bayer3Button.Text = "bayer3";
            bayer3Button.UseVisualStyleBackColor = true;
            bayer3Button.Click += bayer3Button_Click;
            // 
            // bayer4Button
            // 
            bayer4Button.Location = new Point(818, 124);
            bayer4Button.Name = "bayer4Button";
            bayer4Button.Size = new Size(106, 29);
            bayer4Button.TabIndex = 7;
            bayer4Button.Text = "bayer4";
            bayer4Button.UseVisualStyleBackColor = true;
            bayer4Button.Click += bayer4Button_Click;
            // 
            // bayer8Button
            // 
            bayer8Button.Location = new Point(818, 159);
            bayer8Button.Name = "bayer8Button";
            bayer8Button.Size = new Size(106, 29);
            bayer8Button.TabIndex = 8;
            bayer8Button.Text = "bayer8";
            bayer8Button.UseVisualStyleBackColor = true;
            bayer8Button.Click += bayer8Button_Click;
            // 
            // bayer16Button
            // 
            bayer16Button.Location = new Point(818, 194);
            bayer16Button.Name = "bayer16Button";
            bayer16Button.Size = new Size(106, 29);
            bayer16Button.TabIndex = 9;
            bayer16Button.Text = "bayer16";
            bayer16Button.UseVisualStyleBackColor = true;
            bayer16Button.Click += bayer16Button_Click;
            // 
            // floydSteinbergButton
            // 
            floydSteinbergButton.Location = new Point(818, 229);
            floydSteinbergButton.Name = "floydSteinbergButton";
            floydSteinbergButton.Size = new Size(106, 29);
            floydSteinbergButton.TabIndex = 10;
            floydSteinbergButton.Text = "FS";
            floydSteinbergButton.UseVisualStyleBackColor = true;
            floydSteinbergButton.Click += floydSteinbergButton_Click;
            // 
            // sierraLiteButton
            // 
            sierraLiteButton.Location = new Point(818, 264);
            sierraLiteButton.Name = "sierraLiteButton";
            sierraLiteButton.Size = new Size(106, 29);
            sierraLiteButton.TabIndex = 11;
            sierraLiteButton.Text = "SL";
            sierraLiteButton.UseVisualStyleBackColor = true;
            sierraLiteButton.Click += sierraLiteButton_Click;
            // 
            // sierraButton
            // 
            sierraButton.Location = new Point(818, 299);
            sierraButton.Name = "sierraButton";
            sierraButton.Size = new Size(106, 29);
            sierraButton.TabIndex = 12;
            sierraButton.Text = "sierra";
            sierraButton.UseVisualStyleBackColor = true;
            sierraButton.Click += sierraButton_Click;
            // 
            // atkinsonButton
            // 
            atkinsonButton.Location = new Point(818, 334);
            atkinsonButton.Name = "atkinsonButton";
            atkinsonButton.Size = new Size(106, 29);
            atkinsonButton.TabIndex = 13;
            atkinsonButton.Text = "atkinson";
            atkinsonButton.UseVisualStyleBackColor = true;
            atkinsonButton.Click += atkinsonButton_Click;
            // 
            // bayer3bppButton
            // 
            bayer3bppButton.Location = new Point(930, 54);
            bayer3bppButton.Name = "bayer3bppButton";
            bayer3bppButton.Size = new Size(106, 29);
            bayer3bppButton.TabIndex = 14;
            bayer3bppButton.Text = "bayer3bpp";
            bayer3bppButton.UseVisualStyleBackColor = true;
            bayer3bppButton.Click += bayer3bppButton_Click;
            // 
            // bayer6bppButton
            // 
            bayer6bppButton.Location = new Point(930, 89);
            bayer6bppButton.Name = "bayer6bppButton";
            bayer6bppButton.Size = new Size(106, 29);
            bayer6bppButton.TabIndex = 15;
            bayer6bppButton.Text = "bayer6bpp";
            bayer6bppButton.UseVisualStyleBackColor = true;
            bayer6bppButton.Click += bayer6bppButton_Click;
            // 
            // bayer9bppButton
            // 
            bayer9bppButton.Location = new Point(930, 124);
            bayer9bppButton.Name = "bayer9bppButton";
            bayer9bppButton.Size = new Size(106, 29);
            bayer9bppButton.TabIndex = 16;
            bayer9bppButton.Text = "bayer9bpp";
            bayer9bppButton.UseVisualStyleBackColor = true;
            bayer9bppButton.Click += bayer9bppButton_Click;
            // 
            // bayer12bppButton
            // 
            bayer12bppButton.Location = new Point(930, 159);
            bayer12bppButton.Name = "bayer12bppButton";
            bayer12bppButton.Size = new Size(106, 29);
            bayer12bppButton.TabIndex = 17;
            bayer12bppButton.Text = "bayer12bpp";
            bayer12bppButton.UseVisualStyleBackColor = true;
            bayer12bppButton.Click += bayer12bppButton_Click;
            // 
            // bayer15bppButton
            // 
            bayer15bppButton.Location = new Point(930, 194);
            bayer15bppButton.Name = "bayer15bppButton";
            bayer15bppButton.Size = new Size(106, 29);
            bayer15bppButton.TabIndex = 18;
            bayer15bppButton.Text = "bayer15bpp";
            bayer15bppButton.UseVisualStyleBackColor = true;
            bayer15bppButton.Click += bayer15bppButton_Click;
            // 
            // bayer18bppButton
            // 
            bayer18bppButton.Location = new Point(930, 229);
            bayer18bppButton.Name = "bayer18bppButton";
            bayer18bppButton.Size = new Size(106, 29);
            bayer18bppButton.TabIndex = 19;
            bayer18bppButton.Text = "bayer18bpp";
            bayer18bppButton.UseVisualStyleBackColor = true;
            bayer18bppButton.Click += bayer18bppButton_Click;
            // 
            // lblBlackAndWhite
            // 
            lblBlackAndWhite.AutoSize = true;
            lblBlackAndWhite.Location = new Point(818, 31);
            lblBlackAndWhite.Name = "lblBlackAndWhite";
            lblBlackAndWhite.Size = new Size(39, 20);
            lblBlackAndWhite.TabIndex = 20;
            lblBlackAndWhite.Text = "灰度";
            // 
            // lblCtrlI
            // 
            lblCtrlI.AutoSize = true;
            lblCtrlI.Location = new Point(930, 28);
            lblCtrlI.Name = "lblCtrlI";
            lblCtrlI.Size = new Size(39, 20);
            lblCtrlI.TabIndex = 21;
            lblCtrlI.Text = "色阶";
            // 
            // DUpDownBayer
            // 
            DUpDownBayer.Location = new Point(950, 374);
            DUpDownBayer.Name = "DUpDownBayer";
            DUpDownBayer.Size = new Size(86, 27);
            DUpDownBayer.TabIndex = 22;
            DUpDownBayer.Text = "domainUpDown1";
            DUpDownBayer.SelectedItemChanged += DUpDownBayer_SelectedItemChanged;
            // 
            // dUpDownFS
            // 
            dUpDownFS.Location = new Point(950, 407);
            dUpDownFS.Name = "dUpDownFS";
            dUpDownFS.Size = new Size(86, 27);
            dUpDownFS.TabIndex = 25;
            dUpDownFS.Text = "domainUpDown1";
            dUpDownFS.SelectedItemChanged += dUpDownFS_SelectedItemChanged;
            // 
            // dUpDownSierra
            // 
            dUpDownSierra.Location = new Point(950, 440);
            dUpDownSierra.Name = "dUpDownSierra";
            dUpDownSierra.Size = new Size(86, 27);
            dUpDownSierra.TabIndex = 26;
            dUpDownSierra.Text = "domainUpDown2";
            dUpDownSierra.SelectedItemChanged += dUpDownSierra_SelectedItemChanged;
            // 
            // radioBayer
            // 
            radioBayer.AutoSize = true;
            radioBayer.Location = new Point(818, 377);
            radioBayer.Name = "radioBayer";
            radioBayer.Size = new Size(120, 24);
            radioBayer.TabIndex = 28;
            radioBayer.TabStop = true;
            radioBayer.Text = "Bayer RGB N";
            radioBayer.UseVisualStyleBackColor = true;
            radioBayer.CheckedChanged += bayerRadioButton_CheckedChanged;
            // 
            // radioFS
            // 
            radioFS.AutoSize = true;
            radioFS.Location = new Point(818, 410);
            radioFS.Name = "radioFS";
            radioFS.Size = new Size(121, 24);
            radioFS.TabIndex = 29;
            radioFS.TabStop = true;
            radioFS.Text = "      FS RGB N";
            radioFS.UseVisualStyleBackColor = true;
            radioFS.CheckedChanged += fsRadioButton_CheckedChanged;
            // 
            // radioSerra
            // 
            radioSerra.AutoSize = true;
            radioSerra.Location = new Point(818, 440);
            radioSerra.Name = "radioSerra";
            radioSerra.Size = new Size(122, 24);
            radioSerra.TabIndex = 30;
            radioSerra.TabStop = true;
            radioSerra.Text = "Sierra RGB N";
            radioSerra.UseVisualStyleBackColor = true;
            radioSerra.CheckedChanged += sierraRadioButton_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1044, 657);
            Controls.Add(radioSerra);
            Controls.Add(radioFS);
            Controls.Add(radioBayer);
            Controls.Add(dUpDownSierra);
            Controls.Add(dUpDownFS);
            Controls.Add(DUpDownBayer);
            Controls.Add(lblCtrlI);
            Controls.Add(lblBlackAndWhite);
            Controls.Add(bayer18bppButton);
            Controls.Add(bayer15bppButton);
            Controls.Add(bayer12bppButton);
            Controls.Add(bayer9bppButton);
            Controls.Add(bayer6bppButton);
            Controls.Add(bayer3bppButton);
            Controls.Add(atkinsonButton);
            Controls.Add(sierraButton);
            Controls.Add(sierraLiteButton);
            Controls.Add(floydSteinbergButton);
            Controls.Add(bayer16Button);
            Controls.Add(bayer8Button);
            Controls.Add(bayer4Button);
            Controls.Add(bayer3Button);
            Controls.Add(bayer2Button);
            Controls.Add(reset);
            Controls.Add(grayScaleCheckBox);
            Controls.Add(statusStrip);
            Controls.Add(pictureBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "ImageDithering";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 文件ToolStripMenuItem;
        private ToolStripMenuItem 打开ToolStripMenuItem;
        private ToolStripMenuItem 导出ToolStripMenuItem;
        private PictureBox pictureBox;
        private StatusStrip statusStrip;
        private CheckBox grayScaleCheckBox;
        private ToolStripMenuItem 退出ToolStripMenuItem1;
        private ToolStripStatusLabel toolStripStatusLabel;
        private Button reset;
        private Button bayer2Button;
        private Button bayer3Button;
        private Button bayer4Button;
        private Button bayer8Button;
        private Button bayer16Button;
        private Button floydSteinbergButton;
        private Button sierraLiteButton;
        private Button sierraButton;
        private Button atkinsonButton;
        private Button bayer3bppButton;
        private Button bayer6bppButton;
        private Button bayer9bppButton;
        private Button bayer12bppButton;
        private Button bayer15bppButton;
        private Button bayer18bppButton;
        private Label lblBlackAndWhite;
        private Label lblCtrlI;
        private DomainUpDown DUpDownBayer;
        private DomainUpDown dUpDownFS;
        private DomainUpDown dUpDownSierra;
        private RadioButton radioBayer;
        private RadioButton radioFS;
        private RadioButton radioSerra;
        private ToolStripMenuItem 关于ToolStripMenuItem;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
    }
}
