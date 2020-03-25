using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using System.Windows;
using Telbot.model;
using Telbot.storage;
using Telbot.system;

namespace Telbot.helper
{
    class Excel_helper
    {



        public static List<Mobile_model> read(string location)
        {
            List<Mobile_model> mobiles = new List<Mobile_model>();
            Excel.Application xlApp ;
            Excel.Workbook xlWorkBook ;
            Excel.Worksheet xlWorkSheet ;
            Excel.Range range ;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(location, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int row_count = 0;
            int column_count = 0;

            range = xlWorkSheet.UsedRange;
            row_count = range.Rows.Count;
            column_count = range.Columns.Count;

            string str = "";

            for (int row = 1; row  <= row_count; row++)
            {
                Mobile_model mobile = new Mobile_model(); ;
                for (int column = 1; column  <= column_count; column++)
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
                    catch(Exception e)
                    {
                        Log.e("reading from excel failed. error=" + e.ToString(), "Excel_helper", "read");
                    }

                    switch (column)
                    {
                        case 1:
                            mobile.number = str;
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

                mobiles.Add(mobile);
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            return mobiles;
        }




        public static int getRowCount(string location)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(location, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int row_count = 0;
            range = xlWorkSheet.UsedRange;
            row_count = range.Rows.Count;


            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            return row_count;
        }
    }
}
