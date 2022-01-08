using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication2
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SetLineChartData(ChartControl _chart, ViewType _viewType, DataTable _df)
        {
            Dictionary<string, Series> seriesList = new Dictionary<string, Series>();

            foreach(DataRow row in _df.Rows)
            {
                string product = row["PRODUCT"].ToString();
                string _year = row["YEAR"].ToString();
                int _qty = (int)row["QTY"];

                Series _series;
                if(seriesList.TryGetValue(product, out _series) == false)
                {
                    seriesList.Add(product, _series = new Series(product, _viewType));

                    // Label 표시
                    _series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    chartControl1.Series.Add(_series);

                    // SeriesPoint 생성
                    SeriesPoint _point = new SeriesPoint(_year, _qty);
                    _series.Points.Add(_point);

                    // ChartTitle 생성
                    ChartTitle _title = new ChartTitle();
                    _title.Text = _viewType.ToString();
                    chartControl1.Titles.Add(_title);

                }
            }
        }
       
    }
}
