## WinForm으로 만드는 사용자 인터페이스



#### 1. System.Windows.Forms.Application 클래스

##### 역할

- 윈도우 응용 프로그램을 시작하고 종료시키는 메소드를 제공
- 원도우 메시지를 처리하는 것

```C#
class MyForm : System.Windows.Forms.Form
{
 //    1. System.Windows.Forms.Form클래스에서 파생된 윈도우 폼 클래스
}
class MainApp
{
    static void Main(string[] args)
    {
        MyForm form = new MyForm(); //폼 인스턴스화
         form.Click += new  EventHandler((sender, eventArgs) =>
                {
                Console.WriteLine("Closing Window...");
                Application.Exit();
            }); //Click 이벤트는 윈도우를 클릭했을 때 발생하는 이벤트 따라서 이 코드는 윈도우를 클릭하면 Application.Exit();를 호출하도록 한다.
        Application.Run(form); //1.의 클래스의 인스턴스를 인수로 넘겨 Run()메소드 호출
    }
}
```

#### 1-1) Application 클래스의 기능 : 메시지 필터링 (Message Filtering)

- 윈도우 기반의 응용 프로그램들은 갑자기 일어나는 사건(이벤트 : Event)에 반응해서 코드가 실행되는 이벤트 기반(Event Driven)방식으로 만들어 진다.  ex) 클릭, 키보드 입력 등
- 메시지 필터링 : 수많은 메시지 중에 관심 있는 메시지만 걸러낼 수 있는 기능

- 과정 : 메시지발생->메시지 필터-> 이벤트 발생(필터에서 걸러지지 않은 메시지는 이벤트 발생)->이벤트 처리기 

| 프로퍼티 | 설명                                                         |
| -------- | ------------------------------------------------------------ |
| HWnd     | 메시지를 받는 윈도우의 핸들(Handle), 윈도우의 인스턴스를 식별하고 관리하기 위해 운영체제가 붙여놓은 번호 |
| Msg      | 메시지ID                                                     |
| LParam   | 메시지를 처리하는데 필요한 정보가 담겨 있다.                 |
| WParam   | 메시지를 처리하는데 필요한 부가 정보가 담겨 있다.            |
| Result   | 메시지 처리에 대해 응답으로 윈도우 os에 반환되는 값을 지정한다. |

```C#
/*
메시지 필터 예제 프로그램
*/
using System;
using System.Windows.Forms;

namespace MessageFilter
{   
    class MessageFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m) //IMessageFilter 인터페이스의 PreFilterMessage()를 구현해야함.
        {
            if (m.Msg == 0x0F || m.Msg == 0xA0 || //Msg :메시지 ID
                m.Msg == 0x200 || m.Msg == 0x113)
                return false;

            Console.WriteLine($"{m.ToString()} : {m.Msg}");

            if (m.Msg == 0x201)
                Application.Exit();
            return true;
        }
    }
    class MainApp : Form
    {
        static void Main(string[] args)
        {
            Application.AddMessageFilter(new MessageFilter()); // 메시지 필터를 설치, IMessageFilter인터페이스를 구현하는 파생 클래스(MessageFilter)의
            //인스턴스를 인수로 받는다.
            Application.Run(new MainApp());
        }
    }
}

```

### 2. 윈도우를 표현하는 Form클래스

- 다양한 컨트롤을 올려 사용할 수 있는 도화
- 다양한 윈도우 메시지에 대응하는 이벤트 정의

```C#
/*
Form 클래스의 MouseDown 이벤트에 대한 이벤트 처리를 하는 프로그램(이벤트 처리기 활용)
폼 위에서 마우스를 누를때마다 이벤트를 발생시킨 객체, 마우스 버튼, 마우스 커서의 좌표등을 콘솔에 출력
 */
using System;
using System.Windows.Forms;

namespace FormEvent
{
    class MainApp : Form
    {   
        /*
        1. sender : 이벤트가 발생한 객체를 가리키는데 현재 Form 클래스의 이벤트처리기에 대해 알아보고 있으니 sender는 Form객체 자신임.
        ex) 만약 Button 클래스의 이벤트 처리기 였다면 Button 객체
        2. MouseEventArgs 형식으로 프로퍼티(Button, Clicks, Delta, X , Y) 제공으로 마우스 이벤트의 상세 정보를 제공
         */
        public void MyMouseHandler(object sender, MouseEventArgs e) //이벤트 처리기 메소드 선언
        {
            Console.WriteLine($"Sender : {((Form)sender).Text}");
            Console.WriteLine($"X:{e:X}, Y:{e.Y}");
            Console.WriteLine($"Button:{e.Button}, Clicks:{e.Clicks}");
            Console.WriteLine();

        }
        public MainApp(string title)
        {
            this.Text = title;
            this.MouseDown += new MouseEventHandler(MyMouseHandler); //이벤트 처리기를 이벤트에 연결, 인수를 이벤트처리기 선언을 넣는다. MouseEventHandler는 대리자
        }
        static void Main(string[] args)
        {
            Application.Run(new MainApp("Mouse Event Test"));
        }
    }
}

```

### 3. Form위에 컨트롤 올리기

- 사용자 인터페이스를 위해 메뉴, 콤보박스, 리스트뷰, 버튼, 텍스트박스 등과 같은 표준 컨트롤을 제공한다.

```C#
//컨트롤 생성 과정 , 모든 컨트롤은 System.Windows.Forms.Control을 상속

//1. 컨트롤의 인스턴스 생성
Button button = new Button();

//2. 컨트롤의 프로퍼티에 값 지정
button.Text = "Click Me!";
button.Left = 100;
button.Top = 50;

//3. 컨트롤의 이벤트에 이벤트 처리기 등록 (이벤트 처리기를 메소드로 선언하지 않고 람다식으로 구현한것)
button.Click +=
    (object sender , EventArgs e) =>
{
    MessageBox.Show("딸깍!");
};

//4. 폼에 컨트롤 추가
MainApp form = new MainApp(); //Form인스턴스 생성
form.Controls.Add(button); // Controls프로퍼티의 Add()메소드를 호출하여 button객체를 Form에 올린다.
```

```C#
using System;
using System.Windows.Forms;

namespace FormAndControl
{
    class MainApp : Form
    {   
        static void Main(string[] args)
        {
            Button button = new Button();

            button.Text = "Click Me!";
            button.Left = 100;
            button.Top = 50;

            button.Click +=
                (object sender, EventArgs e) =>
                {
                    MessageBox.Show("딸깍!");
                };

            MainApp form = new MainApp();
            form.Text = "Form & Control";
            form.Height = 150;

            form.Controls.Add(button);

            Application.Run(form);
        }
    }
}
```

