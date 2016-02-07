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
    public partial class Form1 : Form
    {
        string filePathCars;
        string filePathFitment;
        public Form1()
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
            try
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Sku");
                dataTable.Columns.Add("Fitment");
                filePathFitment = readFileTextBox.Text;
                StreamReader streamReader = new StreamReader(filePathFitment);
                string[] totalData = new string[File.ReadAllLines(filePathFitment).Length];
                totalData = streamReader.ReadLine().Split(',');
                while (!streamReader.EndOfStream)
                {
                    totalData = streamReader.ReadLine().Split(',');
                    dataTable.Rows.Add(totalData[0], totalData[1]);
                }
                fitmentDataGridView.DataSource = dataTable;

                //////////////First and second table split//////////////////////

                DataTable writeDataTable = new DataTable();
                writeDataTable.Columns.Add("Fitment");
                writeDataTable.Columns.Add("Sku");
                filePathCars = writeFileTextBox.Text;
                StreamReader streamReaderWrite = new StreamReader(filePathCars);
                string[] totalDataWrite = new string[File.ReadAllLines(filePathCars).Length];
                totalDataWrite = streamReaderWrite.ReadLine().Split(',');
                while (!streamReaderWrite.EndOfStream)
                {
                    totalDataWrite = streamReaderWrite.ReadLine().Split(',');
                    writeDataTable.Rows.Add(totalDataWrite[0], totalDataWrite[1]);
                }
                writeDataGridView.DataSource = writeDataTable;

            }
            catch (Exception ex)
            {

            }
        }
        private void writeButton_Click(object sender, EventArgs e)
        {
            Thread workerThread = new Thread(ConversionTime,0);
            workerThread.Start();

        }
        private void ConversionTime()
        {
            try
            {
                // create new table
                DataTable resultDataTable = new DataTable();
                // add columns
                resultDataTable.Columns.Add("Fitment");
                resultDataTable.Columns.Add("Sku's");

                //initiate the timer
                double time = 0.0;
                double averageTime = 0.0;

                //find the fitment sheet
                string justFitment = writeFileTextBox.Text;

                //write the new file
                //using (StreamWriter resultFile = new StreamWriter(newFileNameTextBox.Text))
                //{
                int numOfLines = File.ReadAllLines(filePathCars).Length;
                int linesRemaining = numOfLines;
                int linesCompleted = 0;
                this.Invoke(new MethodInvoker(delegate ()
                {
                    timeRemaining.Visible = true;
                    estTimeRemainingLabel.Visible = true;
                    loadingBar.Visible = true;
                    loadingBar.Minimum = 0;
                    loadingBar.Maximum = numOfLines;
                    loadingBar.Value = 0;
                    loadingBar.Step = 1;
                }));

                using (StreamReader carReader = new StreamReader(filePathCars))
                {
                    //    MessageBox.Show("We are in the writer after the reader");
                    string combinedSkus = "";
                    string currentLine;

                    string[] newCSVArray = new string[File.ReadAllLines(filePathCars).Length]; // sets the array length
                    string[] skuAndFitmentArray = new string[File.ReadAllLines(filePathFitment).Length];
                    string currentVehicle;


                    newCSVArray = carReader.ReadLine().Split(','); // sets the first values in the array
                                                                   //loop through the csv and write to the grid we have made

                    // MessageBox.Show("we are inside the car reader before lookingt at each car");
                    while (!carReader.EndOfStream)
                    {
                        var watch = Stopwatch.StartNew();
                        string currentCarString = carReader.ReadLine(); // get the current car of the line were on
                        currentVehicle = currentCarString.Substring(0, currentCarString.IndexOf(",")); //parse out the exact car string
                        //MessageBox.Show(currentVehicle);
                        //newCSVArray = carReader.ReadLine().Split(',');

                        using (StreamReader skuAndFitmentReader = new StreamReader(filePathFitment)) //open a new stream reader and search for the fitment data and return the sku(s)
                        {
                            combinedSkus = "";

                            while (!skuAndFitmentReader.EndOfStream)
                            {
                                currentLine = skuAndFitmentReader.ReadLine();

                                if (currentLine.Contains(currentVehicle))// if the car is found , grab the sku
                                {
                                    //MessageBox.Show("Car was found in\n " + currentLine);
                                    if (combinedSkus == "" || combinedSkus == null)
                                    {

                                        combinedSkus = currentLine.Substring(0, currentLine.IndexOf(","));
                                        //MessageBox.Show(combinedSkus);
                                    }
                                    else
                                    {

                                        combinedSkus = combinedSkus + "|" + currentLine.Substring(0, currentLine.IndexOf(","));
                                        //MessageBox.Show(combinedSkus);
                                    }
                                }
                            }
                            combinedSkus = combinedSkus.Replace('"', Convert.ToChar(" "));
                            combinedSkus = combinedSkus.Replace(" ", string.Empty);
                            resultDataTable.Rows.Add(currentVehicle, combinedSkus); // here we will add the cars with the skus to the Grid
                        }
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            loadingBar.PerformStep();
                            watch.Stop();
                            linesRemaining--;
                            linesCompleted++;
                            var timeForCar = watch.ElapsedMilliseconds;
                            time = time + timeForCar;
                            averageTime = time / linesCompleted;

                            // this will tell the approximate time of completion
                            timeRemaining.Text = Convert.ToString(DateTime.Now.AddMinutes(((averageTime * linesRemaining)/1000)/60));

                        }));

                    }



                }

                loadingBar.Visible = false;
                timeRemaining.Visible = false;
                estTimeRemainingLabel.Visible = false;
                resultGridView.DataSource = resultDataTable;






            }
            catch (Exception exc)
            {

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
