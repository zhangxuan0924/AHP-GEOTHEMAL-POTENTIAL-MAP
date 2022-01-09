using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using OSGeo.GDAL;
using System.IO;
using System.Drawing.Imaging;
using System.Collections;
using System.Windows.Controls;

namespace F
{
    class GiveColour
    {
        Dataset dsToWrite = null;
        public void ToGiveColour(Dataset ds1) //参数为待读取的影像数据集
        {
            
            if (ds1 != null)
            {

                SaveFileDialog savedialog = new SaveFileDialog(); //保存对话框

                savedialog.Title = "Save";
                savedialog.Filter = "tiff文件|*.tif| img文件|*.img|bmp文件|*.bmp| jpg文件 | *.jpg"; //要保存的类型

                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    
                    byte[] colourImageToCreateR = new byte[ds1.RasterXSize * ds1.RasterYSize]; //一个存储R波段值的数组，后面用来给要创建的待保存的图上色；
                    byte[] colourImageToCreateG = new byte[ds1.RasterXSize * ds1.RasterYSize];//一个存储G波段值的数组，后面用来给要创建的待保存的图上色；
                    byte[] colourImageToCreateB = new byte[ds1.RasterXSize * ds1.RasterYSize];//一个存储B波段值的数组，后面用来给要创建的待保存的图上色；
                    double[] bufferArray = new double[ds1.RasterXSize * ds1.RasterYSize];//缓冲区的数组
                    Driver driver = Gdal.GetDriverByName("GTiff");  //创建驱动，在GDAL中创建影像,先须要明白待创建影像的格式,并获取到该影像格式的驱动

                    dsToWrite = driver.Create(savedialog.FileName, ds1.RasterXSize, ds1.RasterYSize,
                        3, DataType.GDT_Byte, null); //要创建并写入的保存在对话框所选路径的数据集；

                    Band bandWrite1 = dsToWrite.GetRasterBand(1); //要写入的数据集的第1个波段
                    Band bandWrite2 = dsToWrite.GetRasterBand(2); //要写入的数据集的第2个波段
                    Band bandWrite3 = dsToWrite.GetRasterBand(3); //要写入的数据集的第3个波段

                    ds1.GetRasterBand(1).ReadRaster(0, 0, ds1.RasterXSize, ds1.RasterYSize, bufferArray, ds1.RasterXSize, ds1.RasterYSize, 0, 0);
                    double[] minmax = new double[2]; //读取ds1数据集的第一个波段所有值到bufferArray数组
                    ds1.GetRasterBand(1).ComputeRasterMinMax(minmax, 0); //读取ds1数据集的第一个波段最小值和最大值到minmax数组，minmax[0]为最小值，minmax[1]为最大值

                    

                    for (int i = 0; i < ds1.RasterXSize; i++)
                    {
                        for (int j = 0; j < ds1.RasterYSize; j++)
                        {
                            double m;

                            m = (bufferArray[i + j * ds1.RasterXSize] - minmax[0]) / (minmax[1] - minmax[0]) * 10;
                            
                            if (m < 1)
                            {
                                colourImageToCreateR[i + j * ds1.RasterXSize] = 64;//要上的颜色R波段的值，赋值到这个数组的元素中
                                colourImageToCreateG[i + j * ds1.RasterXSize] = 0;//要上的颜色G波段的值，赋值到这个数组的元素中
                                colourImageToCreateB[i + j * ds1.RasterXSize] = 255;//要上的颜色B波段的值，赋值到这个数组的元素中
                            }
                            else if (m < 2)
                            {
                                colourImageToCreateR[i + j * ds1.RasterXSize] = 2; //要上的颜色R波段的值，赋值到这个数组的元素中
                                colourImageToCreateG[i + j * ds1.RasterXSize] = 140;//要上的颜色G波段的值，赋值到这个数组的元素中
                                colourImageToCreateB[i + j * ds1.RasterXSize] = 253;//要上的颜色B波段的值，赋值到这个数组的元素中
                            }
                            else if (m < 3)
                            {
                                colourImageToCreateR[i + j * ds1.RasterXSize] = 4;//要上的颜色R波段的值，赋值到这个数组的元素中
                                colourImageToCreateG[i + j * ds1.RasterXSize] = 240;//要上的颜色G波段的值，赋值到这个数组的元素中
                                colourImageToCreateB[i + j * ds1.RasterXSize] = 250;//要上的颜色B波段的值，赋值到这个数组的元素中
                            }
                            else if (m < 4)
                            {
                                colourImageToCreateR[i + j * ds1.RasterXSize] = 4;//要上的颜色R波段的值，赋值到这个数组的元素中
                                colourImageToCreateG[i + j * ds1.RasterXSize] = 250;//要上的颜色G波段的值，赋值到这个数组的元素中
                                colourImageToCreateB[i + j * ds1.RasterXSize] = 175;//要上的颜色B波段的值，赋值到这个数组的元素中
                            }
                            else if (m < 5)
                            {
                                colourImageToCreateR[i + j * ds1.RasterXSize] = 4;//要上的颜色R波段的值，赋值到这个数组的元素中
                                colourImageToCreateG[i + j * ds1.RasterXSize] = 252;//要上的颜色G波段的值，赋值到这个数组的元素中
                                colourImageToCreateB[i + j * ds1.RasterXSize] = 109;//要上的颜色B波段的值，赋值到这个数组的元素中
                            }
                            else if (m < 6)
                            {
                                colourImageToCreateR[i + j * ds1.RasterXSize] = 3;//要上的颜色R波段的值，赋值到这个数组的元素中
                                colourImageToCreateG[i + j * ds1.RasterXSize] = 252;//要上的颜色G波段的值，赋值到这个数组的元素中
                                colourImageToCreateB[i + j * ds1.RasterXSize] = 22;//要上的颜色B波段的值，赋值到这个数组的元素中
                            }
                            else if (m < 7)
                            {
                                colourImageToCreateR[i + j * ds1.RasterXSize] = 60;//要上的颜色R波段的值，赋值到这个数组的元素中
                                colourImageToCreateG[i + j * ds1.RasterXSize] = 252;//要上的颜色G波段的值，赋值到这个数组的元素中
                                colourImageToCreateB[i + j * ds1.RasterXSize] = 3;//要上的颜色B波段的值，赋值到这个数组的元素中
                            }
                            else if (m < 8)
                            {
                                colourImageToCreateR[i + j * ds1.RasterXSize] = 227;//要上的颜色R波段的值，赋值到这个数组的元素中
                                colourImageToCreateG[i + j * ds1.RasterXSize] = 252;//要上的颜色G波段的值，赋值到这个数组的元素中
                                colourImageToCreateB[i + j * ds1.RasterXSize] = 3;//要上的颜色B波段的值，赋值到这个数组的元素中
                            }
                            else if (m < 9)
                            {
                                colourImageToCreateR[i + j * ds1.RasterXSize] = 250;//要上的颜色R波段的值，赋值到这个数组的元素中
                                colourImageToCreateG[i + j * ds1.RasterXSize] = 121;//要上的颜色G波段的值，赋值到这个数组的元素中
                                colourImageToCreateB[i + j * ds1.RasterXSize] = 3;//要上的颜色B波段的值，赋值到这个数组的元素中
                            }
                            else if (m < 11)
                            {
                                colourImageToCreateR[i + j * ds1.RasterXSize] = 250;//要上的颜色R波段的值，赋值到这个数组的元素中
                                colourImageToCreateG[i + j * ds1.RasterXSize] = 52;//要上的颜色G波段的值，赋值到这个数组的元素中
                                colourImageToCreateB[i + j * ds1.RasterXSize] = 2;//要上的颜色B波段的值，赋值到这个数组的元素中
                            }
                        }
                    }

                    bandWrite1 = dsToWrite.GetRasterBand(1);
                    bandWrite2 = dsToWrite.GetRasterBand(2);
                    bandWrite3 = dsToWrite.GetRasterBand(3);
                    //分别把三个R，G，B数组的的值写入待创建图像的相应波段
                    bandWrite1.WriteRaster(0, 0, ds1.RasterXSize, ds1.RasterYSize, colourImageToCreateR, ds1.RasterXSize, ds1.RasterYSize, 0, 0);
                    bandWrite2.WriteRaster(0, 0, ds1.RasterXSize, ds1.RasterYSize, colourImageToCreateG, ds1.RasterXSize, ds1.RasterYSize, 0, 0);
                    bandWrite3.WriteRaster(0, 0, ds1.RasterXSize, ds1.RasterYSize, colourImageToCreateB, ds1.RasterXSize, ds1.RasterYSize, 0, 0);
                    
                    MessageBox.Show("完成!");
                    bandWrite1.FlushCache();
                    bandWrite1.Dispose();
                    bandWrite2.FlushCache();
                    bandWrite2.Dispose();
                    bandWrite3.FlushCache();
                    bandWrite3.Dispose();
                    dsToWrite.Dispose();
                }
            }
        }
    }
}
