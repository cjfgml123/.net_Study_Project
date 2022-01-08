## DevExpress



#### 1. chart Control 사용하기

```c#
Series[] seriesArray = new Series[2];
//seriesArray[0] = new Series(string name,ViewType viewType);
this.seriesArray[0] = new Series("시리즈2", ViewType.Line);
this.seriesArray[1] = new Series("시리즈2", ViewType.Line);

 // 그래프 추가
this.chartControl1.Series.Add(this.seriesArray[0]);
this.chartControl1.Series.Add(this.seriesArray[1]);
```

![image-20210426085925729](C:\Users\CHLee\Desktop\DevExpress_test\DevExpress_image\image-20210426085925729.png)

```C#
// 밑의 코드를 주석 처리 하면 빨간 표시가 나옴. 기본값 true 인듯
//this.chartControl1.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

```

```C#
//XYDiagram 객체를 통해 다이어그램의 속성을 변경한다.
XYDiagram diagram = (XYDiagram)this.chartControl1.Diagram;
diagram.AxisY.WholeRange.Auto = false;      //y축 범위 자동변경 설정
diagram.AxisY.WholeRange.MinValue = -100;   //y축 최소값
diagram.AxisY.WholeRange.MaxValue = 200;    //y축 최대값
diagram.AxisX.WholeRange.SideMarginsValue = 0;  //x축과 선 그래프와의 공백을 의미
```

```C#
// ConstantLine은 X축 혹은 Y축에 선을 추가하는 것이다.
// Y축 0의 위치에 X축과 평행하게 Red 컬러의 줄을 만든다.
ConstantLine constantLine = new ConstantLine();
constantLine.Color = Color.Red;
constantLine.AxisValue = 0;
constantLine.ShowInLegend = false; //true이면 범례에 빨간선이 추가됨.
diagram.AxisY.ConstantLines.Add(constantLine);

diagram.EnableAxisXScrolling = false;
diagram.EnableAxisXZooming = false;
```

```C#
// x축은 최고 10개 까지만 찍도록 하고, 10개가 넘어가면 첫번째 찍었던 값을 지운다. 그러면 
            // 그래프가 왼쪽으로 이동한다.
            if (this.seriesArray[0].Points.Count > 10)
            {
                this.seriesArray[0].Points.RemoveAt(0);
                this.seriesArray[1].Points.RemoveAt(0);
            }

//값은 랜덤으로 범위 지정
// 이벤트가 발생할때 마다 index가 증가하면서 데이터를 추가한다.
this.seriesArray[0].Points.Add(new SeriesPoint(this.index,this.random.Next(-100,200)));

this.seriesArray[1].Points.Add(new SeriesPoint(this.index,this.random.Next(-50, 100)));
this.index++;
```

