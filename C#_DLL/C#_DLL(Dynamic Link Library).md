### 1. DLL(Dynamic Link Library)

- 동적 링크 라이브러리의 약자로 일반적으로 확장자가 DLL인 파일이다. 라이브러리라는 말에서 알 수 있듯이 다른 프로그램에서 이용하는 함수들을 모아둔 것이다.

- DLL을 만들기 위해선 프로젝트 생성시, Windows Forms 응용 프로그램이 아닌, 클래스 라이브러리로 만들어야한다.
- DLL 내에서 사용하는 함수나 클래스등은 모두 외부에서 사용할 수 있어야 하므로, 한정자는 반드시 public으로 지정하여야 한다. 그 후 솔루션 빌드를 하면 DLL이 생성된다.

참조 사이트 : https://lena19760323.tistory.com/92

#### 1-1) 예시

```C#
// DLL 만들기
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_CreateDLL
{
    public class dllTest
    {
        public int fn_Add(int a, int b)
        {
            return a + b;
        }
    }
}
```

```C# 
// 사용예시
using Test_CreateDLL;  //using으로 dll 참조하면 dll 메소드 사용가능

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
            MessageBox.Show(var_dll.fn_Add(5, 10).ToString());
        }
    }
}
//15 출력
```

