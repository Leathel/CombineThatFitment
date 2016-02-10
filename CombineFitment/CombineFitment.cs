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


            DataTable skuAndFitmentDataTable = new DataTable();
            skuAndFitmentDataTable.Columns.Add("Sku");
            skuAndFitmentDataTable.Columns.Add("Fitment");
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
                        skuAndFitmentDataTable.Rows.Add(totalData[0], totalData[1]);
                    }
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        fitmentDataGridView.DataSource = skuAndFitmentDataTable;
                    }));
                    
                }

            }


            //////////////First and second table split//////////////////////

            DataTable carsDataTable = new DataTable();
            carsDataTable.Columns.Add("Fitment");
            filePathCars = writeFileTextBox.Text;
            using (StreamReader streamReaderWrite = new StreamReader(filePathCars))
            {
                string[] totalDataWrite = new string[File.ReadAllLines(filePathCars).Length];
                totalDataWrite = streamReaderWrite.ReadLine().Split(',');
                if (totalDataWrite.Length != 1)
                {
                    MessageBox.Show("Your file must have 2 columns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    while (!streamReaderWrite.EndOfStream)
                    {
                        totalDataWrite = streamReaderWrite.ReadLine().Split(',');
                        carsDataTable.Rows.Add(totalDataWrite[0]);
                    }
                }

            }

            this.Invoke(new MethodInvoker(delegate ()
            {
                writeDataGridView.DataSource = carsDataTable;
            }));


            //here we will create the dictionary for all the cars
            //hopefully it works lol


            // myDictionary.Add(File.ReadAllLines(filePathCars).Select(l => l.Split(',')), );
            foreach (DataRow line in skuAndFitmentDataTable.Rows)
            {
                try
                {
                    string[] allCars = line[1].ToString().Split(new[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string car in allCars)
                    {
                        //if (!myDictionary.ContainsKey(car))
                        //{
                        //    myDictionary.Add(car, line[0].ToString());

                        //}
                        List<string> list;
                        if (!myDictionary.TryGetValue(car, out list))
                        {
                            myDictionary[car] = new List<string> { line[0].ToString() };
                        }
                        else
                        {
                            list.Add(line[0].ToString());
                        }

                    }


                }
                catch (Exception dictionaryException)
                {
                    MessageBox.Show(dictionaryException.ToString());
                }




            }










            // create new table
            DataTable resultDataTable = new DataTable();
            // add columns
            //resultDataTable.Columns.Add("Fitment");
            //The car data will be parsed into these 4 columns
            resultDataTable.Columns.Add("Year");
            resultDataTable.Columns.Add("Make");
            resultDataTable.Columns.Add("Model");
            resultDataTable.Columns.Add("Trim");
            resultDataTable.Columns.Add("Engine");

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

            int numOfLines = myDictionary.Count;
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

                
                string currentLine;
                string currentVehicle;
                

                while ((currentVehicle = carReader.ReadLine()) != null)
                {
                    string combinedSkus = "";
                    var watch = Stopwatch.StartNew();
                    List<string> skus;
                    if (myDictionary.TryGetValue(currentVehicle, out skus))
                    {
                         combinedSkus = string.Join(",", skus);
                        
                    }
                    string[] splitCarArray = currentVehicle.Split('|');
                    if(splitCarArray.Length == 5)
                    {
                        string currentYear = splitCarArray[0];
                        string currentMake = splitCarArray[1];
                        string currentModel = splitCarArray[2];
                        string currentTrim = splitCarArray[3];
                        string currentEngine = splitCarArray[4];
                        combinedSkus = '"' + combinedSkus + '"';
                        resultDataTable.Rows.Add(currentYear, currentMake, currentModel, currentTrim, currentEngine, combinedSkus); // here we will add the cars with the skus to the Grid

                    }

                    ////////////////



                    //newCSVArray = carReader.ReadLine().Split(',');

                    //we must search the dictionary for the currentVehicle, if it is found, return the sku (Key) and continue searching






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
                this.Invoke(new MethodInvoker(delegate ()
                {
                    loadingBar.Visible = false;
                    timeRemaining.Visible = false;
                    estTimeRemainingLabel.Visible = false;
                    resultGridView.DataSource = resultDataTable;
                    exportToCSV(resultDataTable);
                }));


            }






            /////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        private void writeButton_Click(object sender, EventArgs e)
        {
            if (newFileNameTextBox.Text == "" || newFileNameTextBox.Text == null || newFileNameTextBox.Text == string.Empty)
            {
                MessageBox.Show("You must enter in an output file!\n Also make sure its empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Thread workerThread = new Thread(ConversionTime, 0);
                workerThread.Start();
            }

            // Thread workerThread1 = new Thread(dictionaryConversionTime(myDictionary), 0);
            //workerThread1.Start();


        }
        private void ConversionTime()
        {
            try
            {
                // create new table
                DataTable resultDataTable = new DataTable();
                // add columns
                //resultDataTable.Columns.Add("Fitment");
                //The car data will be parsed into these 5 columns
                resultDataTable.Columns.Add("Year");
                resultDataTable.Columns.Add("Make");
                resultDataTable.Columns.Add("Model");
                resultDataTable.Columns.Add("Trim");
                resultDataTable.Columns.Add("Engine");

                // this column will remain the same
                resultDataTable.Columns.Add("Skus");

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
                        currentVehicle = carReader.ReadLine(); // get the current car of the line were on
                                                               // currentVehicle = currentCarString.Substring(0, currentCarString.IndexOf(",")); //parse out the exact car string
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
                                combinedSkus = combinedSkus.Replace('"', Convert.ToChar(" "));
                                combinedSkus = combinedSkus.Replace(" ", string.Empty);
                                combinedSkus = '"' + combinedSkus + '"';

                            }


                            string[] splitCarArray = new string[4];
                            splitCarArray = currentVehicle.Split('|');


                            ////////////////
                            string currentYear = splitCarArray[0];
                            string currentMake = splitCarArray[1];
                            string currentModel = splitCarArray[2];
                            string currentTrim = splitCarArray[3];
                            string currentEngine = splitCarArray[4];

                            resultDataTable.Rows.Add(currentYear, currentMake, currentModel, currentTrim, currentEngine, combinedSkus); // here we will add the cars with the skus to the Grid
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

        private void dictionaryConversionTime(Dictionary<string, List<string>> myDictionary)
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
                numOfLines = myDictionary.Count();
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

                        //we must search the dictionary for the currentVehicle, if it is found, return the sku (Key) and continue searching

                        ////if(myDictionary.ContainsValue(currentVehicle))
                        ////{

                        ////}





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
