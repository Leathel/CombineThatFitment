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
            try
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Sku");
                dataTable.Columns.Add("Fitment");
                filePathFitment = readFileTextBox.Text;
                using (StreamReader streamReader = new StreamReader(filePathFitment))
                {
                    string[] totalData = new string[File.ReadAllLines(filePathFitment).Length];
                    totalData = streamReader.ReadLine().Split(',');
                    while (!streamReader.EndOfStream)
                    {
                        totalData = streamReader.ReadLine().Split(',');
                        dataTable.Rows.Add(totalData[0], totalData[1]);
                    }
                    fitmentDataGridView.DataSource = dataTable;
                }


                //////////////First and second table split//////////////////////

                DataTable writeDataTable = new DataTable();
                writeDataTable.Columns.Add("Fitment");
                writeDataTable.Columns.Add("Sku");
                filePathCars = writeFileTextBox.Text;
                using (StreamReader streamReaderWrite = new StreamReader(filePathCars))
                {
                    string[] totalDataWrite = new string[File.ReadAllLines(filePathCars).Length];
                    totalDataWrite = streamReaderWrite.ReadLine().Split(',');
                    while (!streamReaderWrite.EndOfStream)
                    {
                        totalDataWrite = streamReaderWrite.ReadLine().Split(',');
                        writeDataTable.Rows.Add(totalDataWrite[0], totalDataWrite[1]);
                    }
                }


                writeDataGridView.DataSource = writeDataTable;

                //here we will create the dictionary for all the cars
                //hopefully it works lol

                var myDictionary = new Dictionary<string, List<string>>();
                // myDictionary.Add(File.ReadAllLines(filePathCars).Select(l => l.Split(',')), );
                foreach (DataRow line in writeDataTable.Rows)
                {
                    try
                    {
                        if (!myDictionary.ContainsKey(line[0].ToString()))
                        {
                            myDictionary.Add(line[0].ToString(), new List<string>());

                        }
                    }
                    catch(Exception dictionaryException)
                    {
                        MessageBox.Show(dictionaryException.ToString());
                    }




                }
            }
            catch (Exception ex)
            {

            }
        }
        private void writeButton_Click(object sender, EventArgs e)
        {
            Thread workerThread = new Thread(ConversionTime, 0);
            workerThread.Start();

        }
        private void ConversionTime()
        {
            try
            {
                // create new table
                DataTable resultDataTable = new DataTable();
                // add columns
                //resultDataTable.Columns.Add("Fitment");
                //The car data will be parsed into these 4 columns
                resultDataTable.Columns.Add("Year");
                resultDataTable.Columns.Add("Make");
                resultDataTable.Columns.Add("Model");
                resultDataTable.Columns.Add("Trim");

                // this column will remain the same
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
                                    if (combinedSkus == "" || combinedSkus == null || combinedSkus == string.Empty)
                                    {

                                        combinedSkus = currentLine.Substring(0, currentLine.IndexOf(","));
                                        //MessageBox.Show(combinedSkus);
                                    }
                                    else
                                    {

                                        combinedSkus = combinedSkus + "," + currentLine.Substring(0, currentLine.IndexOf(','));
                                        //MessageBox.Show(combinedSkus);
                                    }

                                }
                            }
                            if (combinedSkus != "" && combinedSkus != null && combinedSkus != string.Empty)
                            {

                                combinedSkus = '"' + combinedSkus + '"';
                            }

                            //combinedSkus = combinedSkus.Replace('"', Convert.ToChar(" "));
                            //combinedSkus = combinedSkus.Replace(" ", string.Empty);
                            string[] splitCarArray = new string[4];
                            splitCarArray = currentCarString.Split('|');


                            ////////////////
                            string currentYear = splitCarArray[0];
                            string currentMake = splitCarArray[1];
                            string currentModel = splitCarArray[2];
                            string currentTrim = splitCarArray[3];

                            resultDataTable.Rows.Add(currentYear, currentMake, currentModel, currentTrim, combinedSkus); // here we will add the cars with the skus to the Grid
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

                            // this will tell the approximate time of completion in a date format
                            timeRemaining.Text = Convert.ToString(DateTime.Now.AddMinutes(((averageTime * linesRemaining) / 1000) / 60));

                        }));

                    }



                }
                //set everything back to default once completed
                this.Invoke(new MethodInvoker(delegate ()
                {
                    loadingBar.Visible = false;
                    timeRemaining.Visible = false;
                    estTimeRemainingLabel.Visible = false;
                    resultGridView.DataSource = resultDataTable;
                    exportToCSV(resultDataTable);
                }));






            }
            catch (Exception exc)
            {
                // still have to put in some exeptions
            }
        }

        private void dictionaryConversionTime()
        {
            try
            {
                // create new table
                DataTable resultDataTable = new DataTable();
                // add columns
                //resultDataTable.Columns.Add("Fitment");
                //The car data will be parsed into these 4 columns
                resultDataTable.Columns.Add("Year");
                resultDataTable.Columns.Add("Make");
                resultDataTable.Columns.Add("Model");
                resultDataTable.Columns.Add("Trim");

                // this column will remain the same
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

                    
                    string[] skuAndFitmentArray = new string[File.ReadAllLines(filePathFitment).Length];
                    string currentVehicle;

                    //////////////////////////////////Implement this./////////////////
                    //using (var reader = new StreamReader(filePath))
                    //{
                    //    string line; while ((line = reader.ReadLine()) != null)
                    //    { // splitting and stuff}
                            ///////////////////////////



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
                                    if (combinedSkus == "" || combinedSkus == null || combinedSkus == string.Empty)
                                    {

                                        combinedSkus = currentLine.Substring(0, currentLine.IndexOf(","));
                                        //MessageBox.Show(combinedSkus);
                                    }
                                    else
                                    {

                                        combinedSkus = combinedSkus + "," + currentLine.Substring(0, currentLine.IndexOf(','));
                                        //MessageBox.Show(combinedSkus);
                                    }

                                }
                            }
                            if (combinedSkus != "" && combinedSkus != null && combinedSkus != string.Empty)
                            {

                                combinedSkus = '"' + combinedSkus + '"';
                            }

                            //combinedSkus = combinedSkus.Replace('"', Convert.ToChar(" "));
                            //combinedSkus = combinedSkus.Replace(" ", string.Empty);
                            string[] splitCarArray = new string[4];
                            splitCarArray = currentCarString.Split('|');


                            ////////////////
                            string currentYear = splitCarArray[0];
                            string currentMake = splitCarArray[1];
                            string currentModel = splitCarArray[2];
                            string currentTrim = splitCarArray[3];

                            resultDataTable.Rows.Add(currentYear, currentMake, currentModel, currentTrim, combinedSkus); // here we will add the cars with the skus to the Grid
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

                            // this will tell the approximate time of completion in a date format
                            timeRemaining.Text = Convert.ToString(DateTime.Now.AddMinutes(((averageTime * linesRemaining) / 1000) / 60));

                        }));

                    }



                }
                //set everything back to default once completed
                this.Invoke(new MethodInvoker(delegate ()
                {
                    loadingBar.Visible = false;
                    timeRemaining.Visible = false;
                    estTimeRemainingLabel.Visible = false;
                    resultGridView.DataSource = resultDataTable;
                    exportToCSV(resultDataTable);
                }));






            }
            catch (Exception exc)
            {
                // still have to put in some exeptions
            }
        }
        private void exportToCSV(DataTable resultDataTable)
        {
            using (StreamWriter csvWriter = new StreamWriter(newFileNameTextBox.Text, true))
            {
                int iColCount = resultDataTable.Columns.Count;
                for (int i = 0; i < iColCount; i++)
                {
                    csvWriter.Write(resultDataTable.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        csvWriter.Write(",");
                    }
                }
                csvWriter.Write(csvWriter.NewLine);
                // Now write all the rows.
                foreach (DataRow dr in resultDataTable.Rows)
                {
                    for (int i = 0; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            csvWriter.Write(dr[i].ToString());
                        }
                        if (i < iColCount - 1)
                        {
                            csvWriter.Write(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
                        }
                    }
                    csvWriter.Write(csvWriter.NewLine);
                }
                csvWriter.Close();
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
