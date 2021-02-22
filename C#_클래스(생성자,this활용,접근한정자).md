### 객체 복사하기

```C#
using System;

namespace DeepCopy
{
    class Field
    {
        public int MyField1;  //필드
        public int MyField2;

        public Field Deepcopy() // 중요
        {
            Field newCopy = new Field();
            newCopy.MyField1 = this.MyField1;
            newCopy.MyField2 = this.MyField2;
            return newCopy;
        }
    }
    class MyClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Shallow Copy");
            {
                Field source = new Field();
                source.MyField1 = 10;
                source.MyField2 = 20;

                Field target = source;
                target.MyField2 = 30;

                Console.WriteLine("{0},{1}", source.MyField1, source.MyField2);
                Console.WriteLine("{0},{1}", target.MyField1, target.MyField2);
            }
            Console.WriteLine("Deep Copy");
            {
                Field source = new Field();
                source.MyField1 = 10;
                source.MyField2 = 20;

                Field target = source.Deepcopy();
                target.MyField2 = 30;

                Console.WriteLine("{0},{1}", source.MyField1, source.MyField2);
                Console.WriteLine("{0},{1}", target.MyField1, target.MyField2);
            }
        }
    }
}
/* shallow Copy 
	10 30
	10 30
	Deep Copy
	10 20
	10 30  */
	
```

### this 키워드

```C#
/* 객체 외부에서 객체의 필드나 메소드에 접근할 때 객체의 이름(변수 또는 식별자)을 사용한다면 객체 내부에서는 자신의 필드난 메소드에 접근할 때 this 키워드를 사용한다 */

class Employee
{
    private string Name;
    
    public void SetName( string Name )
    {
        this.Name = Name; //this.Name는 Employee의 필드변수 , Name은 SetName의 매개변수(파라미터)
    }
}
```

### this 생성자 개선 활용

``` C#
using System;

namespace ThisConstructor
{   
    class Myclass
    {
        int a, b, c;

        public Myclass()
        {
            this.a = 5425;
            Console.WriteLine("MyClass()");

        }

        public Myclass(int b) : this() //앞의 생성자 포함
        {
            this.b = b;
            Console.WriteLine("MyClass({0})", a);
        }

        public Myclass(int b,int c) : this(b) //앞의 생성자 포함
        {
            this.c = c;
            Console.WriteLine("MyClass({0},{1})", b, c);

        }

        public void PrintFields()
        {
            Console.WriteLine("a:{0},b{1},c{2}", a, b, c);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Myclass a = new Myclass();
            a.PrintFields();
            Console.WriteLine();

            Myclass b = new Myclass(1);
            b.PrintFields();
            Console.WriteLine();

            Myclass c = new Myclass(10, 20);
            c.PrintFields();
        }
    }
}

/*
MyClass()
a:5425,b0,c0

MyClass()
MyClass(5425)
a:5425,b1,c0

MyClass()
MyClass(5425)
MyClass(10,20)
a:5425,b10,c20 */
```

#### 접근 한정자

| 접근 한정자 | 설명                                                  |
| ----------- | ----------------------------------------------------- |
| public      | 클래스의 내부/외부 모든 곳 접근 가능                  |
| protected   | 클래스의 외부에서는 접근x, 파생 클래스에서 접근 가능  |
| private     | 클래스의 내부에서만 접근 가능, 파생 클래스에서 접근 x |

* 접근 한정자로 수식하지 않으면 base는 private로 지정

```C#
using System;

namespace AccessModifier
{   
    class WaterHeater
    {
        protected int temperature;

        public void SetTemperature(int temperature)
        {
            if (temperature <-5 || temperature > 42) // -5 ~ 42
            {
                throw new Exception("Out of temperature range"); //예외 발생
            }
            this.temperature = temperature; //protected로 수식되었으므로 외부에서 직접 접근x, public 메소드를 통해 접근
        }
        internal void TurnOnWater()
        {
            Console.WriteLine("Turn on wanter : {0}", temperature);
        }
    }
    class Program
    {   
        static void Main(string[] args)
        {
            try
            {
                WaterHeater heater = new WaterHeater();
                heater.SetTemperature(20);
                heater.TurnOnWater();

                heater.SetTemperature(-2);
                heater.TurnOnWater();

                heater.SetTemperature(50); //여기서 예외 발생함 밑의 TurnOnWater 실행x, 예외 메시지 실행
                heater.TurnOnWater();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
/*
Turn on wanter : 20
Turn on wanter : -2
Out of temperature range
*/
```

