using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExportExcelController : ControllerBase
    {
        public readonly GeneralContext _context;
        public ExportExcelController(GeneralContext context)
        {
            _context = context;
        }

        [HttpPost("saveReport")]
        public async Task<IActionResult> SaveExcel(Excel1 result) 
        {
            try
            {
                if (result != null)
                {
                    List1[] items = JsonConvert.DeserializeObject<List1[]>(result.result);
                    //Microsoft.Office.Interop.Excel.Application xlApp;
                    //Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    //Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                    //object misValue = System.Reflection.Missing.Value;

                    //xlApp = new Microsoft.Office.Interop.Excel.Application();
                    //xlWorkBook = xlApp.Workbooks.Add(misValue);
                    //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    ////add data 
                    //xlWorkSheet.Cells[1, 1] = "";
                    //xlWorkSheet.Cells[1, 2] = "Student1";
                    //xlWorkSheet.Cells[1, 3] = "Student2";
                    //xlWorkSheet.Cells[1, 4] = "Student3";

                    //xlWorkSheet.Cells[2, 1] = "Term1";
                    //xlWorkSheet.Cells[2, 2] = "80";
                    //xlWorkSheet.Cells[2, 3] = "65";
                    //xlWorkSheet.Cells[2, 4] = "45";

                    //xlWorkSheet.Cells[3, 1] = "Term2";
                    //xlWorkSheet.Cells[3, 2] = "78";
                    //xlWorkSheet.Cells[3, 3] = "72";
                    //xlWorkSheet.Cells[3, 4] = "60";

                    //xlWorkSheet.Cells[4, 1] = "Term3";
                    //xlWorkSheet.Cells[4, 2] = "82";
                    //xlWorkSheet.Cells[4, 3] = "80";
                    //xlWorkSheet.Cells[4, 4] = "65";

                    //xlWorkSheet.Cells[5, 1] = "Term4";
                    //xlWorkSheet.Cells[5, 2] = "75";
                    //xlWorkSheet.Cells[5, 3] = "82";
                    //xlWorkSheet.Cells[5, 4] = "68";

                    //Microsoft.Office.Interop.Excel.Range chartRange;

                    //Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
                    //Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
                    //Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;

                    //chartRange = xlWorkSheet.get_Range("A1", "d5");
                    //chartPage.SetSourceData(chartRange, misValue);
                    //chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered;


                    //xlWorkBook.SaveAs("csharp.net-informations.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    //xlWorkBook.Close(true, misValue, misValue);
                    //xlApp.Quit();

                    //releaseObject(xlWorkSheet);
                    //releaseObject(xlWorkBook);
                    //releaseObject(xlApp);
                }
            }
            catch (Exception exx) { }
            return new ObjectResult(result);
        }
        private void releaseObject(object obj)
        {
            try
            {
                #pragma warning disable CA1416 // Validate platform compatibility
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                #pragma warning restore CA1416 // Validate platform compatibility
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
    public class Excel1
    {
        public string result { get; set; }
    }
    public class List1
    {
        public int num_chosen;
        public decimal[] final_reach;
        public string[] final_combination;
        public decimal[] final_freq;
        public decimal[] final_vol;
        public int fixedList;
    }
}
