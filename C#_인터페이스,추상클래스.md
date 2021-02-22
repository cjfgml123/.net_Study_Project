### 인터페이스

* 클래스와 비슷하지만 인터페이스는 메소드, 이벤트, 인덱서, 프로퍼티만 가질 수 있지만 구현부가 없다. 인터페이스를 구지는 파생클래스는 모든 멤버에 대한 구현(implementation)을 제공해야 한다.
* 클래스는 접근 제한 한정자로 수식하지 않으면 기본적으로 private로 선언되지만, 인터페이스는 접근 제한 한정자를 사용할 수 없고 모든 것이 public 으로 선언된다. 인스턴스도 만들 수 없다.

```C#
/* 
interface 인터페이스이름
{
    반환형식 메소드이름1(매개변수 목록);
}  */

interface ILogger
{
    void WriteLog( string log ); //접근 제한 한정자 사용x
}
```

```C#
using System;
using System.IO;

namespace Interface
{   
    interface ILogger
    {
        void WriteLog(string message);

    }

    class ConsoleLogger : ILogger //인터페이스 메소드 구현 필요
    {
        public void WriteLog(string message)
        {
            Console.WriteLine("{0},{1}",DateTime.Now.ToLocalTime(),message);
        }
    }

    class FileLogger : ILogger  //인터페이스 메소드 구현 필요
    {
        private StreamWriter writer; //StreamWriter, StreamReader : 텍스트 파일을 읽고 쓸 수 있는 클래스 

        public FileLogger(string path)
        {
            writer = File.CreateText(path);
            writer.AutoFlush = true;
        }

        public void WriteLog(string message)
        {
            writer.WriteLine("{0} {1}", DateTime.Now.ToShortTimeString(), message);
        }
    }

    class ClimateMonitor
    {
        private ILogger logger;
        public ClimateMonitor(ILogger logger)
        {
            this.logger = logger;
        }

        public void start ()
        {
            while ( true )
            {
                Console.Write("온도를 입력해주세요.:");
                string temperature = Console.ReadLine();
                if (temperature == "")
                    break;

                logger.WriteLog("현재 온도 : " + temperature);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {   /*
            ClimateMonitor monitor = new ClimateMonitor( // monitor 객체는 응용 프로그램이 시작된 디렉토리에
                                                         // MyLog.txt를 만들고 여기에 로그를 남깁니다.
                new FileLogger("MyLog.txt"));

            monitor.start(); */
            ConsoleLogger console = new ConsoleLogger();
            console.WriteLog("test");
        }
    }
}
/*
 폴더 MyLog.txt 파일에저장됨.
 */
```

### 인터페이스를 상속하는 인터페이스

* 인터페이스를 상속할 수 있는 것: 클래스 , 구조체, 인터페이스

  ```C#
  interface 파생인터페이스 : 부모인터페이스
  {
      // ... 추가할 메소드 목록
  }
  ```

```C#
using System;

namespace DerivedInterface
{   
    interface ILogger
    {
        void WriteLog(string message);

    }

    interface IFormattableLogger : ILogger //IFormattableLogger 인터페이스는 ILogger에 선언된 void WriteLog(string message);도 갖고 있음.
    {
        void WriteLog(string format, params Object[] args); 
    }
    
    class ConsoleLogger2 : IFormattableLogger
    {
        public void WriteLog(string message)
        {
            Console.WriteLine("{0} {1}", DateTime.Now.ToLocalTime(), message);
        }

        public void WriteLog(string format, params Object[] args)
        {
            String message = String.Format(format, args);
            Console.WriteLine("{0} {1}", DateTime.Now.ToLocalTime(), message);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IFormattableLogger logger = new ConsoleLogger2();
            logger.WriteLog("The world is not flat.");
            logger.WriteLog("{0}+{1}={2}", 1, 1, 2);
        }
    }
}
/*
2021-02-22 오전 9:22:37 The world is not flat.
2021-02-22 오전 9:22:37 1+1=2
 */
```

### 여러 개의 인터페이스, 한꺼번에 상속하기

* 클래스는 여러 클래스를 한꺼번에 상속할 수 없다.

```C#
using System;

namespace MultiInterfaceInheritance
{   
    interface IRunnable
    {
        void Run();
    }

    interface IFlyable
    {
        void Fly();
    }

    class FlyingCar : IRunnable, IFlyable
    {
        public void Run()
        {
            Console.WriteLine("Run! Run!");
        }

        public void Fly()
        {
            Console.WriteLine("Fly! Fly!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            FlyingCar car = new FlyingCar();
            car.Run();
            car.Fly();

           
        }
    }
}
/*
Run! Run!
Fly! Fly!
*/
```

## 추상클래스

* 인터페이스와 달리 구현을 가질 수 있지만 인스턴스를 가질 수는 없다.
* 인터페이스는 모든 메소드가 public으로 선언되며 추상클래스는 한정자 명시안할시 private로 선언됨.
* 추상 클래스는 또 다른 추상 클래스를 상속할 수 있음. 이 경우 자식 추상 클래스는 부모 추상 클래스의 추상 메소드를 구현하지 않아도 된다.

```C#
abstract class 클래스이름
{
    // 클래스와 동일하게 구현
}
```

```C#
using System;

namespace AbstractBase
{   
    abstract class AbstractBase
    {
        protected void PrivateMethodA()  //구현된 메소드
        {
            Console.WriteLine("AbstractBase.PrivateMethodA()");
        }
        public void PublicMethodA()
        {
            Console.WriteLine("AbstractBase.PublicMethodA()");
        }

        public abstract void AbstractMethodA();  //추상메소드
    }

    class Derived : AbstractBase
    {
        public override void AbstractMethodA()
        {
            Console.WriteLine("Derived.AbstractMethodA()");
            PrivateMethodA();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AbstractBase obj = new Derived();
            obj.AbstractMethodA();
            obj.PublicMethodA();
        }
    }
}
/*
Derived.AbstractMethodA()
AbstractBase.PrivateMethodA()
AbstractBase.PublicMethodA()
 */
```

