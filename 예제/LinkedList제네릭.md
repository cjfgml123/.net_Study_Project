```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DataStructure.chlee
{
    public class Node<E>
    {
        private Node<E> NextNode;
        private E item;

        public Node(E newitem, Node<E> node)
        {
            item = newitem;
            NextNode = node;

        }
        public E getItem() { return item; }
        public Node<E> getNextNode() { return NextNode; }
        public void setItem(E NewItem) { item = NewItem; }
        public void setNextNode(Node<E> NewNode) { NextNode = NewNode; }
    }

    class LinkedList_chlee<E>
    {
        public Node<E> head;    //연결리스트 첫노드
        private int size;           // 연결리스트 크기

        public LinkedList_chlee()
        {
            this.head = null;
            this.size = 0;
        }

        public int Count() { return size; } //항목수 리턴
        public void insertFront(E newItem) // 리스트 맨 앞에 새 노드 삽입
        {
            head = new Node<E>(newItem, head); //아이템을 갖는 노드를 만들어 맨 앞에 추가
            size++;
        }

        public void InsertAfter(E newItem, Node<E> p)   // 노드 p 바로 다음에 새 노드 삽입 
        {
            
            p.setNextNode(new Node<E>(newItem, p.getNextNode()));
            size++;
        }

        public void deleteFornt()           // 리스트의 첫 노드 삭제 
        {
            if (head == null)
                Console.WriteLine("linked list is Empty.");
            else
            {
                head = head.getNextNode();
                size--;
            }
        }

        public void deleteAfter(Node<E> p)  // p가 가리키는 노드의 다음 노드를 삭제한다.
        {
            if (size == 0)                  // 처음 이 메소드 실행 제어
                Console.WriteLine("linked list is Empty.");
            else
            {
                Node<E> t = p.getNextNode();
                p.setNextNode(t.getNextNode());
                t.setNextNode(null);
                size--;
            }
        }

        public void Print()
        {
            Node<E> CopyHead = head;
            // for (Node<E> node = head; node.getNextNode() != null; node = node.getNextNode())
            while (CopyHead != null)
            {   
                Console.Write(CopyHead.getItem() + " -> ");
                CopyHead = CopyHead.getNextNode();
               
                

                //winform에서의 콘솔 출력 코드 필요. 객체만 출력됨.
                
            }
            Console.WriteLine();
        }
    }
}

```

