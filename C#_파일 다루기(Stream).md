### 1. 파일 다루기

### 1) 파일 정보와 디렉터리 정보 다루기

* 파일 : 컴퓨터 저장 매체에 기록되는 데이터 묶음
* 디렉터리 : 파일이 위치하는 주소 (폴더)



##### C#은 파일,디렉토리를 다룰 수 있도록 System.IO 네임스페이스 아래에 이런 클래스를 제공

| 클래스        | 설명                                                         |
| ------------- | ------------------------------------------------------------ |
| File          | 파일의 생성,복사,삭제,이동,조회를 처리하는 정적 메소드 제공  |
| FileInfo      | File 클래스와 하는 일은 동일하지만 정적 메소드 대신 인스턴스 메소드를 제공 |
| Directory     | 디렉터리의 생성, 삭제, 이동, 조회를 체리하는 정적 메소드 제공 |
| DirectoryInfo | Directory 클래스와 하는 일은 동일하지만 정적 메소드 대신 인스턴스 메소드를 제공 |

-하나의 파일에 대해 한 두가지 정도의 작업을 할때는 File 클래스의 정적 메소드를 이용하는 편이고 하나의 파일에 여러 작업을 수행할 때는 FileInfo클래스의 인스턴스 메소드를 이용하는 편이다. Directory도 마찬가지.



##### ()가 붙은 항목은 메소드, () 없는 항목은 프로퍼티

| 기능               | File            | FileInfo   | Directory         | DirectoryInfo    |
| ------------------ | --------------- | ---------- | ----------------- | ---------------- |
| 생성               | Create()        | Create()   | CreateDirectory() | Create()         |
| 복사               | Copy()          | CopyTo()   | -                 | -                |
| 삭제               | Delete()        | Delete()   | Delete()          | Delete()         |
| 이동               | Move()          | MoveTo()   | Move()            | MoveTo()         |
| 존재 여부 확인     | Exists()        | Exists     | Exists()          | Exists           |
| 속성 조회          | GetAttributes() | Attributes | GetAttributes()   | Attributes       |
| 하위 디렉토리 조회 | -               | -          | GetDirectories()  | GetDirectories() |
| 하위 파일 조회     | -               | -          | GetFiles()        | GetFiles()       |



#### File과 FileInfo 클래스 기능 예제 차이 (a.dat : 원본 , b.dat : 대상)

| 기능           | File                                            | FileInfo                                                     |
| -------------- | ----------------------------------------------- | ------------------------------------------------------------ |
| 생성           | FileStream fs =File.Create("a.dat");            | FileInfo file = new FileInfo("a.dat");         FileStream fs = file.Create(); |
| 복사           | File.Copy("a.dat","b.dat"); //원본,대상         | FileInfo src= new FileInfo("a.dat"); FileInfo dst = src.CopyTo("b.dat"); |
| 삭제           | File.Delete("a.dat");                           | FileInfo file = new FileInfo("a.dat"); file.Delete();        |
| 이동           | File.Move("a.dat","b.dat");                     | FileInfo file = new FileInfo("a.dat");                       |
| 존재 여부 확인 | if (File.Exists("a.dat") ) //...                | FileInfo file = new FileInfo("a.dat"); if(file.Exists) //... |
| 속성 조회      | Console.WriteLine(File.GetAttributes("a.dat")); | FileInfo file = new FileInfo("a.dat"); Console.WriteLine(file.Attributes); |



#### Directory과 DirectoryInfo 클래스 기능 예제 차이

| 기능               | Directory (정적)                                            | Directory(동적)                                              |
| ------------------ | ----------------------------------------------------------- | ------------------------------------------------------------ |
| 생성               | DirectoryInfo dir =Directory.CreateDirectory("a");          | DirectoryInfo dir =new DirectoryInfo("a"); dir.Create();     |
| 삭제               | Directory.Delete("a");                                      | DirectoryInfo dir = new DirectoryInfo("a"); dir.Delete();    |
| 이동               | Directory.Move("a","b");                                    | DirectoryInfo dir = new DirectoryInfo("a"); dir.MoveTo("b"); |
| 존재 여부 확인     | if(Directory.Exists("a.dat")) //...                         | DirectoryInfo dir = new DirectoryInfo("a"); if(dir.Exist) //... |
| 속성 조회          | Console.WriteLine(Directory.GetAttributes("a"));            | DirectoryInfo dir = new DirectoryInfo("a"); Console.WriteLine(dir.Attributes); |
| 하위 디렉터리 조회 | string[] dirs = Directory.GetDirectories("a"); //a 디렉토리 | DirectoryInfo dir = new DirectoryInfo("a"); DirectoryInfo[] dirs = dir.GetDirectories(); |
| 하위 파일 조회     | string[] files = Directory.GetFiles("a");                   | DirectoryInfo dir = new DirectoryInfo("a"); FileInfo[] files=dir.GetFiles(); |





```C#
//디렉터리 /파일 정보 조회하기
using System;
using System.Linq;
using System.IO;
namespace Dir
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory;
            if (args.Length < 1)   
                directory = ".";
            else
                directory = args[0];

            Console.WriteLine($"{directory} directory Info");
            Console.WriteLine("- Directories :");
            var directories = (from dir in Directory.GetDirectories(directory) // 하위 디렉터리 목록 조회
                               let info = new DirectoryInfo(dir) //let은 LINQ 안에서 변수를 만듬. LINQ의 var라고 생각하면 됨.
                               select new
                               {
                                   Name = info.Name,
                                   Attributes = info.Attributes
                               }).ToList();

            foreach (var d in directories)
                Console.WriteLine($"{d.Name} : {d.Attributes}");

            Console.WriteLine("- Files :");
            var files = (from file in Directory.GetFiles(directory) // 하위 파일 목록 조회
                         let info = new FileInfo(file)
                         select new
                         {
                             Name = info.Name,
                             FileSize = info.Length,
                             Attributes = info.Attributes 
                         }).ToList();
            foreach (var f in files)
                Console.WriteLine(
                    $"{f.Name}:{f.FileSize}, {f.Attributes}");
        }
    }
}

```



### 2) 파일을 읽고 쓰기 위해 알아야 할 것들

##### 2-1) 스트림(Stream)

- 데이터가 흐르는 통로, 저장 장치간의 스트림을 만들어 둘 사이를 연결한 후 메모리에 있는 데이터를 바이트 단위로 옮김.

- 스트림은 데이터의 "흐름"이기 때문에 스트림을 이용하여 파일을 다룰 때는 처음부터 끝까지 순서대로 읽고 쓰는 것이 보통입니다. -> 순차 접근 방식(Sequential Access)

##### 2-2) 하드디스크

- 암과 헤드를 움직여 디스크의 어떤 위치에 기록된 데이터에라도 즉시 찾아갈 수 있다. 이렇게 임의의 주소에 있는 데이터에 접근하는 것을 가리켜 임의 접근 방식(Random Access)



##### 2-3) System.IO.Stream 클래스 : 입력, 출력 스트림의 역할을 모두 수행 , 순차접근,임의 접근 모두 지원 

* Stream 클래스가 추상클래스로 파생클래스 : FileStream, NetworkStream, GZipStream, BufferedStream ...

```c#
//FileStream 클래스의 인스턴스 생성
Stream stream1 = new FileStream("a.dat", FileMode.Create); // 새 파일 생성
Stream stream2 = new FileStream("b.dat", FileMode.Open); // 파일 열기
Stream stream3 = new FileStream("c.dat", FileMode.OpenOrCreate); // 파일을 열거나 없으면 생성
Stream stream4 = new FileStream("d.dat", FileMode.Truncate);// 파일 비워서 열기
Stream stream5 = new FileStream("e.dat", FileMode.Append); // 덧붙이기 모드로 열기

```

```C#
// a.dat 파일에 long형식의 0x123456789ABCDEF0 쓰기
long someValue = 0x123456789ABCDEF0; //16진수
Stream outStream = new FileStream("a.dat", FileMode.Create); //파일 스트림 생성, a.dat이라는 파일을 만들고

 byte[] wBytes = BitConverter.GetBytes(someValue);
//someValue의 8바이트를 바이트 배열에 나눠 넣는다.someValue(long형식)를 byte배열로 변환
// WBytes는 8의 길이의 배열이 된다.

outStream.Write(wBytes, 0, wBytes.Length); //변환한 byte 배열을 파일 스트림을 통해 파일에 기록
/* public override void Write(
	byte[] array, //쓸 데이터가 담겨 있는 byte배열
	int offset, //byte 배열 내의 시작 오프셋 : 0 이면 0부터 시작 ,10이면 10부터 시작
	int count    //기록할 데이터의 총 길이(단위는 바이트)
);
*/
outStream.Close();
```

```C#
byte[] rBytes = new byte[8];

// 스트림 방법 1. 
//1. 파일 스트림 생성
Stream inStream = new FileStream("a.dot",FileMode.Open); //읽기위해 여는모드는 Open 설정

//2. rBytes의 길이만큼(8바이트) 데이터를 읽어 rBytes에 저장
inStream.Read(rBytes, 0, rBytes.Length); //0부터 읽을 것이다. 8바이트까지

//3. BitConverter를 이용하여 rBytes에 담겨 있는 값을 long 형식으로 변환
long readValue = BitConverter.ToInt64(rbytes, 0); 

//4. 파일 스트림 닫기
inStream.Close();

// 스트림 방법 2. using 키워드 사용
{
    using Stream inStream = new FileStream("a.dot",FileMode.Open);
    inStream.Read(rBytes, 0, rBytes.Length);
    long readValue = BitConverter.ToInt64(rbytes, 0);
} //inStream.Close(); 실행 안해도됨. 자동으로 inStream.Dispose() 호출

// 스트림 방법 2. using 키워드 사용

    using (Stream inStream = new FileStream("a.dot",FileMode.Open))
{
    inStream.Read(rBytes, 0, rBytes.Length);
    long readValue = BitConverter.ToInt64(rbytes, 0);
} //inStream.Close(); 실행 안해도됨. 자동으로 inStream.Dispose() 호출
```

```C#
//FileStream클래스를 이용해서 파일에 데이터를 읽고 쓰는 과정 예제
using System;
using System.IO;

namespace BasicIO
{
    class Program
    {
        static void Main(string[] args)
        {
            long someValue = 0x123456789ABCDEF0;
            Console.WriteLine("{0,-1} : 0x{1:X16}", "Original Data", someValue);

            Stream outStream = new FileStream("a.dat", FileMode.Create);
            byte[] wBytes = BitConverter.GetBytes(someValue); //someValue의 8바이트를 바이트 배열에 나눠 넣는다.someValue(long형식)를 byte배열로 변환

            Console.Write("{0,-13}:", "Byte array");

            foreach (byte b in wBytes)
                Console.Write("{0:X2} ", b);
            Console.WriteLine();

            outStream.Write(wBytes, 0, wBytes.Length); //Write() 메소드를 이용해 단번에 파일에 기록 , wBytes.Length = 현재 8
            outStream.Close();

            Stream inStream = new FileStream("a.dat",FileMode.Open);
            byte[] rbytes = new byte[8];

            int i = 0;
            while (inStream.Position < inStream.Length)
                rbytes[i++] = (byte)inStream.ReadByte();

            long readValue = BitConverter.ToInt64(rbytes, 0);

            Console.WriteLine("{0,-13} : 0x{1:X16}", "Read Data", readValue);
            inStream.Close();

        }
    }
}
/*
Original Data : 0x123456789ABCDEF0
Byte array   :F0 DE BC 9A 78 56 34 12
Read Data     : 0x123456789ABCDEF0
 */
```



#### 임의 접근 방식

```C#
using System;
using System.IO;
namespace SeqNRand
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream outStream = new FileStream("a.dat", FileMode.Create); //스트림 생성
            Console.WriteLine($"Position : {outStream.Position}"); // 객체 생성할때 Position 은 0

            outStream.WriteByte(0x01); // 포지션 0에 들어감
            Console.WriteLine($"Position : {outStream.Position}");

            outStream.WriteByte(0x02);// 포지션 1에 들어감
            Console.WriteLine($"Position : {outStream.Position}");

            outStream.WriteByte(0x03);// 포지션 2에 들어감
            Console.WriteLine($"Position : {outStream.Position}");

            outStream.Seek(5, SeekOrigin.Current);  // 현재 위치에서 5바이트 뒤로 이동
            Console.WriteLine($"Position : {outStream.Position}");  // 포지션 3-> 8로 이동

            outStream.WriteByte(0x04); // 포지션 8에 들어감
            Console.WriteLine($"Position : {outStream.Position}");

            outStream.Close();
        }
    }
}
/*
Position : 0
Position : 1
Position : 2
Position : 3
Position : 8
Position : 9
 */
/*position  0  1  2	 3  4  5  6  7  8  9
  Offset	00 01 02 03 04 05 06 07 08 09
  value	    01 02 03 00 00 00 00 00 04
*/
```

#### 이진 데이터 처리를 위한 BinaryWriter/BinaryReader

* 데이터를 byte나 byte의 배열로 변환하지 않아도 됨. BitConverter() 안써도된다.

```C#
// BinaryWriter
BinaryWriter bw = new BinaryWriter(new FileStream("a.dat", FileMode.Create));
bw.Write("안녕하세요!");
bw.Write(double.MaxValue);
bw.Close();
//BinaryWriter의 생성자를 호출하면서 FileStream의 인스턴스를 인수로 넘기고 있다.

// BinaryReader
BinaryReader br =new BinaryReader(new FileStream("a.dat", FileMode.Open));
Console.WriteLine($"{br.ReadInt32()}");
Console.WriteLine($"{br.ReadString()}");
Console.WriteLine($"{br.ReadUInt32()}");
br.Close();
```



#### 텍스트 파일 처리를 위한 StreamWriter/StreamReader

```C#
// StreamWriter
StreamWriter sw = new BinaryWriter(new FileStream("a.dat", FileMode.Create));
sw.Write("안녕하세요!");
sw.Write(double.MaxValue);
sw.Close();
//BinaryWriter의 생성자를 호출하면서 FileStream의 인스턴스를 인수로 넘기고 있다.

// StreamReader
StreamReader sr =new BinaryReader(new FileStream("a.dat", FileMode.Open));
while (sr.EndOfStream == false) //EndOfStream 프로퍼티는 스트림의 끝에 도달했는지를 알려준다.
{
    Console.WriteLine(sr.ReadLine());
}
sr.Close();
```



#### 객체 직렬화(Serialization)하기

- 직렬화 : 객체의 상태(객체의 필드에 저장된 값)를 메모리나 영구 저장 장치에 저장이 가능한 0과 1의 순서로 바꾸는 것. (.NET은 json이나 xml같은 텍스트 형식으로의 직렬화도 지원)
- Binary Writer/Reader와 Stream Writer/Reader는 기본 데이터 형식을 스트림에 쓰고 읽을 수 있도록 메소드들을 제공하지만 클래스나 구조체 같은 복합 데이터 형식은 지원하지 않는데 직렬화가 이것을 지원

```C#
[Serializable]// 애트리뷰트 , 이거 붙여주면 이 클래스는 메모리나 영구 저장 장치에 저장할 수 있는 형식이 된다.
class MyClass
{
    public int myField1;
    public int myField2;
    
    [NonSerialized]
    public int myField3; //필드3을 제외한 나머지 필드들만 직렬화된다.
    public int myField4;
}
// ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ직렬화ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
Stream ws = new FileStream("a.dat", FileMode.Create);
// BinaryFormatter 클래스: 개체 또는 연결된 개체의 전체 그래프를 이진 형식으로 직렬화 및 역직렬화합니다.
BinaryFormatter serializer = new BinaryFormatter();

MyClass obj = new MyClass(); //obj의 필드에 값을 저장할 것이다 라는 뜻으로 이해

serializer.Serialize(ws, obj); // 직렬화
ws.Close();

// ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ역직렬화ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
Stream rs = new FileStream("a.dat", FileMode.Open);
BinaryFormatter deserializer = new BinaryFormatter();
MyClass obj = (MyClass)deserializer.Deserialize(rs); //역직렬화
rs.Close();
```

* Serializable 애트리뷰트를 이용해서 복합 데이터 형식을 직렬화하고자 할 때 직렬화 하지 ''않는'' 필드 뿐 아니라 직렬화하지 ''못하는'' 필드도 Nonserialized 애트리뷰트로 수식해야 한다.

```C#

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{   
    [Serializable]
    class NameCard
    {
        public string Name;
        public string Phone;
        //[NonSerialized]
        public int Age;
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (Stream ws = new FileStream("a.dat", FileMode.Create))
            {   // BinaryFormatter 클래스: 개체 또는 연결된 개체의 전체 그래프를 이진 형식으로 직렬화 및 역직렬화합니다.
                BinaryFormatter serializer = new BinaryFormatter();

                NameCard nc = new NameCard();
                nc.Name = "박상현";
                nc.Phone = "010-123-4567";
                nc.Age = 33;
                serializer.Serialize(ws, nc);
            }

            using Stream rs = new FileStream("a.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            NameCard nc2;
            nc2 = (NameCard)deserializer.Deserialize(rs);

            Console.WriteLine($"Name: {nc2.Name}");
            Console.WriteLine($"Phone: {nc2.Phone}");
            Console.WriteLine($"Age: {nc2.Age}");

        }
    }
}
/*
Name: 박상현
Phone: 010-123-4567
Age: 33
*/
```



- List를 비롯한 컬렉션들도 직렬화를 지원

```C#
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializingCollection
{   
    [Serializable]
    class NameCard
    {
        public NameCard(string Name, string Phone, int Age)
        {
            this.Name = Name;
            this.Phone = Phone;
            this.Age = Age;
        }

        public string Name;
        public string Phone;
        public int Age;
    }
    class Program
    {   
        static void Main(string[] args)
        {
            using (Stream ws = new FileStream("a.dat", FileMode.Create))
            {
                BinaryFormatter serializer = new BinaryFormatter();

                List<NameCard> list = new List<NameCard>(); //List를 비롯한 컬렉션들도 직렬화를 지원
                list.Add(new NameCard("박상현", "010-123-4567", 33));
                list.Add(new NameCard("김연아", "010-323-1111", 22));
                list.Add(new NameCard("장미란", "010-555-5555", 26));

                serializer.Serialize(ws, list);
            }

            using Stream rs = new FileStream("a.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            List<NameCard> list2;
            list2 = (List<NameCard>)deserializer.Deserialize(rs);

            foreach (NameCard nc in list2)
            {
                Console.WriteLine($"Name : {nc.Name}, Phone : {nc.Phone}, Age:{nc.Age}");
            }

        }
    }
}
/*
Name : 박상현, Phone : 010-123-4567, Age:33
Name : 김연아, Phone : 010-323-1111, Age:22
Name : 장미란, Phone : 010-555-5555, Age:26
 */
```

