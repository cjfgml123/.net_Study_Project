### 1. TcpListener와 TcpClient

- .NET이 TCP/IP 통신을 위해 제공하는 클래스이다.
- 이들 클래스가 속해 있는 System.Net.Sockets 네임스페이스에는 보다 다양한 옵션과 메소드를 제공하는 Socket 클래스도 있지만 사용이 복잡하여 이 두 클래스를 이용한다.



#### 1.1 TcpListener

- 서버 애플리케이션에서 사용되며, 클라이언트의 연결 요청을 기다리는 역할을 한다.



#### 1.2 TcpClient

- 서버 애플리케이션과 클라이언트 애플리케이션 양쪽에서 사용된다.
  - 클라이언트에서는 TcpClient가 서버에 연결 요청을 하는 역할을 수행하며, 서버에서는 클라이언트의 요청을 수락하면 클라이언트와의 통신에 사용할 수 있는 TcpClient의 인스턴스가 반환된다.
  - GetStream() 메소드를 갖고 있어서, 양쪽 응용 프로그램은 이 메소드가 반환하는 NetworkStream 객체를 통해 데이터를 주고 받는다.
- 데이터를 보낼 때는 NetworkStream.Write()
- 데이터를 읽을 때는 NetworkStream.Read()를 호출
- 일을 마치고 서버와 클라이언트의 연결을 종료할 때는 NetworkStream객체와 TcpClient 객체 모두의 Close() 메소드를 호출한다.



| 클래스      | 메소드            | 설명                                                         |
| ----------- | ----------------- | ------------------------------------------------------------ |
| TcpListener | Start()           | 연결 요청 수신 대기를 시작함.                                |
|             | AcceptTcpClient() | 클라이언트의 연결 요청을 수락한다. 이 메소드는 TcpClient 객체를 반환한다. |
|             | Stop()            | 연결 요청 수신 대기를 종료한다.                              |
| TcpClient   | Connect()         | 서버에 연결을 요청한다.                                      |
|             | GetStream()       | 데이터를 주고받는 데 사용하는 매개체인 NetworkStream을 가져온다. |
|             | Close()           | 연결을 닫는다.                                               |

```C#
// 서버의 TcpListener를 시작하는 코드

// IPEndPoint는 IP 통신에 필요한 IP 주소와 출입구(포트)를 나타낸다.
IPEndPoint localAddress = 
    new IPEndPoint(IPAddress.Parse("192.168.100.17"), 5425);

// server 객체는 클라이언트가 TcpClient.Connect()를 호출하여 연결 요청해오기를 기다리기 시작한다.
TcpListener server = new TcpListener(localAddress);
server.Start();

```

```C#
// 클라이언트에서 TcpClient 객체를 생성하고 서버에 연결을 요청하는 코드

// 포트를 0으로 지정하면 OS에서 임의의 번호로 포트를 할당해준다.
IPEndPoint clientAddress = new IPEndPoint(IPAddress.Parse("192.168.100.18"),0);

TcpClient client = new TcpClient(clientAddress);

IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("192.168.100.17"), 5425);

// 서버가 수신 대기하고 있는 IP 주소와 포트 번호를 향해 연결 요청을 수행한다.
client.Connect(serverAddress);
```

```C#
// 서버

// 서버에서 다음과 같이 AcceptTcpClient()를 호출하면 코드는 블록되어 이 메소드가 반환할 때까지 진행하지 않는다. 클라이언트의 연결 요청이 있기 전까지는 반환되지 않는다. 기다리고 기다리던 연결 요청이 오면 이 메소드는 클라이언트와 통신을 수행할 수 있도록 TcpClient 형식의 객체를 반환한다.
TcpClient client = server.AcceptTcpClient();
```

```C#
// 이제 서버와 클라이언트에 있는 TcpClient 형식의 객체로 부터 NetworkStream 형식의 객체를 가져와서 데이터를 읽고 쓸 수 있다. 다음의 코드는 TcpClient 객체가 NetworkStream객체를 반환하고 NetworkStream 객체를 이용하여 데이터를 읽고 쓰는 예제

//TcpClient 를 통해 NetworkStream 객체를 얻는다.
NetworkStream stream = client.GetStream();

int length;
string data = null;
byte[] bytes = new byte[256];

// NetworkStream.Read() 메소드는 상대방이 보내온 데이터를 읽어들입니다. 상대와의 연결이 끊어지면 이 메소드는 0을 반환한다. 즉 이 루프는 연결이 끊어지기 전까지는 계속된다.
// Read(buffer,offset,size) buffer의 공간중에 시작점, 크기
// Read 연산은 size 매개 변수에서 지정하는 바이트 수 한도까지 사용할 수 있는 데이터를 모두 읽는다. 원격 호스트에서 연결을 끊고 사용할 수 있는 데이터가 모두 수신된 경우, Read 메서드가 즉시 완료되고 0바이트가 반환된다.
// Read()메소드는 데이터를 buffer 매개 변수로 읽어들이고 성공적으로 읽은 바이트 수를 반환한다.
while((length = stream.Read(bytes, 0, bytes.Length)) != 0)
{	
    //GetString(array, index, count) <- 인터넷에서 보기
    // Byte[]을 string으로 변환
    data = Encoding.Default.GetString(bytes, 0, length);
    Console.WriteLine(String.Format("수신: {0}", data));
    
    byte[] msg = Encoding.Default.GetBytes(data);
    
    // NetworkStream.Write()메소드를 통해 상대방에게 메시지를 전송
    stream.Write(msg, 0, msg.Length);
    Console.WriteLine(String.Format("송신: {0}", data));
}
```



