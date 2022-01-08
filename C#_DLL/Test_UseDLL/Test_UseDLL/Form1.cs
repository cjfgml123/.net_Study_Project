using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_CreateDLL;       //dll 사용

namespace Test_UseDLL
{
    public partial class Form1 : Form
    {
        dllTest var_dll = new dllTest();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(var_dll.fn_Add(5,10).ToString());
        }
    }
}
