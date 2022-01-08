### 이벤트 : 객체에 일어난 사건 알리기

* 이벤트는 대리자를 event 한정자로 수식해서 만든다.
* 특정 상황이 발생했을 때 알리고자 하는 용도(호출을 의미 + 데이터)
* 이벤트를 발생시키는 클래스를 게시자
* 이벤트를 받거나 처리하는 클래스를 구독자
* 델리게이트를 기반으로 한다.(메서드 호출)
* 이벤트는 메서드 안에서만 사용가능

```C#
using System;

namespace EventTest
{
    //1. 대리자를 선언한다. 이 대리자는 클래스 밖에 선언해도 되고 안에 선언해도 된다.
    delegate void EventHandler(string message);

    //2. 클래스 내에 1. 에서 선언한 대리자의 인스턴스를 event 한정자로 수식해서 선언한다.
    class MyNotifier
    {
        public event EventHandler SomethingHappened; //대리자의 인스턴스 : EventHandler 이벤트 선언 : SomethingHappened
        public void DoSomething(int number)
        {
            int temp = number % 10;

            if (temp !=0 && temp % 3 ==0) // number가 3,6,9로 끝나는 값일때 이벤트발생
            {
                SomethingHappened(String.Format("{0} : 짝", number));
                //String.Format("{0} : 짝", number)->MyHandler(string message)
                //SomethingHappened 이벤트 안의 것들이 string message로 대입되서 넘어감.
            }
        }
    }

    class Program
    {   
        static public void MyHandler(string message)
        { //3. 이벤트 핸들러를 작성, 이벤트 핸들러는 1.에서 선언한 대리자와 형식과 일치하는 메소드면 된다.
            Console.WriteLine(message);
        } 
        static void Main(string[] args)
        { //4. 클래스의 인스턴스를 생성하고 이 객체의 이벤트에 3.에서 작성한 이벤트 핸들러를 등록한다.
            MyNotifier notifier = new MyNotifier();
            notifier.SomethingHappened += new EventHandler(MyHandler);
            // SometingHappened이벤트에 MyHandler()메소드를 이벤트 핸들러로 등록한다.
            // 이벤트 핸들러 선언 : 1에서 함.
            // delegate는 인자를 메소드를 넣으므로
            for (int i =1; i<30; i++)
            {
                notifier.DoSomething(i);
            }
        }
    }
}

```



##### 대리자는 대리자대로 콜백 용도로 사용하고, 이벤트는 이벤트대로 객체의 상태 변화나 사건의 발생을 알리는 용도로 구분해서 사용

```C# 
deleate void EventHandler(string message);

class MyNotifier
{
    public event EventHandler SomethingHappened;
    //...
}

class MainApp
{
    static void Main(string[] args)
    {
        MyNotifier notifier = new MyNotifier();
        notifier.SomethingHappened("테스트"); // 에러. 이벤트는 객체 외부에서 직접 호출할 수 없다.
    }
}
```

