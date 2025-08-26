namespace ImageDithering
{
    partial class Help
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
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(404, 371);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "    所有的功能按钮均在窗口上，灰度会切换模式，使得变成黑白或彩色模式。\n    点击重置后，您的图像将会回到导入时的状态。\n    在RGB模式中，您可以通过点击上下，调整抖动强度，同时也支持灰度和色阶\n\n";
            // 
            // Help
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 395);
            Controls.Add(richTextBox1);
            Name = "Help";
            Text = "帮助";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
    }
}