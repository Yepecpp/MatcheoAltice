﻿using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using MatcheoAltice.Properties;
using Microsoft.Office.Interop.Excel;
using MySqlX.XDevAPI.Common;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.Xml.Linq;
using OfficeOpenXml;

namespace MatcheoAltice
{
    class ExcelFn
    {
        public static void ExportExcel(string path, System.Windows.Forms.DataGridView dataGridView1)
        {
            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the package
                var worksheet = package.Workbook.Worksheets.Add("Datos");

                // Fill the header row
                int columnIndex = 1;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    worksheet.Cells[1, columnIndex].Value = column.HeaderText;
                    columnIndex++;
                }

                // Prepare the data in a 2D array
                int rowCount = dataGridView1.Rows.Count;
                int colCount = dataGridView1.Columns.Count;
                object[,] data = new object[rowCount, colCount];
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        data[row, col] = dataGridView1.Rows[row].Cells[col].Value;
                    }
                }

                // Add the data to the worksheet
                var startCell = worksheet.Cells[2, 1];
                var endCell = startCell.Offset(rowCount - 1, colCount - 1);
                var dataRange = worksheet.Cells[startCell.Address + ":" + endCell.Address];
                dataRange.Value = data;

                // Save the package to a file
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                package.SaveAs(file);


            }
        }
    }
}