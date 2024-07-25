using CARS.Controller.Inquiry;
using CARS.Controller.Masterfiles;
using CARS.Controllers.Masterfiles;
using CARS.Customized_Components;
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
    public partial class frm_promo_encode : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private PartsController _PartsController = new PartsController();
        private PartsModel _PartsModel = new PartsModel();
        private PartsModel partsDetails = new PartsModel();
        private MasterfileController _MasterfileController = new MasterfileController();
        private PromoController _PromoController = new PromoController();
        private PromoPartDetail _PromoPartDetail = new PromoPartDetail();
        private DataTable FilterPartTable = new DataTable();
        public event EventHandler SearchButton;

        public frm_promo_encode(string id)
        {
            InitializeComponent();
            BtnClose.BackColor = PnlHeader.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderDetails.BackColor = PnlHeaderFilter.BackColor = PnlHeaderOEM.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            BtnClose.ForeColor = LblHeader.ForeColor = LblFilter.ForeColor = LblDetails.ForeColor = LblOEM.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            ComboPromoParts.ItemSelected += CustomTextBox_ItemSelected;
            ComboFilter.SelectedIndex = 0;
            //GetPartData(id);
        }

        private void GetPartData(string id)
        {
            if (id != "")
            {
                partsDetails = GetParts(id);
                LblHeader.Text = "EDIT PROMO " + id;
                TxtPromoID.Textt = id;
                TxtPromoID.ReadOnly = true;
                TxtPromoName.Textt = partsDetails.PartName;

                DataGridPart.DataSource = _PartsController.dt(id);
                DataGridPart.ClearSelection();
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
            if (TxtPromoName.Textt.TrimEnd() != "" && TxtPromoID.Textt.TrimEnd() != "" && ComboPromo.SelectedIndex != 0)
            {
                if (Helper.Confirmator("Are you sure you want to save this Promo?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    string CustomMsg = "";
                    List<string> PartList = new List<string>();
                    foreach (DataGridViewRow row in DataGridPart.Rows)
                    {
                        PartList.Add(row.Cells[1].Value.ToString());
                    }

                    if (LblHeader.Text != "PROMO ENTRY")
                    {
                        //string image = "";
                        //_PartsModel = new PartsModel { PartNo = TxtPromoID.Textt.TrimEnd(), BPartNo = BsbPart, PartName = TxtPromoName.Textt.TrimEnd(),
                        //                               Uom = uom, Brand = brand, OtherName = TxtOtherName.Textt.TrimEnd(),
                        //                               Sku = TxtStockKeeping.Textt.TrimEnd(), IUpack = NumericInnerUnit.Value, MUpack = NumericMotherUnit.Value, PPosition = TxtPosition.Textt.TrimEnd(),
                        //                               PSize = TxtSize.Textt.TrimEnd(), Ptype = TxtType.Textt.TrimEnd(), IsActive = CheckActive.Checked,
                        //                               BListPrice = NumericBsbListPrice.Value, PartApplication = TxtDescription.Textt.TrimEnd(), Description = desc,
                        //                               AlternateList = AlternatePartsList, Image = image };
                        //CustomMsg = _PartsController.Update(_PartsModel);
                        //Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //_PartsModel = new PartsModel { PartNo = TxtPromoID.Textt.TrimEnd(), BPartNo = BsbPart, PartName = TxtPromoName.Textt.TrimEnd(),
                        //                               Uom = uom, Brand = brand,
                        //                               OtherName = TxtOtherName.Textt.TrimEnd(), Sku = TxtStockKeeping.Textt.TrimEnd(), IUpack = NumericInnerUnit.Value,
                        //                               MUpack = NumericMotherUnit.Value, PPosition = TxtPosition.Textt.TrimEnd(), PSize = TxtSize.Textt.TrimEnd(), Ptype = TxtType.Textt.TrimEnd(),
                        //                               IsActive = CheckActive.Checked, ListPrice = NumericListPrice.Value, BListPrice = NumericBsbListPrice.Value,
                        //                               PartApplication = TxtDescription.Textt.TrimEnd(), Description = desc,
                        //                               AlternateList = AlternatePartsList, Image = Convert.ToBase64String(PartImage) };
                        //CustomMsg = _PartsController.Create(_PartsModel);
                        //Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                TxtPromoID.Textt = TxtPromoName.Textt = TxtDescription.Textt = "";
                ComboPromo.Text = ComboFilter.Text = "";
                DataGridPart.Rows.Clear();
            }
        }

        private void frm_parts_encodeNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose.PerformClick();
            }
        }

        private void ComboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                ComboPromoParts.Text = "";
                FilterPartTable.Rows.Clear();
                _MasterfileController.ChangeDistinctFilter(comboBox.SelectedItem.ToString());
            }
        }

        private void CustomTextBox_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            FilterPartTable = new DataTable();
            var boolColumn = new DataColumn("ForSelection", typeof(bool));
            switch (ComboFilter.SelectedItem.ToString())
            {
                case "ALL":
                    if (e.SelectedItem != "")
                    {
                        _PromoPartDetail = _PromoController.GetSuggestedPart(e.SelectedItem);
                        DataGridViewRow row = DataGridPart.Rows
                                              .Cast<DataGridViewRow>()
                                              .FirstOrDefault(x => x.Cells["PartNo"].Value.ToString() == _PromoPartDetail.PartNo);
                        if (row == null)
                        {
                            DataGridPart.Rows.Add(false, (DataGridPart.Rows.Count + 1).ToString(), _PromoPartDetail.PartNo, _PromoPartDetail.PartName,
                                                    _PromoPartDetail.DescName, _PromoPartDetail.BrandName, _PromoPartDetail.Sku, _PromoPartDetail.UomName,
                                                    false, Convert.ToDecimal(1), _PromoPartDetail.ListPrice, Convert.ToDouble(0),
                                                    Convert.ToDecimal(_PromoPartDetail.ListPrice));
                            ComboPromoParts.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("The selected part is already included in the parts list.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        FilterPartTable = _PromoController.PartsWithBegBalDataTable(ComboPromoParts.Text.TrimEnd());
                        boolColumn.DefaultValue = false;
                        FilterPartTable.Columns.Add(boolColumn);
                        DataGridPartFilter.DataSource = FilterPartTable;
                        DataGridPartFilter.ClearSelection();
                    }
                    break;

                case "BRAND":
                    FilterPartTable = _PromoController.PartsWithBegBalDataTableFilterBrand(ComboPromoParts.Text.TrimEnd());
                    boolColumn.DefaultValue = false;
                    FilterPartTable.Columns.Add(boolColumn);
                    DataGridPartFilter.DataSource = FilterPartTable;
                    DataGridPartFilter.ClearSelection();
                    break;

                case "DESCRIPTION":
                    FilterPartTable = _PromoController.PartsWithBegBalDataTableFilterDesc(ComboPromoParts.Text.TrimEnd());
                    boolColumn.DefaultValue = false;
                    FilterPartTable.Columns.Add(boolColumn);
                    DataGridPartFilter.DataSource = FilterPartTable;
                    DataGridPartFilter.ClearSelection();
                    break;
            }
        }

        private void DataGridPartFilter_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DataGridPartFilter.IsCurrentCellDirty)
            {
                DataGridPartFilter.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridPartFilter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DataGridPartFilter.Columns["ForSelection"].Index)
            {
                bool isChecked = (bool)DataGridPartFilter.Rows[e.RowIndex].Cells["ForSelection"].Value;
                if (isChecked)
                {
                    DataGridViewRow row = DataGridPart.Rows
                                      .Cast<DataGridViewRow>()
                                      .FirstOrDefault(x => x.Cells["PartNo"].Value.ToString() == DataGridPartFilter.CurrentRow.Cells["PartNoFilter"].Value.ToString());
                    if (row == null)
                    {
                        DataGridViewRow selectedRow = DataGridPartFilter.CurrentRow;
                        DataGridPart.Rows.Add(false, (DataGridPart.Rows.Count + 1).ToString(), selectedRow.Cells["PartNoFilter"].Value.ToString(),
                                                selectedRow.Cells["PartNameFilter"].Value.ToString(), selectedRow.Cells["DescNameFilter"].Value.ToString(),
                                                selectedRow.Cells["BrandNameFilter"].Value.ToString(), selectedRow.Cells["SkuFilter"].Value.ToString(),
                                                selectedRow.Cells["UomNameFilter"].Value.ToString(), false, Convert.ToDecimal(1),
                                                Convert.ToDouble(selectedRow.Cells["ListPriceFilter"].Value), Convert.ToDouble(0), Convert.ToDecimal(selectedRow.Cells["ListPriceFilter"].Value));
                    }
                    else
                    {
                        MessageBox.Show("The selected part is already included in the parts list.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
