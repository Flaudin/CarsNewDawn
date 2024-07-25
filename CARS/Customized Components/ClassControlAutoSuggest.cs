using CARS.Components.Transactions.SalesOrder;
using CARS.Controller.Inquiry;
using CARS.Controller.Masterfiles;
using CARS.Controller.Transactions;
using CARS.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CARS.Customized_Components
{
    public class ItemSelectedEventArgs : EventArgs
    {
        public string SelectedItem { get; }

        public ItemSelectedEventArgs(string selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }

    public class ClassControlAutoSuggest : TextBox
    {
        private ListBox ListBoxForAutoSuggest;
        private bool BoolIsAdded;
        private String[] StrrayValues;
        private String StrFormerValue = String.Empty;
        private bool BoolSelect;
        public static Dictionary<string, string> dictionary = new Dictionary<string, string>();
        TransactionController _TransactionController = new TransactionController();
        InquiryController _InquiryController = new InquiryController();
        MasterfileController _MasterfileController = new MasterfileController();
        public event EventHandler<ItemSelectedEventArgs> ItemSelected;

        public ClassControlAutoSuggest()
        {
            InitializeComponent();
            ResetListBox();
        }

        private void InitializeComponent()
        {
            ListBoxForAutoSuggest = new ListBox();
            this.ListBoxForAutoSuggest.Font = new System.Drawing.Font("Segoe", 9F, System.Drawing.FontStyle.Regular);
            KeyDown += AutoSuggest_KeyDown;
            KeyUp += AutoSuggest_KeyUp;
            Leave += AutoSuggest_Leave;
            TextChanged += AutoSuggest_TextChanged;
            Enter += AutoSuggest_Enter;
            MouseDown += AutoSuggest_MouseDown;
            ListBoxForAutoSuggest.MouseDown += ListBoxForAutoSuggest_MouseDown;
            ListBoxForAutoSuggest.MouseMove += ListBoxForAutoSuggest_MouseMove;
        }

        private void ShowListBox()
        {
            if (!BoolIsAdded)
            {
                //Parent.Controls.Add(ListBoxForAutoSuggest);
                //ListBoxForAutoSuggest.Left = this.Left;
                //ListBoxForAutoSuggest.Top = this.Top + this.Height;
                //
                Control cntrl = this.Parent;
                Control SuperCntrl = cntrl.Parent;
                Control MegaCntrl = SuperCntrl.Parent;
                MegaCntrl.Controls.Add(ListBoxForAutoSuggest);
                ListBoxForAutoSuggest.Left = cntrl.Left + this.Left + SuperCntrl.Left;
                ListBoxForAutoSuggest.Top = cntrl.Top + cntrl.Height + this.Height;
                ListBoxForAutoSuggest.BringToFront();
                BoolIsAdded = true;
            }
            ListBoxForAutoSuggest.Visible = true;
        }

        private void AutoSuggest_Leave(object sender, EventArgs e)
        {
            if (ListBoxForAutoSuggest.Visible == true && ListBoxForAutoSuggest.ContainsFocus != true)
            {
                ListBoxForAutoSuggest.Visible = false;
            }

            if (this.Text != "")
            {
                this.Select(0, 0);
            }
            BoolSelect = false;
        }

        private void AutoSuggest_Enter(object sender, EventArgs e)
        {
            this.SelectAll();
            BoolSelect = true;
        }

        private void AutoSuggest_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && BoolSelect)
            {
                BoolSelect = false;
                this.SelectAll();
            }
        }

        public void AutoSuggest_TextChanged(object sender, EventArgs e)
        {
            dictionary.Clear();

            if (this.Text != "")
            {
                switch (this.Name)
                {
                    case "ComboSalesParts":
                        dictionary = _TransactionController.GetDynamicDictionaryPartsWithBalance(this.Text);
                        break;

                    case "ComboInquiryParts":
                        dictionary = _InquiryController.GetDynamicDictionaryParts(this.Text);
                        break;

                    case "CombPromoParts":
                        dictionary = _MasterfileController.GetDynamicDictionaryParts(this.Text);
                        break;

                    case "Combo_Order_Taking_Parts" :
                        dictionary = _TransactionController.GetDynamicDictionaryPartsWithBalance(this.Text);
                        break;
                }
            }
            this.Values = dictionary.Keys.ToArray();

            if (ListBoxForAutoSuggest.Visible == true)
            {
                if (ListBoxForAutoSuggest.Items.Count != 0)
                {
                    ListBoxForAutoSuggest.SelectedIndex = 0;
                }
            }
        }

        private void ResetListBox()
        {
            ListBoxForAutoSuggest.Visible = false;
        }

        private void AutoSuggest_KeyUp(object sender, KeyEventArgs e)
        {
            if (StrrayValues != null)
            {
                UpdateListBox();
            }
        }

        private void UpdateListBox()
        {
            if (this.Text != StrFormerValue && this.ReadOnly != true)
            {
                StrFormerValue = this.Text;
                String word = GetWord();

                if (word.Length > 0)
                {
                    string[] normalizedValues = StrrayValues.Select(x => Regex.Replace(x, @"\W", "")).ToArray();
                    string normalizedWord = Regex.Replace(word, @"\W", "");
                    string[] matches = StrrayValues.Where((value, index) => normalizedValues[index].Contains(normalizedWord)).ToArray();
                    if (matches.Length > 0)
                    {
                        ShowListBox();
                        ListBoxForAutoSuggest.Items.Clear();
                        Array.ForEach(matches, x => ListBoxForAutoSuggest.Items.Add(x));
                        ListBoxForAutoSuggest.SelectedIndex = 0;
                        ListBoxForAutoSuggest.Height = 0;
                        ListBoxForAutoSuggest.Width = 0;
                        this.Focus();
                        this.SuspendLayout();

                        if (ListBoxForAutoSuggest.Items.Count < 5)
                        {
                            using (Graphics graphics = ListBoxForAutoSuggest.CreateGraphics())
                                for (int i = 0; i < ListBoxForAutoSuggest.Items.Count; i++)
                                {
                                    ListBoxForAutoSuggest.Height += ListBoxForAutoSuggest.GetItemHeight(i);
                                }
                        }
                        else
                        {
                            ListBoxForAutoSuggest.Height = 80;
                        }
                        ListBoxForAutoSuggest.Width = this.Width;
                        this.ResumeLayout();
                    }
                    else
                    {
                        ResetListBox();
                    }
                }
                else
                {
                    ResetListBox();
                }
            }
        }

        private void ListBoxForAutoSuggest_MouseMove(object sender, MouseEventArgs e)
        {
            if (ListBoxForAutoSuggest.Visible)
            {
                int index = ListBoxForAutoSuggest.IndexFromPoint(e.Location);
                ListBoxForAutoSuggest.SelectedIndex = index;
            }
        }

        private void ListBoxForAutoSuggest_MouseDown(object sender, MouseEventArgs e)
        {
            if (ListBoxForAutoSuggest.Visible)
            {
                this.Focus();
                InsertWord((String)ListBoxForAutoSuggest.SelectedItem);
                ResetListBox();
                StrFormerValue = this.Text;
                this.Focus();
            }
        }

        private void AutoSuggest_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.Tab:
                case Keys.Enter:
                    {
                        if (ListBoxForAutoSuggest.Visible)
                        {
                            e.SuppressKeyPress = true;
                            InsertWord((String)ListBoxForAutoSuggest.SelectedItem);
                            ResetListBox();
                            StrFormerValue = this.Text;
                            this.Focus();
                        }
                        else
                        {
                            OnItemSelected(new ItemSelectedEventArgs(""));
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if ((ListBoxForAutoSuggest.Visible) && (ListBoxForAutoSuggest.SelectedIndex < ListBoxForAutoSuggest.Items.Count - 1))
                        {
                            ListBoxForAutoSuggest.SelectedIndex++;
                        }
                        break;
                    }
                case Keys.Up:
                    {
                        e.SuppressKeyPress = true;
                        if ((ListBoxForAutoSuggest.Visible) && (ListBoxForAutoSuggest.SelectedIndex > 0))
                        {
                            ListBoxForAutoSuggest.SelectedIndex--;
                        }
                        break;
                    }
                case Keys.Back:
                    {
                        break;
                    }
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        private string GetWord()
        {
            String text = this.Text;
            return text;
        }

        private void InsertWord(String newTag)
        {
            this.Text = newTag;
            this.SelectionStart = newTag.Length;
            OnItemSelected(new ItemSelectedEventArgs(newTag.Split(',').First()));
        }

        protected virtual void OnItemSelected(ItemSelectedEventArgs e)
        {
            ItemSelected?.Invoke(this, e);
        }

        public String[] Values
        {
            get
            {
                return StrrayValues;
            }
            set
            {
                StrrayValues = value;
            }
        }
    }
}

