## 스레드와 태스크

#### 1. 프로세스와 스레드

- 프로세스 : 예시로 word.exe가 실행 파일이라면 이 실행 파일을 실행한 것
- 프로세스는 반드시 하나 이상의 스레드(Thread)로 구성되는데, 스레드는 운영체제가 CPU 시간을 할당하는 기본 단위이다.

```C#
using System;
using System.Threading;
namespace BasicThread
{
    class Program
    {   
        static void DoSomething() //스레드가 실행할 메소드
        {
            for (int i=0; i<5; i++)
            {
                Console.WriteLine($"DoSomething : {i}");
                Thread.Sleep(10); //인수 만큼 cpu사용을 멈춤. 단위는 밀리초
            }
        }
        static void Main(string[] args)
        {    
            // Thread의 인스턴스를 생성한다. 이때 생성자의 인수로 스레드가 실행할 메소드를 넘긴다.
            Thread t1 = new Thread(new ThreadStart(DoSomething));

            Console.WriteLine("Starting thread...");
            t1.Start(); //스레드를 시작 DoSomething() 메소드 호출

            for (int i=0; i<5; i++) //t1 스레드가 실행되는 동시에 메인 스레드의 이 반복문도 실행됨.
            {
                Console.WriteLine($"Main : {i}");
                Thread.Sleep(10);

            }

            Console.WriteLine("Wating until thread stops...");
            t1.Join(); // 스레드가 끝날 때까지 기다린다.

            Console.WriteLine("Finished");
        }
    }
}
/*
Starting thread...
Main : 0
DoSomething : 0
DoSomething : 1
Main : 1
Main : 2
DoSomething : 2
Main : 3
DoSomething : 3
Main : 4
DoSomething : 4
Wating until thread stops...
Finished
 */
```

#### 스레드 임의로 종료시키기(Thread.Abort(), Thread.Interrupt())

- 작업관리자 등을 이용해 프로세스를 임의로 죽일 수 있는데 프로세스 안에서 동작하는 스레드는 이런식으로 종료할 수 없다. 종료시키기 위해 Thread 객체의 Abort()메소드를 호출 해야 한다.
- Thread.Abort() : 스레드 강제 종료 는 프로세스 자신이나 시스템에 영향을 받지 않는 작업에 사용하는 것이 좋다.
- Thread.Interrupt()는 Running상태를 피해서 WaitJoinSleep상태에 들어갔을 때 exception을 던져 스레드를 중지해서 더 안정적이다.

```C#
static void DoSomething()
{
    try
    {
        for (int i =0; i<10000; i++)
        {
            Console.WriteLine("DoSomething : {0}",i);
            Thread.Sleep(10);
        }
    }
    catch(ThreadAbortedException e) //ThreadInterruptedException e
    {
        //...
    }
    finally
    {
        //...
    }
}
static void Main(string[] args)
{
    Thread t1 = new Thread(new ThreadStart(DoSomething));
    t1.Start();
    
    t1.Abort(); //스레드 취소(종료) , 사용하지 않는 것이 좋다.
    //t1.Interrupt();
    
    t1.Join();
}
// try~catch문이 있으면 ThreadAbortException 예외를 처리 후 finally 블록 까지 실행 후
// 스레드 종료
```



#### 스레드의 일생과 상태변화

- ThreadState 가 Flags 애트리뷰트를 갖고 있다. Flags 자신이 수식하는 열거형을 비트 필드

| 상태          | 설명                                                         |
| ------------- | ------------------------------------------------------------ |
| Unstarted     | 스레드 객체를 생성한 후 Thread.Start() 메소드가 호출되기 전의 상태 |
| Running       | 스레드가 시작하여 동작 중인 상태를 나타낸다.                 |
| Suspended     | 일시 중단 상태                                               |
| WaitSleepJoin | 블록(block)된 상태                                           |
| Aborted       | 취소된 상태                                                  |
| Stopped       | 중지된 상태                                                  |
| Background    | 백그라운드로 동작하고 있음을 나타냄                          |

![image-20210225143120900](C:\Users\CHLee\AppData\Roaming\Typora\typora-user-images\image-20210225143120900.png)



#### 스레드 간의 동기화(lock키워드, Monitor클래스)

- 다른 스레드가 어떤 자원을 잡고 사용하고 있는 중인데 갑자기 끼어들어 자기가 자원을 사용하는 경우를 예방하는 것.
- 동기화 (Synchronization) : 스레드들이 순서를 갖춰 자원을 사용하게 하는 것

- 자원을 한번에 하나의 스레드가 사용하도록 보장 하도록 하는 것. lock키워드와 Monitor클래스사용
- 크리티컬 섹션(Critical Section) : 한 번에 한 스레드만 사용할 수 있는 코드 영역, lock키워드로 감싼 영역

```c#
// lock 키워드
class Counter
{
    public int count = 0;
    private readonly object thisLock = new object();
    
    public void Increase()
    {	
        lock (thisLock) //크리티컬 섹션
        {
        	count = count +1;    
        } //한 스레드가 이 코드를 실행하다가 lock블록이 끝나는 괄호를 만나기 전까지 다른 스레드는 이 코드를 실행할 수 없다.
        
    }
}

//... main함수
CounterClass obj = new CounterClass();
Thread t1 = new Thread(new ThreadStart(obj.Increase));
Thread t2 = new Thread(new ThreadStart(obj.Increase));
Thread t3 = new Thread(new ThreadStart(obj.Increase));
t1.Start();
t2.Start();
t3.Start();

t1.Join();
t2.Join();
t3.Join();
Console.WriteLine(obj.count);
```

##### Monitor 클래스로 동기화하기

- Monitor.Enter()와 Monitor.Exit()가 look키워드의 {}를 대신함.
- Enter()는 한 스레드만 크리티컬 섹션에 들어가게 하고 Exit()는 락킹을 해제하여 다음 스레드가 크리티컬 섹션 블럭을 실행하게 한다.

```C#
// lock
public void Increase()
{
    int loopCount = 1000;
    while(loopCount-- >0)
    {
        lock (thisLock)
        {
            count++;
        }
    }
}

//Monitor.Enter()와 Monitor.Exit()
public void Increase()
{
    int loopCount = 1000;
    while (loopCount-- > 0)
    {
        Monitor.Enter(thisLock);
        try
        {
            count++;
        }
        finally
        {
            Monitor.Exit(thislock);
        }
    }
}
```



##### Monitor.Wait()와 Monitor.Pulse()로 하는 저수준 동기화

- 크리티컬섹션에서 사용
- Monitor.Wait()는 스레드를 WaitSleepJoin상태로 만든다. 스레드 후에 Waiting Queue에 입력되고 다른 스레드가 락을 얻어 작업을 수행, 작업을 수행하던 스레드가 일을 마친 뒤 Pulse() 메소드를 호출하면 Waiting Queue에서 첫 번째 위치에 있는 스레드를 꺼낸 뒤 Ready Queue에 입력시킨다. Ready Queue에 입력된 스레드는 입력된 차례에 따라 락을 얻어 Running상태로 들어간다.


![image-20210226140139572](C:\Users\CHLee\AppData\Roaming\Typora\typora-user-images\image-20210226140139572.png)

```C#
//1. 클래스 안에 다음과 같이 동기화 객체 필드를 선언
readonly object thisLock = new object();

//2. 스레드를 WaitSleepJoin 상태로 바꿔 블록시킬 조건(Wait()를 호출할 조건)을 결정할 필드 선언
bool lockedCount - false;

//3. 동기화 코드, 스레드를 블록시키고 싶은 곳에서는 lock블록 안에서 2.에서 선언한 필드를 검사하여 Monitor.Wait()를 호출한다.
lock (thisLock)
{
    while (count > 0 || lockedCount == true)
        Monitor.Wait(thisLock);
    //...
}

//4.
/*
블록되어 있던 스레드가 작업을 해야한다. 
*/
lock (thisLock)
{
    while (count > 0 || lockedCount == true)
        Monitor.Wait(thisLock);
    
    lockedCount = true; // true해두면 다른 스레드가 이 코드에 접근방지
    count++;
    lockedCount = false;//작업을 마치면 false로 바꾼 후 
    
    Monitor.Pulse(thisLock); //메소드 호출, 그럼 Waiting 큐에 대기하고 있던 다른 스레드가 깨어나서 false로 바뀐 lockedCount를 보고 작업을 수행
}

```



### 2. Task와 Task<TResult> 그리고 Parallel

- 병렬 처리 - 하나의 작업을 여러 작업자가 나눠서 수행 후 다시 하나의 결과로 만드는 것.
- 비동기 처리 - 작업 A를 시작한 후 A의 결과가 나올 때까지 마냥 대기하는 대신 다른 작업을 수행하다가 작업 A가 끝나면 그때 결과를 받아내는 처리 방식

##### -> 병렬처리와 비동기 처리 작업을 위한것이 이것.



- 태스크는 하나의 작업을 쪼갠 뒤 쪼개진 작업들을 동시에 처리하는 코드와 비동기 코드를 위해 설계되고 스레드는 여러 개의 작업을 (나누지 않고) 각각 처리해야 하는 코드에 적합



#### 2-1) System.Threading.Tasks.Task 클래스

- 동기(Synchronous) : 메소드를 호출한 뒤 이 메소드의 실행이 완료되야 다음 메소드를 호출할 수 있다.
-  비동기(Asynchronous) : 한 메소드의 결과 완료 기다리지 않고 바로 다음 할일 실행

```C#
/*
- Action 대리자를 실행
- Start() 메소드 : Action 대리자 비동기 실행
- Factory.StartNew()메소드 : Task 객체 생성 및 Action 대리자 비동기 실행
- Wait() 메소드 : Action 대리자 실행 완료 대기
*/

//Task 클래스는 인스턴스를 생성할 때 Action 대리자를 넘겨 받는다.(반환형을 갖지 않는 메소드와 익명 메소드,무명 함수를 넘겨 받는다.)

Action someAction = () =>
{
    Thread.Sleep(1000);
    Console.WriteLine("Printed asynchronously.");. //비동기
};

Task myTask = new Task(someAction);
myTask.Start();

Console.WriteLine("Printed synchronously."); //동기

myTask.Wait(); //비동기 호출이 완료될때까지 기다린다.
/* 결과
Printed synchronously.
Printed asynchronously. */
```

#### 2-2) 코드의 비동기 실행 결과를 주는 Task<TResult> 클래스

- 15개의 비동기 작업을 한 후 그 결과를 취합해야 한다고 할때 사용, Task<TResult>는 코드의 비동기 실행 결과를 손쉽게 취합할 수 있도록 도와준다.
- Task 클래스는 비동기로 수행할 코드를 Action대리자로 받는 대신 이것은 Func 대리자로 받고 결과를 반환받을 수 있다.

```c#
var myTask = Task<List<int>>.Run(
() =>
    {
        Thread.Sleep(1000);
        
        List<int> list = new List<int>();
        list.Add(3);
        list.Add(4);
        list.Add(5);
        return list; //<-Task<TRsult>는 TRsult형식의 결과를 반환한다.
    }
);
var myList = new List<int>();
myList.Add(0);
myList.Add(1);
myList.Add(2);

myTask.Wait();
myList.AddRange(myTask.Result.ToArray()); //myList의 요소는 0,1,2,3,4,5
//myTask.Result로 태스크 결과값 반환
```

#### 2-3) 손쉬운 병렬 처리를 가능케 하는 Parallel 클래스

- System.Threading.Tasks.Parallel 클래스는 For(), Foreach()등의 메소드를 제공해서 Task<TResult>를 이용해 직접 구현했던 병렬 처리를 더 쉽게 구현할 수 있게 해준다.

```C#
bool IsPrime(long number) {/*...*/}
//... 소수 구하기
{
    int from = 0;
    int to = 10000000;
    List<long>total = new List<long>();
    
    Parallel.For(from, to, (long i) => //병렬 처리로 소수 구해줌
                 {
                     if (IsPrime(i))
                         lock(total) // 멀티스레드를 기본으로 활용해서 공유되는 자원에 대해 동기화가 필요해서 lock이 필요
                         	total.Add(i);
                 });
}
```



#### 2-4) async 한정자와 await 연산자로 만드는 비동기 코드

##### 2-4-1) async 한정자

- async 한정자는 메소드, 이벤트 처리기, 태스크, 람다식 등을 수식함
- C# 컴파일러가 async 한정자로 수식한 코드의 호출자를 만날 때 호출 결과를 기다리지 않고 바로 다음 코드로 이동하도록 실행 코드를 생성
- async로 한정하는 메소드는 반환 형식이 Task나 Task<TResult>, 또는 void 여야 함.
  	- 실행하고 잊어버릴(Shoot and Forget) 작업을 담고 있는 메소드면 반환형식을 void로 선언
  	- 작업이 완료될 때까지 기다리는 메소드라면 Task, Task<TResult>로 선언

```C#
public static async Task MyMethodAsync()
{
    //...
}
```

![image-20210226152423649](C:\Users\CHLee\AppData\Roaming\Typora\typora-user-images\image-20210226152423649.png)