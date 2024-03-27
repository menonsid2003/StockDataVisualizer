using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockAnalysisProject
{
    /* Name: Sidharth Menon (U75158684)
     * This program displays a chosen csv file's data following Yahoo Finance formatting.
     * It shows a spreadsheet display and two charts, one with the performance in Candlesticks and a second Bar chart for the Volume.
     * This data can be filtered to only show the performance between two chosen dates and update the charts seamlessly.
     */
    public partial class Form_Candlesticks : Form
    {
        // create list and binding list globally with access in all functions
        private List<Candlestick> candlesticks = null; // general List named candlesticks
        private BindingList<Candlestick> boundCandlesticks = null; // Binding List named boundCandlesticks
        private List<string> filenames = null; // list of filenames to loop through and open.

        // Upon initialization of the form
        public Form_Candlesticks()
        {
            InitializeComponent(); // Initializes the Form's user interface.
            candlesticks = new List<Candlestick>(1024); // Initialize the candlesticks list with 1024 entries as a safety cage.
            boundCandlesticks = new BindingList<Candlestick>(); // Initialize boundCandlesticks
        }

        // new constructor that takes in a filename as a parameter for creation of new forms.
        public Form_Candlesticks(string filename)
        {
            InitializeComponent(); // Initializes the Form's user interface.
            candlesticks = new List<Candlestick>(1024); // Initialize the candlesticks list with 1024 entries as a safety cage.
            boundCandlesticks = new BindingList<Candlestick>(); // Initialize boundCandlesticks

            // Read data from the provided file
            readCandlesticksFromFile(filename);
        }

        // Called when Load File is selected
        private void button_LoadFile_Click(object sender, EventArgs e)
        {
            openFileDialog_FileOpen.ShowDialog(); // A File Explorer windows is opened for users to select their csv file.
        }

        // Called when Update is selected
        private void button_UpdateDate_Click(object sender, EventArgs e)
        {
            update(); // direct call to the update function
        }

        /// <summary>
        /// This function calls the loaded readCandlesticksFromFile() with the parameter taken from the File Dialogue.
        /// Not used anymore as there are multiple files being worked on at once, and so a global filename option does not make sense.
        /// </summary>
        void readCandlesticksFromFile()
        {
            // Calls the loaded readCandlesticksFromFile function and uses the File selected from the File Dialogue as the paramter.
            foreach (String file in openFileDialog_FileOpen.FileNames)
            {
                readCandlesticksFromFile(file);
            }
        }

        /// <summary>
        /// This function is intended to take a filename input and read information and create a candlestick List from it.
        /// The referenceString is to compare the first line of the csv to ensure proper order of information.
        /// The 'using' block opens a StreamReader as long as within that block.
        /// The candlesticks List is cleared first to ensure no data hazards, and the first line is read in and compared to referenceString.
        /// If they match properly, then the following lines are read in, new Smart Candlesticks are made per line and added to the List.
        /// The file is then closed and the function returns the candlesticks List.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns candlesticks List></returns>
        private List<Candlestick> readCandlesticksFromFile(string filename)
        {
            // string to compare to the first line of the csv to ensure order accuracy.
            const string referenceString = "Date,Open,High,Low,Close,Adj Close,Volume";

            // sr is being used as Streamreader for the entirety of this block
            using (StreamReader sr = new StreamReader(filename))
            {
                candlesticks.Clear(); // the candlesticks list is cleared first to avoid data hazards.

                // read in the first line as the header
                string line = sr.ReadLine();

                // if the header matches the reference string, continue scanning
                if (line == referenceString)
                {
                    // keep reading until end of file
                    while ((line = sr.ReadLine()) != null)
                    {
                        // create new Smart Candlestick object for each line
                        SmartCandlestick scs = new SmartCandlestick(line);
                        // this new smart candlestick is then added to the List
                        candlesticks.Add(scs);
                    }
                }
                
                else { Text = "Bad File" + filename; } // if the csv first line does not match the referenceString, then the file is "Bad" and the user is notified.
                sr.Close(); // The file is closed before exiting the StreamReader using block
            }

            // return the now fully populated List.
            return candlesticks;
        }

        /// <summary>
        /// This void function will call the List version of the function
        /// and the returned list is saved as a Candlestick List called filtered.
        /// This is set as a new BindingList named boundCandlesticks.
        /// </summary>
        private void filterCandlesticks()
        {
            // A new List named filtered is created where the contents are the return value of the loaded filterCandlesticks function.
            // The parameters that are sent in are the current full List, the start date and end date for the updated timeframe.
            List<Candlestick> filtered = filterCandlesticks(candlesticks, date_Start.Value, date_End.Value);
            // boundCandlesticks is updated to be the contents of the new filtered List converted to a Binding List
            boundCandlesticks = new BindingList<Candlestick>(filtered);

            // displayCandlesticks is called to update the visual charts from this new BindingList
            displayCandlesticks();
        }

        /// <summary>
        /// This function takes a List, a start and end date and then filters the list to only dates within the range and returns it.
        /// LinQ is used to neatly select values within the range and saved into a new 'selected' List which is then returned.
        /// </summary>
        /// <param name="unfilteredList"></param>
        /// <param name="date_Start"></param>
        /// <param name="date_End"></param>
        /// <returns Selected List></returns>
        private List<Candlestick> filterCandlesticks(List<Candlestick> unfilteredList, DateTime date_Start, DateTime date_End)
        {
            /* A new List named selected is created and the data within this List is curated using LinQ functions.
             * This is primarily done with the .Where() function.
             * c = one single Candlestick within the candlesticks list and .date can pull only the date value.
             * So this line chooses every single Candlestick whose date is greater than the start date & less than the end date,
             * and converts this information to a List and is assigned/copied into the newly created 'selected' List.
             */
            List<Candlestick> selected = candlesticks.Where(c => c.date >= date_Start && c.date <= date_End).ToList();
            // Returns this newly created and curated List.
            return selected;
        }

        /// <summary>
        /// This function is designed to update the charts on the Form with an updated Binding List.
        /// BindingList returning function to update the datagridview and visual charts.
        /// Does so by taking the input boundCandlesticks Binding List and assigning it as the 
        /// data source for the data grid view. Then the same is done for the chart, along with
        /// a .DataBind() call on the chart which binds the data to the chart and returns the bound List.
        /// </summary>
        /// <param name="boundCandlesticks"></param>
        /// <returns boundCandlesticks></returns>
        private BindingList<Candlestick> displayCandlesticks(BindingList<Candlestick> boundCandlesticks)
        {
            chart_Candlesticks.Series[0].Points.Clear();
            // boundCandlesticks is used as the Data Source for the data grid View.
            // Commented out as Data Grid View is no longer needed.
            //dataGridView_Candlesticks.DataSource = boundCandlesticks;

            // boundCandlesticks is also used as the Data Source for the chart.
            chart_Candlesticks.DataSource = boundCandlesticks;

            // Bind uses the Data Source and  updates the chart visual.
            // Commented out as this causes chronological x-axis dating to break in the chart.
            //chart_Candlesticks.DataBind();

            // Loop through each candlestick in the bound candlesticks
            foreach (var scs in boundCandlesticks)
            {
                DataPoint dataPoint = new DataPoint(); // Create a new data point
               
                dataPoint.SetValueXY(scs.date, scs.low, scs.high, scs.open, scs.close); // Set the X and Y values for the data point

                dataPoint.Tag = scs; // Set the tag of the data point to the candlestick object
                
                chart_Candlesticks.Series[0].Points.Add(dataPoint); // Add the data point to the chart series
            }


            // another normalize call to lock the annotations to place.
            normalize();

            // call to visually update the chart with the annotations
            chart_Candlesticks.Update();

            // returns the boundCandlesticks.
            return boundCandlesticks;
        }

        /// <summary>
        /// void version of the displayCandlesticks that calls the loaded version and returns nothing to signify end of function.
        /// </summary>
        private void displayCandlesticks()
        {
            // A call to the loaded function with boundCandlesticks as the parameter.
            displayCandlesticks(boundCandlesticks);
            // empty return to end function.
            return;
        }

        /// <summary>
        /// void version of normalize function to call the loaded function with the parameter of boundCandlesticks
        /// </summary>
        private void normalize()
        {
            BindingList<Candlestick> normalList = normalize(boundCandlesticks); // calls normalize with boundCandlesticks as the parameter
            // empty return to end function.
            return;
        }

        /// <summary>
        /// This function takes a given BindingList and normalizes the chart Y-axis bounds by 5% over and under the bounds of the given info.
        /// This is intended to maximize the space used on the charts while also giving space for readability.
        /// </summary>
        /// <param name="candleSticks"></param>
        /// <returns candlesticks List></returns>
        private BindingList<Candlestick> normalize(BindingList<Candlestick> candlesticks)
        {
            // maxHigh is initialized to the smallest decimal value to start off.
            decimal maxHigh = decimal.MinValue;
            // minLow is initialized to the largest decimal value to start off.
            decimal minLow = decimal.MaxValue; 

            // for each candlestick in List
            foreach (var cs in candlesticks)
            {
                // if the High value of the current candlestick is greater than Max High, then set the current maxHigh to the value.
                if (cs.high > maxHigh)
                {
                    maxHigh = cs.high; // set maxHigh to current candlestick High value.
                }
                if (cs.low < minLow) // if the Low value of the current candlestick is less than Min Low, then set the current minLow to the value.
                {
                    minLow = cs.low; // set minLow to current candlestick Low value.
                }
            }

            // previous project we used 2%, but that led to a few arrows getting off visually here, so I bumped it up to 5%.

            // Floor rounds down the calculated bounds on the Y-axis for better readablity. 
            double minVal = Math.Floor((double)minLow * 0.95); // multiplied by 0.95 to get 5% lower than the current bound.
            // Ceiling rounds up the calculated bounds on the Y-axis for better readablity.
            double maxVal = Math.Ceiling((double)maxHigh * 1.05); // multiplied by 1.05 to get 5% higher than the current bound.

            

            // set the chart's minimum Y value to the calculated and floored minVal.
            chart_Candlesticks.ChartAreas[0].AxisY.Minimum = minVal;
            // set the chart's maximum Y value to the calculated and ceiled maxVal.
            chart_Candlesticks.ChartAreas[0].AxisY.Maximum = maxVal;

            // returns the candlesticks List.
            return candlesticks;
        }

        /// <summary>
        /// this function is called whenver the update button is pressed
        /// this will first filter the candlesticks, then normalize to 2% bounds,
        /// then visually update the charts with displayCandlesticks.
        /// </summary>
        private void update()
        {
            // call to void filterCandlesticks function to restrict the dates
            filterCandlesticks();
            // call to void normalize function to restrict chart bounds to +-5% to give breathing room for the arrows visibility.
            normalize();
            // call to populatePatternDropdown to update the combo box options whenever any change is made to the chart information.
            populatePatternDropdown();
            // call to void displayCandlesticks function to visually update datagridview and charts with bounded info
            displayCandlesticks();

            return;
        }

        /// <summary>
        /// Opens new forms for multiple files selected and makes the first file selected open in the original parent form.
        /// Also marks each form as a parent or child, to know which form will close the rest.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog_FileOpen_FileOk(object sender, CancelEventArgs e)
        {
            // Read and process the first selected file in the current form
            string parentFileName = openFileDialog_FileOpen.FileNames[0];
            readCandlesticksFromFile(parentFileName); // Calls the readcandlesticksfromfile function for the parent instead of calling a constructor.

            // If more than one file is selected, open new windows for subsequent files
            if (openFileDialog_FileOpen.FileNames.Length > 1)
            {
                // Iterate through each selected file starting from index 1 (excluding the first one)
                for (int i = 1; i < openFileDialog_FileOpen.FileNames.Length; i++)
                {
                    // Instantiate a new Form_Candlesticks for each subsequent file
                    string childFileName = openFileDialog_FileOpen.FileNames[i];
                    Form_Candlesticks newForm = new Form_Candlesticks(childFileName); //  Create new form using the constructor that takes in a file name.
                    newForm.Text = Path.GetFileName(childFileName) + " (Child)"; // Set form title with file name and mark as child
                    newForm.Show(); // Show the new form
                    newForm.update(); // Update new forms with visuals
                }
            }

            // Set parent form title with file name and mark as parent
            this.Text = Path.GetFileName(parentFileName) + " (Parent)";

            // Update the current form
            update();
        }

        /// <summary>
        /// This function fills the combo box with every possible pattern, and how many of them currently appear.
        /// Functions by setting every candlestick to have 0 patterns marked as default in a new count dictionary,
        /// then loops through again, this time incrementing every pattern actually detected.
        /// This is then sorted in descending count order and then shown in the combo box as eg: "Bullish: 10"
        /// </summary>
        private void populatePatternDropdown()
        {
            // Clear existing items
            comboBox_Patterns.Items.Clear();

            List<SmartCandlestick> smartCandlesticks = boundCandlesticks.OfType<SmartCandlestick>().ToList();
            Dictionary<string, int> numOfPatterns = new Dictionary<string, int>();

            // Default set every pattern count to 0 by looping through every candlestick
            foreach (var smartCandlestick in smartCandlesticks)
            {
                // Iterate through each pattern key in the candlestick's patterns
                foreach (var patternKey in smartCandlestick.Patterns.Keys)
                {
                    // If the pattern is not detected in any smartCandlestick, set its count to 0
                    if (!numOfPatterns.ContainsKey(patternKey))
                    {
                        numOfPatterns[patternKey] = 0; // set the value to 0 as initialization
                    }
                }
            }

            // Increment the count for each detected pattern
            foreach (var smartCandlestick in smartCandlesticks)
            {
                // Iterate through each pattern in the candlestick's patterns
                foreach (var pattern in smartCandlestick.Patterns)
                {
                    // If the pattern is detected in the candlestick, increment its count
                    if (pattern.Value)
                    {
                        // If the pattern is already in the count dictionary, increment its count
                        if (numOfPatterns.ContainsKey(pattern.Key))
                        {
                            numOfPatterns[pattern.Key]++; // increment the value by 1
                        }
                        // If the pattern is not in the count dictionary, add it with a count of 1
                        else
                        {
                            numOfPatterns[pattern.Key] = 1; // set the value to 1
                        }
                    }
                }
            }

        // sorts the patterns by number of appearances for ease of use.
        var sortedPatterns = numOfPatterns.OrderByDescending(pair => pair.Value);
            // loop through every pattern in this list of patterns
            foreach (var pattern in sortedPatterns)
            {
                comboBox_Patterns.Items.Add($"{pattern.Key}: {pattern.Value}"); //Adds each pattern and the number of them to the combo box.
            }

            // set default selection to the first option in the combo box so that the user isnt forced to select the highest option every time.
            if (comboBox_Patterns.Items.Count > 0)
            {
                comboBox_Patterns.SelectedIndex = 0; // index number 0 is the first one.
            }
        }


        private void date_Start_ValueChanged(object sender, EventArgs e)
        {

        }

        private void date_End_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView_Candlesticks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart_Candlesticks_Click(object sender, EventArgs e)
        {

        }

        private void Form_Candlesticks_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event handler function for the combo box. This function creates adds arrow annotations to the chart
        /// for every Data Point marked for the selected patterns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Patterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            // First clears the annotations already present to give a fresh slate to start on
            chart_Candlesticks.Annotations.Clear();

            // As the combo box options are written as eg: "Bullish: 0" we split the string from the colon so
            // that only the pattern name is used for comparison to the dictionary.
            string pattern = comboBox_Patterns.SelectedItem.ToString().Split(':')[0];

            // Loop through every datapoint in the chart.
            foreach (DataPoint dataPoint in chart_Candlesticks.Series[0].Points)
            {
                // Tag each DataPoint to a SmartCandlestick object
                SmartCandlestick smartCandlestick = dataPoint.Tag as SmartCandlestick;

                // smartCandlestick exists and if its dictionary contains said pattern and if its marked as true.
                if(smartCandlestick != null && smartCandlestick.Patterns.ContainsKey(pattern) && smartCandlestick.Patterns[pattern])
                {
                    //Below lines create an arrow and various parameters for it. Most are self evident
                    ArrowAnnotation arrow = new ArrowAnnotation();
                    arrow.AnchorDataPoint = dataPoint; // selects where the arrow is anchored to, here it is the data point
                    arrow.AnchorOffsetX = 0;
                    arrow.AnchorOffsetY = 1;
                    arrow.ArrowSize = 2;
                    arrow.LineWidth = 1;
                    arrow.BackColor = Color.Blue;
                    arrow.Width = 1.5;
                    arrow.Height = 2;
                    arrow.ClipToChartArea = chart_Candlesticks.ChartAreas[0].Name; // Selects and clips the arrow to a specific chart area specified.
                    chart_Candlesticks.Annotations.Add(arrow); // Finally adds the created arrow to the chart itself.
                }
            }
        }
    }
}
