using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Net.Mail;

namespace Postcard
{
    /// <summary>
    /// Interaction logic for AddTextScreen.xaml
    /// </summary>
    public partial class AddTextScreen : Window
    {
        public AddTextScreen()
        {
            InitializeComponent();
    

           
        }

        private void addTextToImage(string greetingText, string message)
        {

            //used to find my documents, get the most recent file saved from the previous screen
            //create a new fold for text edited files
            
            string localData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//special call for my doc
            var imagePath = localData + @"\Postcards";//saved in MyDocuments/Postcards
            var dir = new DirectoryInfo(imagePath);//makes a dir whhere image path is, to be used with GetFile
            var recentFile = dir.GetFiles().OrderByDescending(f => f.LastWriteTime).First();//gets recent file
            var dirFile = dir + @"\" + recentFile;//string of recent file
            var ndirFile=dir +@"\" + "text";//string of recent file
            var ndir = new DirectoryInfo(ndirFile);//makes a dir whhere image path is, to be used with GetFile 
            PointF greetings;
            PointF userMessage;
            
            //location of text
            //can add more options here
            if (Right.IsSelected)
            {
                greetings = new PointF(400, 330);
                userMessage = new PointF(330, 400);
            }

            else
            {
                 greetings = new PointF(10f, 10f);
                 userMessage = new PointF(40f, 70f);
            }

            Bitmap bitmap = (Bitmap)System.Drawing.Image.FromFile((dirFile));//load the image file


            //write on image and save new file
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont = new Font("Segoe UI", 48))
                {
                    graphics.DrawString(greetingText, arialFont, System.Drawing.Brushes.DarkBlue, greetings);
                    graphics.DrawString(message, arialFont, System.Drawing.Brushes.Red, userMessage);
                }
            }

            //saves to new folder in mydoc/post/text
            if (!Directory.Exists(ndirFile))
            {
                DirectoryInfo createdir = Directory.CreateDirectory(ndirFile);
                MessageBox.Show("New Directory Created", "Success");
            }

            
            //check if file exists, if yes append n and update most recent file
            FileInfo fInfo = new FileInfo(ndirFile + @"\" + recentFile);

            if (fInfo.Exists)
            {
                recentFile = ndir.GetFiles().OrderByDescending(f => f.LastWriteTime).First();//gets recent file

                bitmap.Save(ndirFile + @"\" + "_"+recentFile);//save the image file
                
            }
            else
            {
                bitmap.Save(ndirFile + @"\" + recentFile);//save the image file
            }

            recentFile = ndir.GetFiles().OrderByDescending(f => f.LastWriteTime).First();//gets recent file

            ImageSource imageSource = new BitmapImage(new Uri((ndirFile + @"\" + recentFile)));

            DisplayCard.Source = imageSource;
        }
        /// <summary>
        /// returns to previous screen
        /// </summary>

        private void Button_ClickReturn(object sender, RoutedEventArgs e)
        {
            MainWindow win1 = new MainWindow();
            win1.Show();
            this.Close();

        }

        //on submit button, passs in string text and generate image
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string greeting = UserGreeting.Text;
            string message = UserText.Text;
            DisplayCard.Source = null;
            addTextToImage(greeting, message);
        }

        //create a mail message, get user info from text
        private void Email_send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //create mail struct
                //works with outlook

                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");
                
                //user inputs email and send to email 
                string sendto= email_to.Text;
                string sendfrom= email_from.Text;
                string greeting = UserGreeting.Text;
                string message = UserText.Text;
                string subject = greeting + message;
                string password = passBox.Password.ToString();
                string sendName = userName.Text;
                string recip = recipient.Text;

                mail.To.Add(sendto);
                mail.From = new MailAddress(sendfrom);
                mail.Subject = (subject);

                StringBuilder emailText = new StringBuilder();

                //create body of email
                emailText.AppendLine("Hello " + recip);
                emailText.AppendLine("");
                emailText.AppendLine(greeting+ " "+ sendName);
     
                mail.Body = emailText.ToString();

                //get the recent file and attach
                string localData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//special call for my doc
                var imagePath = localData + @"\Postcards" + @"\" + "text";//saved in MyDocuments/Postcards
                var dir = new DirectoryInfo(imagePath);//makes a dir whhere image path is, to be used with GetFile
                var recentFile = dir.GetFiles().OrderByDescending(f => f.LastWriteTime).First();//gets recent file
                var dirFile = dir + @"\" + recentFile;//string of recent file

                Attachment attachment = new Attachment(dirFile);

                mail.Attachments.Add(attachment);

                //email is created, access server and use pass. port number from outlook.com
                SmtpServer.Credentials = new System.Net.NetworkCredential(sendfrom, password);
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("PostCard Has been send to "+ sendto);
                MainWindow win1 = new MainWindow();
                win1.Show();
                this.Close();

            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString());
            }
        }

    }
}
