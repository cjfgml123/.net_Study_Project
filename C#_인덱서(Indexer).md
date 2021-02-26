#### 인덱서(Indexer)

* 인덱스(Index)를 이용해서 객체 내의 데이터에 접근하게 해주는 프로퍼티
* 프로퍼티가 이름을 통해 객체 내의 데이터에 접근하게 해준다면 인덱서는 인덱스를 통해 객체 내의 데이터에 접근하게 해줌.

```C#
class 클래스이름
{
    한정자 인덱서형식 this[형식 index] // index의 식별자가 꼭 index일 필요는 없음. 
   {
        	get
    	{
        // index를 이용하여 내부 데이터 반환
    	}
    		set
    	{
        // index를 이용하여 내부 데이터 저장
    	}
    }
}
```

```C#
using System;

namespace Indexer
{   
    class MyList
    {
        private int[] array;

        public MyList() //생성자 ?
        {
            array = new int[3];
        }

        public int this[int index]
        {
            get
            {
                return array[index];
            }

            set
            {
                if (index >= array.Length)
                {
                    Array.Resize<int>(ref array, index + 1);
                    Console.WriteLine($"Array Resized : {array.Length}");
                }
                array[index] = value;
            }


        }

        public int Length  //프로퍼티
        {
            get { return array.Length;  }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyList list = new MyList();
            for (int i =0; i<5; i++)
                list[i] = i;

            for (int i = 0; i < list.Length; i++)
                Console.WriteLine(list[i]);
        }
    }
}
/*
Array Resized : 4
Array Resized : 5
0
1
2
3
4
 */
```

#### foreach가 가능한 객체 만들기

* 아무 형식의 객체에서 사용할 수 없고 배열, 리스트 같은 컬렉션에서만 사용할 수 있다. 하지만 foreach 구문은 IEnumerable을 상속하는 형식을 지원하기에 위의 코드 MyList클래스도 IEnumerable을 상속하기만 하면 사용 가능하다.

| 메소드                      | 설명                           |
| --------------------------- | ------------------------------ |
| IEnumerator GetEnumerator() | IEnumerator 형식의 객체를 반환 |

GetEnumerator()는 IEnumerator 인터페이스를 상속하는 클래스의 객체를 반환해야 하는데 yield문을 이용하면 따로 인스턴스화 하지 않아도 된다.

```C#
using System;
using System.Collections;
namespace Yield
{   
    class MyEnumerator
    {
        int[] numbers = { 1, 2, 3, 4 };
        public IEnumerator GetEnumerator()
        {
            yield return numbers[0];
            yield return numbers[1];
            yield return numbers[2];
            yield break; //GetEnumerator() 메소드 종료 시킴
            yield return numbers[3];  //실행 x

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new MyEnumerator();
            foreach (int i in obj)
                Console.WriteLine(i);
        }
    }
}
/*
1
2
3
 */
```

