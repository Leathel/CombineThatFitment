using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace CombineFitment
{
    public partial class CombineFitment : Form
    {
        string filePathCars;
        string filePathFitment;

        public CombineFitment()
        {
            InitializeComponent();
            loadingBar.Visible = false;
            timeRemaining.Visible = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void readButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(newFileNameTextBox.Text))
            {
                MessageBox.Show("You must enter in an output file!\n Also make sure its empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                Thread workerThread = new Thread(readAndWrite, 0);
                workerThread.Start();
            }

        }
        private void readAndWrite()
        {
            var myDictionary = new Dictionary<string, List<string>>();



            filePathFitment = readFileTextBox.Text;
            using (StreamReader streamReader = new StreamReader(filePathFitment))
            {
                string[] totalData = new string[File.ReadAllLines(filePathFitment).Length];
                totalData = streamReader.ReadLine().Split(',');
                // MessageBox.Show(totalData.Length.ToString());
                if (totalData.Length != 2)
                {
                    MessageBox.Show("Your file must have 2 columns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    while (!streamReader.EndOfStream)
                    {
                        totalData = streamReader.ReadLine().Split(',');

                    }
                    this.Invoke(new MethodInvoker(delegate ()
                    {

                    }));

                }

            }


            //////////////First and second table split//////////////////////


            filePathCars = writeFileTextBox.Text;





            //here we will create the dictionary for all the cars
            //hopefully it works lol


            // myDictionary.Add(File.ReadAllLines(filePathCars).Select(l => l.Split(',')), );
            //foreach (DataRow line in skuAndFitmentDataTable.Rows)
            using (StreamReader sr = new StreamReader(filePathCars))
            {
                string line;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    myDictionary[line.Split(',')[0]] = new List<string>();
                }




            }
            //initiate the timer
            double time = 0.0;
            double averageTime = 0.0;

            //find the fitment sheet
            string justFitment = writeFileTextBox.Text;

            //write the new file
            //using (StreamWriter resultFile = new StreamWriter(newFileNameTextBox.Text))
            //{





            using (StreamReader sr = new StreamReader(filePathFitment))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    var split = line.Split(',');
                    string[] allCars = split[1].Split(new[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string car in allCars)
                    {
                        //if (!myDictionary.ContainsKey(car))
                        //{
                        //    myDictionary.Add(car, line[0].ToString());

                        //}
                        List<string> list;
                        if (myDictionary.TryGetValue(car, out list))
                        {
                            list.Add(split[0]);
                        }

                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(newFileNameTextBox.Text))
            {
                foreach (var kvp in myDictionary)
                {
                    string combinedSkus = String.Join(",",kvp.Value);
                    var watch = Stopwatch.StartNew();
                  
                    string[] splitCarArray = kvp.Key.Split('|');
                    if (splitCarArray.Length == 5)
                    {
                        //split the car string up
                        string currentYear = splitCarArray[0];
                        string currentMake = splitCarArray[1];
                        string currentModel = splitCarArray[2];
                        string currentTrim = splitCarArray[3];
                        string currentEngine = splitCarArray[4];
                        // here we will add the cars with the skus to the Grid
                        sw.WriteLine($"{currentYear},{currentMake},{currentModel},{currentTrim},{currentEngine},\"{combinedSkus}\"");
                    }



                }


            }

        }


       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void newFileNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
