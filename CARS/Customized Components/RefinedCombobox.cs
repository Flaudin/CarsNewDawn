using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace CARS.Customized_Components
{
    public partial class RefinedCombobox : UserControl
    {
        private Color borderColor = Color.Silver;
        private int borderSize = 1;
        private bool underlinedStyle = false;
        private int borderRadius = 8;
        private Color borderFocusColor = Color.MidnightBlue;
        private bool isFocused = false;

        private Color placeholderColor = Color.Gray;
        public RefinedCombobox()
        {
            InitializeComponent();
        }

        [Category("Flaudin Combobox Controls")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Category("Flaudin Combobox Controls")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        [Category("Flaudin Combobox Controls")]
        public bool UnderlinedStyle
        {
            get
            {
                return underlinedStyle;
            }
            set
            {
                underlinedStyle = value;
                this.Invalidate();
            }
        }

        [Category("Flaudin Combobox Controls")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                cmbBox.BackColor = value;
            }
        }

        [Category("Flaudin Combobox Controls")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                cmbBox.ForeColor = value;
            }
        }

        [Category("Flaudin Combobox Controls")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                cmbBox.Font = value;
                //if (this.DesignMode)
                //    UpdateControlHeight();
            }
        }

        [Category("Flaudin Combobox Controls")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                if (value >= 0)
                {
                    borderRadius = value;
                    this.Invalidate();
                }
            }
        }

        [Category("Flaudin Combobox Controls")]
        public Color BorderFocusColor
        {
            get
            {
                return borderFocusColor;
            }
            set
            {
                borderFocusColor = value;
            }
        }

        [Category("Flaudin ComboBox Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [MergableProperty(false)]
        public ComboBox.ObjectCollection Items
        {
            get
            {
                return cmbBox.Items;
            }
        }
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        public object DataSource
        {
            get
            {
                return cmbBox.DataSource;
            }
            set
            {
                cmbBox.DataSource = value;
            }
        }

        [Category("Flaudin ComboBox Data")]
        [DefaultValue(AutoCompleteMode.None)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteMode AutoCompleteMode
        {
            get
            {
                return cmbBox.AutoCompleteMode;
            }
            set
            {
                cmbBox.AutoCompleteMode = value;
            }
        }

        [Category("Flaudin ComboBox Data")]
        [DefaultValue(AutoCompleteSource.None)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteSource AutoCompleteSource
        {
            get
            {
                return cmbBox.AutoCompleteSource;
            }
            set
            {
                cmbBox.AutoCompleteSource = value;
            }
        }

        [Category("Flaudin ComboBox Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get
            {


                return cmbBox.AutoCompleteCustomSource;
            }
            set
            {
                cmbBox.AutoCompleteCustomSource = value;
            }
        }

        [Category("Flaudin ComboBox Data")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public int SelectedIndex
        {
            get
            {
                return cmbBox.SelectedIndex;
            }
            set
            {
                cmbBox.SelectedIndex = value;
            }
        }

        [Category("Flaudin ComboBox Data")]
        [Browsable(false)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public object SelectedItem
        {
            get
            {
                return cmbBox.SelectedItem;
            }
            set
            {
                cmbBox.SelectedItem = value;
            }
        }
    }
}
