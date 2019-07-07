using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Expression.Encoder.Devices;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using WebEye.Controls.Wpf;
using System.IO;

namespace Postcard
{
    /// <summary>
    /// Interaction logic for CapturePost.xaml
    ///
    /// </summary>
    public partial class CapturePost : Window
    {



        public CapturePost()
        {
            InitializeComponent();
            InitializeComboBox();

            this.DataContext = this;
        }

        /// <summary>
        /// reads number of devices
        /// asks to exit if no camera device is found
        /// for this current app, only selects the windows default camera at index 0
        /// </summary>
        private void InitializeComboBox()
        {
            comboBox.ItemsSource = webCameraControl.GetVideoCaptureDevices();


            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedItem = comboBox.Items[0];
            }
            else if (comboBox.Items.Count == 0)
            {
                MessageBoxResult result = MessageBox.Show("Exit Application?","No Camera Device Found" , MessageBoxButton.YesNo, MessageBoxImage.Question);


                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
               
            }
        }

        /// <summary>
        /// Below is logic for ensuring camera is selected
        /// </summary>

        //start button is enabled if list has at least one item
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startButton.IsEnabled = e.AddedItems.Count > 0;
        }
        
        //on start button click start camera
        //camera is selected from list
        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            var cameraId = (WebCameraId)comboBox.SelectedItem;
            webCameraControl.StartCapture(cameraId);
        }
        //stop capture
        private void OnStopButtonClick(object sender, RoutedEventArgs e)
        {
            webCameraControl.StopCapture();


        }

        /// <summary>
        /// captures photos
        /// </summary>

        private void OnImageButtonClick(object sender, RoutedEventArgs e)
        {
            string localData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var imagePath = localData + @"\Postcards";
            if (!Directory.Exists(imagePath))
            {
                DirectoryInfo dir = Directory.CreateDirectory(imagePath);
                MessageBox.Show("New Directory Created", "Success");
            }

            var saveFile = new SaveFileDialog { InitialDirectory= imagePath,  Filter = "Bitmap Image|*.bmp" };
        
            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    webCameraControl.GetCurrentImage().Save(saveFile.FileName);
                    MessageBox.Show("File saved sucessfully", "Success");
                    DisplayImage(imagePath);
                }

                catch (Exception except)
                {
                MessageBox.Show("Try a different file name", "Error");
                }
            }
        }   

        //function for population the image by most recent
        private void DisplayImage(string directory)
        {
            var dir = new DirectoryInfo(directory);
            var recentFile = dir.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            var dirFile = directory + @"\"+recentFile;
            ImageSource imageSource = new BitmapImage(new Uri(dirFile));

            DisplayRecent.Source = imageSource;
            Add_text.Visibility=Visibility.Visible;

        }

        /// <summary>
        /// when return to previous scree stop capture, to prevent mem leaks
        /// </summary>
  
        private void Button_ClickReturn(object sender, RoutedEventArgs e)
        {
            //if camera is capturing, stop
            if (webCameraControl.IsCapturing)
            {
                webCameraControl.StopCapture();
            }
            MainWindow win1= new MainWindow();
            win1.Show();
            this.Close();

        }

        //add text brings to the add text screen and stops a running camera
        private void Add_text_Click(object sender, RoutedEventArgs e)
        {

            //if camera is capturing, stop and open next screen
            if (webCameraControl.IsCapturing)
            {
                webCameraControl.StopCapture();
            }
            AddTextScreen win2 = new AddTextScreen();
            win2.Show();
            this.Close();
        }
    }
}
