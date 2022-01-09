using System;
using System.Windows;
using System.Windows.Controls;
using OSGeo.GDAL;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Controls.Primitives;

namespace F.Pagess
{

    public partial class Page33 : Page
    {
        public Dataset dsCurrent;
        readonly GDALprovider myGdalProvider = new GDALprovider();
        private Bitmap bitmapCurrent;

        System.Windows.Controls.ListView listView;
        System.Windows.Controls.ComboBox comboRed; System.Windows.Controls.ComboBox comboGreen; System.Windows.Controls.ComboBox comboBlue; System.Windows.Controls.ComboBox comboGrey; ToggleButton toggleButton;

        public Page33(System.Windows.Controls.ListView listView, System.Windows.Controls.ComboBox comboRed, System.Windows.Controls.ComboBox comboGreen, System.Windows.Controls.ComboBox comboBlue, System.Windows.Controls.ComboBox comboGrey, ToggleButton toggleButton)
        {
            this.comboGrey = comboGrey;
            this.toggleButton = toggleButton;
            this.listView = listView;
            this.comboBlue = comboBlue;
            this.comboGreen = comboGreen;
            this.comboRed = comboRed;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImageLoad();
        }

        private void ImageShow(Dataset ds, int[] disband)
        {
            Rectangle pictureRect = new Rectangle
            {
                X = 0,
                Y = 0,
                Width = pictureBoxImage.Width,
                Height = pictureBoxImage.Height
            };

            Bitmap bitmap = myGdalProvider.GetImage(ds, pictureRect, disband, listView);   //遥感影像构建位图
            pictureBoxImage.Image = bitmap;                   //将位图传递给PictureBox控件进行显示
            bitmapCurrent = bitmap;
        }
        private void ImageLoad()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "jpg文件|*.jpg|Tiff文件|*.tif|Erdas img文件|*.img|bmp文件|*.bmp|所有文件|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filename = dlg.FileName;

                try
                {
                    dsCurrent = Gdal.Open(filename,
                        Access.GA_ReadOnly);

                    if (dsCurrent == null)
                    {
                        System.Windows.MessageBox.Show("影像打开失败, 数据集为空\n Fail to show the image");
                        return;
                    }
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.Message);
                    return;
                }
                comboBlue.Items.Clear();
                comboGreen.Items.Clear();
                comboRed.Items.Clear();
                comboGrey.Items.Clear();

                for (int i = 1; i <= dsCurrent.RasterCount; i++)
                {
                    comboRed.Items.Add("Band-" + i);
                    comboBlue.Items.Add("Band-" + i);
                    comboGreen.Items.Add("Band-" + i);
                    comboGrey.Items.Add("Band-" + i);
                }
            }

        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (dsCurrent == null)
            {
                System.Windows.MessageBox.Show("  加载影像\n  Plese load images！");
            }
            else
            {
                int[] disband;

                if (((comboRed.SelectedIndex < 0 || comboGreen.SelectedIndex < 0 || comboBlue.SelectedIndex < 0) && (toggleButton.IsChecked == true))
                    || ((comboGrey.SelectedIndex < 0 && toggleButton.IsChecked == false)))
                    System.Windows.MessageBox.Show("    请选择波段!\n    Please select each band!");
                else
                {
                    if (toggleButton.IsChecked == true)
                    {
                        int[] disbandRGB = new int[3];
                        disbandRGB[0] = comboRed.SelectedIndex + 1;
                        disbandRGB[1] = comboGreen.SelectedIndex + 1;
                        disbandRGB[2] = comboBlue.SelectedIndex + 1;
                        disband = disbandRGB;
                    }
                    else
                    {
                        int[] disbandGray = new int[1];
                        disbandGray[0] = comboGrey.SelectedIndex + 1;
                        disband = disbandGray;
                    }

                    ImageShow(dsCurrent, disband);
                }
            }
        }
    }
}
