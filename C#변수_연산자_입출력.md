# C# 기본 문법 및 주의 사항

* 한줄 주석 : //
* 다중 주석 : /* ~ */



```C#
char a = '안'; // 한글자는 작은 따옴표 하나로 안하면 에러
string b = "안녕"; // 두글자 이상은 큰따옴표
```



```c#
//문자열 -> 숫자
int a = int.Parse("123");
float b = 3.14f; //float 형은 뒤에 f 붙이기 안하면 에러
double c = 3.14; // 안붙여도 됨 
float b = float.Parse("3.14");

//숫자 -> 문자열
float e = 123.45;
string f = e.ToString(); 

const int a = 21; 
/* const -> 상수선언 , 값 변경하면 에러남
   ex) const int a = 21;
   	   int a = 4; 에러 발생 */

var a = 21; // 컴파일러가 알아서 int형으로 인식
var b = "Hi"; // 알아서 문자열로 인식
//var : object타입이랑 비슷함, object: 박싱과 언박싱 참조 개념 있음

//출력
Console.Write("줄바꿈 안함");
Console.WriteLine("줄바꿈 함");
Console.Write("Type : {0}, Value : {1}", a.GetType(), a);

//입력
input = Console.ReadLine();

    
// 열거 사용법 : enum 열거형식명 : 기반자료형 {상수1, 상수2, 상수3, ,,,}
enum Result {YES, NO , OK }
Console.WriteLine(Result.YES);

// Nullable 형식 : ? 사용
// 데이터형식? 변수이름;
int a = null;
Console.WriteLine(a.HasValue); // false 출력
a = 12;
Console.WriteLine(a.Value); //true 출력

```



#### 연산자

```C#
//조건 연산자 : 조건식(==(이런것들)) ? 참일때의값 : 거짓일때의 값
string result = (10 % 2) == 0 ? "짝수" : "홀수";
```

