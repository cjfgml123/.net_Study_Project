### 단일 스택 구현 

```C#
using System;
using StackExample;

namespace StackExample
{   
    

    class MyStack
    {   

        private object[] array;
        private int top;

        public MyStack()
        {
            array = new string[1]; // 초기 크기1인 배열 생성
            this.top = -1;
        }
        public bool isEmpty()
        {
            return (top == -1);
        }
        public void push(string item)
        {
            array[++top] = item;  
            Array.Resize(ref array,array.Length+1); 
            Console.WriteLine($"Length : {array.Length}");
        }
        public object Pop()
        {
            object Popitem;
            if (isEmpty())
            {
                Console.WriteLine("Array is Empty.");
                return default;
            }
            else
            {
                Popitem = array[top];  
                array[top--] = null;  
                Array.Resize(ref array, array.Length - 1);
                return Popitem;
            }
        }

        public void print()
        {   
            for(int i=0; i<array.Length-1; i++)
            {
                Console.Write($"({array[i]}) ");
            }
            Console.WriteLine();
        }

    }
    class Program
    {         
        static void Main(string[] args)
        {   
            //가변배열 크기
            object[][] StackData = new object[3][]; //mydata 에 넣을 스택 가변 배열 
            
            CodeFile1 mydata = new CodeFile1();
            
            MyStack S = new MyStack();
            
            S.push("a");
            S.print();

            S.push("b");
            S.print();

            S.push("c");
            S.print();   
            
            S.Pop();
            S.print();
            
            S.Pop();
            S.print(); 
        }
    }
}

```

