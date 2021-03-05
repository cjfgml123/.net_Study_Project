### 예외 처리하기

```C#
try
{
	//실행하고자 하는 코드    
}
catch(예외_객체1)
{
    //예외가 발생했을 때의 처리
}
catch(예외_객체2)
{
    //예외가 발생했을 때의 처리
}
```

```C#
using System;

namespace TryCatch
{   
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3 };

            try
            {
                for (int i=0; i<5; i++)
                {
                    Console.WriteLine(arr[i]);
                }
            }
            catch( IndexOutOfRangeException e)
            {
                Console.WriteLine($"예외가 발생했습니다 : {e.Message}");
            }

            Console.WriteLine("종료");
        }
    }
}
```



#### System.Exception 클래스

* C#에서 모든 예외 클래스는 반드시 System.Exception 클래스로부터 상속받아야 한다.

```C#
// 주의 사항
// 방법 1.
try
{
    
}
catch(IndexOutOfRangeException e)
{
    
}
catch(DivideByZeroException e)
{
    
}
//ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
//방법 2. 
try
{
    
}
catch (Exception e)
{
    
}
// 방법2. 가 안좋은 이유 : 모든 예외를 잡을 수 있지만 한 예외가 현재 코드가 아닌 상위 코드에서 처리해야 할 예외라면 문제가 발생
```

```C#
using System;

namespace Throw
{
    class Program
    {   
        static void DoSomething(int arg)
        {
            if (arg < 10)
                Console.WriteLine($"arg {arg}");
            else
                throw new Exception("arg가 10보다 큽니다."); //throw문을 통해 던져진 예외 객체는 catch문을 통해 받는다.
        }

        static void Main(string[] args)
        {
            try
            {
                DoSomething(1);
                DoSomething(3);
                DoSomething(5);
                DoSomething(9);
                DoSomething(11);
                DoSomething(13); // 위에서 예외가 발생하여 이 코드는 실행 x

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
/*
arg 1
arg 3
arg 5
arg 9
arg가 10보다 큽니다.
 */
```

```C#
// throw는 보통 문statement로 사용하지만 C#7.0부터는 식(expression)으로도 사용할 수 있도록 개선되었다.
using System;

namespace ThrowExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int? a = null; // a는 null이므로 b에 a를 할당하지 않고 throw 식이 실행된다.
                int b = a ?? throw new ArgumentNullException();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            } // catch (...) {}

            try
            {   // 조건 연산자 안에서도 사용할 수 있다.
                int[] array = new[] { 1, 2, 3 };
                int index = 4;
                int value = array[
                    index >= 0 && index < 3
                    ? index : throw new IndexOutOfRangeException()];
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
            } // catch (...) {}
        }
    }
}
```



#### try~catch와 finally

* try 절안에서 return문이나 throw문이나 뭐든 finally절은 반드시 실행된다.

```C#
static int Divide(int dividend, int divisor)
{
    try
    {
        Console.WriteLine("Divide() 시작");
        return dividend / divisor;
    }
    catch (DivideByZeroException e)
    {
        Console.WriteLine("Divide() 예외 발생");
        throw e;
    }
    finally
    {
        Console.WriteLine("Divide() 끝");
    }
}

```

* finally 안에서 예외가 또 발생하면? 

  * 예외를 받아주거나 처리해주는 코드가 없으므로 이 예외는 "처리되지 않은 예외" 가 된다.

    코드에서 예외가 발생하지 않도록하거나 이 안에서 try~catch 절을 사용한다. 



#### 사용자 정의 예외 클래스 만들기

* 모든 예외 객체는 System.Exception 클래스로 부터 파생되어야 하는데 이 규칙에 의거해서 Exception 클래스를 상속하기만 하면 새로운 예외 클래스를 만들 수 있다.

```C#
class MyException : Exception
{
    // ...
}
```



#### 예외 필터하기

* C#6.0 부터 catch절이 받아들일 예외 객체에 제약 사항을 명시해서 해당 조건을 만족하는 예외 객체에 대해서만 예외 처리 코드를 실행할 수 있도록 하는 예외 필터(Exception Filter)가 도입 되었다. 
* catch() 절 뒤에 when 키워드를 이용해서 제약 조건을 기술하면 된다.

```C#
class FilterableException : Exception 
{
	public int ErrorNo {get; set;}    
}

try
{
    int num = GetNumber();
    
    if (num < 0 || num > 10)
        throw new FilterableException() {ErrorNo - num};
    else
        Console.WriteLine($"Output : {num}");
}
catch (FilterableException e) when (e.ErrorNo < 0)
{
    Console.WriteLine("Negative input is not allowed.");
} //FilterableException의 객체를 받아오고 e.ErrorNo조건에서만
```



#### 



