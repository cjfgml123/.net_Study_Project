using DevExpress.Utils;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        int i = 0;
        Random r = new Random();
        // Series 객체는 그래프 그려지는 객체, 소스상에 배열로 선언하여 두개를 입력해서 두개의 라인이 표시된다.
        
        Series[] series = new Series[2];

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {   
            // 그래프 타입은 ViewType.Line 으로 라인 형태
            series[0] = new Series("time1", ViewType.Line);
            // 선 굵기는 LineSeriesView로 형변환하여 변경이 가능하다. tickness가 선 그래프의 굵기가 된다.
            ((LineSeriesView)series[0].View).LineStyle.Thickness = 1;

            series[1] = new Series("time2", ViewType.Line);

            // chartControl에 그래프 추가
            chartControl1.Series.Add(series[0]);
            chartControl1.Series.Add(series[1]);

            chartControl1.CrosshairEnabled = DefaultBoolean.False;

            // XYDiagram 객체를 통해 다이어그램의 속성을 변경한다.
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.AxisY.WholeRange.MaxValue = 200; //y 축 최대값
            diagram.AxisY.WholeRange.MinValue = -100; //y 축 최소값
            diagram.AxisY.WholeRange.Auto = false; // y축 범위 자동변경 설정

            // TextPattern은 날짜형식이다. A는 X축에 표시되는 데이터를 의미
            
            diagram.AxisX.Label.TextPattern = "{A:dd-MM HH:mm}";
            diagram.AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Manual;
            diagram.AxisX.DateTimeScaleOptions.GridSpacing = 1;
            diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Minute;
            diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Hour;

            diagram.AxisX.WholeRange.SideMarginsValue = 0;  // X축과 선그래프와의 공백을 의미

            // ConstantLine은 X축 혹은 Y축에 선을 추가하는 것이다.
            // Y축 0의 위치에 X축과 평행하게 Red 컬러의 줄을 만든다.
            ConstantLine zeroLine = new ConstantLine();
            zeroLine.Color = Color.Red;
            zeroLine.AxisValue = 0;
            zeroLine.ShowBehind = false;
            diagram.AxisY.ConstantLines.Add(zeroLine); // y값 0인 x축 생성

            //diagram.EnableAxisXScrolling = true;  스크롤과 줌은 절대 true로 하지 않도록 한다.
            //diagram.EnableAxisXZooming = true;

            timer_One.Interval = 400; //0.4초
            timer_One.Tick += new EventHandler(timer_Tick);
            timer_One.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {   
            // x축은 최고 10개 까지만 찍도록 하고, 10개가 넘어가면 첫번째 찍었던 값을 지운다. 그러면 
            // 그래프가 왼쪽으로 이동한다.
            if(series[0].Points.Count > 10) // x축은 10개 까지만 값을 출력하게 한다.
            {
                series[0].Points.RemoveAt(0);
                series[1].Points.RemoveAt(0);

            }
            //값은 랜덤으로 범위 지정
            series[0].Points.Add(new SeriesPoint(i, r.Next(-100,200)));
            series[1].Points.Add(new SeriesPoint(i++, r.Next(-50, 100)));

        }



    }
}
