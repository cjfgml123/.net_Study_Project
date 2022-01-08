### 중첩 클래스

* 클래스 외부에 공개하고 싶지 않은 형식을 만들고자 할 때
* 현재의 클래스의 일부분처럼 표현할 수 있는 클래스를 만들고자 할 때 사용

```C#
using System;
using System.Collections.Generic;

namespace NestedClass // 중첩클래스
{   
    class Configuration
    {
        List<ItemValue> listConfig = new List<ItemValue>();

        public void SetConfig(string item, string value)
        {
            ItemValue iv = new ItemValue();
            iv.SetValue(this, item, value);
        }

        public string GetConfig(string item)
        {
            foreach (ItemValue iv in listConfig)
            {
                if (iv.GetItem() == item)
                    return iv.GetValue();
            }
                return "";
        }
        private class ItemValue // Configuration클래스 안에 선언된 중첩 클래스,praivate 때문에 Configuration클래스 밖에서 안보임
        {
            private string item;
            private string value;

            public void SetValue(Configuration config, string item, string value)
            {
                this.item = item;
                this.value = value;
                bool found = false;
                for (int i=0; i<config.listConfig.Count; i++) // Count():List<T>에 포함된 요소 수를 가져옵니다.
                {
                    if (config.listConfig[i].item == item)
                    {
                        config.listConfig[i] = this;
                        break;
                    }
                }

                if (found == false)
                    config.listConfig.Add(this);

            }
            public string GetItem()
            {
                return item;
            }
            public string GetValue()
            {
                return value;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Configuration config = new Configuration();
            config.SetConfig("Version", "V 4.0");
            config.SetConfig("Size", "655,324 KB");

            Console.WriteLine(config.GetConfig("Version"));
            Console.WriteLine(config.GetConfig("Size"));

            config.SetConfig("Version", "V 4.0.1");
            Console.WriteLine(config.GetConfig("Version"));
        }
    }
}
/*
V 4.0
655,324 KB
V 4.0.1
 */
```

### 분할 클래스

* 클래스를 여러 번 나눠서 구현하는 클래스

* 클래스 구현이 길어질 경우 사용

  ```C#
  using System;
  
  namespace ParticalClass
  {   
      partial class Myclass  //분할 메소드 키워드 : partial 
      {
          public void Method1()
          {
              Console.WriteLine("Method1");
          }
          public void Method2()
          {
              Console.WriteLine("Method2");
          }
          
      }
  
      partial class Myclass
      {
          public void Method3()
          {
              Console.WriteLine("Method3");
          }
          public void Method4()
          {
              Console.WriteLine("Method4");
          }
      }
      class Program
      {
          static void Main(string[] args)
          {
              Myclass obj = new Myclass();
              obj.Method1();
              obj.Method2();
              obj.Method3();
              obj.Method4();
  
          }
      }
  }
  /*
  Method1
  Method2
  Method3
  Method4 */
  ```

  ### 확장 메소드

  * 기반클래스를 물려받아 파생 클래스를 만든 뒤 필드나 메소드를 추가하는 상속과는 다르다. 기존 클래스의 기능을 확장함.

    ```C#
    namespace 네임스페이스이름
    {
        public static class 클래스이름
        {
            public static 반환형식 메소드이름( this 대상형식 식별자, 매개 변수 목록)
            {
                //
            }
        }
    }
    /* 메소드 선언시 static 한정자로 수식
    , 이 메소드의 첫 번째 매개변수는 반드시 this 키워드와 함께 확장하고자 하는 클래스의 인스턴스(타입)여야 함. 그 뒤에 따라오는 매개 변수 목록이 실제로 확장 메소드를 호출할 때 입력되는 매개 변수임.
    ```

    

```C#
using System;

namespace MyExtension
{   
    public static class IntegerExtension
    {
        public static int Square(this int myInt)
        {
            return myInt * myInt;
        }
        public static int Power(this int myInt, int exponent)
        {
            int result = myInt;
            for (int i=1; i< exponent;i++)
            {
                result = result * myInt;
            }
            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0}^2 : {1}", 3, 3.Square());
            Console.WriteLine("{0}^{1}:{2}", 3, 4, 3.Power(4));
            Console.WriteLine("{0}^{1}:{2}", 2, 10, 2.Power(10));

        }
    }
}
/* 
3^2 : 9
3^4:81
2^10:1024
 */
```

### 구조체

```C#
struct 구조체이름
{
    // 필드 ...
    // 메소드 ...
}
// 클래스와 비슷하지만 클래스는 실세계의 객체를 추상화하려는 데 존재의 이규가 있지만 구조체는 데이터를 담기 위한 자료 구조로 사용됨. 그래서 public으로 자주 사용
```

| 특징          | 클래스                          | 구조체                                                       |
| ------------- | ------------------------------- | ------------------------------------------------------------ |
| 키워드        | class                           | struct                                                       |
| `형식`        | `참조 형식`                     | `값 형식`                                                    |
| 복사          | 얕은 복사(Shallow Copy)         | 깊은 복사(Deep Copy)                                         |
| 인스턴스 생성 | new 연산자와 생성자 필요        | 선언만으로도 생성                                            |
| 생성자        | 매개 변수 없는 생성자 선언 가능 | 매개 변수 없는 생성자 선언 불가능                            |
| 상속          | 가능                            | 모든 구조체는 System.Object 형식을 상속하는 System.ValueType으로부터 직접 상속받음 |

```C#
using System;

namespace Structure
{   
    struct Point3D
    {
        public int X;
        public int Y;
        public int Z;

        public Point3D(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public override string ToString() 
        {
            return string.Format("{0},{1},{2}",X,Y,Z);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Point3D p3d1;  // 선언만으로도 인스턴스 가능
            p3d1.X = 10;
            p3d1.Y = 20;
            p3d1.Z = 40;

            Console.WriteLine(p3d1.ToString());

            Point3D p3d2 = new Point3D(100, 200, 300); //생성자를 이용한 인스턴스 생성도 가능
            Point3D p3d3 = p3d2;
            p3d3.Z = 400;

            Console.WriteLine(p3d2.ToString());
            Console.WriteLine(p3d3.ToString());

        }
    }
}
/*
10,20,40
100,200,300
100,200,400 */
```

