# C# If , Switch, While, for,foreach

* if, for 문은 세미콜론 x , while문은 세미콜론 o

```C#
using System;

namespace IfElse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("숫자를 입력하세요. :");

            string input = Console.ReadLine();
            int number = int.Parse(input);

            if (number < 0)
                Console.WriteLine("음수");
            else if (number > 0)
                Console.WriteLine("양수");
            else
                Console.WriteLine("0");
//ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
            if (number % 2 == 0)
                Console.WriteLine("짝수");
            else
                Console.WriteLine("홀수");

        }
    }
}
// 10 입력하면 양수,짝수 출력
```

```C#
using System;

namespace IfElse
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 5;
            if (number < 0) {
                Console.Write("음수");
            }
            else {
                Console.Write("양수");
            }
        }
    }
}
```



```c#
//스위치문
using System;

namespace IfElse
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 1;
            switch (a)
            {
                case 1:
                    Console.WriteLine("하나");
                    break;
                case 2:
                    Console.WriteLine("둘");
                    break;
                case 3:
                    Console.WriteLine("셋");
                    break;
                default:
                    Console.WriteLine("x");
                    break;

            }

        }
    }
}


```

#### While

```C#
//while 은 세미콜론 x, do while 은 세미콜론o

while (i>0)
{
    Console.WriteLine(i);
}
//ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
do
{
    Console.WriteLine(a);
    a -=2;
}
while (a > 0);
```

### for

```c#
 static void Main(string[] args)
        {
            for (int i=0; i<5; i++)
            {
                for (int j=0; j<=i; j++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
        }
```

### foreach

```C#
/* foreach (데이터형식 변수명 in 배열 또는 컬렉션)
	코드 */
int[] arr = new int[] { 0, 1, 2, 3, 4 }; // 배열선언

 foreach (int a in arr)
{
  Console.WriteLine(a);
}

```



