## 대리자(delegate)와 이벤트

* 대리자는 (인스턴스, 정적)메소드에 대한 참조이기 때문에 자신이 참조할 메소드의 반환형식과 매개 변수를 명시해줘야 한다.
* 대리자에 메소드의 주소를 할당한 후 대리자를 호출하면 이 대리자가 메소드를 호출해준다.
* 대리자는 인스턴스가 아닌 형식(Type)이다. 

```C#
// 한정자 delegate 반환_형식 대리자_이름(매개변수 목록);
delegate int MyDelegate(int a, int b);

// 대리자가 참조할 메소드
int Plus(int a, int b)
{
    return a+b;
}

int Minus(int a , int b)
{
    return a - b;
}

// 메소드를 MyDelegate가 참조하도록 
MyDeletate Callback; //1. 대리자 선언

Callback = new MyDelegate(Plus); // 2. 대리자 인스턴스 생성, 인수는 참조할 메소드
Console.WriteLine(Callback(3,4)); // 7  3. 대리자 호출

Callback = new MyDelegate(Minus); // 대리자의 인스턴스를 만들 때도 new연산자가 필요
Console.WriteLine(Callback(7,5)); // 2

```



### 대리자는 왜 그리고 언제 사용하나?

#### 질문 : 그냥 메소드를 호출하면 되지 ,왜?

- 메소드를 메소드에 넘겨 주기 위해서 근데 왜? 람다식이나 링큐 무명메소드를 통해서 함수를 작성하는데 더 효율적이다. 이벤트에서 이벤트 핸들러를 전달할 때 델리게이트가 사용이 되는데

콜백 함수 : 어떤 특정 일이 발생할때 실행되는 함수 ,비동기

```C#
using System;

namespace UsingCallback
{
    delegate int Compare(int a, int b); //델리게이트 선언
    class Program
    {   
        static int AscendCompare(int a, int b) //오름차순
        {
            if (a > b)
                return 1;
            else if (a == b)
                return 0;
            else
                return - 1;
        }

        static int DescendCompare(int a, int b) //내림차순
        {
            if (a < b)
                return 1;
            else if (a == b)
                return 0;
            else
                return -1;
        }

        static void BubbleSort(int[] DataSet, Compare Comparer)
        {						// 델리게이트는 타입 형식이므로 Compare
            int i = 0;
            int j = 0;
            int temp = 0;

            for (i=0; i<DataSet.Length-1; i++)
            {
                for (j = 0; j < DataSet.Length - (i + 1); j++)
                {
                    if (Comparer(DataSet[j], DataSet[j+1]) > 0)
                    {
                        temp = DataSet[j + 1];
                        DataSet[j + 1] = DataSet[j];
                        DataSet[j] = temp;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int[] array = { 3, 7, 4, 2, 10 };

            Console.WriteLine("Sorting ascending...");
            BubbleSort(array, new Compare(AscendCompare));//델리게이트 호출

            for (int i = 0; i < array.Length; i++)
                Console.Write($"{array[i]} ");

            int[] array2 = { 7, 2, 8, 10, 11 };
            Console.WriteLine("\nSorting descending...");
            BubbleSort(array2, new Compare(DescendCompare));

            for (int i = 0; i < array2.Length; i++)
                Console.Write($"{array2[i]} ");

            Console.WriteLine();
        }
    }
}
```



### 일반화 대리자

* 일반화 메소드도 참조할 수 있다.
* 이 경우 대리자도 일반화 메소드를 참조할 수 있도록 형식 매개변수를 이용하여 선언, 요령은 메소드와 같다.

```C#
delegate int Compare<T>(T a , T b);

// ...
static void BubbleSort<T>(T[] DataSet, compare<T> Comparer)
{
    // ...
}    
    
```



#### 대리자 체인

```C#
delegate void ThereIsAFire( string location );

void Call119( string location )
{
    Console.WriteLine("소방서죠? 불났어요! 주소는 {0}", location);
}

void ShotOut( string location)
{
    Console.WriteLine("피하세요! {0}에 불이 났어요!", location);
}

void Escape( string location)
{
    Console.WriteLine("{0}에서 나갑시다!", location);
}

// 대리자의 인스턴스가 메소드를 대리자 체인을 따라 차례대로 호출, 참조할 수 있도록 += 연산자 사용
ThereIsAFire Fire = new ThereIsAFire(Call119);
Fire += new ThrerIsAFire(ShotOut);
Fire += new ThrerIsAFire(Escape);

// 메서드 3개 모두 호출
Fire("우리집");
/*
소방서죠? 불났어요! 주소는 .....
*/

//ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
// 또다른 방법
// 1. + 연산자와 = 연산자 사용하기
ThereIsAFire Fire = new ThereIsAFire(Call119)
					+ new ThrerIsAFire(ShotOut)
					+ new ThrerIsAFire(Escape);

// 2. Delegate.Combine() 메소드 사용하기
ThereIsAFire Fire = (ThereIsAFire) Delegate.Combine(
    				 new ThereIsAFire(Call119),
					 new ThrerIsAFire(ShotOut),
					 new ThrerIsAFire(Escape) );
// 대리자 체인에서 특정 대리자를 끊어내야 할 때는 -= 연산자,Delegate.Remove() 사용
```

```C#
using System;

namespace DelegateChains
{
    delegate void Notify(string message);

    class Notifier //Notify 대리자의 인스턴스인 EventOccured를 가지는 클래스 Notifier클래스 선언
    {
        public Notify EventOccured;
    }

    class EventListener
    {
        private string name;
        public EventListener(string name)
        {
            this.name = name;
        }

        public void SomethingHappend(string message)
        {
            Console.WriteLine($"{name}.SometingHappened : {message}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Notifier notifier = new Notifier();
            EventListener listener1 = new EventListener("Listener1");
            EventListener listener2 = new EventListener("Listener2");
            EventListener listener3 = new EventListener("Listener3");

            notifier.EventOccured += listener1.SomethingHappend;
            notifier.EventOccured += listener2.SomethingHappend;
            notifier.EventOccured += listener3.SomethingHappend;
            notifier.EventOccured("You've got mail."); // 대리자 메소드 모두 호출
            /*
            Listener1.SometingHappened : You've got mail.
            Listener2.SometingHappened : You've got mail.
            Listener3.SometingHappened : You've got mail.
            */

            notifier.EventOccured -= listener2.SomethingHappend; //체인끊기
            notifier.EventOccured("Download complete."); 
            Console.WriteLine();
            /*
             Listener1.SometingHappened : Download complete.
             Listener3.SometingHappened : Download complete.
             */

            Notify notify1 = new Notify(listener1.SomethingHappend);
            Notify notify2 = new Notify(listener2.SomethingHappend);

            notifier.EventOccured =
                (Notify)Delegate.Combine(notify1, notify2); // 체인만들기
            notifier.EventOccured("Fire!!");
            /*
             Listener1.SometingHappened : Fire!!
             Listener2.SometingHappened : Fire!!
            */
            Console.WriteLine();

            notifier.EventOccured =
                (Notify)Delegate.Remove(notifier.EventOccured, notify2); //체인 끊기
            notifier.EventOccured("RPG!");
            /*
            Listener1.SometingHappened : RPG!
             */
        }
    }
}
```



### 익명 메소드(Anonymous Method)

* 메소드는 보통 한정자가 없어도, 반환할 값이 없어도, 매개변수가 없어도 괜찮지만 이름만은 있어야 한다. 하지만 익명 메소드는 이름이 없는 메소드를 말한다.
* 익명 메소드는 delegate 키워드를 이용하여 선언
* 자신을 참조할 대리자의 형식과 동일한 형식으로 선언 되어야 한다.

```C#
void DoSomething()
{
    // 일반 메소드
}
//ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
delegate int Calculate(int a , int b);

public static void Main()
{
    Calculate Calc; //대리자의 인스턴스
    Calc = delegate (int a, int b) //이름을 제외한 메소드의 구현(익명 메소드)
    {
        return a+b;
    };
    Console.WriteLine("3+4 :{0}", Calc(3,4));
}
//ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
대리자_인스턴스 = delegate (매겨변수_목록)
{
    //실행 코드
}
```

