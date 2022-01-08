using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_CreateDLL
{   
    // DLL 내에서 사용하는 함수나 클래스등은 모두 외부에서 사용할 수 있어야 하므로, 한정자는 반드시 public으로 지정
    // 빌드에서 솔루션 빌드를 하면 DLL이 생성된다.
    public class dllTest
    {
        public int fn_Add(int a, int b)
        {
            return a + b;
        }
    }
}
