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
using System.Windows.Shapes;

namespace Postcard
{
    /// <summary>
    /// Interaction logic for ShowPhoto.xaml
    /// </summary>
    public partial class ShowPhoto : Window
    {
        public ShowPhoto()
        {
            InitializeComponent();
            ShowRecentPhto();
        }

        private void Button_ClickReturn(object sender, RoutedEventArgs e)
        {
            Image1.Source = null;
            Image2.Source = null;
            Image3.Source = null;
            Image4.Source = null;
            Image5.Source = null;
            Image6.Source = null;

            MainWindow win1 = new MainWindow();
            win1.Show();
            this.Close();
        }

        private void ShowRecentPhto()
        {
            string localData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//special call for my doc
            var imagePath = localData + @"\Postcards" + @"\" + "text";//saved in MyDocuments/Postcards
            var dir = new System.IO.DirectoryInfo(imagePath);//makes a dir whhere image path is, to be used with GetFile
            //creates an array for recent items

            var filenames = dir.GetFiles().OrderByDescending(f => f.LastWriteTime).ToList(); ;

            
            //grab most recent 6
            if (filenames.Count >= 6)
            {
                ImageSource imageSource0 = new BitmapImage(new Uri((dir + @"\" + filenames[0])));
                ImageSource imageSource1 = new BitmapImage(new Uri((dir + @"\" + filenames[1])));
                ImageSource imageSource2 = new BitmapImage(new Uri((dir + @"\" + filenames[2])));
                ImageSource imageSource3 = new BitmapImage(new Uri((dir + @"\" + filenames[3])));
                ImageSource imageSource4 = new BitmapImage(new Uri((dir + @"\" + filenames[4])));
                ImageSource imageSource5 = new BitmapImage(new Uri((dir + @"\" + filenames[5])));

                Image1.Source = imageSource0;
                Image2.Source = imageSource1;
                Image3.Source = imageSource2;
                Image4.Source = imageSource3;
                Image5.Source = imageSource4;
                Image6.Source = imageSource5;
            }

            //grab most recent 5
            else if (filenames.Count == 5)
            {
                ImageSource imageSource0 = new BitmapImage(new Uri((dir + @"\" + filenames[0])));
                ImageSource imageSource1 = new BitmapImage(new Uri((dir + @"\" + filenames[1])));
                ImageSource imageSource2 = new BitmapImage(new Uri((dir + @"\" + filenames[2])));
                ImageSource imageSource3 = new BitmapImage(new Uri((dir + @"\" + filenames[3])));
                ImageSource imageSource4 = new BitmapImage(new Uri((dir + @"\" + filenames[4])));

                Image1.Source = imageSource0;
                Image2.Source = imageSource1;
                Image3.Source = imageSource2;
                Image4.Source = imageSource3;
                Image5.Source = imageSource4;
            }

            else if (filenames.Count == 4)
            {
                ImageSource imageSource0 = new BitmapImage(new Uri((dir + @"\" + filenames[0])));
                ImageSource imageSource1 = new BitmapImage(new Uri((dir + @"\" + filenames[1])));
                ImageSource imageSource2 = new BitmapImage(new Uri((dir + @"\" + filenames[2])));
                ImageSource imageSource3 = new BitmapImage(new Uri((dir + @"\" + filenames[3])));

                Image1.Source = imageSource0;
                Image2.Source = imageSource1;
                Image3.Source = imageSource2;
                Image4.Source = imageSource3;
            }

            else if (filenames.Count == 3)
            {
                ImageSource imageSource0 = new BitmapImage(new Uri((dir + @"\" + filenames[0])));
                ImageSource imageSource1 = new BitmapImage(new Uri((dir + @"\" + filenames[1])));
                ImageSource imageSource2 = new BitmapImage(new Uri((dir + @"\" + filenames[2])));

                Image1.Source = imageSource0;
                Image2.Source = imageSource1;
                Image3.Source = imageSource2;
            }

            else if (filenames.Count == 2)
            {
                ImageSource imageSource0 = new BitmapImage(new Uri((dir + @"\" + filenames[0])));
                ImageSource imageSource1 = new BitmapImage(new Uri((dir + @"\" + filenames[1])));

                Image1.Source = imageSource0;
                Image2.Source = imageSource1;
            }

            else if (filenames.Count == 1)
            {
                ImageSource imageSource0 = new BitmapImage(new Uri((dir + @"\" + filenames[0])));

                Image1.Source = imageSource0;

            }
        }

    }

}
