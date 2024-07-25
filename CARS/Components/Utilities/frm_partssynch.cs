using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CARS.Model.Masterfiles;
using CARS.Controller.Masterfiles;

namespace CARS.Components.Transactions
{
    public partial class frm_partssynch : Form
    {

        private BrandController carsbrandcontroller = new BrandController();
        private DescriptionController carsdescontroller = new DescriptionController();
        public frm_partssynch()
        {
            InitializeComponent();
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            GetAllBSBAppParts();

        }

        private async void GetAllBSBAppParts()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("http://172.16.15.10:96/api/cars/getpartslibrary"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        //var productJsonString = await response.Content.ReadAsStringAsync();
                        var bsbpartsJsonString = await response.Content.ReadAsStringAsync();

                        List<AppPartsModel> bsbappparts = JsonConvert.DeserializeObject<List<AppPartsModel>>(bsbpartsJsonString).ToList();
                        List<AppAlternateModel> bsbappaltparts = JsonConvert.DeserializeObject<List<AppAlternateModel>>(bsbpartsJsonString).ToList();
                        List<AppOemModel> bsbappoem = JsonConvert.DeserializeObject<List<AppOemModel>>(bsbpartsJsonString).ToList();
                        //dataGridView1.DataSource = JsonConvert.DeserializeObject<Product[]>(productJsonString).ToList();

                        var bsbappdesclst = from x in bsbappparts
                                            where x.DescName.Length != 0 
                                            select new { DescName = x.DescName };
                        
                        bsbappdesclst = bsbappdesclst.Distinct().ToList();

                        bsbappdesclst.ToList().ForEach(x => Console.WriteLine(x.DescName));

                        DescriptionModel carsdescmodel = new DescriptionModel();
                        carsdescontroller.dt(carsdescmodel);

                        var bsbappuomlst = from x in bsbappparts
                                            where x.UomName.Length != 0
                                            select new { UomName = x.UomName};

                        bsbappuomlst = bsbappuomlst.Distinct().ToList();

                        bsbappuomlst.ToList().ForEach(x => Console.WriteLine(x.UomName));

                        var bsbappbrandlst = from x in bsbappparts
                                           where x.BrandName.Length != 0
                                           select new { BrandName = x.BrandName, BrandCode = x.BrandCode  };

                        bsbappbrandlst = bsbappbrandlst.Distinct().ToList();

                        bsbappbrandlst.ToList().ForEach(x => Console.WriteLine(x.BrandName, x.BrandCode));




                        foreach (var part in bsbappparts)
                        {
                            Console.WriteLine($"Part No: {part.PartNo}");
                        }

                    }
                }
            }
        }
    }
}
