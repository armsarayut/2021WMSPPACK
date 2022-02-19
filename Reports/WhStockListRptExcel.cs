using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using GoWMS.Server.Data;
using GoWMS.Server.Models.Inv;

namespace GoWMS.Server.Reports
{
    public class WhStockListRptExcel
    {
        MemoryStream _memoryStream = new MemoryStream();
        List<InvStockList> _ListRpt = new List<InvStockList>();
        public byte[] Report(List<InvStockList> ListRpt)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("STOCK LIST");
                int startRows = 4;
                #region Excel Report Header
                var imagePath = VarGlobals.Imagelogoreport();
                worksheet.Column(1).Width = 18;
                worksheet.Row(1).Height = 60;
                var image = worksheet.AddPicture(imagePath).MoveTo(worksheet.Cell("A1")); //this will throw an error
                image.ScaleWidth(.3);
                image.ScaleHeight(.2);
                worksheet.Cell("B1").Value = "STOCK LIST" + " Report";
                worksheet.Cell("B1").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.Cell("B2").Value = $"PrintDate : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                #endregion Excel

                #region Excel Report Data
                var rptRows = 4;
                worksheet.Cell(rptRows, 1).Value = "MATERIAL";
                worksheet.Cell(rptRows, 2).Value = "DESCRIPTION";
                worksheet.Cell(rptRows, 3).Value = "STOCK";
                worksheet.Cell(rptRows, 4).Value = "PACKID";
                worksheet.Cell(rptRows, 5).Value = "PALLET";
                worksheet.Cell(rptRows, 6).Value = "AREA";
                worksheet.Cell(rptRows, 7).Value = "STROAGEBIN";

                foreach (var rpt in ListRpt)
                {
                    rptRows++;
                    worksheet.Cell(rptRows, 1).Value = rpt.Item_code;
                    worksheet.Cell(rptRows, 2).Value = rpt.Item_code;
                    worksheet.Cell(rptRows, 3).Value = string.Format(VarGlobals.FormatN2, rpt.Qty);

                    worksheet.Cell(rptRows, 4).DataType = XLDataType.Text;

   
                    worksheet.Cell(rptRows, 4).Value = "'" + rpt.Su_no.ToString();

                    worksheet.Cell(rptRows, 5).Value = rpt.Palletcode;
                    worksheet.Cell(rptRows, 6).Value = rpt.StorageArae;
                    worksheet.Cell(rptRows, 7).DataType = XLDataType.Text;
                    worksheet.Cell(rptRows, 7).Value = "'" + rpt.Shelfname.ToString();

                    #endregion

                    worksheet.SheetView.Freeze(startRows, 1);
                    workbook.SaveAs(_memoryStream);
                }
                return _memoryStream.ToArray();
            }
        }
    }
}
