### C# 마리아디비 연동

#### 1. C# 마리아 디비 쓰기(insert, create, update )

```c#
using System.Data;
using MySql.Data.MySqlClient;

public static void WriteDB()
        {	
    // db 접속 정보 기입
            string _strConn = "Server=localhost;Database=test;Uid=root;Pwd=;";
    // 디비 연결 구동
            using (MySqlConnection conn = new MySqlConnection(_strConn))
            {
                try
                {	//연결
                    conn.Open();
                    //db cmd 객체에 쿼리문 작성 insert, create, select.. 등등
                    MySqlCommand cmd = new MySqlCommand("CREATE TABLE chleeData(" +
"createData DATETIME," +
"modifyData DATETIME," +
"id VARCHAR(30) NOT null," +
"pw VARCHAR(30) NOT NULL," +
"age INT NOT NULL," +
"agechk boolean NOT null," +
"tall FLOAT(3, 1) NOT NULL," +
"sexType boolean NOT NULL," +
"PRIMARY KEY(id));", conn);
                    //결과를 알고자 한다면 executeNonQuery()메소드를 호출해서 결과를 반환받아 처리하면 된다.
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {	//접속실패 
                    Console.WriteLine($"{ex}, DB접속 실패");
                }
            }

        }

// insert 시 주의사항 ... values('ae') -> '도 문자열 표시따로 해줘야 한다. ex)"''"
// string aaa = "insert into chlee(id,age) values ('"
                                  +data.name + "','" + data.age+"')";
```

```C#
// executeNonQuery()메소드 활용
static void Main(string[] args)
{
    string comtext = "insert into Books values ('ADO.NET', 12000, '홍길동', '2984756325')";
    string constr = @"Data Source=[서버 이름];Initial Catalog=[DB 명]; User ID=[ID];Password=[PW]";
    SqlConnection scon = new SqlConnection(constr);
    SqlCommand command = new SqlCommand(comtext, scon);
    scon.Open();
    if (command.ExecuteNonQuery() == 1)
    {
        Console.WriteLine("추가 성공");
    }
    else
    {
        Console.WriteLine("추가 실패");
    }
    //연결 종료
    scon.Close();
    
}

```

#### 2. C# 마리아 디비 테이블, 데이터 읽기 (select)

```C#
// 방법 1.  ExecuteReader()를 이용
// 명령 수행하고 수행한 결과를 확인할 때 사용할 SqlDataReader 개체를 빌드하여 반환하는 ExecuteReader 메소드를 제공한다.
// select 문처럼 명령 수행 결과가 집할일 때 사용하는 메소드, 사용이 끝나면 반드시 SqlReader 개체의 Close 메소드를 호출하여야 다른 명령을 수행할 수 있다.
public static void ReadDB()
        {
            string _strConn = "Server=localhost;Database=test;Uid=root;Pwd=;";
    		//연결 객체 생성
            using (MySqlConnection conn = new MySqlConnection(_strConn))
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT * FROM chleedata";

                    //ExecuteReader를 이용하여 연결 모드로 데이터 가져오기
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read()) //데이터를 모두 읽어온다.
                        {
                            Console.WriteLine($"{rdr["id"]}: {rdr["age"]} : {rdr["tall"]}");
                        }
                    }
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex}, DB접속 실패");
                }
            }
        }
```

```C#
// reader.Close(); scon.Close(); 사용 예제
            
using System;
using System.Data.SqlClient;
 
namespace ExecuteReader_메서드
{
    class Program
    {
        static void Main(string[] args)
        {
            string comtext = "Select * From Books";           
            string constr = @"Data Source=516-41\SQLEXPRESS2;Initial Catalog=EHDB;Integrated Security=True;Pooling=False;";
            //516-41\SQLEXPRESS2 대신 실제 DBMS 인스턴스 명으로 변경하세요.
            //Catalog 대신 연결할 데이터 베이스 명으로 변경하세요.
            //이 코드는 윈도우 계정으로 연결한 예제입니다.
            SqlConnection scon = new SqlConnection(constr);
            SqlCommand command = new SqlCommand(comtext, scon);
            scon.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.Write("도서제목:{0}", reader["Title"]);
                Console.Write(" ISBN:{0}", reader["ISBN"]);
                Console.Write(" 저자:{0}", reader["Author"]);
                Console.WriteLine(" 가격:{0}", reader["Price"]);
            }
            reader.Close();
            scon.Close();
        }
 
    }
}
```

```C#
//MySqlDataAdapter 클래스를 이용하여 비연결 모드로 데이터 가져오기
public static void ReadDB1()
        {
            DataSet dataset = new DataSet();
            string _strConn = "Server=localhost;Database=test;Uid=root;Pwd=;";

            using (MySqlConnection conn = new MySqlConnection(_strConn))
            {
                //MySqlDataAdapter 클래스를 이용하여 비연결 모드로 데이터 가져오기
                // 서버에서 데이터를 가져온 후 연결을 끊는다.
                string sql = "select * from chleedata";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(dataset);
            }

            foreach(DataRow r in dataset.Tables[0].Rows)
            {
                Console.WriteLine($"{r["id"]} : {r["age"]} : {r["tall"]}");
            }
            Console.ReadLine(); 
        }
```

#### 윈폼 버튼에서 데이터베이스 연결 확인

```C#
private void button2_Click(object sender, EventArgs e){
    Conn.Open();
    if(Conn.State == ConnectionState.Open)  
    {
        MessageBox.Show("데베 열음");
    }
    else if (Conn.State == ConnectionState.Closed)
    {
        MessageBox.Show("데베 닫음");
    }
    else{
        MessageBox.Show("데베 오픈 에러");
    }
}
```

