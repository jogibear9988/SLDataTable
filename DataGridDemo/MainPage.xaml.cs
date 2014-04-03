using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BindableDataGrid.Data;
using System.Globalization;
using System.Windows.Markup;

namespace DataGridDemo
{
    /// <summary>
    /// Main page of the application to show how this works
    /// </summary>
    public partial class MainPage : UserControl
    {
        #region "Methods"

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            // Changing the culture to show dates in different formats for example
            //CultureInfo ci = new CultureInfo("fr-fr");
            //System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            //this.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }

        /// <summary>
        /// Handle the click event of the button to create dummy data
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void btnCreateData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.LoadData();
            myBindableDG.SelectionChanged += new SelectionChangedEventHandler(myBindableDG_SelectionChanged);
        }

        /// <summary>
        /// Handle the click event of the button to "serialize" the DataSource
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void btnToString_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (myBindableDG.DataSource != null)
            {
                MessageBox.Show(myBindableDG.DataSource.ToString());
            }
        }

        /// <summary>
        /// Sample method to load random data into the grid
        /// </summary>
        public void LoadData()
        {
            // Create some sample names
            List<string> firstNames = new List<string>() { "Peter", "Frank", "Joe", "Lewis", "Jack", "Andrew", "Susan", "Marie", "Linda", "Anne", "Claire", "Debra" };
            List<string> lastNames = new List<string>() { "Smith", "Brown", "Green", "Parker", "Johnson", "Jackson", "Ford", "Sullivan" };
            List<string> icons = new List<string>() { "UserGreen.png", "UserYellow.png", "UserRed.png" };

            // Create a sample DataTable
            DataTable dt = new DataTable("MyDataTable");

            // Create a column
            DataColumn dc1 = new DataColumn("col1");
            dc1.Caption = "First Name";
            dc1.ReadOnly = true;
            dc1.DataType = typeof(String);
            dc1.AllowResize = true;
            dc1.AllowSort = true;
            dc1.AllowReorder = true;
            dt.Columns.Add(dc1);

            // Create a column
            DataColumn dc2 = new DataColumn("col2");
            dc2.Caption = "Last Name";
            dc2.ReadOnly = true;
            dc2.DataType = typeof(String);
            dc2.AllowResize = true;
            dc2.AllowSort = true;
            dc2.AllowReorder = true;
            dt.Columns.Add(dc2);

            // Create a column
            DataColumn dc3 = new DataColumn("col3");
            dc3.Caption = "Age";
            dc3.ReadOnly = false;
            dc3.DataType = typeof(Int32);
            dt.Columns.Add(dc3);

            // Create a column
            DataColumn dc4 = new DataColumn("col4");
            dc4.Caption = "Married";
            dc4.ReadOnly = true;
            dc4.DataType = typeof(Boolean);
            dt.Columns.Add(dc4);

            // Create a column
            DataColumn dc5 = new DataColumn("col5");
            dc5.Caption = "Membership expiration";
            dc5.ReadOnly = true;
            dc5.DataType = typeof(DateTime);
            dt.Columns.Add(dc5);

            // Create a column
            DataColumn dc6 = new DataColumn("col6", typeof(Image));
            dc6.Caption = "Status";
            dc6.ReadOnly = true;
            dt.Columns.Add(dc6);

            // Add sample rows to the table
            Random r = new Random();
            for (int i = 0; i < 15; i++)
            {
                DataRow dr = new DataRow();
                dr["col1"] = firstNames[r.Next(firstNames.Count)];
                dr["col2"] = lastNames[r.Next(lastNames.Count)];
                dr["col3"] = r.Next(20, 81);
                dr["col4"] = (r.Next(0,2) == 1);
                dr["col5"] = DateTime.Now.AddDays(r.Next(10));

                Uri uri = new Uri("Images/" + icons[r.Next(icons.Count)], UriKind.RelativeOrAbsolute);
                ImageSource imgSource = new BitmapImage(uri);
                Image img = new Image();
                img.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(img_ImageFailed);
                img.Source = new BitmapImage(uri);
                
                dr["col6"] = img;
                dt.Rows.Add(dr);
            }

            // Create a DataSet and add the table to it
            DataSet ds = new DataSet("MyDataSet");
            ds.Tables.Add(dt);

            // Do the binding
            myBindableDG.AutoGenerateColumns = false;
            myBindableDG.DataSource = ds;
            myBindableDG.DataMember = "MyDataTable";
            myBindableDG.DataBind();

            // We could do this as well
            // myBindableDG.DataSource = dt;
            // myBindableDG.DataBind();
        }

        /// <summary>
        /// Change selected rows information
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">SelectionChangedEventArgs</param>
        private void myBindableDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowCollection dcol = myBindableDG.SelectedItems;
            List<string> items = new List<string>();
            foreach (DataRow dr in dcol)
            {
                items.Add(dr["col1"].ToString() + " " + dr["col2"].ToString());
            }

            this.lbSelectedItems.ItemsSource = items;
        }

        /// <summary>
        /// If there are any errors loading any image
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">ExceptionRoutedEventArgs</param>
        private void img_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            // Handle the error here
        }

        #endregion
    }
}