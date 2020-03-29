using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telbot.db;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Telbot.model;
using Telbot.system;
using Telbot.helper;

namespace Telbot.Dialogs
{
    /// <summary>
    /// Interaction logic for ProgressDialog.xaml
    /// </summary>
    public partial class ProgressDialog : Window
    {

        string file_path;
        public ProgressDialog(string path)
        {
            InitializeComponent();
            this.file_path = path;

          
        }
      
        public void UpdateProgress(int percentage)
        {
            pb_process.Value = percentage;
            if (percentage == 100)
            {
                Close();
            }
        }
        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        }


        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pb_process.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100)
            {
                this.Close();
            }
        }



        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Mobile_db db = new Mobile_db();
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(file_path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            double row_count = 0;
            int column_count = 0;

            range = xlWorkSheet.UsedRange;
            row_count = range.Rows.Count;
            column_count = range.Columns.Count;

            string str = "";

            for (double row = 1; row <= row_count; row++)
            {
                Mobile_model mobile = new Mobile_model(); ;
                for (int column = 1; column <= column_count; column++)
                {
                    try
                    {
                        if (((range.Cells[row, column] as Excel.Range).Value2).GetType() == typeof(int) || ((range.Cells[row, column] as Excel.Range).Value2).GetType() == typeof(double))
                        {
                            str = ((double)(range.Cells[row, column] as Excel.Range).Value2).ToString();
                        }
                        else
                        {
                            str = (range.Cells[row, column] as Excel.Range).Value2;
                        }
                    }
                    catch (Exception e1)
                    {
                        Log.e("reading from excel failed. error=" + e1.ToString(), "Excel_helper", "excelToDb");
                    }

                    switch (column)
                    {
                        case 1:
                            mobile.number = Mobile_helper.fixNumber(str);
                            break;
                        case 2:
                            mobile.first_name = str;
                            break;
                        case 3:
                            mobile.last_name = str;
                            break;
                        default:
                            break;
                    }
                }

                Mobile_model mob = db.findMobile(mobile.number);
                if (mob == null)
                {
                    db.saveMobile(mobile);
                }

                double percent = (row / row_count) * 100;
                (sender as BackgroundWorker).ReportProgress((int)percent);


            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);


        }

       






       

        
    }
}
