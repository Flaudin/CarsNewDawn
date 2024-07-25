using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Transactions;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace CARS.Components.Transactions.SupplierQuotation
{
    public partial class frm_supplier_quotation : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        SupplierQuotationController supplierQuotation = new SupplierQuotationController();
        DataTable  SupplierQuotationTable = new DataTable();
        SupplierQuotationModel quotationModel = new SupplierQuotationModel();
        DataTable PartsTable = new DataTable();
        private SortedDictionary<string, string> _brandsDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _descsDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _supplierDictionary = new SortedDictionary<string, string>();
        public frm_supplier_quotation()
        {
            InitializeComponent();
            BtnClose.BackColor = PnlHeader.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlFilter.BackColor = PnlPartsList.BackColor = PnlSupplier.BackColor = PnlQuotation.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            BtnClose.ForeColor = LblHeader.ForeColor = LblFilter.ForeColor = LblHeader.ForeColor = LblParts.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);

            _brandsDictionary = supplierQuotation.getBrands();
            cmbBrands.DataSource = new BindingSource(_brandsDictionary, null);
            cmbBrands.DisplayMember = "Value";
            cmbBrands.ValueMember = "Key";

            _descsDictionary = supplierQuotation.getDescriptions();
            cmbDesc.DataSource = new BindingSource(_descsDictionary, null);
            cmbDesc.DisplayMember = "Value";
            cmbDesc.ValueMember = "Key";

            _supplierDictionary = supplierQuotation.getSupplier();
            cmbSupplier.DataSource = new BindingSource(_supplierDictionary, null);
            cmbSupplier.DisplayMember = "Value";
            cmbSupplier.ValueMember = "Key";

            dgvPartsBreakdown.CellValueChanged += dgvPartsBreakdown_CellValueChanged;
            dgvPartsBreakdown.CellEndEdit += dgvPartsBreakdown_CellEndEdit;
            dgvPartsBreakdown.CellValidating += dgvPartsBreakdown_CellValidating;
            dgvPartsBreakdown.EditingControlShowing += dgvPartsBreakdown_EditingControlShowing;
        }

        

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PartsTable = supplierQuotation.getParts();
            dgvParts.DataSource = PartsTable;
        }

        private void dgvParts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvParts != null)
            {
                DataTable dt = dgvPartsBreakdown.DataSource as DataTable;
                if(dt != null)
                {
                    dt.Rows.Clear();
                }
                DataGridViewRow currentPartNo = dgvParts.Rows[e.RowIndex];
                string PartNo = currentPartNo.Cells["PartNo"].Value.ToString();

                foreach(DataGridViewRow row in dgvPartsBreakdown.Rows)
                {
                    string existingPartsNo = row.Cells["PartNoSupplier"].Value.ToString();
                    if(existingPartsNo == PartNo)
                    {
                        MessageBox.Show("The selected order is already been selected");
                        return;
                    }
                }
                //SupplierQuotationTable = supplierQuotation.get
            }
        }

        private void dgvParts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvParts.CurrentRow != null)
            {
                DataTable dt = dgvPartsBreakdown.DataSource as DataTable;
                if(dt != null)
                {
                    dt.Rows.Clear();
                }
                DataGridViewRow currentPartsRow = dgvParts.CurrentRow;
                string PartNo = currentPartsRow.Cells["PartNo"].Value.ToString().Trim();
                //decimal UnitPrice = 

                foreach(DataGridViewRow row in dgvPartsBreakdown.Rows)
                {
                    string existingPartNo = row.Cells["PartNoSupplier"].Value.ToString();
                    if(existingPartNo == PartNo)
                    {
                        MessageBox.Show("The selected item cannot be added to the table.");
                        return;
                    }
                }
                DataGridViewRow dataRpw = new DataGridViewRow();
                dataRpw.CreateCells(dgvPartsBreakdown);
                dataRpw.Cells[0].Value = PartNo.Trim();
                dataRpw.Cells[1].Value = 0.00.ToString();
                dataRpw.Cells[2].Value = 0.00.ToString();
                dataRpw.Cells[3].Value = 0.00.ToString();
                dgvPartsBreakdown.Rows.Add(dataRpw);
            }
            //dgvPartsBreakdown.Rows.Add(currentPartNo.Cells["PartNo"].Value.ToString(), "", "", "");
        }

        private void dgvPartsBreakdown_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvPartsBreakdown.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvPartsBreakdown_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dgvPartsBreakdown.Columns["UnitPrice"].Index || e.ColumnIndex == dgvPartsBreakdown.Columns["PriceDiscount"].Index)
            {
                CalculateNetPrice(e.RowIndex);
            }
        }

        private void dgvPartsBreakdown_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if(e.ColumnIndex == dgvPartsBreakdown.Columns["UnitPrice"].Index || e.ColumnIndex == dgvPartsBreakdown.Columns["PriceDiscount"].Index)
            {
                if(!decimal.TryParse(e.FormattedValue.ToString(), out _))
                {
                    MessageBox.Show("Please enter a valid numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private void dgvPartsBreakdown_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if(dgvPartsBreakdown.CurrentCell.ColumnIndex == dgvPartsBreakdown.Columns["UnitPrice"].Index || 
                dgvPartsBreakdown.CurrentCell.ColumnIndex == dgvPartsBreakdown.Columns["PriceDiscount"].Index)
            {
                if(e.Control is TextBox textBox)
                {
                    textBox.KeyDown -= TextBox_KeyDown;
                    textBox.KeyDown += TextBox_KeyDown;
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(!char.IsControl((char)e.KeyCode) && !char.IsDigit((char)e.KeyCode) && (e.KeyCode != Keys.Decimal) && (e.KeyCode != Keys.OemPeriod))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void CalculateNetPrice(int rowIndex)
        {
            try
            {
                var unitPriceCell = dgvPartsBreakdown.Rows[rowIndex].Cells["UnitPrice"].Value;
                var discountPriceCell = dgvPartsBreakdown.Rows[rowIndex].Cells["PriceDiscount"].Value;
                var quantityCell = dgvPartsBreakdown.Rows[rowIndex].Cells["Qty"].Value;

                if(unitPriceCell != null && discountPriceCell != null )
                {
                    decimal unitPrice = Convert.ToDecimal(unitPriceCell);
                    decimal discountPrice = Convert.ToDecimal(discountPriceCell);
                    decimal quantity = Convert.ToDecimal(quantityCell);

                    decimal netPrice =  (unitPrice - discountPrice);

                    dgvPartsBreakdown.Rows[rowIndex].Cells["ListPrice"].Value = netPrice;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error calculating net price: " + ex.Message);
            }
        }

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            dgvPartsBreakdown.Rows.Remove(dgvPartsBreakdown.CurrentRow);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(cmbSupplier.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a supplier", "Supplier Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to save the data?","Saving Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if(dgvPartsBreakdown.Rows.Count != 0)
                        {
                            List<SupplierQuotationDet> quatationDetails = new List<SupplierQuotationDet>();
                            string termId = supplierQuotation.getTermID(cmbSupplier.SelectedValue.ToString());
                            foreach(DataGridViewRow row in dgvPartsBreakdown.Rows)
                            {
                                SupplierQuotationDet quotationDet = new SupplierQuotationDet
                                {
                                    PartNo = row.Cells["PartNoSupplier"].Value.ToString(),
                                    Qty = Convert.ToDecimal(row.Cells["Qty"].Value),
                                    UnitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value),
                                    Discount = Convert.ToDecimal(row.Cells["PriceDiscount"].Value),
                                    ListPrice = Convert.ToDecimal(row.Cells["ListPrice"].Value),
                                };
                                quatationDetails.Add(quotationDet);
                                quotationModel = new SupplierQuotationModel
                                {
                                    supplierQuotationDets = quatationDetails,
                                    SuppID = cmbSupplier.SelectedValue.ToString(),
                                    Status = 1,
                                    TermID = termId
                                };
                            }
                            string msgResult = supplierQuotation.saveQuotation(quotationModel);
                            Helper.Confirmator(msgResult, "Quotation Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if(msgResult == "Quotation Saved")
                            {
                                dgvParts.Rows.Clear();
                                dgvPartsBreakdown.Rows.Clear();
                            }
                        }
                        else
                        {
                            Helper.Confirmator("No items found in the list", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void btnExeImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.InitialDirectory = "";
                openFile.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                openFile.FilterIndex = 1;
                openFile.RestoreDirectory = true;

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFile.FileName;
                    DataTable partsTable = ReadExcelFile(filePath);
                    dgvPartsBreakdown.Rows.Clear();
                    dgvPartsBreakdown.DataSource = partsTable;
                }
            }
        }

        private DataTable ReadExcelFile(string filePath)
        {
            DataTable partsTable = new DataTable();
            const int MaxColumns = 5;

            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
            Excel._Worksheet worksheet = workbook.Sheets[1];
            Excel.Range range = worksheet.UsedRange;

            int rowCount = range.Rows.Count;
            int columnCount = Math.Min(range.Columns.Count, MaxColumns);

            // Add columns to DataTable based on DataGridView columns
            foreach (DataGridViewColumn col in dgvPartsBreakdown.Columns)
            {
                partsTable.Columns.Add(col.HeaderText);
            }

            // Start reading from the second row to skip headers
            for (int i = 2; i <= rowCount; i++)
            {
                DataRow row = partsTable.NewRow();
                for (int j = 1; j <= columnCount; j++)
                {
                    var cellValue = (range.Cells[i, j] as Excel.Range).Value2;
                    row[j - 1] = cellValue != null ? cellValue.ToString() : string.Empty;
                }
                partsTable.Rows.Add(row);
            }

            // Cleanup
            workbook.Close(false);
            excelApp.Quit();

            ReleaseObject(worksheet);
            ReleaseObject(workbook);
            ReleaseObject(excelApp);

            return partsTable;
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the item: " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
