using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OSGeo.GDAL;
using F.Pagess;

namespace F
{
    public partial class ToProcessImages : Window
    {
        Dataset dsToWrite;
        Page11 page11;
        Page22 page22;
        Page33 page33;
        double[] assignedWeights; double[] childWeights0; double[] childWeights1; double[] childWeights2;

        public ToProcessImages(double[] assignedWeights, double[] childWeights0, double[] childWeights1, double[] childWeights2)
        {
            this.assignedWeights = assignedWeights;
            this.childWeights0 = childWeights0;
            this.childWeights1 = childWeights1;
            this.childWeights2 = childWeights2;

            InitializeComponent();
            GdalConfiguration.ConfigureGdal();
            Gdal.AllRegister();
            //myGdalProvider = new GDALprovider();

            page11 = new Page11(listview, comboRed, comboGreen, comboBlue, comboGrey, toggleButton);
            page22 = new Page22(listview, comboRed, comboGreen, comboBlue, comboGrey, toggleButton);
            page33 = new Page33(listview, comboRed, comboGreen, comboBlue, comboGrey, toggleButton);
            this.PagesNavigation.NavigationService.Navigate(page11);
        }
  

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("E-mail: 1781041179@qq.com\n\nContact: +86 137 8124 6948");
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void slope_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page11);
        }

        private void rain_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page11);
        }

        private void drain_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page11);
        }

        private void soil_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page11);
        }

        private void lava_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page33);
        }

        private void earthquake_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page22);
        }

        private void fault_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page11);
        }

        private void ColorizeButton_Click(object sender, RoutedEventArgs e)
        {
           
            GiveColour giveColour = new GiveColour();
            giveColour.ToGiveColour(page11.dsCurrent);
        
           // if (this.PagesNavigation.NavigationService.Equals(page11))
            //{
              //  giveColour.ToGiveColour(page11.dsCurrent);
            //}
           // else if (this.PagesNavigation.NavigationService.Equals(page22))
           // {
             //   giveColour.ToGiveColour(page22.dsCurrent);
           // }
           // else if (this.PagesNavigation.NavigationService.Equals(page33))
            //{
              //  giveColour.ToGiveColour(page33.dsCurrent);
            //}

        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {

            if (page11.dsCurrent == null || page22.dsCurrent == null || page33.dsCurrent == null)
                System.Windows.MessageBox.Show("Please load all images");
            else
            {

                SaveFileDialog savedialog = new SaveFileDialog(); //保存对话框

                savedialog.Title = "Save";
                savedialog.Filter = "tiff文件|*.tif| bmg文件|*.bmg|imp文件|*.imp| jpg文件 | *.jpg"; //要保存的类型

                if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    double[] ImageToCreate = new double[page11.dsCurrent.RasterXSize * page11.dsCurrent.RasterYSize]; 
       
                    double[] bufferArray1= new double[page11.dsCurrent.RasterXSize*page11.dsCurrent.RasterYSize];
                    double[] bufferArray2 = new double[page22.dsCurrent.RasterXSize*page22.dsCurrent.RasterYSize];
                    double[] bufferArray3= new double[page33.dsCurrent.RasterXSize*page33.dsCurrent.RasterYSize];
                    Driver driver = Gdal.GetDriverByName("GTiff"); 

                    dsToWrite = driver.Create(savedialog.FileName, page11.dsCurrent.RasterXSize, page11.dsCurrent.RasterYSize,
                        1, DataType.GDT_Float64, null); //要创建并写入的保存在对话框所选路径的数据集；

                    Band bandWrite1 = dsToWrite.GetRasterBand(1);              

                    page11.dsCurrent.GetRasterBand(1).ReadRaster(0, 0, page11.dsCurrent.RasterXSize, page11.dsCurrent.RasterYSize, bufferArray1, page11.dsCurrent.RasterXSize, page11.dsCurrent.RasterYSize, 0, 0);
                    page22.dsCurrent.GetRasterBand(1).ReadRaster(0, 0, page22.dsCurrent.RasterXSize, page22.dsCurrent.RasterYSize, bufferArray2, page22.dsCurrent.RasterXSize, page22.dsCurrent.RasterYSize, 0, 0);
                    page33.dsCurrent.GetRasterBand(1).ReadRaster(0, 0, page33.dsCurrent.RasterXSize, page33.dsCurrent.RasterYSize, bufferArray3, page33.dsCurrent.RasterXSize, page33.dsCurrent.RasterYSize, 0, 0);

                    for (int i = 0; i < page11.dsCurrent.RasterXSize; i++)
                    {
                        for (int j = 0; j < page11.dsCurrent.RasterYSize; j++)
                        {
                            if (bufferArray1[i + j * page11.dsCurrent.RasterXSize] <=1000)
                            {
                                bufferArray1[i + j * page11.dsCurrent.RasterXSize] = childWeights0[0];
                            }
                            else if (bufferArray1[i + j * page11.dsCurrent.RasterXSize] <=2000)
                            {
                                bufferArray1[i + j * page11.dsCurrent.RasterXSize] = childWeights0[1];
                            }
                            else if (bufferArray1[i + j * page11.dsCurrent.RasterXSize] <=3000)
                            {
                                bufferArray1[i + j * page11.dsCurrent.RasterXSize] = childWeights0[2];
                            }
                            else if (bufferArray1[i + j * page11.dsCurrent.RasterXSize] <= 4000)
                            {
                                bufferArray1[i + j * page11.dsCurrent.RasterXSize] = childWeights0[3];
                            }
                            else if (bufferArray1[i + j * page11.dsCurrent.RasterXSize] <=9000)
                            {
                                bufferArray1[i + j * page11.dsCurrent.RasterXSize] = childWeights0[4];
                            }
                            else if (bufferArray1[i + j * page11.dsCurrent.RasterXSize] >9000)
                            {
                                bufferArray1[i + j * page11.dsCurrent.RasterXSize] = childWeights0[5];
                            }

                        }
                    }


                    for (int i = 0; i < page22.dsCurrent.RasterXSize; i++)
                    {
                        for (int j = 0; j < page22.dsCurrent.RasterYSize; j++)
                        {
                            if (bufferArray2[i + j * page22.dsCurrent.RasterXSize] <= 5000)
                            {
                                bufferArray2[i + j * page22.dsCurrent.RasterXSize] = childWeights1[0];
                            }
                            else if (bufferArray2[i + j * page22.dsCurrent.RasterXSize] <= 10000)
                            {
                                bufferArray2[i + j * page22.dsCurrent.RasterXSize] = childWeights1[1];
                            }
                            else if (bufferArray2[i + j * page22.dsCurrent.RasterXSize] <= 15000)
                            {
                                bufferArray2[i + j * page22.dsCurrent.RasterXSize] = childWeights1[2];
                            }
                            else if (bufferArray2[i + j * page22.dsCurrent.RasterXSize] <= 20000)
                            {
                                bufferArray2[i + j * page22.dsCurrent.RasterXSize] = childWeights1[3];
                            }
                            else if (bufferArray2[i + j * page22.dsCurrent.RasterXSize] <= 25000)
                            {
                                bufferArray2[i + j * page22.dsCurrent.RasterXSize] = childWeights1[4];
                            }
                            else if (bufferArray2[i + j * page22.dsCurrent.RasterXSize] <= 30000)
                            {
                                bufferArray2[i + j * page22.dsCurrent.RasterXSize] = childWeights1[5];
                            }
                            else if (bufferArray2[i + j * page22.dsCurrent.RasterXSize] > 30000)
                            {
                                bufferArray2[i + j * page22.dsCurrent.RasterXSize] = childWeights1[6];
                            }
                        }
                    }


                    for (int i = 0; i < page33.dsCurrent.RasterXSize; i++)
                    {
                        for (int j = 0; j < page33.dsCurrent.RasterYSize; j++)
                        {
                            if (bufferArray3[i + j * page33.dsCurrent.RasterXSize] <= 5000)
                            {
                                bufferArray3[i + j * page33.dsCurrent.RasterXSize] = childWeights2[0];
                            }
                            else if (bufferArray3[i + j * page33.dsCurrent.RasterXSize] <= 10000)
                            {
                                bufferArray3[i + j * page33.dsCurrent.RasterXSize] = childWeights2[1];
                            }
                            else if (bufferArray3[i + j * page33.dsCurrent.RasterXSize] <= 15000)
                            {
                                bufferArray3[i + j * page33.dsCurrent.RasterXSize] = childWeights2[2];
                            }
                            else if (bufferArray3[i + j * page33.dsCurrent.RasterXSize] <= 20000)
                            {
                                bufferArray3[i + j * page33.dsCurrent.RasterXSize] = childWeights2[3];
                            }
                            else if (bufferArray3[i + j * page33.dsCurrent.RasterXSize] <= 25000)
                            {
                                bufferArray3[i + j * page33.dsCurrent.RasterXSize] = childWeights2[4];
                            }
                            else if (bufferArray3[i + j * page33.dsCurrent.RasterXSize] <= 30000)
                            {
                                bufferArray3[i + j * page33.dsCurrent.RasterXSize] = childWeights2[5];
                            }
                            else if (bufferArray3[i + j * page33.dsCurrent.RasterXSize] > 30000)
                            {
                                bufferArray3[i + j * page33.dsCurrent.RasterXSize] = childWeights2[6];
                            }
                        }
                    }
                    double[] bufferArray=new double[page11.dsCurrent.RasterXSize * page11.dsCurrent.RasterYSize];
                    for (int i = 0; i < page11.dsCurrent.RasterXSize; i++)
                    {
                        for (int j = 0; j < page11.dsCurrent.RasterYSize; j++)
                        {
                            bufferArray[i + j * page11.dsCurrent.RasterXSize] =
                            (bufferArray1[i + j * page11.dsCurrent.RasterXSize] * assignedWeights[0]) 
                         + (bufferArray2[i + j * page11.dsCurrent.RasterXSize] * assignedWeights[1]) 
                         + (bufferArray3[i + j * page11.dsCurrent.RasterXSize] * assignedWeights[2]);
                        }
                    }

                    //数组的值写入待创建图像的波段
                    bandWrite1.WriteRaster(0, 0, page11.dsCurrent.RasterXSize, page11.dsCurrent.RasterYSize, bufferArray, page11.dsCurrent.RasterXSize, page11.dsCurrent.RasterYSize, 0, 0);
                    System.Windows.MessageBox.Show("OK");
                    bandWrite1.FlushCache();
                    bandWrite1.Dispose();
                    dsToWrite.Dispose();
                }
            }
        }
    }
}
