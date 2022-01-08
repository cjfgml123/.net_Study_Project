### C# IDisposable

- Dispose의 단어 의미는 제거하다, 처분하다 라는 뜻을 가진다. 여기에 가능하다는 의미인 able이 붙어 제거 가능한, 사용후 버리게 되어 있는, 일회용의 란 뜻이다.
- C# 에서 무엇을 사용하고 제거해야 할까? 바로 메모리다. 메모리를 사용하고 다 썼으면 메모리를 끊어줘야 한다.

#### 질문 ? C#은 Garbage Collertor를 가지고 있는데, 힙 메모리 영역에서 더이상 사용하지 않는 객체들을 소거하는 역할을 한다. 자동으로 소거하기 때문에 개발자는 소거하는 일 자체는 신경쓰지 않아도 된다. 그런데 왜 Dispose가 필요할까?

1. GC는 관리되지 않는 리소스들을 인식하지 못한다.
2. GC는 개발자가 동작을 지시하는 것이 아니기 때문에 어느 시점에 메모리 해제가 일어나는지 알 수 없다.
3. GC가 자주 발생하게 되면 오버헤드로 인한 비용이 증가한다.



```C#
public class TestClass : IDisposable
{
    public void TestMethod()
    {
        Console.WriteLine("this is TestClass");
    }
    public void Dispose()
    {
        Console.WriteLine("Dispose");
    }
}
//위와 같이 IDisposable 인터페이스를 클래스에 상속 시켜 Dispose()를 구현하게 되면 이제 TestClass는 사용이 끝나면 Dispose()를 호출하고 메모리가 해제 된다.

class Program
{
    static void Main(string[] args)
    {
        using(TestClass t = new TestClass())
        {
            t.TestMethod();
        }
    }
}
/*
this is TestClass
Dispose
*/
```

- 위와 같이 IDsposable 인터페이스를 클래스에 상속 시켜 Dispose()를 구현하게 되면 이제 TestClass는 사용이 끝나면 Dispose()를 호출하고 메모리가 해제된다.

#### 질문 : Main에서 using이란 무엇일까? 만약 using이 없다면 Dispose()가 실행 될까?

- 실행되지 않는다. TestClass의 객체인 t를 언제까지 사용할 것인지 알려주지 않았기 때문에 메모리 해제가 일어나지 않는다. 해당 객체를 어디서부터 어디까지만 사용할 것인지 명시하는 것이 using()의 역할이다.

```C#
using(TestClass t = new TestClass())
{
    //객체 t를 여기서 부터 
    t.TestMethod();
    //여기까지 사용하겠다.
}
```

이런 이유로 using() {} 이후 부터는 객체 t를 사용할 수 없다. 할당된 메모리를 해제해버렸고 그 때문에 Dispose()도 실행된 것이다. 이렇게 Dispose()는 using과 함께 자주 사용된다.

- 중요 데이터와 메모리를 얼른 쓰고 돌려주어야 할 때, 다른 프로세스와의 교착상태를 방지하고자 할때, 메모리 낭비를 방지하고자 할때 유용하게 사용할 수 있다.