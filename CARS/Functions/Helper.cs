using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace CARS.Functions
{
    class Helper
    {
        public static string GenerateUID()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
                .Range(65, 26)
                    .Select(e => ((char)e).ToString())
                    .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                    .Concat(Enumerable.Range(0, 9).Select(e => e.ToString()))
                    .OrderBy(e => Guid.NewGuid())
                    .Take(10)
                    .ToList().ForEach(e => builder.Append(e));
            return Convert.ToString(builder);
        }

        public static bool MenuConfirmator(int counter, int dashboard, string message, string header)
        {
            if (counter == 0 || dashboard == 1)
            {
                return true;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(message, header, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool Confirmator(string message, string header, MessageBoxButtons btn, MessageBoxIcon icon)
        {
            DialogResult dialogResult = MessageBox.Show(message, header, btn, icon);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ClearTextboxes(System.Windows.Forms.Control control)
        {
            foreach (System.Windows.Forms.Control c in control.Controls)
            {
                if (c is TextBox textBox)
                {
                    textBox.Text = string.Empty;
                }
                else
                {
                    // Recurse into nested controls
                    ClearTextboxes(c);
                }
            }
        }

        public static byte[] ImageToByteArray(System.Drawing.Image image)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }

        public static void TranLog(string Module, string ActionTaken, SqlConnection connection, SqlCommand command, SqlTransaction transaction)
        {
            try
            {
                command = Connection.setTransactionCommand($"INSERT INTO TblTranLog(module_name, userid, date_time, action_taken, auth_no) " +
                                                           $"          VALUES(@module_name, @userid, GETDATE(), @action_taken, @auth_no)", connection, transaction);
                command.Parameters.AddWithValue("@module_name", Module);
                command.Parameters.AddWithValue("@userid", "Justin");
                command.Parameters.AddWithValue("@action_taken", ActionTaken);
                command.Parameters.AddWithValue("@auth_no", GenerateUID());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { }
        }

        public static TextBox ColoumnSearcher(DataGridView dgv, int fontsize, int boxwidth)
        {
            TextBox TxtColumnSearch = new TextBox();
            TxtColumnSearch.CharacterCasing = CharacterCasing.Upper;
            TxtColumnSearch.Font = new Font("Segoe UI", fontsize); //20 default
            TxtColumnSearch.Size = new Size(boxwidth, TxtColumnSearch.Height);//300 default
            dgv.Controls.Add(TxtColumnSearch);
            TxtColumnSearch.Location = new System.Drawing.Point(dgv.Width, 50); //50 default
            TxtColumnSearch.Visible = false;
            return TxtColumnSearch;
        }

        public static string EscapeLikeValue(string valueWithoutWildcards)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '%' || c == '[' || c == ']' || c == '*')
                {
                    sb.Append('[').Append(c).Append(']');
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        public static Bitmap ModalEffect(System.Drawing.Point clientPoint, Rectangle clientRectangle)
        {
            Bitmap bmp = new Bitmap(clientRectangle.Width, clientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                G.CopyFromScreen(clientPoint, new System.Drawing.Point(0, 0), clientRectangle.Size);
                double percent = 0.60;
                Color darken = Color.FromArgb((int)(255 * percent), Color.Black);
                using (Brush brsh = new SolidBrush(darken))
                {
                    G.FillRectangle(brsh, clientRectangle);
                }
            }

            return bmp;
        }

        public static bool IsBase64Encoded(string input)
        {
            string base64Pattern = @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=$)?$";

            return Regex.IsMatch(input, base64Pattern);
        }

        public static string EncryptPasswordDesktopAppVersion(string encryptpassword)
        {
            try
            {
                string decryptedpassword = "";

                for (int i = 0; i < encryptpassword.Length; i++)
                {
                    string strget = encryptpassword.Substring(i, 1);
                    char c = Convert.ToChar(Convert.ToInt32(Convert.ToChar(strget) - 14));
                    decryptedpassword = decryptedpassword + c.ToString();
                }
                return decryptedpassword;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string DecryptPasswordDesktopAppVersion(string encryptpassword)
        {
            try
            {
                string decryptedpassword = "";

                for (int i = 0; i < encryptpassword.Length; i++)
                {
                    string strget = encryptpassword.Substring(i, 1);
                    char c = Convert.ToChar(Convert.ToInt32(Convert.ToChar(strget) + 14));
                    decryptedpassword = decryptedpassword + c.ToString();
                }
                return decryptedpassword;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable ReadExcelFile(string filePath, string sheetName = "Main")
        {
            DataTable dataTable = new DataTable();
            bool firstRow = true;

            using (XLWorkbook workbook = new XLWorkbook(filePath))
            {
                IXLWorksheet worksheet = workbook.Worksheet(sheetName);

                foreach (IXLRow row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.CellsUsed())
                        {
                            dataTable.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        bool isnoerror = true;
                        int columncount = dataTable.Columns.Count;
                        for (int i = 1; i != columncount; i++)
                        {
                            IXLCell cells = row.Cell(i);
                            //if (string.IsNullOrEmpty(cells.GetValue<dynamic>()))
                            if (cells.Value.ToString() == "" || cells.Value.ToString() == null)
                            {
                                Helper.Confirmator("There's a null value on the data", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                isnoerror = false;
                                break;
                            }
                            else if (!cells.Value.IsNumber)
                            {
                                Helper.Confirmator("There's a null value on the data", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                isnoerror = true;
                            }
                        }
                        if (isnoerror)
                        {
                            dataTable.Rows.Add(row.CellsUsed().Select(cell => cell.Value.ToString()).ToArray());

                        }
                    }
                    Console.WriteLine(dataTable.Rows.Count);
                }
                return dataTable;
            }
        }



        //public static DataTable MergeDataTable(DataTable currentTable, DataTable newTable)
        //{
        //    if(currentTable == null)
        //    {
        //        return newTable;
        //    }
        //    else
        //    {
        //        currentTable.Merge(newTable);
        //        return currentTable;
        //    }
        //}

        public static DataTable MergeDataTable(DataTable currentTable, DataTable newTable, DataTable masterTable)
        {
            DataTable mergedTable = new DataTable();
            mergedTable.Columns.Add("PartNo");
            mergedTable.Columns.Add("PartName");
            mergedTable.Columns.Add("BrandName");
            mergedTable.Columns.Add("DescName");
            mergedTable.Columns.Add("Qty");
            foreach (DataRow row in currentTable.Rows)
            {
                mergedTable.ImportRow(row);
            }

            foreach (DataRow newRow in newTable.Rows)
            {
                var partNo = newRow.Field<string>("PartNo").Trim();
                var newQty = Convert.ToInt32(newRow.Field<string>("Qty"));
                bool isExist = masterTable.AsEnumerable().Any(ex => ex.Field<string>("PartNo").Trim() == partNo);
                if (isExist)
                {
                    var existingRow = mergedTable.AsEnumerable().FirstOrDefault(ex => ex.Field<string>("PartNo").Trim() == partNo);
                    if (existingRow != null)
                    {
                        var existQty = Convert.ToInt32(existingRow.Field<string>("Qty"));
                        existQty += newQty;
                        existingRow.SetField("Qty", Convert.ToString(existQty));
                    }
                    else
                    {
                        DataRow mergedRow = mergedTable.NewRow();
                        foreach (DataColumn column in newTable.Columns)
                        {
                            if (mergedTable.Columns.Contains(column.ColumnName))
                            {
                                mergedRow[column.ColumnName] = newRow[column.ColumnName];
                            }
                        }
                        mergedTable.Rows.Add(mergedRow);
                    }
                }
                else
                {
                    MessageBox.Show($"The PartNo {partNo} does not exist", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }
            return mergedTable;
        }



        public static Image ResizeImage(Image img, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0);
            }
            return resizedImage;
        }
    }
}


//foreach (IXLCell cells in row.Cells())
//{
//    if (cells.Value.ToString() != null || cells.Value.ToString().TrimEnd() != "")
//    {

//        dataTable.Rows.Add(row.Cells().Select(cell => cell.Value.ToString()).ToArray());
//    }
//    else
//    {
//        Helper.Confirmator("There's a null value on the data", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        break;
//    }
//}
//int i = 0;
//foreach (IXLCell cell in row.Cells())
//{
//    dataTable.Rows[dataTable.Rows.Count - 1][i] = cell.Value.ToString();
//    i++;
//}