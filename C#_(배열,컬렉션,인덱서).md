### 배열과 컬렉션, 그리고 인덱서

##### 1. 배열

```C#
데이터형식[] 배열이름 - new 데이터형식[용량];
```

##### 2. 배열을 초기화하는 3가지 방법

```C#
string[] array1 = new string[3]{"a","b","c"};
string[] array2 = new string[]{"a","b","c"}; //용량 생략
string[] array1 = {"a","b","c"};
```



<T>는 형식 매개 변수(Type Parameter)

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
| 인스턴스 메소드 | GetLenth()        | 배열에서 지정한 차원의 길이 반환,다차원 배열에서 사용        |
| 프로퍼티        | Length            | 배열의 길이 반환                                             |
| 프로퍼티        | Rank              | 배열의 차원 반환                                             |

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

