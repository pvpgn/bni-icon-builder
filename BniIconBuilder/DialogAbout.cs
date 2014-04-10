using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BniIconBuilder
{
    public partial class DialogAbout : Form
    {
        public DialogAbout()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            // assembly version   
            string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            txtInfo.Text = string.Format("{0} Version: {1}\r\n\r\n{2}", Helper.ProgramName, assemblyVersion, txtInfo.Text);

            this.Text = "About " + Helper.ProgramName;

            // remove selection from the textbox
            txtInfo.SelectionStart = txtInfo.Text.Length;
        }

    }
}
