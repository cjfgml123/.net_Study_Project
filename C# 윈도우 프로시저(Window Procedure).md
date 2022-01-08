### 1. C# 윈도우 프로시저(Window Procedure)

```C#
switch(iMessage) {
        case Msg1;
        처리 1;
        return 0;
        case Msg2;
        처리2;
        default;
        return DefWindowProc(...);
}
//DefWindowProc()는 WndProc에서 처리하지 않은 나머지 메시지에 관한 처리를 한다. 
// ex) 시스템 메뉴를 더블클릭하면 프로그램이 종료되는데 이런 처리는 별도로 하지 않아도 DefWindowProc함수에서 알아서 한다. 그래서 윈도우 이동이나 크기 변경등의 자질구레 한것들을 일일히 직접 case로 지정할 필요 없이 DefWindowProc로 넘기기만 하면 된다.
```



- WndProc를 말하며 WinMain에서 호출하는 것이 아니라 운영체제에 의해 호출된다.
- WinMain내의 메시지 루프는 메시지를 메시지 처리 함수로 보내기만 할 뿐이며 WndProc은 메시지가 입력되면 운영체제에 의해 호출되어 메시지를 처리한다.
- 운영체제에 의해 호출되는 응용 프로그램 내의 함수를 콜백 함수라고 한다.

- WinProc는 메시지를 무사히 처리했으면 0을 반드시 리턴하도록 약속되어 있다.

