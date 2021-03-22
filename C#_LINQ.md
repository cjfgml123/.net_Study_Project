### LINQ

1. 모든 LINQ 쿼리식(Query Expression)은 반드시 from 절로 시작한다.
   - 쿼리식의 대상이 될 데이터 원본과 데이터 원본 안에 들어 있는 각 요소 데이터를 나타내는 범위 변수를 from절에서 지정해줘야 한다.
   - 쿼리식의 범위 변수는 LINQ 질의 안에서만 사용가능 (외부에서 선언된 변수에 범위 변수의 데이터를 복사하거나 하는 일은 할 수 없다.)

```C#
int[] numbers = {1,2,3,4,5,6,7,8,9,10};
var result = from n(범위 변수) in numbers(데이터 원본)
    where 	n % 2 == 0
    orderby n
    select n;
```

```C#
using System;
using System.Linq;
namespace From
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 9,2,6,4,5,3,7,8,1,10};
            var result = from n in numbers
                         where n % 2 == 0		// 필터(Filter) 역할,조건 삽입
                         orderby n				// 데이터의 정렬(기본 : 오름차순)
                       //orderby n ascending    // 내림차순
                		 select n;				// 여기서 타입이 결정됨.
            foreach (int n in result)
                Console.WriteLine($"짝수 : {n}");
        }
    }
}

```

```C#
// 무명 형식 사용
using System;
using System.Linq;
namespace From
{   
    class Profile
    {
        public string Name { get; set; }
        public int Height { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Profile[] arrProfile =
            {
                new Profile(){Name = "a", Height = 1},
                new Profile(){Name = "b", Height = 2},
                new Profile(){Name = "c", Height = 3},
                new Profile(){Name = "d", Height = 4},
                new Profile(){Name = "e", Height = 5}
            };

            var profiles = from profile in arrProfile
                           where profile.Height < 175
                           orderby profile.Height
                           select new           // 무명형식으로 사용 가능
                           {
                               Name = profile.Name,
                               InchHeigth = profile.Height * 0.393
                           };
            foreach (var profile in profiles)
                Console.WriteLine($"{profile.Name}, {profile.InchHeigth}");
        }
    }
}

```



#### 여러 개의 데이터 원본에 질의하기

- from절을 두번 사용하면됨.

```C#
using System;
using System.Linq;

namespace FromFrom
{   
    class Class
    {
        public string Name { get; set; }
        public int[] Score { get; set; }    // 배열 선언
    }
    class Program
    {
        static void Main(string[] args)
        {
            Class[] arrClass =
            {
                new Class(){Name="연두반", Score=new int[]{99,80,70,24}},
                new Class(){Name="분홍반", Score=new int[]{60,45,87,72}},
                new Class(){Name="파랑반", Score=new int[]{92,30,85,94}},
                new Class(){Name="노랑반", Score=new int[]{90,88,0,17}}
            };

            var classes = from c in arrClass
                          from s in c.Score
                          where s < 60
                          orderby s
                          select new { c.Name, Lowest = s };
            
            foreach (var c in classes)
                Console.WriteLine($"낙제 : {c.Name} ({c.Lowest})");
        }
    }
}
/*
낙제 : 노랑반 (0)
낙제 : 노랑반 (17)
낙제 : 연두반 (24)
낙제 : 파랑반 (30)
낙제 : 분홍반 (45)
 */
```

##### group by로 데이터 분류하기

```C#
// A에는 from 절에서 뽑아낸 범위 변수
// B는 분류 기준
// C는 그룹 변수를 위치시킨다.
group A by B into C
    
```

```C#
using System;
using System.Linq;
namespace GroupBy
{   
    class Profile
    {
        public string Name { get; set; }
        public int Height { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Profile[] arrProfile =
            {
                new Profile(){Name="정우성", Height = 186},
                new Profile(){Name="김태희", Height = 158},
                new Profile(){Name="고현정", Height = 172},
                new Profile(){Name="이문세", Height = 178},
                new Profile(){Name="하하", Height = 171},
            };

            var listProfile = from profile in arrProfile
                              orderby profile.Height
                              group profile by profile.Height < 175 into g
                              select new { GroupKey = g.Key, Profiles = g };

            foreach(var Group in listProfile)
            {
                Console.WriteLine($"- 175cm 미만? : {Group.GroupKey}");

                foreach(var profile in Group.Profiles)
                {
                    Console.WriteLine($">>> {profile.Name}, {profile.Height}");
                }
            }
        }
    }
}
/*
- 175cm 미만? : True  // GroupKey
>>> 김태희, 158
>>> 하하, 171
>>> 고현정, 172
- 175cm 미만? : False		// Profiles 
>>> 이문세, 178
>>> 정우성, 186
 */
```

