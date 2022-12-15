using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Collections.Generic;

namespace DebugLoggDecoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members 
        private Dictionary<int, string> data;
        List<string> values = new List<string>();
        String Message;
        #endregion

        #region Constructor 
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            data = new Dictionary<int, string>();
            values = new List<string>();
        }
        #endregion

        #region Private Methods 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReadFile();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void ReadFile()
        {
            textBoxStatus.Text = "Please select the file";
            string[] ReadData = File.ReadAllLines("D:\\DATA\\Documents\\Sample.txt");
            textBoxStatus.Text = "Please select the file";

            // Check the file int the directory 
            // If default file exists then proced futhur decode 
            // else ask user to select the file form the folder location 
            // Update the status in the output window 
            // Update processing status in the window
            // Show the result

            for (int i = 0; i < ReadData.Length; i++)
            {
                string[] Split = ReadData[i].Split(' ');

                int line = int.Parse(Split[0]);
                string timestamp = (Split[3] + ":" + Split[4]);

                data.Add(line, timestamp);

            }
            var lookup = data.ToLookup(x => x.Value, x => x.Key).Where(x => x.Count() > 1);
            foreach (var item in lookup)
            {

                var keys = item.Aggregate("", (s, v) => s + ", " + v);
                var message = "Repeted Timestamp " + item.Key + ":" + keys;
                values.Add(message);
                Message = values.ToString();
                textBoxStatus.Text = message;

            }

        }

        #endregion


    }
}
