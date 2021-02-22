## 메소드

```c#
/*
class 클래스이름
{
    한정자 반환_형식 메소드의_이름(매개변수)
        코드
        
        return 메소드의 결과; // 메소드의 결과의 데이터 형식은 메소드의 반환 형식과 일치
} */


```

```C#
using System;

namespace Method
{
    class Calculator //계산 클래스
    {
        public static int Plus(int a, int b)
        {
            return a + b;
        }

        public static int Minus(int a, int b)
        {
            return a - b;
        }
    }
    class Program
    {   
        static void Main(string[] args)
        {
            int result = Calculator.Plus(3, 4);
            int result1 = Calculator.Minus(7, 5);

            Console.WriteLine("합 : {0} 차 : {1}", result, result1);
        }
    }
}
```

#### ref 키워드 : 참조에 의한 매개변수 전달, 다중 매개변수 반환할때 사용

* 값에 의한 전달(Call by Value)이 안될때 참조에 의한 전달(Call by Reference)를 해야 할때 사용

```C#
using System;

namespace Method
{
    class Program
    {   
          public static void Swap1(int a,int b)
        {
            int temp = b;
            b = a;
            a = temp;
        }
        
        public static void Swap2(ref int a,ref int b)
        {
            int temp = b;
            b = a;
            a = temp;
        }
        
        static void Main(string[] args)
        {
            int x = 2;
            int y = 3;
            
            Swap1(x,y); // 2,3 그대로 출력 스왑 안됨
            Swap2(ref x,ref y); // 스왑 됨  ref 키워드 중요 
            }
    }
}

```

* 다중 출력 전용 매개 변수(메소드 사용 할때)

```C#
using System;

namespace Method
{
    
    class Program
    {   
        public static void Divide(int a, int b ,ref int c,ref int d) //out int c, out int d
        {
            c = a / b;
            d = a % b;
        }
        static void Main(string[] args)
        {
            int x = 20;
            int y = 5;
            int z = 0;  // 주의 
            int g = 0; 	// 주의
            Divide(x, y ,ref z,ref g);  // 주의, out z, out g
            Console.WriteLine("{0},{1}",z,g);
        }
    }
}
/*
out 키워드는 
int z;
int g; 이렇게 선언만해도 실행됨. 출력 전용 키워드
*/
```



### 오버로딩

```c#
using System;

namespace Overloading
{
    class Program
    {   
        static int Plus(int a, int b)
        {
            return a + b;
        }

        static int Plus(int a, int b, int c)
        {
            return a + b + c;
        }

        static float Plus(float a, float b)
        {
            return a + b;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Plus(1,2));
            Console.WriteLine(Plus(1,2,3));
            Console.WriteLine(Plus(1.2f,1.3f));

        }
    }
}
```



### 가변길이 매개 변수

```C#
using System;

namespace UsingParams
{
    class MainApp
    {   
        static int Sum(params int[] args) //params :Sum()메소드에 입력한 모든 값들은
            							 // args 배열에 담김
        {
            Console.Write("Summing...");
            int sum = 0;

            for(int i=0; i<args.Length; i++)
            {
                if (i > 0)
                    Console.Write(",");
                Console.Write(args[i]);
                sum += args[i];
            }
            Console.WriteLine();
            return sum;
        }
        static void Main(string[] args)
        {
            int sum = Sum(1, 2, 3, 4, 5); // params로 몇개의 파라미터들 넣어도됨
            Console.WriteLine("Sum : {0}", sum);
        }
    }
}
```

### 명명된 매개 변수

```C#
using System;

namespace NamedParameter
{
    class Program
    {   
        static void PrintProfile(string name, string phone)
        {
            Console.WriteLine("Name:{0}, Phone:{1}", name, phone);
        }
        static void Main(string[] args)
        {
            PrintProfile(name: "박찬호", phone: "010-4188-2343");
        }
    }
}
```

