namespace StockAnalysisProject
{
    partial class Form_Candlesticks
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button_LoadFile = new System.Windows.Forms.Button();
            this.button_UpdateDate = new System.Windows.Forms.Button();
            this.date_Start = new System.Windows.Forms.DateTimePicker();
            this.date_End = new System.Windows.Forms.DateTimePicker();
            this.chart_Candlesticks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog_FileOpen = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView_Candlesticks = new System.Windows.Forms.DataGridView();
            this.comboBox_Patterns = new System.Windows.Forms.ComboBox();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.highDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lowDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.volumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.candlestickBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Candlesticks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Candlesticks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button_LoadFile
            // 
            this.button_LoadFile.Location = new System.Drawing.Point(1522, 502);
            this.button_LoadFile.Margin = new System.Windows.Forms.Padding(4);
            this.button_LoadFile.Name = "button_LoadFile";
            this.button_LoadFile.Size = new System.Drawing.Size(200, 108);
            this.button_LoadFile.TabIndex = 0;
            this.button_LoadFile.Text = "Load File";
            this.button_LoadFile.UseVisualStyleBackColor = true;
            this.button_LoadFile.Click += new System.EventHandler(this.button_LoadFile_Click);
            // 
            // button_UpdateDate
            // 
            this.button_UpdateDate.Location = new System.Drawing.Point(1730, 502);
            this.button_UpdateDate.Margin = new System.Windows.Forms.Padding(4);
            this.button_UpdateDate.Name = "button_UpdateDate";
            this.button_UpdateDate.Size = new System.Drawing.Size(156, 108);
            this.button_UpdateDate.TabIndex = 1;
            this.button_UpdateDate.Text = "Update";
            this.button_UpdateDate.UseVisualStyleBackColor = true;
            this.button_UpdateDate.Click += new System.EventHandler(this.button_UpdateDate_Click);
            // 
            // date_Start
            // 
            this.date_Start.Location = new System.Drawing.Point(1522, 367);
            this.date_Start.Margin = new System.Windows.Forms.Padding(4);
            this.date_Start.Name = "date_Start";
            this.date_Start.Size = new System.Drawing.Size(364, 31);
            this.date_Start.TabIndex = 2;
            this.date_Start.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            this.date_Start.ValueChanged += new System.EventHandler(this.date_Start_ValueChanged);
            // 
            // date_End
            // 
            this.date_End.Location = new System.Drawing.Point(1522, 456);
            this.date_End.Margin = new System.Windows.Forms.Padding(4);
            this.date_End.Name = "date_End";
            this.date_End.Size = new System.Drawing.Size(364, 31);
            this.date_End.TabIndex = 3;
            this.date_End.ValueChanged += new System.EventHandler(this.date_End_ValueChanged);
            // 
            // chart_Candlesticks
            // 
            this.chart_Candlesticks.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "chartArea_OHLC";
            chartArea2.Name = "chartArea_Volume";
            this.chart_Candlesticks.ChartAreas.Add(chartArea1);
            this.chart_Candlesticks.ChartAreas.Add(chartArea2);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart_Candlesticks.Legends.Add(legend1);
            this.chart_Candlesticks.Location = new System.Drawing.Point(13, 31);
            this.chart_Candlesticks.Margin = new System.Windows.Forms.Padding(4);
            this.chart_Candlesticks.Name = "chart_Candlesticks";
            series1.ChartArea = "chartArea_OHLC";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=0\\, 192\\, 0";
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "series_OHLC";
            series1.XValueMember = "date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "high, low, open, close";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "chartArea_Volume";
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "series_Volume";
            series2.XValueMember = "date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueMembers = "volume";
            this.chart_Candlesticks.Series.Add(series1);
            this.chart_Candlesticks.Series.Add(series2);
            this.chart_Candlesticks.Size = new System.Drawing.Size(1523, 669);
            this.chart_Candlesticks.TabIndex = 5;
            this.chart_Candlesticks.Text = "chart1";
            this.chart_Candlesticks.Click += new System.EventHandler(this.chart_Candlesticks_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1644, 335);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Start Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1640, 427);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "End Date";
            // 
            // openFileDialog_FileOpen
            // 
            this.openFileDialog_FileOpen.FileName = "openFileDialog1";
            this.openFileDialog_FileOpen.Filter = "All|*.csv|Monthly|*-Month.csv|Weekly|*-Week.csv|Daily|*-Day.csv";
            this.openFileDialog_FileOpen.Multiselect = true;
            this.openFileDialog_FileOpen.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOpen_FileOk);
            // 
            // dataGridView_Candlesticks
            // 
            this.dataGridView_Candlesticks.AutoGenerateColumns = false;
            this.dataGridView_Candlesticks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Candlesticks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateDataGridViewTextBoxColumn,
            this.openDataGridViewTextBoxColumn,
            this.closeDataGridViewTextBoxColumn,
            this.highDataGridViewTextBoxColumn,
            this.lowDataGridViewTextBoxColumn,
            this.volumeDataGridViewTextBoxColumn});
            this.dataGridView_Candlesticks.DataSource = this.candlestickBindingSource;
            this.dataGridView_Candlesticks.Location = new System.Drawing.Point(969, 347);
            this.dataGridView_Candlesticks.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_Candlesticks.Name = "dataGridView_Candlesticks";
            this.dataGridView_Candlesticks.RowHeadersWidth = 82;
            this.dataGridView_Candlesticks.RowTemplate.Height = 33;
            this.dataGridView_Candlesticks.Size = new System.Drawing.Size(472, 152);
            this.dataGridView_Candlesticks.TabIndex = 4;
            this.dataGridView_Candlesticks.Visible = false;
            this.dataGridView_Candlesticks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Candlesticks_CellContentClick);
            // 
            // comboBox_Patterns
            // 
            this.comboBox_Patterns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Patterns.FormattingEnabled = true;
            this.comboBox_Patterns.Location = new System.Drawing.Point(1543, 87);
            this.comboBox_Patterns.Name = "comboBox_Patterns";
            this.comboBox_Patterns.Size = new System.Drawing.Size(314, 33);
            this.comboBox_Patterns.TabIndex = 8;
            this.comboBox_Patterns.SelectedIndexChanged += new System.EventHandler(this.comboBox_Patterns_SelectedIndexChanged);
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "date";
            this.dateDataGridViewTextBoxColumn.MinimumWidth = 10;
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.Width = 200;
            // 
            // openDataGridViewTextBoxColumn
            // 
            this.openDataGridViewTextBoxColumn.DataPropertyName = "open";
            this.openDataGridViewTextBoxColumn.HeaderText = "open";
            this.openDataGridViewTextBoxColumn.MinimumWidth = 10;
            this.openDataGridViewTextBoxColumn.Name = "openDataGridViewTextBoxColumn";
            this.openDataGridViewTextBoxColumn.Width = 200;
            // 
            // closeDataGridViewTextBoxColumn
            // 
            this.closeDataGridViewTextBoxColumn.DataPropertyName = "close";
            this.closeDataGridViewTextBoxColumn.HeaderText = "close";
            this.closeDataGridViewTextBoxColumn.MinimumWidth = 10;
            this.closeDataGridViewTextBoxColumn.Name = "closeDataGridViewTextBoxColumn";
            this.closeDataGridViewTextBoxColumn.Width = 200;
            // 
            // highDataGridViewTextBoxColumn
            // 
            this.highDataGridViewTextBoxColumn.DataPropertyName = "high";
            this.highDataGridViewTextBoxColumn.HeaderText = "high";
            this.highDataGridViewTextBoxColumn.MinimumWidth = 10;
            this.highDataGridViewTextBoxColumn.Name = "highDataGridViewTextBoxColumn";
            this.highDataGridViewTextBoxColumn.Width = 200;
            // 
            // lowDataGridViewTextBoxColumn
            // 
            this.lowDataGridViewTextBoxColumn.DataPropertyName = "low";
            this.lowDataGridViewTextBoxColumn.HeaderText = "low";
            this.lowDataGridViewTextBoxColumn.MinimumWidth = 10;
            this.lowDataGridViewTextBoxColumn.Name = "lowDataGridViewTextBoxColumn";
            this.lowDataGridViewTextBoxColumn.Width = 200;
            // 
            // volumeDataGridViewTextBoxColumn
            // 
            this.volumeDataGridViewTextBoxColumn.DataPropertyName = "volume";
            this.volumeDataGridViewTextBoxColumn.HeaderText = "volume";
            this.volumeDataGridViewTextBoxColumn.MinimumWidth = 10;
            this.volumeDataGridViewTextBoxColumn.Name = "volumeDataGridViewTextBoxColumn";
            this.volumeDataGridViewTextBoxColumn.Width = 200;
            // 
            // candlestickBindingSource
            // 
            this.candlestickBindingSource.DataSource = typeof(StockAnalysisProject.Candlestick);
            // 
            // Form_Candlesticks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1915, 745);
            this.Controls.Add(this.comboBox_Patterns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_Candlesticks);
            this.Controls.Add(this.date_End);
            this.Controls.Add(this.date_Start);
            this.Controls.Add(this.button_UpdateDate);
            this.Controls.Add(this.button_LoadFile);
            this.Controls.Add(this.chart_Candlesticks);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_Candlesticks";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_Candlesticks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Candlesticks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Candlesticks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_LoadFile;
        private System.Windows.Forms.Button button_UpdateDate;
        private System.Windows.Forms.DateTimePicker date_Start;
        private System.Windows.Forms.DateTimePicker date_End;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Candlesticks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog_FileOpen;
        private System.Windows.Forms.BindingSource candlestickBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn volumeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lowDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn highDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn closeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn openDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView dataGridView_Candlesticks;
        private System.Windows.Forms.ComboBox comboBox_Patterns;
    }
}

