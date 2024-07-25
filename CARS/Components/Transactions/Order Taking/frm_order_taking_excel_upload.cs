using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Utilities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Transactions.Order_Taking
{
    public partial class frm_order_taking_excel_upload : Form
    {
        private poOrderTakingController _orderTakingController = new poOrderTakingController();
        private DataTable parts = new DataTable();
        private readonly Action<DataTable> _updateOrderList;
        private ColorManager _ColorManager = new ColorManager();
        public frm_order_taking_excel_upload(Action<DataTable> updateOrderList)
        {
            InitializeComponent();
            LblUpload.ForeColor =  Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            pnlHeader.BackColor =  Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            parts = _orderTakingController.PartsFormat();
            _updateOrderList = updateOrderList;
        }
        private void btnDownloadHeader_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files | *.xlsx",
                Title = "Save Format Excel File"
            };

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DownloadFormatExcel(saveFileDialog.FileName);
                Helper.Confirmator("Excel file downloaded successfully", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private  void DownloadFormatExcel(string filepath)
        {
            using (var workBook = new XLWorkbook())
            {
                var mainSheet = workBook.Worksheets.Add("Main");
                mainSheet.Cell(1, 1).Value = "Qty";
                mainSheet.Cell(1, 2).Value = "PartNo";
                mainSheet.Cell(1, 3).Value = "Alter";



                var PartsSheet = workBook.Worksheets.Add("Parts");
                PartsSheet.Cell(1, 1).Value = "PartNo";
                PartsSheet.Cell(1, 2).Value = "PartName";
                PartsSheet.Cell(1, 3).Value = "Brand";
                PartsSheet.Cell(1, 4).Value = "Description";
                FillSheetWithData(PartsSheet, parts);

                PartsSheet.Cells().Style.Protection.Locked = true;


                int lastPartName = parts.Rows.Count + 1;
                var partNameRange = PartsSheet.Range($"A2:A{lastPartName}");
                partNameRange.AddToNamed("PartNoList");
                var partSuggest = mainSheet.Range("A2:A100");
                partSuggest.CreateDataValidation().List(partNameRange);

                PartsSheet.Protect("CARS");

                workBook.SaveAs(filepath);
            }
        }

        private static void FillSheetWithData(IXLWorksheet sheet, DataTable data)
        {
            for(int col = 0; col < data.Columns.Count; col++)
            {
                sheet.Cell(1, col + 1).Value = data.Columns[col].ColumnName;
            }
            for(int row = 0; row < data.Rows.Count; row++)
            {
                for(int col = 0; col < data.Columns.Count; col++)
                {
                    sheet.Cell(row + 2, col + 1).Value = data.Rows[row][col].ToString();
                }
            }
        }

        private void customCloseButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataTable getExecTable = Helper.ReadExcelFile(openFileDialog.FileName);
                        //Debug.WriteLine(getExecTable);
                        DataTable mergedData = _orderTakingController.fetchData(getExecTable);
                        _updateOrderList?.Invoke(mergedData);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Helper.Confirmator($"Error reading file {ex.Message}", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if(Helper.Confirmator("This form will be close, Press Yes to proceed","System Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }
    }
}
