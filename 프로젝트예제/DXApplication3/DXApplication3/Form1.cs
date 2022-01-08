using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication3
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        Timer timer;
        Random random;
        Series[] seriesArray; 
        int index;

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            seriesArray = new Series[2];
            index = 0;
            Chart_Control();
        }

        private void Chart_Control()
        {
            this.seriesArray[0] = new Series("시리즈1",ViewType.Spline);
            this.seriesArray[1] = new Series("시리즈2", ViewType.Spline);

            // 그래프 추가
            this.chartControl1.Series.Add(this.seriesArray[0]);
            this.chartControl1.Series.Add(this.seriesArray[1]);
            
            this.chartControl1.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

            //XYDiagram 객체를 통해 다이어그램의 속성을 변경한다.
            XYDiagram diagram = (XYDiagram)this.chartControl1.Diagram;
            diagram.AxisY.WholeRange.Auto = false;      //y축 범위 자동변경 설정
            diagram.AxisY.WholeRange.MinValue = -100;   //y축 최소값
            diagram.AxisY.WholeRange.MaxValue = 200;    //y축 최대값
            diagram.AxisX.WholeRange.SideMarginsValue = 0;  //x축과 선 그래프와의 공백을 의미

            // ConstantLine은 X축 혹은 Y축에 선을 추가하는 것이다.
            // Y축 0의 위치에 X축과 평행하게 Red 컬러의 줄을 만든다.
            ConstantLine constantLine = new ConstantLine();
            constantLine.Color = Color.Red;
            constantLine.AxisValue = 0;
            constantLine.ShowInLegend = false;
            diagram.AxisY.ConstantLines.Add(constantLine);
            
            diagram.EnableAxisXScrolling = false;
            diagram.EnableAxisXZooming = false;

            this.timer = new Timer();
            this.timer.Interval = 300;

            Load += Form1_Load;
            this.timer.Tick += timer_Tick;
            //timer.Tick += new EventHandler(timer_Tick);
        }

        /// <summary>
        /// 폼 로드시 처리하기
        /// </summary>
        /// <param name="sender">이벤트 발생자</param>
        /// <param name="e">이벤트 인자</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // x축은 최고 10개 까지만 찍도록 하고, 10개가 넘어가면 첫번째 찍었던 값을 지운다. 그러면 
            // 그래프가 왼쪽으로 이동한다.
            if (this.seriesArray[0].Points.Count > 10)
            {
                this.seriesArray[0].Points.RemoveAt(0);
                this.seriesArray[1].Points.RemoveAt(0);
            }
            //값은 랜덤으로 범위 지정
            this.seriesArray[0].Points.Add(new SeriesPoint(this.index, this.random.Next(-100,200)));
            this.seriesArray[1].Points.Add(new SeriesPoint(this.index, this.random.Next(-50, 100)));
            
            this.index++;
        }
    }
}
