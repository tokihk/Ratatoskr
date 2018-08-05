# Basic usage

## 1.Add packet view

You can add any packet view from `[Menu Bar]->[View]->[Add packet view]`.

## 2.Set the gate

You can set the gate from the button of the gate bar.

| Icon | state | operation method |
| :---: | :---: | --- |
| ![](../_images/gate-empty.png) | Not set | Mouse Left /Right click:Gate edit |
| ![](../_images/gate-off.png) | Configured | Left mouse click:Connect /disconnect<br>Mouse right click:Gate edit<br>Mouse right hold:Gate reset | -|

| Icon | State |
|:---:|:---:|
|![](../_images/connect_on.png)| Connection status |
|![](../_images/connect_busy.png)| Preparing for connection |
|![](../_images/connect_off.png)| Disconnected state |

## 3.Send

Data can be sent from the transmission control box at the bottom of the frame.

![](../_images/send_control_box.png)

The destination gate is specified with `wild card 'in`Target alias`.

* ![](../_images/pen_32x32.png) **Edit transmission mode**

    Enter in hexadecimal notation and send with Enter key.
    It recognizes it as a separate data with the 3rd character or space.

    Enclose it with `'...'`to convert the enclosed character to character code.
    You can change the character code by enclosing it with `<...>`.(Default is utf-8)

    If you check `Preview`, you can check the data actually sent.

| Input data | Transmission data |
| :--- | :--- |
| `0123456789`|`01 23 45 67 89 `|
| `0 1 2 3 4`|`00 01 02 03 04`|
| `02'test '03` | `02 74 65 73 74 03 `|
| `02 'あいうえお'03 ` | `02 E 3 81 82 E 3 81 84 E 3 81 86 E 3 81 88 E 3 81 8 A 03` |
| `02 <shift-jis>'あいうえお'03` |  `02 82 A 0 82 A 2 82 A 4 82 A 6 82 A 8 03` |

<br>

* ![](../_images/file_32x32.png) **File transmission mode**

    Drag and drop transmission data or select from the file selection dialog.<br>
    You can not drag and drop when in administrator mode.<br>
    Send with Enter key.

## 4.Receive

When data is input to the gate from the outside, data is displayed in the packet view.

# Useful usage (real time conversion)

Ratatoskr has a function to process transmitted /received packets in real time.
Conversion functions can be freely combined, which is very useful for data analysis.

The converter is added from `[Menu Bar]->[View]->[Add converter]`.
The added transducer will be added under the gate button bar.

![](../_images/converter.png)

The conversion order is processed sequentially from the transducers arranged above.
You can swap the added transducer by dragging the left bar.

Packets subject to converter can be specified for each converter.
If no target packet is specified, all packets passing through the converter are targeted.
