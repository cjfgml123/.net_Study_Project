### (base 키워드)상속으로 코드 재활용하기

```C#
/*
class Base     // 기반 클래스
{
	public void BaseMethod()   //멤버 선언
	{
		Console.WriteLine("BaseMethod")
	};
}

class Derived(파생 클래스) : Base(기반 클래스)   // 상속을 통해 BaseMethod() 메소드를 가짐.
{
// 아무 멤버를 선언하지 않아도 기반 클래스의 모든 것을 물려받아 갖게 된다.
// 단 private 으로 선언된 멤버는 제외
}
```

```C#
//상속 base 키워드
using System;

namespace Inheritance
{
    class Base
    {
        protected string Name;
        public Base(string Name)
        {
            this.Name = Name;
            Console.WriteLine("{0}.Base()", this.Name);
        }

        ~Base() // 소멸자
        {
            Console.WriteLine("{0}.~Base()", this.Name);
        }

        public void BaseMethod()
        {
            Console.WriteLine("{0}.BaseMethod()", Name);
        }
    }
    class Derived : Base
    {
        public Derived(string Name) : base(Name)
        {
            Console.WriteLine("{0}.Derived()", this.Name);
        }
        ~Derived()
        {
            Console.WriteLine("{0}.~Derived()", this.Name);
        }

        public void DerivedMethod()
        {
            Console.WriteLine("{0}.DerivedMethod()", Name);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Base a = new Base("a");
            a.BaseMethod();

            Derived b = new Derived("b");
            b.BaseMethod();
            b.DerivedMethod();
        }
    }
}
/*
a.Base()
a.BaseMethod()
b.Base()
b.Derived()
b.BaseMethod()
b.DerivedMethod() */
```

### 형변환 연산자

| 연산자 | 설명                                                         |
| ------ | ------------------------------------------------------------ |
| is     | 객체가 해당 형식에 해당하는지를 검사하여 그 결과를 bool 값으로 반환합니다. |
| as     | 형식 변환 연산자, 형변환 연산자가 변환실패 경우 예외를 던지지만 as 연산자는 객체 참조를 null로 만듬. //실패시 null 값을 리턴 |

```C#
using System;

namespace TypeCasting
{   
    class Mammal
    {
        public void Nurse()
        {
            Console.WriteLine("Nurse()");
        }
    }

    class Dog : Mammal
    {
        public void Bark()
        {
            Console.WriteLine("Bark()");
        }
    }
    class Cat : Mammal
    {
        public void Meow()
        {
            Console.WriteLine("Meow()");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Mammal mammal = new Dog();
            Dog dog;

            if (mammal is Dog)
            {
                dog = (Dog)mammal;
                dog.Bark();
            }

            Mammal mammal2 = new Cat();

            Cat cat = mammal2 as Cat;
            if (cat != null)
                cat.Meow();

            Cat cat2 = mammal as Cat; // mammal 은 Dog형식
            if (cat2 != null)
                cat2.Meow();
            else
                Console.WriteLine("cat2 is not cat");
        }
    }
}
/*
Bark()
Meow()
cat2 is not cat
 */
```

### 오버라이딩과 다형성(Polymorphism)

```C#
using System;

namespace Overriding
{   
    class ArmorSuite
    {
        public virtual void Initialize() // 오버라이딩 할 부모메소드 앞에 virtual 키워드 작성
        {
            Console.WriteLine("Armored");
        }
    }

    class IronMan : ArmorSuite
    {
        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine("Repulsor Rays Armed");
        }
    }
    
    class WarMachine : ArmorSuite
    {
        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine("Double-Barrel Connons Armed");
            Console.WriteLine("Micro-Rocket");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating ...");
            ArmorSuite armorSuite = new ArmorSuite();
            armorSuite.Initialize();

            Console.WriteLine("\nCreating IronMan...");
            ArmorSuite ironman = new IronMan();
            ironman.Initialize();

            Console.WriteLine("\nCreating WarMaching...");
            ArmorSuite warmachine = new WarMachine();
            warmachine.Initialize();

        }
    }
}
/*
Creating ...
Armored

Creating IronMan...
Armored
Repulsor Rays Armed

Creating WarMaching...
Armored
Double-Barrel Connons Armed
Micro-Rocket */
```

### 오버라이딩 봉인하기

```C#
class Base
{
    public virtual void SealMe()
    {
        //...
    }
}

class Derived : Base
{
    public sealed void SealMe() // sealed 키워드를 통해 오버라이딩 봉인
    {
        //...
    }
}
// 오버라이딩한 메소는 파생 클래스의 파생 클래스에서도 자동으로 오버라이딩이 가능해서 브레이크 기능인 sealed 한정자로 막는 것.
```





