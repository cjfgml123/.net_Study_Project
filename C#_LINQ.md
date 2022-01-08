### 1. LINQ

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



#### 1-1) 여러 개의 데이터 원본에 질의하기

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

##### 1-2) group by로 데이터 분류하기

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



#### 1-3) 두 데이터 원본을 연결하는 join

- join은 두 데이터 원본을 연결하는 연산이다. 각 데이터 원본에서 특정 필드의 값을 비교하여 일치하는 데이터끼리 연결을 수행한다.

#### 1-3-1) 내부 조인(Inner Join) 

- 내부 조인은 교집합과 비슷하다.
- 내부 조인을 수행할 때 기준 데이터 원본에는 존재하지만 연결할 데이터 원본에는 존재하지 않는 데이터는 조인 결과에 포함되지 않는다. 당연히 기준 데이터 원본에는 없지만 연결할 데이터 원본에는 존재하는 데이터의 경우에도 조인 결과에 포함되지 않는다.

```C#
var listProfile = 
    from profile in arrProfile
    join product in arrProduct on profile.Name equals product.Star
    select new
	{
    	Name = profile.Name,
    	Work = product.Title,
    	Height = profile.Height
	};
```



#### 1-3-2) 외부 조인(Outer Join)

- 기준이 되는 데이터 원본의 모든 데이터를 조인 결과에 반드시 포함시키는 특징이 있다. 연결할 데이터 원본에 기준 데이터 원본의 데이터와 일치하는 데이터가 없다면 그 부분은 빈 값으로 결과를 채우게 된다.

| 외부 조인종류           | 설명                                              |
| ----------------------- | ------------------------------------------------- |
| 왼쪽 조인(Left Join)    | 왼쪽 데이터 원본을 기준으로 삼아 조인 수행        |
| 오른쪽 조인(Right Join) | 오른쪽 데이터 원본을 기준으로 삼는다.             |
| 완전 외부 조인          | 왼쪽과 오른쪽 데이터 원본 모두를 기준으로 삼는다. |

-LINQ는 이 세 가지 조인 방식 중에서 왼쪽 조인만을 지원한다.

```C#
using System;
using System.Linq;

namespace Join
{   
    class Profile
    {
        public string Name { get; set; }
        public int Height { get; set; }
    }

    class Product
    {
        public string Title { get; set; }
        public string Star { get; set; }
    }

    class MainApp
    {   
        
        static void Main(string[] args)
        {
            Profile[] arrProfile =
            {
                new Profile(){Name="정우성", Height = 186},
                new Profile(){Name="김태희", Height = 158},
                new Profile(){Name="고현정", Height = 172},
                new Profile(){Name="이문세", Height = 178},
                new Profile(){Name="하하", Height = 171}
            };

            Product[] arrProduct =
            {
                new Product(){Title="비트", Star="정우성"},
                new Product(){Title="CF 다수", Star="김태희"},
                new Product(){Title="아이리스", Star="김태희"},
                new Product(){Title="모레시계", Star="고현정"},
                new Product(){Title="Sole 예찬", Star="이문세"},
            };

            var listProfile =
                from profile in arrProfile
                join product in arrProduct on profile.Name equals product.Star
                select new
                {
                    Name = profile.Name,
                    Work = product.Title,
                    Height = profile.Height
                };
            Console.WriteLine("ㅡㅡㅡ 내부 조인 결과 ㅡㅡㅡ");
            foreach ( var profile in listProfile)
            {
                Console.WriteLine("이름:{0}, 작품:{1}, 키:{2}cm",profile.Name,profile.Work,profile.Height);
            }

            listProfile =
                from profile in arrProfile
                join product in arrProduct on profile.Name equals product.Star
                into ps
                from product in ps.DefaultIfEmpty(new Product() { Title = "그런거 없음" })
                select new
                {
                    Name = profile.Name,
                    Work = product.Title,
                    Height = profile.Height
                };

            Console.WriteLine();
            Console.WriteLine("--------- 외부 조인 결과 ------------");
            foreach(var profile in listProfile)
            {
                Console.WriteLine("이름:{0}, 작품:{1}, 키:{2}cm",profile.Name,profile.Work,profile.Height);                
            }
        }
    }
}

/*
ㅡㅡㅡ 내부 조인 결과 ㅡㅡㅡ
이름:정우성, 작품:비트, 키:186cm
이름:김태희, 작품:CF 다수, 키:158cm
이름:김태희, 작품:아이리스, 키:158cm
이름:고현정, 작품:모레시계, 키:172cm
이름:이문세, 작품:Sole 예찬, 키:178cm

--------- 외부 조인 결과 ------------
이름:정우성, 작품:비트, 키:186cm
이름:김태희, 작품:CF 다수, 키:158cm
이름:김태희, 작품:아이리스, 키:158cm
이름:고현정, 작품:모레시계, 키:172cm
이름:이문세, 작품:Sole 예찬, 키:178cm
이름:하하, 작품:그런거 없음, 키:171cm */
```



#### 1-4) LINQ의 비밀과 LINQ 표준 연산자

```C#
var profiles = from profile in arrProfile
    		   where profile.Height < 175
    		   orderby profile.Height
    		   select new { Name = profile.Name, InchHeight = profile.Height*0.393 };
//ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
var profiles = arrProfile
    			.Where(profile => profile.Height < 175)
    			.OrderBy(profile => profile.Height)
    			.Select(profile => new {
                    Name = profile.Name,
                    InchHeight = profile.Height * 0.393
                });

```

- LINQ 연산 메소드 중에 C#의 쿼리식에서 지원하는 것은 11개 뿐이다. 물론 11가지만으로도 대부분의 데이터 처리가 가능하지만, 나머지 42개를 모두 활용할 수 있다.



```c#
// 키가 180cm 미만인 연예인들의 키 평균 구하기
// LINQ쿼리식과 메소드를 함께 사용하는 방법
 Profile[] arrProfile =
            {
                new Profile(){Name="정우성", Height = 186},
                new Profile(){Name="김태희", Height = 158},
                new Profile(){Name="고현정", Height = 172},
                new Profile(){Name="이문세", Height = 178},
                new Profile(){Name="하하", Height = 171}
            };
//기존의 방법 1.
var profiles = from profile in arrProfile
    			where profile.Height < 180
    			select profile;

double Average = profiles.Average(profile => profile.Height);
Console.WriteLine(Average);

//방법2.
double Average = (from profile in arrProfile
    			where profile.Height < 180
    			select profile).Average(profile=>profile.Height);
Console.WriteLine(Average);
```

```C#
using System;
using System.Linq;

namespace MinMaxAvg
{   
    class Profile
    {
        public string Name { get; set; }
        public int Height { get; set; }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Profile[] arrProfile =
            {
                new Profile(){Name="정우성",Height=186},
                new Profile(){Name="김태희",Height=158},
                new Profile(){Name="고현정",Height=172},
                new Profile(){Name="이문세",Height=178},
                new Profile(){Name="하하",Height=171},
            };

            var heightStat = from profile in arrProfile
                             group profile by profile.Height < 175 into g
                             select new
                             {
                                 Group = g.Key == true ? "175미만" : "175이상",
                                 Count = g.Count(),
                                 Max = g.Max(profile => profile.Height),
                                 Min = g.Min(profile => profile.Height),
                                 Average = g.Average(profile => profile.Height)
                             };

            foreach(var stat in heightStat)
            {
                Console.Write("{0} - Count:{1}, Max:{2},",stat.Group, stat.Count, stat.Max);
                Console.WriteLine("Min:{0}, Average:{1}",stat.Min,stat.Average);
            }
        }
    }
}

/*
175이상 - Count:2, Max:186,Min:178, Average:182
175미만 - Count:3, Max:172,Min:158, Average:167 */
```

