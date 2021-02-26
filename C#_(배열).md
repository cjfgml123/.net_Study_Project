### 배열과 컬렉션, 그리고 인덱서

##### 1. 배열

```C#
데이터형식[] 배열이름 = new 데이터형식[ 용량 ];
```

##### 2. 배열을 초기화하는 3가지 방법

```C#
string[] array1 = new string[3]{"a","b","c"};
string[] array2 = new string[]{"a","b","c"}; //용량 생략
string[] array1 = {"a","b","c"};
```



<T>는 형식 매개 변수(Type Parameter)

ex) Array.Sort(배열이름);

| 분류            | 이름              | 설명                                                         |
| --------------- | ----------------- | ------------------------------------------------------------ |
| 정적 메소드     | Sort()            | 배열을 정렬                                                  |
| 정적 메소드     | BinarySearch<T>() | 이진 탐색 수행                                               |
| 정적 메소드     | IndexOf()         | 배열에서 특정 데이터의 인덱스를 반환                         |
| 정적 메소드     | TrueForAll<T>()   | 배열의 모든 요소가 지정한 조건에 부합하는지 그 여부를 반환   |
| 정적 메소드     | FindIndex<T>()    | 배열에서 지정한 조건에 부합하는 첫 번째 요소의 인덱스를 반환한다. |
| 정적 메소드     | Resize<T>()       | 배열 크기 재조정                                             |
| 정적 메소드     | Clear()           | 배열의 모든 요소 초기화, 숫자형식 : 0 , 논리형식:false, 참조형식 : null로 초기화 |
| 정적 메소드     | ForEach<T>()      | 배열의 모든 요소에 대해 동일한 작업을 수행                   |
| 정적 메소드     | Copy<T> ()        | 배열의 일부를 다른 배열에 복사한다.                          |
| 인스턴스 메소드 | GetLenth()        | 배열에서 지정한 차원의 길이 반환,다차원 배열에서 사용        |
| 프로퍼티        | Length            | 배열의 길이 반환                                             |
| 프로퍼티        | Rank              | 배열의 차원 반환                                             |

```C#
using System;

namespace MoreOnArray
{   
    class Program
    {   
        private static bool CheckPassed(int score)
        {
            return score >= 60;
        }

        private static void Print(int value)
        {
            Console.Write($"{value} "); // 문자열 보간
        }

        static void Main(string[] args)
        {
            int[] scores = new int[] { 80,74,81,90,34};

            foreach (int score in scores)
                Console.Write($"{score} ");
            Console.WriteLine();

            Array.Sort(scores); //정렬
            Array.ForEach<int>(scores, new Action<int>(Print));

            Console.WriteLine();
            
            
            Console.WriteLine("Number of dimensions:{0}", scores.Rank);

            Console.WriteLine("Binary Search : 81 is at {0}", Array.BinarySearch<int>(scores, 81)); //이진탐색 수행
            Console.WriteLine("Lineary Search : 90 is at {0}", Array.IndexOf(scores, 90));

            Console.WriteLine("Everyone passed ? : {0}", Array.TrueForAll<int>(scores, CheckPassed));

            int index = Array.FindIndex<int>(scores, delegate (int score) // 익명메소드는 나중에
            {
                if (score < 60)
                    return true;
                else
                    return false;
            });

            scores[index] = 61;
            Console.WriteLine("Everyone passed ?: {0}", Array.TrueForAll<int>(scores, CheckPassed));

            Console.WriteLine("Old length of scores : {0}", scores.GetLength(0));

            Array.Resize<int>(ref scores, 10); //5였던 배열의 용량을 10으로 재조정

            Console.WriteLine("New length of scores : {0}", scores.Length);

            Array.ForEach<int>(scores, new Action<int>(Print)); // Action : 델리게이트
            Console.WriteLine();

            Array.Clear(scores, 3, 7);

            Array.ForEach<int>(scores, new Action<int>(Print));
            Console.WriteLine();

            int[] sliced = new int[3];
            Array.Copy(scores, 0, sliced, 0, 3); //scores배열의 0번째 부터 3개 요소를 sliced 배열의 0번째~2번째 요소에 차례대로 복사
            Array.ForEach<int>(sliced, new Action<int>(Print));
            Console.WriteLine();
        }
    }
}
/*
80 74 81 90 34
34 74 80 81 90
Number of dimensions:1
Binary Search : 81 is at 3
Lineary Search : 90 is at 4
Everyone passed ? : False
Everyone passed ?: True
Old length of scores : 5
New length of scores : 10
61 74 80 81 90 0 0 0 0 0
61 74 80 0 0 0 0 0 0 0
61 74 80
*/
```



#### 3. 정적 메소드와 인스턴스 메소드

```C#
// 정적 메소드(static Method) 는 클래스의 인스턴스를 생성하지 않아도 호출 가능한 메소드를 말한다. static 키워드 사용
class MyClass 
{
    public static void StaticMethod()
    {
        // ...
    }
}

// 호출 예시 MyClass.StaticMethod(); -> 인스턴스 생성없이 바로 호출 가능
```

```C#
// 인스턴스 메소드(Instance Method)는 이름처럼 클래스의 인스턴스를 생성해야만 호출할 수 있는 메소드를 말함.
class MyClass
{
    public void InctanceMethod()
    {
        // ...
    }
}
// 호출 예시 MyClass obj = new MyClass();
// obj.InctanceMethod();
```

#### 4. 마지막 인덱스 접근방법

```C#
// 기존 방법
int[] scores = new int[5];
scores[scores.Length-1] = 34; // 마지막 인덱스 : score배열길이 - 1

// C# 8.0부터 나온것
// System.Index형식 사용
System.Index last = ^1; // scores.Length-1을 의미함.
scores[last] = 34; // == scores[scores.Length-1] = 34;

// 또 다른 방법
scores[^1] = 34; // == scores[scores.Length-1] = 34;
```

#### 5. 배열 분할하기(System.Range 객체)

```C#
// 방법1. 
System.Range r1 = 0..3; //시작 인덱스 ~ 마지막 인덱스-1
int[] sliced = scores[r1]; //[]사이에 인덱스 대신 System.Range객체를 입력하면 분할된 배열이 반환된다.

// 방법2.
int[] sliced2 = scores[0..3]; //인덱스 0~2 까지 출력 

// 첫 번째 0 부터 세 번째 2 요소까지
int[] sliced3 = scores[..3];

// 두 번째 1 요소부터 마지막 요소 까지
int[] sliced4 = scores[1..];

// 전체
int[] sliced5 = scores[..];

// System.Range객체를 생성할 때 System.Index 객체를 이용
// 방법1.
System.Index idx = ^1;
int[] sliced5 = scores[..idx]; //마지막에서 2번째까지 분할
// 방법2.
int[] sliced6 = scores[..^1]; //마지막에서 2번째까지 분할
```



### 2차원 배열

```C#
데이터형식[,] 배열이름 = new 데이터형식[2차원길이, 1차원길이];
// 각 차원의 용량 or 길이를 콤마(,)로 구분해서 [ 와 ] 사이에 입력
//ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

int[ , ] array = new int[2,3];
// 길이가 3인 1차원 배열을 원소로 2개 갖고 있는 2차원 배열 ->2행 3열
array[0,0] = 1;
array[0,1] = 2;
array[0,2] = 3;
array[1,0] = 4;
array[1,1] = 5;
array[1,2] = 6;
Console.WriteLine( array[0,2]); //3 출력

//ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
// 인덱스는 (0,0) (0,1) (0,2) (1,0) (1,1) (1,2)
// 길이가 3인 1차원 배열을 원소로 2개 갖고 있는 2차원 배열
// 세 가지 초기화 방법
int[,] arr = new int[2,3] {{1,2,3},{4,5,6}}; //형식과 길이 명시
int[,] arr2 = new int[,] {{1,2,3},{4,5,6}}; // 길이 생략
int[,] arr3 = {{1,2,3},{4,5,6}}; //형식과 길이 생략
```

```C#
using System;

namespace _2DArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] arr = new int[2, 3] { { 1, 2, 3, }, { 4, 5, 6 } };
            /*
            int[,] arr1 = new int[,] { { 1, 2, 3, }, { 4, 5, 6 } };
            int[,] arr2 = { { 1, 2, 3, }, { 4, 5, 6 } };
            */

            for (int i=0; i<arr.GetLength(0); i++) //GetLength(0) :1차원 길이 여기서는 2를 나타냄
            {
                for (int j=0; j<arr.GetLength(1); j++) //GetLength(1) : 2차원 길이 여기서는 3을 나타냄
                {
                    Console.Write($"[{i},{j}]:{arr[i, j]}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
/*
[0,0]:1[0,1]:2[0,2]:3
[1,0]:4[1,1]:5[1,2]:6
 */

```



#### 다차원 배열

* 차원이 둘 이상인 배열을 말한다. 선언방법은 2차원 배열 문법과 같다.

```C#
int [,,] = new int[4,3,2]
{
    {{1,2}, {3,4}, {5,6}},
    {{1,4},{2,5},{3,6}},
    {{6,5},{4,3},{2,1}},
    {{6,3},{5,2},{4,1}}
};
// 2차원 배열을 요소로 4개 갖는  3차원 배열이라고 이해.
// 출력할땐 당연히 for문 3개 : GetLength(0),GetLength(1),GetLength(2)

```



#### 가변 배열(Jagged Array)

* 다양한 길이의 배열을 요소로 갖는 다차원 배열

```C#
데이터형식[][] 배열이름 = new 데이터형식[가변 배열의 용량][];
//2차원 배열이랑 헷갈x, 2차원배열은 [,] , 가변배열은 [][]

//방법1.
int[][] aa = new int[3][];
aa[0] = new int[5] {1,2,3,4,5};
aa[1] = new int[]{10,20,30};
aa[2] = new int[]{100,200};

//방법2.
int[][] aa = new int[2][]{
    new int[] {1000, 2000},
    new int[4] {6,7,8,9}};
```

```c#
using System;

namespace JaggedArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] aa = new int[3][];
            aa[0] = new int[5] { 1, 2, 3, 4, 5 };
            aa[1] = new int[] { 10, 20, 30 };
            aa[2] = new int[] { 100, 200 };

            foreach( int[] arr in aa)
            {
                Console.Write($"Length : {arr.Length}, ");
                foreach(int e in arr)
                {
                    Console.Write($"{e} ");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");

        }
    }
}
/*
Length : 5, 1 2 3 4 5
Length : 3, 10 20 30
Length : 2, 100 200
 */
```

