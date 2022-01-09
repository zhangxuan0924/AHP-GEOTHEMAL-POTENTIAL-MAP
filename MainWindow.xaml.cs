using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using F.Pages;

namespace F
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    class a
    {
        internal int f;
    }


    public partial class MainWindow : Window
    {

        
          Page1 page1 = new Page1();
        Page2 page2 = new Page2();
         Page3 page3 = new Page3();
         Page4 page4 = new Page4();

        public MainWindow()
        {
            a aa=new a();

            InitializeComponent();    
            this.PagesNavigation.NavigationService.Navigate(page1);
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void drain_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void soil_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void rain_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void slope_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetNormalizedWeights getNormalized = new GetNormalizedWeights();
            double[] assignedWeights = new double[7];
            double[] normalizedWeights = new double[7];
            
            assignedWeights[0] = Convert.ToDouble(page1.FaultWeight.Text);
            assignedWeights[1] = Convert.ToDouble(page2.EarthquakeWeight.Text);
            assignedWeights[2] = Convert.ToDouble(page3.LavaWeight.Text);

            double[] childWeights0= new double[6];
            double[] childWeights1= new double[7];
            double[] childWeights2= new double[7];
            childWeights0[0] = Convert.ToDouble( page1.FaultWeight0.Text);
            childWeights0[1] = Convert.ToDouble( page1.FaultWeight1.Text);
            childWeights0[2] = Convert.ToDouble( page1.FaultWeight2.Text);
            childWeights0[3] = Convert.ToDouble( page1.FaultWeight3.Text);
            childWeights0[4] = Convert.ToDouble( page1.FaultWeight4.Text);
            childWeights0[5] = Convert.ToDouble( page1.FaultWeight5.Text);

            childWeights1[0] = Convert.ToDouble(page2.EarthquakeWeight0.Text);
            childWeights1[1] = Convert.ToDouble(page2.EarthquakeWeight1.Text);
            childWeights1[2] = Convert.ToDouble(page2.EarthquakeWeight2.Text);
            childWeights1[3] = Convert.ToDouble(page2.EarthquakeWeight3.Text);
            childWeights1[4] = Convert.ToDouble(page2.EarthquakeWeight4.Text);
            childWeights1[5] = Convert.ToDouble(page2.EarthquakeWeight5.Text);
            childWeights1[6] = Convert.ToDouble(page2.EarthquakeWeight6.Text);

            childWeights2[0] = Convert.ToDouble(page3.LavaWeight0.Text);
            childWeights2[1] = Convert.ToDouble(page3.LavaWeight1.Text);
            childWeights2[2] = Convert.ToDouble(page3.LavaWeight2.Text);
            childWeights2[3] = Convert.ToDouble(page3.LavaWeight3.Text);
            childWeights2[4] = Convert.ToDouble(page3.LavaWeight4.Text);
            childWeights2[5] = Convert.ToDouble(page3.LavaWeight5.Text);
            childWeights2[6] = Convert.ToDouble(page3.LavaWeight6.Text);


            normalizedWeights = getNormalized.GetNormalized(assignedWeights);
            ToProcessImages toProcessImages = new ToProcessImages(normalizedWeights, childWeights0,childWeights1,childWeights2);
            toProcessImages.Show();
        }
        private void fault_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page1);
        }
        private void earthquake_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page2);
        }
        private void lava_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page3);
        }
        private void lulc_Click(object sender, RoutedEventArgs e)
        {
            this.PagesNavigation.NavigationService.Navigate(page4);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            double[] a = {1,1,1,1,1,1,1};
           
            ToProcessImages toProcessImages = new ToProcessImages(a,a,a,a);
            toProcessImages.Show();
        }
    }
}   
