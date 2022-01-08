### C# 윈폼 버튼 이벤트 하나로 통일 하기

##### 1. 메인 폼 디자인 에서 버튼 클릭 이벤트에 지정한 클릭 이벤트로 다 설정 한다.

```C#
 /// 모든 클릭 이벤트는 이것으로 처리한다.
private void Leftmenu_Click(object sender, EventArgs e)
        {
            PageChange(sender);
        }

/// <summary>
/// 화면전환 함수
/// </summary>
/// <param name="ucpagebtn">버튼 객체</param>
public void PageChange(object ucpagebtn)
{
	this.panel_Main.Controls.Clear();
	Button ucButton = ucpagebtn as Button;
    if (ucButton == null)
    {
    	return;
    }
    if (ucButton.Name != "btn_record")
    {
    	UserControl ucPage = ucButton.Tag as UserControl;
        if (ucPage != null)
        {
        	panel_Main.Controls.Add(ucPage);
            ucPage.Dock = DockStyle.Fill;
            ucPage.Show();
        }
   	}
    else
    {
    	_ctlBM.ShowRecordForm();
    }
}
```

