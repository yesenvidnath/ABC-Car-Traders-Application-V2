using ABC_Car_Traders.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Car_Traders.GUI
{
    public partial class ReportViwer : Form
    {
        public ReportViwer()
        {
            InitializeComponent();
        }

        private void btngeneraetreport_Click(object sender, EventArgs e)
        {
            /*
            Report report = new Report();
            DataSet dataSet = report.GetOrderAndOrderDetails();

            // Generate the HTML report
            string htmlContent = report.GenerateHtmlReport(dataSet);

            // Let the user choose where to save the report
            report.SaveHtmlReport(htmlContent);
            */
        }
    }
}
