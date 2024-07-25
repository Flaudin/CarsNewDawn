using CARS.Controllers.Masterfiles;
using CARS.Functions;
using CARS.Model.Masterfiles;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Masterfiles
{
    public partial class frm_parts_encodeNew : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private PartsController _PartsController = new PartsController();
        private PartsModel _PartsModel = new PartsModel();
        private SortedDictionary<string, string> _MeasurementDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _BrandDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _DescriptionDictionary = new SortedDictionary<string, string>();
        private PartsModel partsDetails = new PartsModel();
        private string BsbPart = "";
        public event EventHandler SearchButton;

        public frm_parts_encodeNew(string id)
        {
            InitializeComponent();
            BtnClose.BackColor = PnlHeader.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderAlternate.BackColor = PnlHeaderDetails.BackColor = PnlHeaderOEM.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            BtnClose.ForeColor = LblHeader.ForeColor = LblDetails.ForeColor = LblOEM.ForeColor = LblAlternate.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _MeasurementDictionary = _PartsController.GetDictionary("Uom");
            _BrandDictionary = _PartsController.GetDictionary("Brand");
            _DescriptionDictionary = _PartsController.GetDictionary("Description");
            ComboMeasurement.DataSource = new BindingSource(_MeasurementDictionary, null);
            ComboBrand.DataSource = new BindingSource(_BrandDictionary, null);
            ComboDescription.DataSource = new BindingSource(_DescriptionDictionary, null);
            ComboMeasurement.DisplayMember = ComboBrand.DisplayMember = ComboDescription.DisplayMember = "Key";
            ComboMeasurement.ValueMember = ComboBrand.ValueMember = ComboDescription.ValueMember = "Value";
            GetPartData(id);
        }

        private void GetPartData(string id)
        {
            if (id != "")
            {
                partsDetails = GetParts(id);
                LblHeader.Text = "EDIT " + id;
                TxtPartNo.Textt = id;
                TxtPartNo.ReadOnly = true;
                TxtPartName.Textt = partsDetails.PartName;
                ComboMeasurement.SelectedValue = partsDetails.Uom;
                ComboBrand.SelectedValue = partsDetails.Brand;
                TxtOtherName.Textt = partsDetails.OtherName;
                TxtStockKeeping.Textt = partsDetails.Sku;
                NumericInnerUnit.Value = partsDetails.IUpack;
                NumericMotherUnit.Value = partsDetails.MUpack;
                TxtPosition.Textt = partsDetails.PPosition;
                TxtSize.Textt = partsDetails.PSize;
                TxtType.Textt = partsDetails.Ptype;
                NumericListPrice.Value = partsDetails.ListPrice;
                NumericListPrice.ReadOnly = true;
                NumericListPrice.Increment = 0;
                NumericBsbListPrice.Value = partsDetails.BListPrice;
                TxtApplication.Textt = partsDetails.PartApplication;
                ComboDescription.SelectedValue = partsDetails.Description;
                CheckActive.Checked = partsDetails.IsActive;
                BsbPart = partsDetails.BPartNo;
                if (BsbPart != "")
                {
                    CheckBSB.Checked = true;
                }
                else
                {
                    CheckBSB.Checked = false;
                }

                if (partsDetails.Image != "")
                {
                    byte[] PartImages;
                    if (Helper.IsBase64Encoded(partsDetails.Image))
                    {
                        PartImages = Convert.FromBase64String(partsDetails.Image);
                    }
                    else
                    {
                        PartImages = Encoding.Default.GetBytes(partsDetails.Image);
                    }
                    using (MemoryStream ms = new MemoryStream(PartImages))
                    {
                        Image NewImage = Image.FromStream(ms);
                        ImagePart.Image = NewImage;
                        ms.Dispose();
                    }
                }

                DataGridOem.DataSource = _PartsController.dt(id);
                DataGridOem.ClearSelection();

                List<dynamic[]> AlternateParts = new List<dynamic[]>();
                DataTable AlternateDt = _PartsController.dt(new AlternatePartsModel { PartNo = id });
                foreach (DataRow datarow in AlternateDt.Rows)
                {
                    AlternateParts.Add(new dynamic[] { 0, datarow[0].ToString().TrimEnd(), datarow[1], datarow[2], 0 });
                }
                if (AlternateParts.Count != 0)
                {
                    for (int i = 0; AlternateParts.Count != DataGridAlternate.Rows.Count; i++)
                    {
                        DataGridAlternate.Rows.Add(AlternateParts[i]);
                        DataGridAlternate.Rows[i].Cells[0].ReadOnly = true;
                        DataGridAlternate.Rows[i].Cells[1].ReadOnly = true;
                    }
                }
            }
        }

        private PartsModel GetParts(string id) => _PartsController.Read(id);

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("All unsaved changes will be discarded. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (CheckBSB.Checked || (TxtPartName.Textt.TrimEnd() != "" && ComboMeasurement.SelectedIndex != 0 && ComboBrand.SelectedIndex != 0 && ComboDescription.SelectedIndex != 0 && NumericListPrice.Value != 0))
            {
                if (Helper.Confirmator("Are you sure you want to save this Part?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    string CustomMsg = "";
                    string uom = string.IsNullOrEmpty(ComboMeasurement.Text) ? null : ComboMeasurement.SelectedValue.ToString();
                    string brand = string.IsNullOrEmpty(ComboBrand.Text) ? null : ComboBrand.SelectedValue.ToString();
                    string desc = string.IsNullOrEmpty(ComboDescription.Text) ? null : ComboDescription.SelectedValue.ToString();
                    List<AlternatePartsModel> AlternatePartsList = new List<AlternatePartsModel>();
                    foreach (DataGridViewRow row in DataGridAlternate.Rows)
                    {
                        AlternatePartsModel model = new AlternatePartsModel { PartNo = TxtPartNo.Textt.TrimEnd(), AltPartNo = row.Cells[1].Value.ToString(), BOwn = Convert.ToBoolean(row.Cells[2].Value),
                                                                              IsActive = Convert.ToBoolean(row.Cells[3].Value), IsNew = Convert.ToBoolean(row.Cells[4].Value) };
                        AlternatePartsList.Add(model);
                    }

                    if (LblHeader.Text != "PARTS ENTRY")
                    {
                        string image = "";
                        if (BsbPart == "")
                        {
                            byte[] PartImage = Helper.ImageToByteArray(ImagePart.Image);
                            image = Convert.ToBase64String(PartImage);
                        }
                        _PartsModel = new PartsModel { PartNo = TxtPartNo.Textt.TrimEnd(), BPartNo = BsbPart, PartName = TxtPartName.Textt.TrimEnd(),
                                                       Uom = uom, Brand = brand, OtherName = TxtOtherName.Textt.TrimEnd(),
                                                       Sku = TxtStockKeeping.Textt.TrimEnd(), IUpack = NumericInnerUnit.Value, MUpack = NumericMotherUnit.Value, PPosition = TxtPosition.Textt.TrimEnd(),
                                                       PSize = TxtSize.Textt.TrimEnd(), Ptype = TxtType.Textt.TrimEnd(), IsActive = CheckActive.Checked,
                                                       BListPrice = NumericBsbListPrice.Value, PartApplication = TxtApplication.Textt.TrimEnd(), Description = desc,
                                                       AlternateList = AlternatePartsList, Image = image };
                        CustomMsg = _PartsController.Update(_PartsModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        byte[] PartImage = Helper.ImageToByteArray(ImagePart.Image);
                        _PartsModel = new PartsModel { PartNo = TxtPartNo.Textt.TrimEnd(), BPartNo = BsbPart, PartName = TxtPartName.Textt.TrimEnd(),
                                                       Uom = uom, Brand = brand,
                                                       OtherName = TxtOtherName.Textt.TrimEnd(), Sku = TxtStockKeeping.Textt.TrimEnd(), IUpack = NumericInnerUnit.Value,
                                                       MUpack = NumericMotherUnit.Value, PPosition = TxtPosition.Textt.TrimEnd(), PSize = TxtSize.Textt.TrimEnd(), Ptype = TxtType.Textt.TrimEnd(),
                                                       IsActive = CheckActive.Checked, ListPrice = NumericListPrice.Value, BListPrice = NumericBsbListPrice.Value,
                                                       PartApplication = TxtApplication.Textt.TrimEnd(), Description = desc,
                                                       AlternateList = AlternatePartsList, Image = Convert.ToBase64String(PartImage) };
                        CustomMsg = _PartsController.Create(_PartsModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        SearchButton?.Invoke(this, e);
                        this.Close();
                    }
                }
            }
            else
            {
                Helper.Confirmator("Please fill all the required fields before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Unsaved entries will be discarded. Are you sure you want to clear the input field(s)?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtPartName.Textt = TxtOtherName.Textt = TxtStockKeeping.Textt = TxtPosition.Textt = TxtSize.Textt = TxtType.Textt = TxtAsOf.Textt = TxtApplication.Textt = "";
                ComboMeasurement.Text = ComboBrand.Text = ComboDescription.Text = "";
                NumericInnerUnit.Value = NumericMotherUnit.Value = NumericListPrice.Value = NumericBsbListPrice.Value = 0;
                ImagePart.Image = null;
                CheckActive.Checked = true;
                CheckBSB.Checked = false;
                if (LblHeader.Text != "PARTS ENTRY")
                {
                    DataGridAlternate.Rows.Clear();
                    GetPartData(TxtPartNo.Textt);
                }
                else
                {
                    DataGridAlternate.Rows.Clear();
                }
            }
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            if (BsbPart == "")
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select an Image";
                    openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            ImagePart.Image = Image.FromFile(openFileDialog.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading image: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot modify BSB part image.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnAddAlternate_Click(object sender, EventArgs e)
        {
            bool allCellsHaveValue = true;
            if (DataGridAlternate.DataSource != null)
            {
                DataTable dt = DataGridAlternate.DataSource as DataTable;
                if (dt.Columns.Contains("AltPartNo"))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        object value = row["AltPartNo"];
                        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                        {
                            allCellsHaveValue = false;
                            break;
                        }
                    }

                    if (allCellsHaveValue)
                    {
                        DataRow newrow = dt.NewRow();
                        newrow["ToDelete"] = false;
                        newrow["AltPartNo"] = "";
                        newrow["BOwn"] = false;
                        newrow["IsActive"] = true;
                        newrow["IsNew"] = true;
                        dt.Rows.Add(newrow);
                    }
                    else
                    {
                        MessageBox.Show("Please fill all Alternate Part No fields before adding a new row.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in DataGridAlternate.Rows)
                {
                    object cellValue = row.Cells["AltPartNo"].Value;
                    if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                    {
                        allCellsHaveValue = false;
                        break;
                    }
                }

                if (allCellsHaveValue)
                {
                    DataGridAlternate.Rows.Add(false, "", false, true, true);
                }
                else
                {
                    MessageBox.Show("Please fill all Alternate Part No fields before adding a new row.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnDeleteAlternate_Click(object sender, EventArgs e)
        {
            bool hasCheckedCell = DataGridAlternate.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToBoolean(row.Cells["ToDelete"].Value));
            switch (hasCheckedCell)
            {
                case true:
                    if (DataGridAlternate.CurrentRow != null && Helper.Confirmator("Are you sure you want to deleted the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        for (int i = DataGridAlternate.Rows.Count - 1; i >= 0; i--)
                        {
                            DataGridViewRow row = DataGridAlternate.Rows[i];
                            DataGridViewCheckBoxCell checkboxCell = row.Cells[0] as DataGridViewCheckBoxCell;

                            if (Convert.ToBoolean(row.Cells[4].Value) && checkboxCell != null && Convert.ToBoolean(checkboxCell.Value))
                            {
                                DataGridAlternate.Rows.RemoveAt(i);
                            }
                        }
                    }
                    break;

                case false:
                    if (DataGridAlternate.CurrentRow != null)
                    {
                        if (Convert.ToBoolean(DataGridAlternate.CurrentRow.Cells["IsNew"].Value) &&
                            Helper.Confirmator("Are you sure you want to deleted the selected row?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            DataGridAlternate.Rows.RemoveAt(DataGridAlternate.CurrentRow.Index);
                        }
                        else if (!Convert.ToBoolean(DataGridAlternate.CurrentRow.Cells["IsNew"].Value))
                        {
                            MessageBox.Show("User cannot delete saved Alternate No.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is no row selected for deletion.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }
        }

        private void DataGridAlternate_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DataGridAlternate.CurrentCell.ColumnIndex == 1 && e.Control is TextBox)
            {
                ((TextBox)e.Control).CharacterCasing = CharacterCasing.Upper;
            }
        }

        private void frm_parts_encodeNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose.PerformClick();
            }
        }

        private void DataGridAlternate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && DataGridAlternate.CurrentCell.ReadOnly)
            {
                MessageBox.Show("User cannot delete saved Alternate No.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CheckBSB_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBSB.Checked)
            {
                ComboBrand.Enabled = ComboDescription.Enabled = ComboMeasurement.Enabled = false;
                NumericInnerUnit.ReadOnly = NumericMotherUnit.ReadOnly = TxtPosition.ReadOnly = TxtSize.ReadOnly = TxtType.ReadOnly = TxtApplication.ReadOnly = true;
            }
            else
            {
                ComboBrand.Enabled = ComboDescription.Enabled = ComboMeasurement.Enabled = true;
                NumericInnerUnit.ReadOnly = NumericMotherUnit.ReadOnly = TxtPosition.ReadOnly = TxtSize.ReadOnly = TxtType.ReadOnly = TxtApplication.ReadOnly = false;
            }
        }
    }
}
