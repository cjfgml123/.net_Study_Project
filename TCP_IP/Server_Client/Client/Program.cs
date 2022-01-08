using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{   
    
    class Program
    {

        public static Socket socket;
        static byte[] headerBuffer = new byte[10];
        static byte[] DataBuffer;

        static Header header = new Header();


        public static string cmd = string.Empty;
        static string receivedString = String.Empty;

        public const int sPort = 8000;

        /// <summary>
        /// 패킷 설정
        /// </summary>
        /// <returns></returns>
        public static byte[] Packet(DataType _dataType, FunctionType _functionType,int _sync_cnt, int _dataLength)
        {
            Header _header = new Header()
            {
                dataType = (byte)_dataType,
                functionType = (byte)_functionType,
                sync_cnt = _sync_cnt,
                bodyLen = _dataLength
            };
                
            byte[] _headerArray = new byte[10];
            _headerArray[0] = _header.dataType;
            _headerArray[1] = _header.functionType;
            byte[] sync_cnt = BitConverter.GetBytes(_header.sync_cnt);
            byte[] a = BitConverter.GetBytes(_header.bodyLen);
            Array.Copy(sync_cnt,0,_headerArray,2,4);
            // a를 _headerArray로 복사한다. a의 0번지부터 a.Length까지를 _headerArray 번지부터 복사한다.
            Array.Copy(a,0,_headerArray,6,4);  
                return _headerArray;
        }

        public static void ServerConnect()
        {
            try
            {

                //서버의 IP주소, 포트번호 IPEndPoint 객체로 저장
                //127.0.0.1 => localhost
                IPAddress serverIP = IPAddress.Parse("192.168.99.112");
                IPEndPoint serverEndPoint = new IPEndPoint(serverIP, sPort);
                // 소켓의 프로토콜타입, 주소 패밀리 등을 초기화 하고 소켓을 생성
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine("서버 접속 시도...[엔터치면 연결됩니다.]");
                Console.ReadLine();
                Console.WriteLine("데이터 전송함");

                // 서버에 연결요청
                socket.Connect(serverEndPoint);

                if (socket.Connected)
                {
                    Console.WriteLine("서버의 연결이 성공했습니다.");
                    Console.WriteLine("전송할 데이터를 입력해 주세요.");
                }
                else
                {
                    Console.WriteLine("서버의 연결이 실패 되었습니다.");
                }

            }catch(SocketException e)
            {
                Console.WriteLine($"{e}, 서버 연결 실패");
            }
            catch (Exception commonEx)
            {
                Console.WriteLine("[Error]:{0}", commonEx.Message);
            }

        }

        public static void Send_To_Client(byte[] _header, byte[] _body)
        {
            byte[] _packet = new byte[_header.Length + _body.Length];
            Array.Copy(_header, 0, _packet, 0, _header.Length);
            Array.Copy(_body, 0, _packet, _header.Length, _body.Length);
            socket.Send(_packet, 0, _packet.Length, SocketFlags.None);
        }

        public static void Receive_To_Client()
        {
            int totalDataSize = 0;
            int leftDataSize = 0;
            int accumnlatedDataSize = 0;
            int receivedDataSize = 0;

            socket.Receive(headerBuffer, 0, 10, SocketFlags.None);
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
                receivedDataSize = socket.Receive(DataBuffer, accumnlatedDataSize, leftDataSize, SocketFlags.None);
                accumnlatedDataSize += receivedDataSize;
                leftDataSize -= receivedDataSize;
            }
            receivedString = Encoding.UTF8.GetString(DataBuffer, 0, totalDataSize);
        }
        public static void DataSendRecv()
        {
            try
            {
                while (socket.Connected)
                {
                    Receive_To_Client();

                    if (header.dataType == (byte)DataType.SEND_DATA)
                    {
                        switch (header.functionType)
                        {
                            case (byte)FunctionType.SYNC_SEND:
                                Console.WriteLine($"동기화할 데이터 :{receivedString}|길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                // id 중복검사 후 데이터 받기
                                Console.WriteLine($"저장할 데이터 :{receivedString}|길이: {DataBuffer.Length}");

                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Console.WriteLine($"업데이트할 데이터 :{receivedString}|길이: {DataBuffer.Length}");

                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Console.WriteLine($"삭제할 데이터 :{receivedString}|길이: {DataBuffer.Length}");

                                break;
                        }
   

                    }
                    else if (header.dataType == (byte)DataType.RESP_DATA_SUCCESS)
                    {
                        switch (header.functionType)
                        {
                            case (byte)FunctionType.SYNC_SEND:
                                Console.WriteLine($"서버에 동기화된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                Console.WriteLine($"서버에 저장된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Console.WriteLine($"서버에 업데이트된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Console.WriteLine($"서버에 삭제된 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                        }
                    }
                    else if (header.dataType == (byte)DataType.RESP_DATA_FAIL)
                    {
                        switch (header.functionType)
                        {
                            case (byte)FunctionType.SYNC_SEND:
                                Console.WriteLine($"서버에 동기화된 실패 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                Console.WriteLine($"서버에 저장된 실패 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Console.WriteLine($"서버에 업데이트된 실패 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Console.WriteLine($"서버에 삭제된 실패 데이터 : {receivedString} |길이: {DataBuffer.Length}");
                                break;
                        }
                    }

                    Array.Clear(headerBuffer, 0, headerBuffer.Length);
                    Array.Clear(DataBuffer, 0, DataBuffer.Length);
                }

                if (!socket.Connected)
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

        [STAThread]
        static void Main(string[] args)
        {
            string cmd = "2020-10-11 11:11:11,2020-11-11 12:12:12,6,1,30,0,13";
            byte[] body = Encoding.UTF8.GetBytes(cmd);
            byte[] header = Packet(DataType.SEND_DATA, FunctionType.SAVE_DATA,10, body.Length);
            //byte[] header = Packet(DataType.RESP_DATA, FunctionType.SAVE_DATA, body.Length);

            //byte[] packet = new byte[header.Length + body.Length];

            //Array.Copy(header, 0, packet, 0, header.Length);
            //Array.Copy(body, 0, packet, header.Length, body.Length);

            ServerConnect();
            Send_To_Client(header,body);
            DataSendRecv();





        }
    }
}
