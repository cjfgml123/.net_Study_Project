using System;
using System.Net;      
using System.Net.Sockets;
using System.Text;

    
namespace Server
{
    class server
    {
       
        public static Socket Server, Client;

        public const int sPort = 5000;
        static byte[] headerBuffer = new byte[10];
        static byte[] DataBuffer;

        static Header header = new Header();
        static string receivedString = String.Empty;

        public static void ServerAccept()
        {
            IPAddress serverIP = IPAddress.Parse("192.168.99.112");
            IPEndPoint serverEndPoint = new IPEndPoint(serverIP, sPort);

            Console.WriteLine("현재 서버 정보");
            Console.WriteLine("IP Address : {0}, Port : {1}", serverEndPoint.Address, serverEndPoint.Port);

            // 소켓의 프로토콜타입, 주소 패밀리 등을 초기화 하고 소켓을 생성
            Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 서버의 IP주소와 포트를 소켓과 바인드
            Server.Bind(serverEndPoint);

            //클라이언트 접속 대기 (20개의 클라이언트까지)
            Server.Listen(10);
            Console.WriteLine("클라이언트 접속 대기중입니다!");

            Client = Server.Accept();
            Console.WriteLine("클라이언트 접속 됨");

            //while (true)
            //{
            //    //Accept() 메소드 콜백이 클라이언트 접속 요청시 활성화됨.
            //    Client = Server.Accept();
            //    if (Client.Connected)
            //    {
            //        Console.WriteLine(" 클라이언트 접속 됨");
            //    }
            //}
        }

        public static void Send_To_Client(byte[] _header, byte[] _body)
        {
            byte[] _packet = new byte[_header.Length + _body.Length];
            Array.Copy(_header, 0, _packet, 0, _header.Length);
            Array.Copy(_body, 0, _packet, _header.Length, _body.Length);
            Client.Send(_packet, 0, _packet.Length, SocketFlags.None);

            string _receivedString = Encoding.UTF8.GetString(_body, 0, _body.Length);
            Console.WriteLine($"보낸 메시지 타입 :{_header[0]},기능:{_header[1]}, 데이터 : { _receivedString}|{_body.Length}");
        }

        public static void Receive_To_Client()
        {
            int totalDataSize = 0;
            int leftDataSize = 0;
            int accumnlatedDataSize = 0;
            int receivedDataSize = 0;

            Client.Receive(headerBuffer, 0, 10, SocketFlags.None);
            header = new Header()
            {
                dataType = (byte)headerBuffer[0],
                functionType = (byte)headerBuffer[1],
                sync_cnt = BitConverter.ToInt32(headerBuffer, 2),
                bodyLen = BitConverter.ToInt32(headerBuffer, 6)
            };
            totalDataSize = header.bodyLen;
            leftDataSize = totalDataSize;

            DataBuffer = new byte[totalDataSize];

            while (leftDataSize > 0)
            {
                if (leftDataSize < 0)
                {
                    Console.WriteLine("Error Data Receiving");
                    break;
                }
                else if (leftDataSize == 0)
                {
                    Console.WriteLine("Data success");
                    break;
                }
                receivedDataSize = Client.Receive(DataBuffer, accumnlatedDataSize, leftDataSize, SocketFlags.None);
                accumnlatedDataSize += receivedDataSize;
                leftDataSize -= receivedDataSize;
            }
            receivedString = Encoding.UTF8.GetString(DataBuffer, 0, totalDataSize);
            Console.WriteLine($"받은 메시지 타입 :{header.dataType},기능:{header.functionType}, 데이터 : { receivedString}|{DataBuffer.Length}");
        }

        public static void ReceivedData_Process_To_DB_Node_ListView(bool _eventSuccess)
        {
            try
            {
                if (_eventSuccess)
                {
                    headerBuffer[0] = (byte)DataType.RESP_DATA_SUCCESS;
                }
                else
                {
                    headerBuffer[0] = (byte)DataType.RESP_DATA_FAIL;
                }
                Send_To_Client(headerBuffer, DataBuffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.ToString()}");
            }
        }

        public static void DataSendRecv()
        {
            try
            {
                while (Client.Connected)
                {
                    Receive_To_Client();
                    string[] _receivedData = Split_Received_StringData(receivedString);
                    if (header.dataType == (byte)DataType.SEND_DATA)
                    {   
                        switch (header.functionType)
                        {
                            case (byte)FunctionType.SYNC_SEND:
                                Console.WriteLine($"동기화할 데이터 :{receivedString}|길이: {DataBuffer.Length}");
                                ReceivedData_Process_To_DB_Node_ListView(true);
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                // id 중복검사 후 데이터 받기
                                Console.WriteLine($"저장할 데이터 :{receivedString}|길이: {DataBuffer.Length}");
                                ReceivedData_Process_To_DB_Node_ListView(true);
                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Console.WriteLine($"업데이트할 데이터 :{receivedString}|길이: {DataBuffer.Length}");
                                ReceivedData_Process_To_DB_Node_ListView(true);

                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Console.WriteLine($"삭제할 데이터 :{receivedString}|길이: {DataBuffer.Length}");
                                ReceivedData_Process_To_DB_Node_ListView(true);

                                break;
                        }
                    }
                    else if (header.dataType == (byte)DataType.RESP_DATA_SUCCESS)
                    {
                        switch (header.functionType)
                        {
                            case (byte)FunctionType.SYNC_SEND:
                                Console.WriteLine($"클라이언트에 동기화된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                Console.WriteLine($"클라이언트에 저장된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Console.WriteLine($"클라이언트에 업데이트된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Console.WriteLine($"클라이언트에 삭제된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                        }
                    }
                    else if (header.dataType == (byte)DataType.RESP_DATA_FAIL)
                    {
                        switch (header.functionType)
                        {
                            case (byte)FunctionType.SYNC_SEND:
                                Console.WriteLine($"클라이언트에 동기화 안된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                        
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                Console.WriteLine($"클라이언트에 저장 안된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                        
                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Console.WriteLine($"클라이언트에 수정 안된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                   
                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Console.WriteLine($"클라이언트에 삭제 안된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                    
                                break;
                        }
                    }
                    Array.Clear(headerBuffer, 0, headerBuffer.Length);
                    Array.Clear(DataBuffer, 0, DataBuffer.Length);
                }

                if (!Client.Connected)
                {
                    Console.WriteLine("접속 끊김");
    
                }
            }
            catch (SocketException socketEx)
            {
                Console.WriteLine("[Error]:{0}소켓ㅡServer_chlee.Communication_Server_To_Client()", socketEx.Message);
               
            }
            catch (Exception commonEx)
            {
                Console.WriteLine("[Error]:{0}일반ㅡServer_chlee.Communication_Server_To_Client()", commonEx.Message);
           
            }
        }

        public static string[] Split_Received_StringData(string _receivedString)
        {
            string _data = _receivedString;
            if (_data != null)
            {
                string[] _words = _data.Split(',');

                if (_words.Length != 8)
                {
                    Console.WriteLine("데이터의 개수가 부족합니다.");
                    return null;
                }
                else
                {
                    return _words;
                }
            }
            else
            {
                Console.WriteLine("데이터 분리 실패 ㅡㅡㅡ Server_chlee.StringDataSplit()");
                return null;
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            ServerAccept();
            DataSendRecv();
        }
    }
}
