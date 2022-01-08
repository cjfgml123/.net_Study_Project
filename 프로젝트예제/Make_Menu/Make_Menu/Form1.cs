using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Make_Menu
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            customizeDesing();
        }

        // 초기에 서브 메뉴를 갖는 패널을 숨기기 위해 생성자에 선언
        private void customizeDesing()
        {
            panel_Measure.Visible = false;
            panel_Analysis.Visible = false;
            panel_control.Visible = false;
        }

        //
        private void hideSubMenu()
        {
            if (panel_Measure.Visible == true)
                panel_Measure.Visible = false;
            if (panel_Analysis.Visible == true)
                panel_Analysis.Visible = false;
            if (panel_control.Visible == true)
                panel_control.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            //Button _button = (Button)sender;
            //if(_button.Name = )
        }

        private void btn_measure_Click(object sender, EventArgs e)
        {
            showSubMenu(panel_Measure);
            //if((Button)sender.)
        }

        private void btn_go_Click(object sender, EventArgs e)
        {
            openChildForm(new ucTestForm());
            hideSubMenu();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            openChildForm(new Form2());
            hideSubMenu();
        }

        private void btn_record_Click(object sender, EventArgs e)
        {
            
            //hideSubMenu();
        }

        private void btn_Analysis_Click(object sender, EventArgs e)
        {
            showSubMenu(panel_Analysis);
        }

        private Form activeForm = null;
        private void openChildForm(Form _childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = _childForm;
            _childForm.TopLevel = false;
            _childForm.FormBorderStyle = FormBorderStyle.None;
            _childForm.Dock = DockStyle.Fill;
            main_panel.Controls.Add(_childForm);
            main_panel.Tag = _childForm;
            _childForm.Show();
        }


    }
}
