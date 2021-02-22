#### 프로퍼티 (get, set) -> 접근자(accessor)

```C#
class 클래스이름
{
    데이터형식 필드이름;
    접근한정자 데이터형식 프로퍼티이름
    {
        get
        {
            return 필드이름;
        }
        set
        {
            필드이름 = value; //value 키워드는 C# 컴파일러가 set접근자의 암묵적 매개 변수로 간주해서 문제 x
        }
    }
}
```

```C#
using System;

namespace Property
{   
    class BirthdayInfo
    {
        private string name; //데이터형식 필드이름;
        private DateTime birthday;

        public string Name  //접근한정자 데이터형식 프로퍼티이름
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }

        public int Age
        {
            get
            {
                return new DateTime(DateTime.Now.Subtract(birthday).Ticks).Year; //Subtract() : 값을 빼는 메소드
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            BirthdayInfo birth = new BirthdayInfo();
            birth.Name = "서현";
            birth.Birthday = new DateTime(1991, 6, 28);

            Console.WriteLine("Name : {0}", birth.Name);
            Console.WriteLine("Birthday : {0}", birth.Birthday.ToShortDateString());
            Console.WriteLine("Age : {0}", birth.Age);
        }
    }
}
/*
Name : 서현
Birthday : 1991-06-28
Age : 30
 */
```

#### 자동 구현 프로퍼티

* 필드를 선언할 필요도 없다. 
* get, set 뒤에 세미콜론만 붙여 주면 된다.

```C#
using System;

namespace AutoImplementedProperty
{   
    class BirthdayInfo
    {   // 필드 선언 필요 없다.
        public string Name  //접근한정자 데이터형식 프로퍼티이름
        {
            get;
            set;   
        }

        public DateTime Birthday 
        {
            get;
            set;
        }

        public int Age
        {
            get
            {
                return new DateTime(DateTime.Now.Subtract(Birthday).Ticks).Year; //Subtract() : 값을 빼는 메소드
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            BirthdayInfo birth = new BirthdayInfo();
            birth.Name = "서현";
            birth.Birthday = new DateTime(1991, 6, 28);

            Console.WriteLine("Name : {0}", birth.Name);
            Console.WriteLine("Birthday : {0}", birth.Birthday.ToShortDateString());
            Console.WriteLine("Age : {0}", birth.Age);
        }
    }
}
/*
Name : 서현
Birthday : 1991-06-28
Age : 30
 */
```

#### 프로퍼티와 생성자

* 프로퍼티를 이용한 필드 초기화

```C#
클래스이름 인스턴스 = new 클래스이름()
{
    프로퍼티1 = 값,
    프로퍼티2 = 값,// ;이 아닌 ,다.
    프로퍼티3 = 값 
}
```

```C#
using System;

namespace ConstructorWithProperty
{   
    class BirthdayInfo
    {   // 필드 선언 필요 없다.
        public string Name  //접근한정자 데이터형식 프로퍼티이름
        {
            get;
            set;   
        }

        public DateTime Birthday 
        {
            get;
            set;
        }

        public int Age
        {
            get
            {
                return new DateTime(DateTime.Now.Subtract(Birthday).Ticks).Year; //Subtract() : 값을 빼는 메소드
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            BirthdayInfo birth = new BirthdayInfo()
            {
                Name = "서현", // ;아닌 ,
                Birthday = new DateTime(1991, 6, 28)
            };
          

            Console.WriteLine("Name : {0}", birth.Name);
            Console.WriteLine("Birthday : {0}", birth.Birthday.ToShortDateString());
            Console.WriteLine("Age : {0}", birth.Age);
        }
    }
}
/*
Name : 서현
Birthday : 1991-06-28
Age : 30
 */
```



#### 무명형식(Anonymous Type) : 이름이 없는 형식

* 형식의 선언과 동시에 인스턴스를 할당하기 때문에 인스턴스를 만들고 다시는 사용하지 않을 때 요긴함.
* 무명 형식의 프로퍼티에 할당된 값은 변경 불가 -> 읽기 전용, LINQ와 함께 많이 사용함. 

```C#
using System;

namespace AnonymousType
{   
    class Program
    {
        static void Main(string[] args)
        {
            var a = new { Name = "박상현", Age = 123 };
            Console.WriteLine("Name:{0}, Age:{1}", a.Name, a.Age);


            var b = new { Subject = "수학", Scores = new int[] {90,80,70} };
            Console.Write("Subject:{0}, Scores:", b.Subject);
            foreach (var score in b.Scores)
                Console.Write("{0} ", score);
            Console.WriteLine();
        }
    }
}
/*
Name:박상현, Age:123
Subject:수학, Scores:90 80 70
 */
```

#### 인터페이스의 프로퍼티

* 인터페이스는 메소드, 프로퍼티, 인덱서 가질 수 있다. 상속하는 클래스에서 당연히 구현해야함.
* 인터페이스에서는 구현 x , 자동 구현(get; , set;)과 모습 동일

```C#
using System;

namespace PropertiesInInterface
{   
    interface INamedValue
    {
        string Name // c# 컴파일러는 인터페이스 프로퍼티는 구현 안해준다.
        {
            get;
            set;
        }

        string Value
        {
            get;
            set;
        }
    }

    class NamedValue : INamedValue
    {
        public string Name
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            NamedValue name = new NamedValue()
            { Name = "이름", Value = "박상현" };


            NamedValue height = new NamedValue() //인터페이스는 인스턴스화 못함
            { Name = "키", Value = "177cm" };


            NamedValue weight = new NamedValue()
            { Name = "몸무게", Value = "90kg" };

            Console.WriteLine("{0} : {1}", name.Name, name.Value);
            Console.WriteLine("{0} : {1}", height.Name, height.Value);
            Console.WriteLine("{0} : {1}", weight.Name, weight.Value);

        }
    }
}
/*
이름 : 박상현
키 : 177cm
몸무게 : 90kg
*/
```

#### 추상 클래스와 프로퍼티

* 추상 클래스는 클래스처럼 구현된 프로퍼티를 가질 수도 있고 인터페이스 처럼 구현되지 않은 프로퍼티도 가질 수 있다.

  ```C#
  abstract class 추상클래스이름
  {
      abstract 데이터형식 프로퍼티이름
      {
          get;
          set;
      }
  }
  ```

  ```C#
  using System;
  
  namespace PropertiesInAbstractClass
  {   
      abstract class Product
      {
          private static int serial = 0;
          public string SerialID //구현을 가진 프로퍼티
          {
              get { return String.Format("{0:d5}", serial++);  }
          }
  
          abstract public DateTime ProductDate
          {
              get;
              set;
          }
      }
      class MyProduct : Product
      {
          public override DateTime ProductDate
          {
              get;
              set;
          }
      }
      class Program
      {
          static void Main(string[] args)
          {
              Product product_1 = new MyProduct()
              { ProductDate = new DateTime(2010, 1, 10) };
  
              Console.WriteLine("Product:{0}, Product Date : {1}",
                  product_1.SerialID,
                  product_1.ProductDate);
  
              Product Product2 = new MyProduct()
              { ProductDate = new DateTime(2010, 2, 3) };
  
              Console.WriteLine("Product:{0}, Product Date:{1}", Product2.SerialID,
                  Product2.ProductDate);
  
  
          }
      }
  }
  /*
  Product:00000, Product Date : 2010-01-10 오전 12:00:00
  Product:00001, Product Date:2010-02-03 오전 12:00:00
   */
  ```

  