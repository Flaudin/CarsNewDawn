using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class ReceiptGenerationModel
    {
        public string SONo { get; set; } = string.Empty;
        public string SODate { get; set; } = string.Empty;
        public string CreditTerm { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string DateFrom { get; set; } = string.Empty;
        public string DateTo { get; set; } = string.Empty;
    }
}
