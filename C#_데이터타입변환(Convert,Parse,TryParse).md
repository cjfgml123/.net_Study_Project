### C# 데이터 타입 변환 함수 차이

```C#
// Convert.ToInt32 vs Int32.Parse vs Int32.TryParse
// 테스트를 위한 string 변수 생성 및 다양한 값 할당
string a = "123";
string b = "0.1";
string c = null;
// Convert.ToInt32
Convert.ToInt32(a); // 123
Convert.ToInt32(b); // FormatException
Convert.ToInt32(c); // 0
// Int32.Parse
Int32.Parse(a); // 123
Int32.Parse(b); // FormatException
Int32.Parse(c); // ArgumentNullException
// Int32.TryParse
int i;
Int32.TryParse(a, out i); // true (123)
Int32.TryParse(b, out i); // false (0)
Int32.TryParse(c, out i); // false (0)
// Convert 와 Parse의 차이는 파라미터가 null 일때 0을 반환하는지, 예외처리를 해주는지의 차이이다.
// 상황에 맞게 사용해야 하지만 가급적이면 안전하게 사용하기 위해서 자체 예외 핸들링을 해주는 TryParse 를 쓰도록 하자.

```





