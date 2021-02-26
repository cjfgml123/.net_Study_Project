### 1. 일반화 프로그래밍

* 일반화(Generalization) : 공통된 개념을 찾아 묶는 것

ex) 학교에 철희, 다영, 철수가 다니는데 이들은 학생이라는 공통된 개념을 찾아 묶을 수 있다.



#### 1_1. 일반화 메소드(Generic Method)

* 데이터 형식을 일반화한 메소드

```C#
한정자 반환_형식 메소드이름<형식_매개변수> (매개변수 목록)
{
    // ...
}
```

```C#
//기존의 방법
void CopyArray(int [] source, int [] target)
{
    for(int i=0; i<source.Length; i++)
        target[i] = source[i];
}

// 일반화한 방법
// : T->형식 매개변수(Type Parrameter) , 이 메소드를 쓸때나 객체를 만들때 데이터 타입을 명시해야함.
void CopyArray<T> (T[] source, T[] target)
{
    for(int i=0l i<source.Length; i++)
        target[i] = source[i]
}
```

```C#
using System;

namespace CopyingArray
{   

    class Program
    {   
        static void CopyArray<T>(T[] source, T[] target)
        {
            for (int i = 0; i < source.Length; i++)
                target[i] = source[i];
        }

        static void Main(string[] args)
        {
            int[] source = { 1, 2, 3, 4, 5 };
            int[] target = new int[source.Length];

            CopyArray<int>(source, target); //이 메소드를 쓸때나 객체를 만들때 데이터 타입을 명시해야함.

            foreach (int element in target)
                Console.WriteLine(element);

            string[] source2 = { "하나", "둘", "셋", "넷", "다섯" };
            string[] target2 = new string[source2.Length];

            CopyArray<string>(source2, target2); //이 메소드를 쓸때나 객체를 만들때 데이터 타입을 명시해야함.

            foreach (string element in target2)
                Console.WriteLine(element);
        }
    }
}
/*
1
2
3
4
5
하나
둘
셋
넷
다섯
 */
```

#### 1_2. 일반화 클래스

* 데이터 형식을 일반화한 클래스

```C#
class 클래스이름 <형식_매개변수>
{
    //...
}

// 기존방법
class Array_Int
{
    private int[] array;
    //...
    public int GetElement( int index )
    {
        return array[index];
    }
}

// 일반화 클래스
class Array_Generic<T>  // 클래스에서 T로 일반화 했으면 내부의 필드나 메소드도 T로 일반화해야하는지??
{
    private T[] array;
    // ...
    public T getElement( int index ){
        return array[index];
    }
}
// 이렇게 사용.
// Array_Generic<int> intArr = new Array_Generic<int>();
// Array_Generic<string> intArr = new Array_Generic<string>();
```



#### 1_3. 형식 매개변수 제약시키기

* 형식 매개 변수가 특정 조건을 갖추도록 강제하는 기능
*  문법 : "where 형식매개변수 : 제약조건"

| 제약                     | 설명                                                         |
| ------------------------ | ------------------------------------------------------------ |
| where T:struct           | T는 값 형식이어야 한다.                                      |
| where T:class            | T는 참조 형식이어야 한다.                                    |
| where T:new()            | T는 반드시 매개변수가 없는 생성자가 있어야 한다.             |
| where T:기반_클래스_이름 | T는 명시한 기반 클래스의 파생 클래스여야 한다.               |
| where T:인터페이스_이름  | T는 명시한 인터페이스를 반드시 구현해야 한다. 인터페이스 이름에는 여러 개의 인터페이스를 명시할 수도 있다. |
| where T:U                | T는 또 다른 형식 매개변수 U로부터 상속받은 클래스여야 한다.  |



### 2. 일반화 컬렉션

* 기본 컬렉션은 object 형식에 기반해서 컬렉션의 요소에 접근할 때마다 형식 변환이 일어난다. 이 문제를 해결하기 위해 일반화 컬렉션이 있다.

#### 2-1. System.Collections.Generic

- List<T> , Queue<T>, Stack<T>, Dictionary<TKey, TValue> ...는 각각 ArrayList, Queue, Stack, Hashtable의 일반화 버전.

```c#
//기존 클래스
ArrayList list = new ArrayList();
Queue que = new Queue();
Stack stack = new Stack();
Hashtable ht = new Hashtable(); 
ht["하나"] = "one"; // ht[키] = 값;

// 일반화 클래스
List<int> list = new List<int>();
Queue<int> que = new Queue();
Stack<int> stack = new Stack();
Dictionary<string, string> dic = new Dictionary<string, string>();
dic["하나"] = "one";
//Dictionary<TKey,TValue>
```



##### 