using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WinForms_NumUpDown
{
    public class CustomNumericArrowless : NumericUpDown
    {
        public CustomNumericArrowless()
        {
            Controls[0].Visible = false; // The spin button is the first child control
        }
    }
}