﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using GoWMS.Server.Data;
using GoWMS.Server.Models.Mas;

namespace GoWMS.Server.Reports
{
    public class MasItemPageRptExcel
    {
        MemoryStream _memoryStream = new MemoryStream();
        //List<Inb_Goodreceipt_Go> _Inb_Goodreceive_Go_s = new List<Inb_Goodreceipt_Go>();
        public byte[] Report(List<Mas_Item_Go> rptElements)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("6.1");
                #region Excel Report Header
                var imagePath = VarGlobals.Imagelogoreport();
                worksheet.Column(1).Width = 18;
                worksheet.Row(1).Height = 60;
                var image = worksheet.AddPicture(imagePath).MoveTo(worksheet.Cell("A1")); //this will throw an error
                image.ScaleWidth(.7);
                image.ScaleHeight(.7);
                worksheet.Cell("B1").Value = "6.1.Item" + " - Report";
                worksheet.Cell("B1").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.Cell("B2").Value = $"PrintDate : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                #endregion Excel

                #region Excel Report Data
                var rptRows = 4;
                worksheet.Cell(rptRows, 1).Value = "ITEMCODE";
                worksheet.Cell(rptRows, 2).Value = "ITEMNAME";
                worksheet.Cell(rptRows, 3).Value = "UNIT";
                worksheet.Cell(rptRows, 4).Value = "NETWEIGHT";
                worksheet.Cell(rptRows, 5).Value = "GROSSWEIGHT";
                worksheet.Cell(rptRows, 6).Value = "WEIGHTUNIT";

                foreach (var rpt in rptElements)
                {
                    rptRows++;
                    worksheet.Cell(rptRows, 1).Value = rpt.Itemcode;
                    worksheet.Cell(rptRows, 2).Value = rpt.Itemname;
                    worksheet.Cell(rptRows, 3).Value = rpt.Itemunit;
                    worksheet.Cell(rptRows, 4).Value = string.Format(VarGlobals.FormatN2, rpt.Weightnet);
                    worksheet.Cell(rptRows, 5).Value = string.Format(VarGlobals.FormatN2, rpt.Weightgross);
                    worksheet.Cell(rptRows, 6).Value = rpt.Weightuint;


                }
                #endregion
                workbook.SaveAs(_memoryStream);
            }
            return _memoryStream.ToArray();
        }
    }
}
