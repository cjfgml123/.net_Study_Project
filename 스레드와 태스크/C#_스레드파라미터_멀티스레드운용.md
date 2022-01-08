### 1. Thread 클래스 파라미터 전달

#### 1-1) 파라미터를 전달하지 않는 ThreadStart 델리게이트

- public delegate void ThreadStart();
  - 델리게이트인데 전달하는 파라미터가 없다.

#### 1-2) 파라미터를 직접 전달하는 ParameterizedThreadStart 델리게이트

- ParameterizedThreadStart(object obj); 	
  - 하나의 object 파라미터를 전달하고 리턴 값이 없는 형식이다. 하나의 파라미터를 object 형식으로 전달하기 때문에, 여러 개의 파라미터를 전달하기 위해서는 클래스나 구조체를 만들어 객체를 생성해서 전달할 수 있다.

```C#
class Program
{
    static void Main(string[] args)
    {
        // 파라미터 없는 ThreadStart 사용
        Thread t1 = new Thread(new ThreadStart(Run));
        t1.Start();

        // ParameterizedThreadStart 파라미터 전달
        // Start()의 파라미터로 radius 전달
        Thread t2 = new Thread(new ParameterizedThreadStart(Calc));
        t2.Start(10.00);
        
        // ThreadStart에서 파라미터 전달
        Thread t3 = new Thread(() => Sum(10, 20, 30));
        t3.Start();
    }

    static void Run()
    {
        Console.WriteLine("Run");
    }

    // radius라는 파라미터를 object 타입으로 받아들임
    static void Calc(object radius)
    {
        double r = (double)radius;
        double area = r * r * 3.14;
        Console.WriteLine("r={0},area={1}", r, area);
    }

    static void Sum(int d1, int d2, int d3)
    {
        int sum = d1 + d2 + d3;
        Console.WriteLine(sum);
    }
}
```

### 2. Background 쓰레드 vs Foreground 쓰레드

- Thread 클래스 객체를 생성한 후 Start()를 실행하기 전에 IsBackground 속성을 true/false로 지정할 수 있는데, 만약 true로 지정하면 이 쓰레드는 백그라운드 스레드가 된다. 
- 디폴트 값은 false 즉 Foreground 스레드이다.
- 차이점
  - Foreground 스레드는 메인 스레드가 종료되더라도 Foreground 스레드가 살아 있는 한 프로세스가 종료되지 않고 계속 실행되고, 백그라운드 스레드는 메인 스레드가 종료되면 바로 프로세스를 종료한다는 점이다.

### 3. 현재 사용 중인 스레드 확인 코드

- Thread.CurrentThread.GetHashCode() 메소드 사용



### 4. 멀티 스레드 사용 중 윈도우 폼 컨트롤을 접근할때

#### 4-1) 문제 원인 : 

- 동시성이 있는 멀티 스레드 프로그램 환경에서 특정 스레드나 메인 스레드에서 생성된 Win Form 컨트롤 (TextBox, ListView, Label,...)을 다른 스레드에서 접근할때 "크로스 스레드라는 에러가 발생한다." 안전한 방식으로 접근하기 위해서는 컨트롤을 생성한 스레드가 아닌 윈 폼 컨트롤에 접근해야 할 다른 스레드에서 적절한 방법으로 문제를 해결해야 한다.



#### 4-2) 해결 방법 :

##### 4-2-1) Invoke 메소드 방식

- 별도의 스레드에서 컨트롤 박스에 접근하려고 하면 서로 다른 스레드가 하나의 컨트롤 객체에 접근하는 것을 방지하기 위한 방법
- 멀티 스레드 환경에서 자원 보호를 위한 방지책으로 invoke 없이 멀티스레드를 돌리면 크로스 스레드 오류현상 발생
- 자원 공유 위반으로 invoke나 begininvoke를 사용하라는 에러 메시지를 만나게 된다.
- 밑의 예제와 달리 람다식을 이용한 간결한 코드 작성 가능(인터넷 검색)

```C#
private void List(int data)
{
	// WinForm UI에 관한 컨트롤 메소드    
}
// 델리게이트 선언
private delegate void myDelegate(int data);

private void MyThread()
{
    if(num==1)
    {	// invoke를 사용할땐 invokerequired()를 사용해서 먼저 확인하자. 
        // 생성된 스레드가 아닌 다른 스레드에서 호출될 경우 true를 반환하고 정상적인 경우엔 false를 반환한다.
        if(this.InvokeRequired)
        {
            //형식Invoke(new 델리게이트(메소드이름),메소드에 넣을 파라미터)
        this.Invoke(new myDelegate(List), 10);
        Thread.Sleep(100); 
        }else{
            List(10);
        }
        
    }
}

private void enter_Click(object sender, EventArgs e)
{	//메인 스레드가 아닌 서브 스레드 생성 후 윈폼 컨트롤에 접근하도록 생성함.
    new Thread(new ThreadStart(MyThread)).start();
}
```

