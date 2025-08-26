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

        // ��ǰѡ���Nֵ
        private int bayerNValue = 1;
        private int fsNValue = 1;
        private int sierraNValue = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "����";

            // ��ʼ��DomainUpDown�ؼ�
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

        // ���¿ؼ�״̬
        private void UpdateControlsState()  //ÿ��ѡ�ж������
        {
            DUpDownBayer.Enabled = radioBayer.Checked;
            dUpDownFS.Enabled = radioFS.Checked;
            dUpDownSierra.Enabled = radioSerra.Checked;

            ApplyCurrentRgbNAlgorithm();
        }

        // RadioButton ѡ��ı��¼�
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

        private void ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "��ͼ���ļ�";
                dlg.Filter = "BMP�ļ�|*.bmp|�����ļ�|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        originalImage = new Bitmap(dlg.FileName);
                        processedImage = null;
                        ShowOriginal();
                        toolStripStatusLabel.Text = $"�Ѽ���: {System.IO.Path.GetFileName(dlg.FileName)}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"�޷���ͼ��: {ex.Message}", "����",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toolStripStatusLabel.Text = "��ͼ��ʧ��";
                    }
                }
            }
        }

        private void �˳�ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show("û��ͼ��ɱ���", "��ʾ",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "����ͼ���ļ�";
                dlg.Filter = "BMP�ļ�|*.bmp";
                dlg.DefaultExt = "bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox.Image.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        toolStripStatusLabel.Text = $"�ѱ���: {System.IO.Path.GetFileName(dlg.FileName)}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"�޷�����ͼ��: {ex.Message}", "����",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toolStripStatusLabel.Text = "����ͼ��ʧ��";
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
            toolStripStatusLabel.Text = colorMode ? "���л���ɫ��ģʽ" : "���л����Ҷ�ģʽ";

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
                toolStripStatusLabel.Text = "��ʾԭʼͼ��";
                ditheringMode = "";
            }
        }

        private void ApplyDither(string methodName)
        {
            if (originalImage == null)
            {
                MessageBox.Show("���ȴ�һ��ͼ���ļ�", "��ʾ",
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
                        toolStripStatusLabel.Text = "Ӧ�� Bayer 2x2 ����";
                        break;
                    case "Bayer3":
                        processedImage = Dither.Bayer3(originalImage, colorMode);
                        toolStripStatusLabel.Text = "Ӧ�� Bayer 3x3 ����";
                        break;
                    case "Bayer4":
                        processedImage = Dither.Bayer4(originalImage, colorMode);
                        toolStripStatusLabel.Text = "Ӧ�� Bayer 4x4 ����";
                        break;
                    case "Bayer8":
                        processedImage = Dither.Bayer8(originalImage, colorMode);
                        toolStripStatusLabel.Text = "Ӧ�� Bayer 8x8 ����";
                        break;
                    case "Bayer16":
                        processedImage = Dither.Bayer16(originalImage, colorMode);
                        toolStripStatusLabel.Text = "Ӧ�� Bayer 16x16 ����";
                        break;
                    case "FloydSteinberg":
                        processedImage = Dither.FloydSteinberg(originalImage, colorMode);
                        toolStripStatusLabel.Text = "Ӧ�� Floyd-Steinberg ����";
                        break;
                    case "SierraLite":
                        processedImage = Dither.SierraLite(originalImage, colorMode);
                        toolStripStatusLabel.Text = "Ӧ�� Sierra Lite ����";
                        break;
                    case "Sierra":
                        processedImage = Dither.Sierra(originalImage, colorMode);
                        toolStripStatusLabel.Text = "Ӧ�� Sierra ����";
                        break;
                    case "Atkinson":
                        processedImage = Dither.Atkinson(originalImage, colorMode);
                        toolStripStatusLabel.Text = "Ӧ�� Atkinson ����";
                        break;
                    case "BayerRgb3bpp":
                        processedImage = Dither.BayerRgb3bpp(originalImage);
                        toolStripStatusLabel.Text = "Ӧ�� Bayer RGB 3bpp ����";
                        break;
                    case "BayerRgb6bpp":
                        processedImage = Dither.BayerRgb6bpp(originalImage);
                        toolStripStatusLabel.Text = "Ӧ�� Bayer RGB 6bpp ����";
                        break;
                    case "BayerRgb9bpp":
                        processedImage = Dither.BayerRgb9bpp(originalImage);
                        toolStripStatusLabel.Text = "Ӧ�� Bayer RGB 9bpp ����";
                        break;
                    case "BayerRgb12bpp":
                        processedImage = Dither.BayerRgb12bpp(originalImage);
                        toolStripStatusLabel.Text = "Ӧ�� Bayer RGB 12bpp ����";
                        break;
                    case "BayerRgb15bpp":
                        processedImage = Dither.BayerRgb15bpp(originalImage);
                        toolStripStatusLabel.Text = "Ӧ�� Bayer RGB 15bpp ����";
                        break;
                    case "BayerRgb18bpp":
                        processedImage = Dither.BayerRgb18bpp(originalImage);
                        toolStripStatusLabel.Text = "Ӧ�� Bayer RGB 18bpp ����";
                        break;
                    case "BayerRgbNbpp":
                        processedImage = Dither.BayerRgbNbpp(originalImage, bayerNValue, colorMode);
                        toolStripStatusLabel.Text = $"Ӧ�� Bayer RGB N={bayerNValue} ����";
                        break;
                    case "FSRgbNbpp":
                        processedImage = Dither.FSRgbNbpp(originalImage, fsNValue, colorMode);
                        toolStripStatusLabel.Text = $"Ӧ�� FS RGB N={fsNValue} ����";
                        break;
                    case "SierraRgbNbpp":
                        processedImage = Dither.SierraRgbNbpp(originalImage, sierraNValue, colorMode);
                        toolStripStatusLabel.Text = $"Ӧ�� Sierra RGB N={sierraNValue} ����";
                        break;
                    default:
                        MessageBox.Show("δ֪�Ķ�������", "����",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                pictureBox.Image = processedImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"����ͼ��ʱ����: {ex.Message}", "����",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "����ͼ��ʧ��";
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

                // �����ǰӦ�õ���Bayer RGBN�㷨������Ӧ��
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

                // �����ǰӦ�õ���FS RGBN�㷨������Ӧ��
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

                // �����ǰӦ�õ���FS RGBN�㷨������Ӧ��
                if (ditheringMode == "SierraRgbNbpp")
                {
                    ApplyDither("SierraRgbNbpp");
                }
            }
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            windor windor = new windor();
            windor.Text = "����";
            windor.Show();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();
        }
    }
}
