using ImageDitheringApp;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ImageDithering
{
    public partial class Form1 : Form
    {
        private Bitmap originalImage;
        private Bitmap processedImage;
        private bool colorMode = false;
        private string ditheringMode = "";
        private bool currentMode;

        // 当前选择的N值
        private int bayerNValue = 1;
        private int fsNValue = 1;
        private int sierraNValue = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "就绪";

            // 初始化DomainUpDown控件
            DUpDownBayer.Items.Clear();
            dUpDownFS.Items.Clear();
            dUpDownSierra.Items.Clear();

            for (int i = 1; i <= 15; i++)
            {
                DUpDownBayer.Items.Add(i);
                dUpDownFS.Items.Add(i);
                dUpDownSierra.Items.Add(i);
            }

            DUpDownBayer.SelectedIndex = 0;
            dUpDownFS.SelectedIndex = 0;
            dUpDownSierra.SelectedIndex = 0;

            UpdateControlsState();
        }

        // 更新控件状态
        private void UpdateControlsState()  //每次选中都会更新
        {
            DUpDownBayer.Enabled = radioBayer.Checked;
            dUpDownFS.Enabled = radioFS.Checked;
            dUpDownSierra.Enabled = radioSerra.Checked;

            ApplyCurrentRgbNAlgorithm();
        }

        // RadioButton 选择改变事件
        private void bayerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBayer.Checked)
            {
                ditheringMode = "BayerRgbNbpp";
                UpdateControlsState();
            }
        }

        private void fsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioFS.Checked)
            {
                ditheringMode = "FSRgbNbpp";
                UpdateControlsState();
            }
        }

        private void sierraRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSerra.Checked)
            {
                ditheringMode = "SierraRgbNbpp";
                UpdateControlsState();
            }
        }

        private void ApplyCurrentRgbNAlgorithm()
        {
            if (radioBayer.Checked)
            {
                ApplyDither("BayerRgbNbpp");
            }
            else if (radioFS.Checked)
            {
                ApplyDither("FSRgbNbpp");
            }
            else if (radioSerra.Checked)
            {
                ApplyDither("SierraRgbNbpp");
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "打开图像文件";
                dlg.Filter = "BMP文件|*.bmp|所有文件|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        originalImage = new Bitmap(dlg.FileName);
                        processedImage = null;
                        ShowOriginal();
                        toolStripStatusLabel.Text = $"已加载: {System.IO.Path.GetFileName(dlg.FileName)}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"无法打开图像: {ex.Message}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toolStripStatusLabel.Text = "打开图像失败";
                    }
                }
            }
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show("没有图像可保存", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "保存图像文件";
                dlg.Filter = "BMP文件|*.bmp";
                dlg.DefaultExt = "bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox.Image.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        toolStripStatusLabel.Text = $"已保存: {System.IO.Path.GetFileName(dlg.FileName)}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"无法保存图像: {ex.Message}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toolStripStatusLabel.Text = "保存图像失败";
                    }
                }
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            ShowOriginal();
        }

        private void grayScaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            colorMode = !grayScaleCheckBox.Checked;
            toolStripStatusLabel.Text = colorMode ? "已切换到色阶模式" : "已切换到灰度模式";

            if (pictureBox.Image != null && ditheringMode != "")
            {
                ApplyDither(ditheringMode);
            }
        }

        private void ShowOriginal()
        {
            if (originalImage != null)
            {
                pictureBox.Image = originalImage;
                toolStripStatusLabel.Text = "显示原始图像";
                ditheringMode = "";
            }
        }

        private void ApplyDither(string methodName)
        {
            if (originalImage == null)
            {
                MessageBox.Show("请先打开一个图像文件", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cursor = Cursors.WaitCursor;

            try
            {
                switch (methodName)
                {
                    case "Bayer2":
                        processedImage = Dither.Bayer2(originalImage, colorMode);
                        toolStripStatusLabel.Text = "应用 Bayer 2x2 抖动";
                        break;
                    case "Bayer3":
                        processedImage = Dither.Bayer3(originalImage, colorMode);
                        toolStripStatusLabel.Text = "应用 Bayer 3x3 抖动";
                        break;
                    case "Bayer4":
                        processedImage = Dither.Bayer4(originalImage, colorMode);
                        toolStripStatusLabel.Text = "应用 Bayer 4x4 抖动";
                        break;
                    case "Bayer8":
                        processedImage = Dither.Bayer8(originalImage, colorMode);
                        toolStripStatusLabel.Text = "应用 Bayer 8x8 抖动";
                        break;
                    case "Bayer16":
                        processedImage = Dither.Bayer16(originalImage, colorMode);
                        toolStripStatusLabel.Text = "应用 Bayer 16x16 抖动";
                        break;
                    case "FloydSteinberg":
                        processedImage = Dither.FloydSteinberg(originalImage, colorMode);
                        toolStripStatusLabel.Text = "应用 Floyd-Steinberg 抖动";
                        break;
                    case "SierraLite":
                        processedImage = Dither.SierraLite(originalImage, colorMode);
                        toolStripStatusLabel.Text = "应用 Sierra Lite 抖动";
                        break;
                    case "Sierra":
                        processedImage = Dither.Sierra(originalImage, colorMode);
                        toolStripStatusLabel.Text = "应用 Sierra 抖动";
                        break;
                    case "Atkinson":
                        processedImage = Dither.Atkinson(originalImage, colorMode);
                        toolStripStatusLabel.Text = "应用 Atkinson 抖动";
                        break;
                    case "BayerRgb3bpp":
                        processedImage = Dither.BayerRgb3bpp(originalImage);
                        toolStripStatusLabel.Text = "应用 Bayer RGB 3bpp 抖动";
                        break;
                    case "BayerRgb6bpp":
                        processedImage = Dither.BayerRgb6bpp(originalImage);
                        toolStripStatusLabel.Text = "应用 Bayer RGB 6bpp 抖动";
                        break;
                    case "BayerRgb9bpp":
                        processedImage = Dither.BayerRgb9bpp(originalImage);
                        toolStripStatusLabel.Text = "应用 Bayer RGB 9bpp 抖动";
                        break;
                    case "BayerRgb12bpp":
                        processedImage = Dither.BayerRgb12bpp(originalImage);
                        toolStripStatusLabel.Text = "应用 Bayer RGB 12bpp 抖动";
                        break;
                    case "BayerRgb15bpp":
                        processedImage = Dither.BayerRgb15bpp(originalImage);
                        toolStripStatusLabel.Text = "应用 Bayer RGB 15bpp 抖动";
                        break;
                    case "BayerRgb18bpp":
                        processedImage = Dither.BayerRgb18bpp(originalImage);
                        toolStripStatusLabel.Text = "应用 Bayer RGB 18bpp 抖动";
                        break;
                    case "BayerRgbNbpp":
                        processedImage = Dither.BayerRgbNbpp(originalImage, bayerNValue, colorMode);
                        toolStripStatusLabel.Text = $"应用 Bayer RGB N={bayerNValue} 抖动";
                        break;
                    case "FSRgbNbpp":
                        processedImage = Dither.FSRgbNbpp(originalImage, fsNValue, colorMode);
                        toolStripStatusLabel.Text = $"应用 FS RGB N={fsNValue} 抖动";
                        break;
                    case "SierraRgbNbpp":
                        processedImage = Dither.SierraRgbNbpp(originalImage, sierraNValue, colorMode);
                        toolStripStatusLabel.Text = $"应用 Sierra RGB N={sierraNValue} 抖动";
                        break;
                    default:
                        MessageBox.Show("未知的抖动方法", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                pictureBox.Image = processedImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"处理图像时出错: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "处理图像失败";
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void bayer2Button_Click(object sender, EventArgs e)
        {
            ApplyDither("Bayer2");
            ditheringMode = "Bayer2";
        }

        private void bayer3Button_Click(object sender, EventArgs e)
        {
            ApplyDither("Bayer3");
            ditheringMode = "Bayer3";
        }

        private void bayer4Button_Click(object sender, EventArgs e)
        {
            ApplyDither("Bayer4");
            ditheringMode = "Bayer4";
        }

        private void bayer8Button_Click(object sender, EventArgs e)
        {
            ApplyDither("Bayer8");
            ditheringMode = "Bayer8";
        }

        private void bayer16Button_Click(object sender, EventArgs e)
        {
            ApplyDither("Bayer16");
            ditheringMode = "Bayer16";
        }

        private void floydSteinbergButton_Click(object sender, EventArgs e)
        {
            ApplyDither("FloydSteinberg");
            ditheringMode = "FloydSteinberg";
        }

        private void sierraLiteButton_Click(object sender, EventArgs e)
        {
            ApplyDither("SierraLite");
            ditheringMode = "SierraLite";
        }

        private void sierraButton_Click(object sender, EventArgs e)
        {
            ApplyDither("Sierra");
            ditheringMode = "Sierra";
        }

        private void atkinsonButton_Click(object sender, EventArgs e)
        {
            ApplyDither("Atkinson");
            ditheringMode = "Atkinson";
        }

        private void bayer3bppButton_Click(object sender, EventArgs e)
        {
            ApplyDither("BayerRgb3bpp");
            ditheringMode = "BayerRgb3bpp";
        }

        private void bayer6bppButton_Click(object sender, EventArgs e)
        {
            ApplyDither("BayerRgb6bpp");
            ditheringMode = "BayerRgb6bpp";
        }

        private void bayer9bppButton_Click(object sender, EventArgs e)
        {
            ApplyDither("BayerRgb9bpp");
            ditheringMode = "BayerRgb9bpp";
        }

        private void bayer12bppButton_Click(object sender, EventArgs e)
        {
            ApplyDither("BayerRgb12bpp");
            ditheringMode = "BayerRgb12bpp";
        }

        private void bayer15bppButton_Click(object sender, EventArgs e)
        {
            ApplyDither("BayerRgb15bpp");
            ditheringMode = "BayerRgb15bpp";
        }

        private void bayer18bppButton_Click(object sender, EventArgs e)
        {
            ApplyDither("BayerRgb18bpp");
            ditheringMode = "BayerRgb18bpp";
        }

        private void DUpDownBayer_SelectedItemChanged(object sender, EventArgs e)
        {
            if (DUpDownBayer.SelectedItem != null)
            {
                bayerNValue = (int)DUpDownBayer.SelectedItem;

                // 如果当前应用的是Bayer RGBN算法，重新应用
                if (ditheringMode == "BayerRgbNbpp")
                {
                    ApplyDither("BayerRgbNbpp");
                }

            }
        }

        private void dUpDownFS_SelectedItemChanged(object sender, EventArgs e)
        {
            if (dUpDownFS.SelectedItem != null)
            {
                fsNValue = (int)dUpDownFS.SelectedItem;

                // 如果当前应用的是FS RGBN算法，重新应用
                if (ditheringMode == "FSRgbNbpp")
                {
                    ApplyDither("FSRgbNbpp");
                }
            }
        }

        private void dUpDownSierra_SelectedItemChanged(object sender, EventArgs e)
        {
            if (dUpDownSierra.SelectedItem != null)
            {
                sierraNValue = (int)dUpDownSierra.SelectedItem;

                // 如果当前应用的是FS RGBN算法，重新应用
                if (ditheringMode == "SierraRgbNbpp")
                {
                    ApplyDither("SierraRgbNbpp");
                }
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            windor windor = new windor();
            windor.Text = "关于";
            windor.Show();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();
        }
    }
}
