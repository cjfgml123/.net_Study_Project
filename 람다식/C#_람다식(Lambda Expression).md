### 1. 람다식(Lambda Expression)

- 익명 메소드를 만들기 위해 사용한다.
- 람다식으로 만드는 익명 메소드는 무명 함수(Anonymous Function)라는 이름으로 부릅니다.

```C#
매개변수_목록 => 식
    //=> 연산자는 "입력 연산자"
    			// 역할 : 매개변수를 전달
```

```C#
// 익명 메소드를 만들려면 대리자가 필요하다.
delegate int Calculate(int a, int b);

static void Main(string[] args)
{
    Calculate calc = (int a, int b) => a+b;
}

// Calculate 대리자로부터 형식을 유추 가능
static void Main(string[] args)
{
    Calculate calc = (a,b) => a+b;
}
```

```c#
delegate void DoSomething();
// ...
static void Main(string[] args)
{	
//매개변수가 없는 경우에는 ()에 아무것도 넣지 않는다.
	DoSomething DoIt = () =>
	{
		Console.WriteLine("뭔가를");
		Console.WriteLine("출력해보자.");
		Console.WriteLine("이렇게!");	
	};		// 문장 형식의 람다식은 {와 }로 둘러 싼다.
	DoIt();
}
```



#### 1-1. Func와 Action으로 더 간편하게 무명 함수 만들기

- 익명 메소드와 무명 함수는 코드를 더 간결하게 만들어주는 요소들이다. 대부분 단 하나의 익명 메소드나 무명 함수를 만들기 위해 매번 별개의 대리자를 선언해야 한다. 이것을 해결하기 위해 Func와 Action 대리자를 사용한다.
  - Func 대리자는 결과를 반환하는 메소드
  - Action 대리자는 결과를 반환하지 않는 메소드를 참조한다.



##### 1-1-1) Func 대리자

- 결과를 반환하는 메소드를 참조하기 위해 만들어졌다.

```C#
public delegate TResult Func<out Tresult>()
public delegate TResult Func<in T,out Tresult>(T arg)
public delegate TResult Func<in T1,in T2,out Tresult>(T1 arg1, T2 arg2)
    //...
//17개의 Func있다. 모든 Func 대리자의 형식 매개변수 중 가장 마지막에 있는 것이 반환 형식이다. "out 부분"
```

```C#
// 입력 매개변수가 없는 버전
Func<int> func1 = () => 10; // 입력 매개변수는 없으며, 무조건 10을 반환
Console.WriteLine(func1()); // 10 출력
```

```C#
// 매개변수가 하나 있는 버전
Func<int,int> func2 = (x) => x*2; //입력 매개변수는 int 하나, 반환 형식도 int
Console.WriteLine(func2(3));	// 6 출력
```

```C#
// 매개변수가 하나 있는 버전
Func<int,int> func3 = (x,y) => x+y; //입력 매개변수는 int 하나, 반환 형식도 int
Console.WriteLine(func2(2,3));	// 6 출력
```

#### 1-1-2) Action 대리자

- Action 대리자는 반환 형식이 없다.
- Func 대리자 처럼 17개 버전이 선언되어 있다.

```C#
public delegate void Action<>()
public delegate void Action<in T>(T arg)
public delegate void Action<in T1, in T2>(T1 arg1, T2 arg2)
public delegate void Action<in T1, in T2, in T3>(T1 arg1,T2 arg2,T3 arg3)
    // ...
```

```C#
// 매개변수가 아무것도 없는 Action의 사용 예
Action act1 = () => Console.WriteLine("Action()");
act1();

// 매개변수가 하나뿐인 버전 Action<T>
// result에 x*x의 결과를 저장한다.
int result = 0;
Action<int> act2 = (x) => result = x*x;

act2(3);
Console.WriteLine("result : {0}", result); //9를 출력

//매개변수가 두 개인 Action<T1,T2> 대리자의 사용 예
Action<double,double> act3 = (x,y) =>
{
    double pi = x/y;
    Console.WriteLine("Action<T1, T2>({0},{1} : {2})",x,y,pi);
};
act3(22.0,7.0);
```

