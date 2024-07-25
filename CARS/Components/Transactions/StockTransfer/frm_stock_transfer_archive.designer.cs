﻿namespace CARS.Components.Transactions.StockTransfer
{
    partial class frm_stock_transfer_archive
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PnlMain = new System.Windows.Forms.Panel();
            this.PnlHeader = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.BtnClose = new CARS.Customized_Components.CustomRoundedButton();
            this.LblHeader = new System.Windows.Forms.Label();
            this.PnlBody = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.DataGridParts = new System.Windows.Forms.DataGridView();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BinName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BinToName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhToName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlHeaderParts = new System.Windows.Forms.Panel();
            this.LblParts = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DataGridStockTransfer = new System.Windows.Forms.DataGridView();
            this.CtrlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReasonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlHeaderStockTrans = new System.Windows.Forms.Panel();
            this.LblStockTrans = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.DateTo = new CARS.Customized_Components.CustomDateTime();
            this.label7 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.DateFrom = new CARS.Customized_Components.CustomDateTime();
            this.label5 = new System.Windows.Forms.Label();
            this.PnlHeaderFilter = new System.Windows.Forms.Panel();
            this.LblFilter = new System.Windows.Forms.Label();
            this.TxtSTNo = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnSearch = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlMain.SuspendLayout();
            this.PnlHeader.SuspendLayout();
            this.PnlBody.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridParts)).BeginInit();
            this.PnlHeaderParts.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridStockTransfer)).BeginInit();
            this.PnlHeaderStockTrans.SuspendLayout();
            this.panel5.SuspendLayout();
            this.PnlFilter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel13.SuspendLayout();
            this.PnlHeaderFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlMain
            // 
            this.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlMain.Controls.Add(this.PnlHeader);
            this.PnlMain.Controls.Add(this.PnlBody);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(0, 0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.PnlMain.Size = new System.Drawing.Size(1400, 800);
            this.PnlMain.TabIndex = 9;
            // 
            // PnlHeader
            // 
            this.PnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlHeader.BackColor = System.Drawing.Color.Yellow;
            this.PnlHeader.Controls.Add(this.panel14);
            this.PnlHeader.Controls.Add(this.BtnClose);
            this.PnlHeader.Controls.Add(this.LblHeader);
            this.PnlHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlHeader.Location = new System.Drawing.Point(0, 0);
            this.PnlHeader.Name = "PnlHeader";
            this.PnlHeader.Size = new System.Drawing.Size(1400, 32);
            this.PnlHeader.TabIndex = 9;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(0, 30);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(1400, 2);
            this.panel14.TabIndex = 51;
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.BorderColor = System.Drawing.Color.White;
            this.BtnClose.BorderRadius = 0;
            this.BtnClose.BorderSize = 0;
            this.BtnClose.FlatAppearance.BorderSize = 0;
            this.BtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnClose.Location = new System.Drawing.Point(1364, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.BtnClose.Size = new System.Drawing.Size(33, 31);
            this.BtnClose.TabIndex = 39;
            this.BtnClose.TabStop = false;
            this.BtnClose.Text = "X";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // LblHeader
            // 
            this.LblHeader.AccessibleName = "";
            this.LblHeader.AutoSize = true;
            this.LblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblHeader.Location = new System.Drawing.Point(3, 3);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(250, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "STOCK TRANSFER ARCHIVE";
            // 
            // PnlBody
            // 
            this.PnlBody.BackColor = System.Drawing.SystemColors.Control;
            this.PnlBody.Controls.Add(this.panel2);
            this.PnlBody.Controls.Add(this.panel5);
            this.PnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlBody.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlBody.Location = new System.Drawing.Point(0, 30);
            this.PnlBody.Name = "PnlBody";
            this.PnlBody.Size = new System.Drawing.Size(1398, 768);
            this.PnlBody.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(301, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(1097, 768);
            this.panel2.TabIndex = 25;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.DataGridParts);
            this.panel8.Controls.Add(this.PnlHeaderParts);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(10, 258);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1077, 497);
            this.panel8.TabIndex = 2;
            // 
            // DataGridParts
            // 
            this.DataGridParts.AllowUserToAddRows = false;
            this.DataGridParts.AllowUserToDeleteRows = false;
            this.DataGridParts.AllowUserToResizeColumns = false;
            this.DataGridParts.AllowUserToResizeRows = false;
            this.DataGridParts.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridParts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridParts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridParts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridParts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartNo,
            this.PartName,
            this.OtherName,
            this.DescName,
            this.BrandName,
            this.BinName,
            this.WhName,
            this.LotNo,
            this.BinToName,
            this.WhToName,
            this.Qty});
            this.DataGridParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridParts.Location = new System.Drawing.Point(0, 32);
            this.DataGridParts.MultiSelect = false;
            this.DataGridParts.Name = "DataGridParts";
            this.DataGridParts.ReadOnly = true;
            this.DataGridParts.RowHeadersVisible = false;
            this.DataGridParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridParts.Size = new System.Drawing.Size(1075, 463);
            this.DataGridParts.TabIndex = 9;
            this.DataGridParts.TabStop = false;
            this.DataGridParts.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridParts_ColumnHeaderMouseClick);
            // 
            // PartNo
            // 
            this.PartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartNo.DataPropertyName = "PartNo";
            this.PartNo.FillWeight = 50F;
            this.PartNo.HeaderText = "Part No.";
            this.PartNo.Name = "PartNo";
            this.PartNo.ReadOnly = true;
            // 
            // PartName
            // 
            this.PartName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartName.DataPropertyName = "PartName";
            this.PartName.FillWeight = 60F;
            this.PartName.HeaderText = "Part Name";
            this.PartName.Name = "PartName";
            this.PartName.ReadOnly = true;
            // 
            // OtherName
            // 
            this.OtherName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OtherName.DataPropertyName = "OtherName";
            this.OtherName.FillWeight = 60F;
            this.OtherName.HeaderText = "Other Name";
            this.OtherName.Name = "OtherName";
            this.OtherName.ReadOnly = true;
            // 
            // DescName
            // 
            this.DescName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DescName.DataPropertyName = "DescName";
            this.DescName.FillWeight = 180F;
            this.DescName.HeaderText = "Description";
            this.DescName.Name = "DescName";
            this.DescName.ReadOnly = true;
            this.DescName.Visible = false;
            // 
            // BrandName
            // 
            this.BrandName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BrandName.DataPropertyName = "BrandName";
            this.BrandName.FillWeight = 50F;
            this.BrandName.HeaderText = "Brand";
            this.BrandName.Name = "BrandName";
            this.BrandName.ReadOnly = true;
            // 
            // BinName
            // 
            this.BinName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BinName.DataPropertyName = "BinName";
            this.BinName.FillWeight = 60F;
            this.BinName.HeaderText = "Location";
            this.BinName.Name = "BinName";
            this.BinName.ReadOnly = true;
            // 
            // WhName
            // 
            this.WhName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WhName.DataPropertyName = "WhName";
            this.WhName.FillWeight = 60F;
            this.WhName.HeaderText = "Warehouse";
            this.WhName.Name = "WhName";
            this.WhName.ReadOnly = true;
            // 
            // LotNo
            // 
            this.LotNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LotNo.DataPropertyName = "LotNo";
            this.LotNo.FillWeight = 70F;
            this.LotNo.HeaderText = "Lot Number";
            this.LotNo.Name = "LotNo";
            this.LotNo.ReadOnly = true;
            // 
            // BinToName
            // 
            this.BinToName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BinToName.DataPropertyName = "BinToName";
            this.BinToName.FillWeight = 60F;
            this.BinToName.HeaderText = "Location To";
            this.BinToName.Name = "BinToName";
            this.BinToName.ReadOnly = true;
            // 
            // WhToName
            // 
            this.WhToName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WhToName.DataPropertyName = "WhToName";
            this.WhToName.FillWeight = 60F;
            this.WhToName.HeaderText = "Warehouse To";
            this.WhToName.Name = "WhToName";
            this.WhToName.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0.00";
            this.Qty.DefaultCellStyle = dataGridViewCellStyle2;
            this.Qty.FillWeight = 50F;
            this.Qty.HeaderText = "Quantity";
            this.Qty.MaxInputLength = 20;
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // PnlHeaderParts
            // 
            this.PnlHeaderParts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderParts.Controls.Add(this.LblParts);
            this.PnlHeaderParts.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeaderParts.Location = new System.Drawing.Point(0, 0);
            this.PnlHeaderParts.Name = "PnlHeaderParts";
            this.PnlHeaderParts.Size = new System.Drawing.Size(1075, 32);
            this.PnlHeaderParts.TabIndex = 7;
            // 
            // LblParts
            // 
            this.LblParts.AutoSize = true;
            this.LblParts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblParts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblParts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblParts.Location = new System.Drawing.Point(5, 7);
            this.LblParts.Name = "LblParts";
            this.LblParts.Size = new System.Drawing.Size(69, 19);
            this.LblParts.TabIndex = 0;
            this.LblParts.Text = "Parts List";
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(10, 248);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1077, 10);
            this.panel7.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.DataGridStockTransfer);
            this.panel3.Controls.Add(this.PnlHeaderStockTrans);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1077, 238);
            this.panel3.TabIndex = 0;
            // 
            // DataGridStockTransfer
            // 
            this.DataGridStockTransfer.AllowUserToAddRows = false;
            this.DataGridStockTransfer.AllowUserToDeleteRows = false;
            this.DataGridStockTransfer.AllowUserToResizeColumns = false;
            this.DataGridStockTransfer.AllowUserToResizeRows = false;
            this.DataGridStockTransfer.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridStockTransfer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridStockTransfer.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridStockTransfer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridStockTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridStockTransfer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CtrlNo,
            this.TransferName,
            this.ReasonName,
            this.CreatedDt});
            this.DataGridStockTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridStockTransfer.Location = new System.Drawing.Point(0, 32);
            this.DataGridStockTransfer.MultiSelect = false;
            this.DataGridStockTransfer.Name = "DataGridStockTransfer";
            this.DataGridStockTransfer.ReadOnly = true;
            this.DataGridStockTransfer.RowHeadersVisible = false;
            this.DataGridStockTransfer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridStockTransfer.Size = new System.Drawing.Size(1075, 204);
            this.DataGridStockTransfer.TabIndex = 6;
            this.DataGridStockTransfer.TabStop = false;
            this.DataGridStockTransfer.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridStockTransfer_ColumnHeaderMouseClick);
            this.DataGridStockTransfer.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridStockTransfer_ColumnHeaderMouseClick);
            this.DataGridStockTransfer.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridStockTransfer_RowEnter);
            // 
            // CtrlNo
            // 
            this.CtrlNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CtrlNo.DataPropertyName = "CtrlNo";
            this.CtrlNo.FillWeight = 60F;
            this.CtrlNo.HeaderText = "ST No.";
            this.CtrlNo.Name = "CtrlNo";
            this.CtrlNo.ReadOnly = true;
            // 
            // TransferName
            // 
            this.TransferName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TransferName.DataPropertyName = "TransferName";
            this.TransferName.FillWeight = 140F;
            this.TransferName.HeaderText = "Transfer Type";
            this.TransferName.Name = "TransferName";
            this.TransferName.ReadOnly = true;
            // 
            // ReasonName
            // 
            this.ReasonName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ReasonName.DataPropertyName = "ReasonName";
            this.ReasonName.FillWeight = 140F;
            this.ReasonName.HeaderText = "Reason";
            this.ReasonName.Name = "ReasonName";
            this.ReasonName.ReadOnly = true;
            // 
            // CreatedDt
            // 
            this.CreatedDt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CreatedDt.DataPropertyName = "CreatedDt";
            this.CreatedDt.FillWeight = 50F;
            this.CreatedDt.HeaderText = "ST Date";
            this.CreatedDt.Name = "CreatedDt";
            this.CreatedDt.ReadOnly = true;
            // 
            // PnlHeaderStockTrans
            // 
            this.PnlHeaderStockTrans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderStockTrans.Controls.Add(this.LblStockTrans);
            this.PnlHeaderStockTrans.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeaderStockTrans.Location = new System.Drawing.Point(0, 0);
            this.PnlHeaderStockTrans.Name = "PnlHeaderStockTrans";
            this.PnlHeaderStockTrans.Size = new System.Drawing.Size(1075, 32);
            this.PnlHeaderStockTrans.TabIndex = 7;
            // 
            // LblStockTrans
            // 
            this.LblStockTrans.AutoSize = true;
            this.LblStockTrans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblStockTrans.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStockTrans.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblStockTrans.Location = new System.Drawing.Point(5, 7);
            this.LblStockTrans.Name = "LblStockTrans";
            this.LblStockTrans.Size = new System.Drawing.Size(130, 19);
            this.LblStockTrans.TabIndex = 0;
            this.LblStockTrans.Text = "Stock Transfer List";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.PnlFilter);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.panel5.Size = new System.Drawing.Size(301, 768);
            this.panel5.TabIndex = 10;
            // 
            // PnlFilter
            // 
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.tableLayoutPanel1);
            this.PnlFilter.Controls.Add(this.PnlHeaderFilter);
            this.PnlFilter.Controls.Add(this.TxtSTNo);
            this.PnlFilter.Controls.Add(this.label1);
            this.PnlFilter.Controls.Add(this.BtnClear);
            this.PnlFilter.Controls.Add(this.BtnSearch);
            this.PnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlFilter.Location = new System.Drawing.Point(10, 10);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlFilter.Size = new System.Drawing.Size(291, 197);
            this.PnlFilter.TabIndex = 45;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.panel16, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel15, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel13, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(279, 61);
            this.tableLayoutPanel1.TabIndex = 52;
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.DateTo);
            this.panel16.Controls.Add(this.label7);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(155, 3);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(121, 55);
            this.panel16.TabIndex = 24;
            // 
            // DateTo
            // 
            this.DateTo.BoderSize = 0;
            this.DateTo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.DateTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.DateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTo.Location = new System.Drawing.Point(0, 20);
            this.DateTo.MinimumSize = new System.Drawing.Size(4, 32);
            this.DateTo.Name = "DateTo";
            this.DateTo.Size = new System.Drawing.Size(121, 32);
            this.DateTo.SkinColor = System.Drawing.Color.White;
            this.DateTo.TabIndex = 23;
            this.DateTo.TextColor = System.Drawing.Color.Black;
            this.DateTo.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label7.Size = new System.Drawing.Size(97, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "                              ";
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.label8);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(128, 3);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(21, 55);
            this.panel15.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "To";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.DateFrom);
            this.panel13.Controls.Add(this.label5);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 3);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(119, 55);
            this.panel13.TabIndex = 21;
            // 
            // DateFrom
            // 
            this.DateFrom.BoderSize = 0;
            this.DateFrom.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.DateFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateFrom.Location = new System.Drawing.Point(0, 20);
            this.DateFrom.MinimumSize = new System.Drawing.Size(4, 32);
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Size = new System.Drawing.Size(119, 32);
            this.DateFrom.SkinColor = System.Drawing.Color.White;
            this.DateFrom.TabIndex = 23;
            this.DateFrom.TextColor = System.Drawing.Color.Black;
            this.DateFrom.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(67, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "Date Range";
            // 
            // PnlHeaderFilter
            // 
            this.PnlHeaderFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlHeaderFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderFilter.Controls.Add(this.LblFilter);
            this.PnlHeaderFilter.Location = new System.Drawing.Point(-1, -1);
            this.PnlHeaderFilter.Name = "PnlHeaderFilter";
            this.PnlHeaderFilter.Size = new System.Drawing.Size(291, 32);
            this.PnlHeaderFilter.TabIndex = 51;
            // 
            // LblFilter
            // 
            this.LblFilter.AutoSize = true;
            this.LblFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblFilter.Location = new System.Drawing.Point(5, 6);
            this.LblFilter.Name = "LblFilter";
            this.LblFilter.Size = new System.Drawing.Size(43, 19);
            this.LblFilter.TabIndex = 0;
            this.LblFilter.Text = "Filter";
            // 
            // TxtSTNo
            // 
            this.TxtSTNo.BackColor = System.Drawing.SystemColors.Window;
            this.TxtSTNo.BorderColor = System.Drawing.Color.Silver;
            this.TxtSTNo.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtSTNo.BorderRadius = 8;
            this.TxtSTNo.BorderSize = 1;
            this.TxtSTNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtSTNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtSTNo.Location = new System.Drawing.Point(5, 55);
            this.TxtSTNo.MaxLegnth = 25;
            this.TxtSTNo.MultiLine = false;
            this.TxtSTNo.Name = "TxtSTNo";
            this.TxtSTNo.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtSTNo.PasswordChar = false;
            this.TxtSTNo.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtSTNo.ReadOnly = false;
            this.TxtSTNo.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtSTNo.Size = new System.Drawing.Size(279, 32);
            this.TxtSTNo.TabIndex = 50;
            this.TxtSTNo.Textt = "";
            this.TxtSTNo.UnderlinedStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 35);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 49;
            this.label1.Text = "Stock Transfer No.";
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClear.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnClear.BorderColor = System.Drawing.Color.White;
            this.BtnClear.BorderRadius = 8;
            this.BtnClear.BorderSize = 0;
            this.BtnClear.FlatAppearance.BorderSize = 0;
            this.BtnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.ForeColor = System.Drawing.Color.White;
            this.BtnClear.Image = global::CARS.Properties.Resources.Delete;
            this.BtnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnClear.Location = new System.Drawing.Point(189, 160);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(95, 30);
            this.BtnClear.TabIndex = 32;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "&Clear";
            this.BtnClear.UseVisualStyleBackColor = false;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSearch.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnSearch.BorderColor = System.Drawing.Color.White;
            this.BtnSearch.BorderRadius = 8;
            this.BtnSearch.BorderSize = 0;
            this.BtnSearch.FlatAppearance.BorderSize = 0;
            this.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.ForeColor = System.Drawing.Color.White;
            this.BtnSearch.Image = global::CARS.Properties.Resources.Search;
            this.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSearch.Location = new System.Drawing.Point(88, 160);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(95, 30);
            this.BtnSearch.TabIndex = 33;
            this.BtnSearch.TabStop = false;
            this.BtnSearch.Text = "S&earch";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // frm_stock_transfer_archive
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.PnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frm_stock_transfer_archive";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Oem Parts Selection";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_stock_transfer_archive_KeyDown);
            this.PnlMain.ResumeLayout(false);
            this.PnlHeader.ResumeLayout(false);
            this.PnlHeader.PerformLayout();
            this.PnlBody.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridParts)).EndInit();
            this.PnlHeaderParts.ResumeLayout(false);
            this.PnlHeaderParts.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridStockTransfer)).EndInit();
            this.PnlHeaderStockTrans.ResumeLayout(false);
            this.PnlHeaderStockTrans.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.PnlHeaderFilter.ResumeLayout(false);
            this.PnlHeaderFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel PnlHeader;
        private Customized_Components.CustomRoundedButton BtnClose;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel PnlBody;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel PnlFilter;
        private System.Windows.Forms.Panel PnlHeaderFilter;
        private System.Windows.Forms.Label LblFilter;
        private Customized_Components.RifinedCustomTextbox TxtSTNo;
        private System.Windows.Forms.Label label1;
        private Customized_Components.CustomRoundedButton BtnClear;
        private Customized_Components.CustomRoundedButton BtnSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel PnlHeaderParts;
        private System.Windows.Forms.Label LblParts;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView DataGridStockTransfer;
        private System.Windows.Forms.Panel PnlHeaderStockTrans;
        private System.Windows.Forms.Label LblStockTrans;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label5;
        private Customized_Components.CustomDateTime DateFrom;
        private System.Windows.Forms.Panel panel16;
        private Customized_Components.CustomDateTime DateTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView DataGridParts;
        private System.Windows.Forms.DataGridViewTextBoxColumn CtrlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReasonName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BinName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WhName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BinToName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WhToName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
    }
}