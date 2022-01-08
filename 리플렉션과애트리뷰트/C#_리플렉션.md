### 1. 리플렉션

#### 1-1) Object.GetType() 메소드와 Type 클래스

- 객체를 X-Ray 사진처럼 객체의 형식(Type) 정보를 들여다보는 기능이다.
- 이 기능을 이용하면 우리는 프로그램 실행 중에 객체의 형식 이름부터 프로퍼티 목록, 메소드 목록, 필드, 이벤트 목록까지 모두 열어볼 수 있다.

```C#
//Object.GetType() 메소드와 Type형식을 사용하는 방법
int a = 0;
Type type = a.GetType();
FieldInfo[] fields = type.GetFields(); // 필드 목록 조회

foreach(FieldInfo field in fields)
    Console.WriteLine("Type:{0}, Name:{1}",field.FieldType.Name,field.Name);
```

| 메소드                | 반환 형식         | 설명                                             |
| --------------------- | ----------------- | ------------------------------------------------ |
| GetConstructors()     | ConstructorInfo[] | 해당 형식의 모든 생성자 목록을 반환한다.         |
| GetEvents()           | EventInfo[]       | 해당 형식의 이벤트 목록을 반환한다.              |
| GetFields()           | FieldInfo[]       | 해당 형식의 필드 목록을 반환한다.                |
| GetGenericArguments() | Type[]            | 해당 형식의 형식 매개변수 목록을 반환한다.       |
| GetInterfaces()       | Type[]            | 해당 형식이 상속하는 인터페이스 목록을 반환한다. |
| GetMembers()          | MemberInfo[]      | 해당 형식의 멤버 목록을 반환한다.                |
| GetMethods()          | MethodInfo[]      | 해당 형식의 메소드 목록을 반환한다.              |
| GetNestedTypes()      | Type[]            | 해당 형식의 내장 형식 목록을 반환한다.           |
| GetProperties()       | PropertyInfo[]    | 해당 형식의 프로퍼티 목록을 반환한다.            |

- 앞의 표에 있는 GetFields()나 GetMethods() 같은 메소드는 검색 옵션을 지정할 수 있다.
- 검색 옵션은 System.Reflection.BindingFlags 열거형을 이용해서 구성된다.

```C#
// BindingFlags 열거형을 이용해서 GetFields() 에 검색 옵션을 입력하는 예제 코드
Type type = a.GetType();

// public 인스턴스 필드 조회
var fields1 = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

// 비 public 인스턴스 필드 조회
var fields2 = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

// public 정적 필드 조회
var fields3 = type.GetFields(BindingFlags.Public | BindingFlags.Static);

// 비 public 정적 필드 조회
var fields4 = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
```

```c#
// 필드 , 메소드, 프로퍼티, 인터페이스 목록 뽑아내기
using System;
using System.Reflection;

namespace GetType
{   
    class MainApp
    {   
        static void PrintInterfaces(Type type)
        {
            Console.WriteLine("-------------Interfaces ---------");

            Type[] interfaces = type.GetInterfaces();
            foreach (Type i in interfaces)
                Console.WriteLine("Name:{0}",i.Name);

            Console.WriteLine();
        }

        static void PrintFields(Type type)
        {
            Console.WriteLine("--------------- Fields ---------------");

            FieldInfo[] fields = type.GetFields(
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                String accessLevel = "protected";
                if (field.IsPublic) accessLevel = "public";
                else if (field.IsPrivate) accessLevel = "private";

                Console.WriteLine("Access:{0}, Type:{1}, Name:{2}",accessLevel,field.FieldType.Name,field.Name);
            }
            Console.WriteLine();
        }

        static void PrintMethods(Type type)
        {
            Console.WriteLine("----------------------Methods --------------------");

            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                Console.Write("Type:{0}, Name:{1}, Parameter:",method.ReturnType.Name,method.Name);

                ParameterInfo[] args = method.GetParameters();
                for(int i = 0; i<args.Length; i++)
                {
                    Console.Write("{0}",args[i].ParameterType.Name);
                    if (i < args.Length - 1)
                        Console.Write(", ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void PrintProperties(Type type)
        {
            Console.WriteLine("---------------Properties---------------------");
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
                Console.WriteLine("Type:{0}, Name{1}", property.PropertyType.Name, property.Name);
            Console.WriteLine();
        }


        static void Main(string[] args)
        {
            int a = 0;
            Type type = a.GetType();

            PrintInterfaces(type);
            PrintFields(type);
            PrintProperties(type);
            PrintMethods(type);
        }
    }
}

/*
-------------Interfaces ---------
Name:IComparable
Name:IConvertible
Name:IFormattable
Name:IComparable`1
Name:IEquatable`1
Name:ISpanFormattable

--------------- Fields ---------------
Access:private, Type:Int32, Name:m_value
Access:public, Type:Int32, Name:MaxValue
Access:public, Type:Int32, Name:MinValue

---------------Properties---------------------

----------------------Methods --------------------
Type:Int32, Name:CompareTo, Parameter:Object
Type:Int32, Name:CompareTo, Parameter:Int32
Type:Boolean, Name:Equals, Parameter:Object
Type:Boolean, Name:Equals, Parameter:Int32
Type:Int32, Name:GetHashCode, Parameter:
Type:String, Name:ToString, Parameter:
Type:String, Name:ToString, Parameter:String
Type:String, Name:ToString, Parameter:IFormatProvider
Type:String, Name:ToString, Parameter:String, IFormatProvider
Type:Boolean, Name:TryFormat, Parameter:Span`1, Int32&, ReadOnlySpan`1, IFormatProvider
Type:Int32, Name:Parse, Parameter:String
Type:Int32, Name:Parse, Parameter:String, NumberStyles
Type:Int32, Name:Parse, Parameter:String, IFormatProvider
Type:Int32, Name:Parse, Parameter:String, NumberStyles, IFormatProvider
Type:Int32, Name:Parse, Parameter:ReadOnlySpan`1, NumberStyles, IFormatProvider
Type:Boolean, Name:TryParse, Parameter:String, Int32&
Type:Boolean, Name:TryParse, Parameter:ReadOnlySpan`1, Int32&
Type:Boolean, Name:TryParse, Parameter:String, NumberStyles, IFormatProvider, Int32&
Type:Boolean, Name:TryParse, Parameter:ReadOnlySpan`1, NumberStyles, IFormatProvider, Int32&
Type:TypeCode, Name:GetTypeCode, Parameter:
Type:Type, Name:GetType, Parameter:
*/
```

#### 1-2) 리플렉션을 이용해서 객체 생성하고 이용하기

- 리플렉션을 이용해서 동적으로 인스턴스를 만들기 위해서는 System.Activator 클래스의 도움이 필요하다. 인스턴스를 만들고자 하는 형식의 Type 객체를 매개변수에 넘기면, Activator.CreateInstance() 메소드는 입력받은 형식의 인스턴스를 생성하여 반환한다.

```
object a = Activator.CreateInstance(typeof(int));
```

```C#
// PropertyInfo.SetValue() 메소드를 이용하여 동적으로 프로퍼티에 값을 기록하고 읽기
class Profile
{
    public string Name {get; set; }
    public string Phone{get; set;}
}

static void Main()
{
    Type type = typeof(Profile);
    Object profile = Activator.CreateInstance(type);
    
    PropertyInfo name = type.GetProperty("Name");
    PropertyInfo phone = type.GetProperty("Phone");
    
    name.SetValue(profile, "박찬호", null);
    phone.SetValue(profile, "997-5511", null);
    
    Console.WriteLine("{0},{1}",name.GetValue(profile, null),
                     phone.GetValue(profile, null));
}
//이 예제에서 SetValue()와 GetValue()의 마지막 인수가 null인 이유는 PropertyInfo 클래스는 프로퍼티뿐 아니라 인덱서의 정보도 담을 수 있는데 SetValue()와 GetValue()의 마지막 인수는 인덱서의 인덱스를 위해 사용된다. 프로퍼티는 인덱서가 필요없으므로 이 예제에서는 null로 할당한 것.
```

```C#
//MethodInfo 클래스를 이용해서 메소드를 동적으로 호출하는 예제
using System;
using System.Reflection;

namespace DynamicInstance
{   
    class Profile
    {
        private string name;
        private string phone;
        public Profile()
        {
            name = ""; phone = "";
        }

        public Profile(string name, string phone)
        {
            this.name = name;
            this.phone = phone;
        }

        public void Print()
        {
            Console.WriteLine($"{name},{phone}");
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
    }
    class MainApp
    {
        static void Main(string[] args)
        {
            Type type = Type.GetType("DynamicInstance.Profile");
            MethodInfo methodInfo = type.GetMethod("Print");

            PropertyInfo nameProperty = type.GetProperty("Name");
            PropertyInfo phoneProperty = type.GetProperty("Phone");

            object profile = Activator.CreateInstance(type, "박상현", "512-1234");
            // null 인수가 오는 자리에는 Invoke()메소드가 호출할 메소드의 인수가 와야한다.
            // Print()메소드의 인수가 없으므로 null을 넘기는 것이다.
            methodInfo.Invoke(profile, null);

            profile = Activator.CreateInstance(type);
            nameProperty.SetValue(profile,"박찬호",null);
            phoneProperty.SetValue(profile, "997-5511", null);

            Console.WriteLine("{0},{1}",nameProperty.GetValue(profile,null),phoneProperty.GetValue(profile,null));
        }
    }
}
/*
박상현,512-1234
박찬호,997-5511
*/

```

