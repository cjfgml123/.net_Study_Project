### 컬렉션(Collection)

* 같은 성격을 띈 데이터의 모음을 담는 자료구조
* ArrayList, Queue, Stack, Hashtable 등 컬렉션 클래스



#### 1. ArrayList

* 용량을 미리 지정하는 배열과 달리 자동으로 용량이 늘어나거나 줄어드는 장점

| 메소드     | 설명                                                 |
| ---------- | ---------------------------------------------------- |
| Add()      | 컬렉션의 가장 마지막에 있는 요소 뒤에 새 요소를 추가 |
| RemoveAt() | 특정 인덱스에 있는 요소를 제거                       |
| Insert()   | 원하는 위치에 새 요소를 삽입                         |

```C#
ArrayList list = new ArrayList(); //여러 데이터 타입을 넣을 수 있다.
list.Add(10);
list.Add(20);
list.Add(30);

list.RemoveAt(1); //1번 인덱스 20 삭제

list.Insert(1,25); //25를 1번 인덱스에 삽입
```

```C#
using System;
using System.Collections;

namespace UsingList
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList list = new ArrayList();
            for (int i=0; i<5; i++)
            {
                list.Add(i);
            };

            foreach (object obj in list)
                Console.Write($"{obj} ");
            Console.WriteLine();

            list.RemoveAt(2);

            foreach (object obj in list)
            {
                Console.Write($"{obj} ");
            }
            Console.WriteLine();

            list.Insert(2, 2);
            foreach (object obj in list)
                Console.Write($"{obj} ");
            Console.WriteLine();

            list.Add("abc");
            list.Add("def");

            for (int i = 0; i < list.Count; i++) //배열은 Length, 컬렉션은 Count속성
                Console.Write($"{list[i]} ");
            Console.WriteLine();
        }
    }
}
/*
0 1 2 3 4
0 1 3 4
0 1 2 3 4
0 1 2 3 4 abc def
 */
```



#### 2. Queue

* 데이터가 먼저 오면 그 데이터가 먼저 나감. 입력은 뒤에서 출력은 앞에서만 이루어짐.

```C#
Queue que = new Queue();
que.Enqueue(1); //Enqueue() 입력 메소드
que.Enqueue(2);
que.Enqueue(3);
que.Enqueue(4);
que.Enqueue(5);
// 출력은 Dequeue()
int a = que.Dequeue();
```



#### 3. Stack

* First In - Last Out : 먼저 들어온 데이터가 나중에 나감.

```C#
Stack stack = new Stack();
stack.Push(1);
stack.Push(2);
stack.Push(2);
```

```C#
using System;
using System.Collections;
namespace UsingStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5); //입력


            while (stack.Count > 0)
                Console.WriteLine(stack.Pop());  //출력
        }
    }
}
/*
5
4
3
2
1
 */
```



#### Hashtable

* 키(Key)와 값(Value)의 쌍으로 이루어진 데이터를 다룰 때 사용.
* 탐색속도 빠르고 사용 편리
* 해시테이블은 키를 이용해서 단번에 데이터가 저장된 컬렉션 내의 주소를 계산하는데 이 작업을 해싱(Hashing)이라고 한다.

```C#
using System;
using System.Collections;
using static System.Console;

namespace UsingHashtable
{   

    class Program
    {   
        static void Main(string[] args)
        {
            Hashtable ht = new Hashtable(); 
            ht["하나"] = "one"; // ht[키] = 값;
            ht["둘"] = "two";
            ht["셋"] = "three";
            ht["넷"] = "four";
            ht["다섯"] = "five";

            WriteLine(ht["하나"]);
            WriteLine(ht["둘"]);
            WriteLine(ht["셋"]);
            WriteLine(ht["넷"]);
            WriteLine(ht["다섯"]);
        }
    }
}
/*
one
two
three
four
five
 */
```



#### 컬렉션을 초기화하는 방법

```C#
int[] arr = {123, 456, 789};

ArrayList list = new ArrayList(arr); //123 ,456,789

Stack stack = new Stack(arr); //789, ,456, 123

Queue queue = new Queue(arr); //123,456,789

// ArrayList는 배열의 도움 없이 직접 컬렉션 초기자를 이용해서 초기화 할 수 있다.
ArrayList list2 = new ArrayList() {11, 22, 33}; <- 컬렉션 초기자
// 스택, 큐는 컬렉션 초기자 이용 x
    
// 해시테이블 초기화할때는 딕셔너리 초기자를 이용
Hashtable ht = new Hashtable()
{
    ["하나"] =1,  // ;가 아니라 ,
    ["둘"] =2,
    ["셋"] =3
}
```

