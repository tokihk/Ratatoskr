# [Packet Converter] - Filtering

Add `[Menu Bar]->[View]->[Add converter]- [Filter]`.

Passes only packets that match the specified pattern.

Patterns can be combined with conditions by `(...)`priority specification and conditional operator `&&` `||`.<br>

| Conditional operator | operation |
|:---|:---|
| `A && B` | The condition is satisfied when both A and B conditions are satisfied.|
| `A || B` | The condition is satisfied when either condition of A or B is satisfied.|

Packet elements can be referenced by the following variables.(Not case sensitive)
| Variable name | Content | Data type |
|:---|:---|:---|
| `PacketCount`| Returns the number of packets that arrived at the converter.| Number |
| `LastDelta`| Returns the difference time from the last arrived packet.| DateTimeOffset |
| `IsControl`| Returns whether the packet is a control packet.(Unused)| Bool |
| `IsMessage`| Returns if the packet is a message packet.| Bool |
| `IsData`| Returns whether the packet is a data packet.| Bool |
| `Alias`| Returns the Alias ​​of the gate that created the packet.| Text |
| `DateTime`| Returns the time the packet occurred.| DateTime |
| `Information`| Returns additional information on that packet.| Text |
| `Mark`| Returns the mark value of that packet.(Unused)| Number |
| `IsSend`| Returns whether it is a send packet or not.| Bool |
| `IsRecv`| Returns whether or not it is a received packet.| Bool |
| `Source`| Returns the sender information of the packet.| Text |
| `Destination`| Returns the destination information of the packet.| Text |
| `DataSize`| Returns the data size of that packet.| Text |
| `BitText`| Returns the data part in binary notation.| Text |
| `HexText`| Returns the data part in hexadecimal notation.| Text |
| `AsciiText`| Returns the data part in ASCII notation.| Text |
| `Utf8Text`| Returns the data part in UTF-8 notation.| Text |
| `UnicodeLText`| Returns the data part in UTF-16LE notation.| Text |
| `UnicodeBText`| Returns the data part in UTF-16BE notation.| Text |

Parameter elements can be defined with the following description.
| Type | regular expression | description example |
|:---|:---|:---|
| Number   | `[0]|([1-9][0-9]{0,8})(\.[0-9]{1,8}){0,1}` | `0`<br>`12345`<br>`12345.0123` |
| Number   | `0[xX][0-9a-fA-F]{1,8}`                    | `0x01234567` |
| DateTime | `{TY}-{TM}-{TD}T{Th}:{Tm}:{Ts}\.{Tf}([\+\-]{Th}:{Tm}|Z)` | `2018-01-01T01:00:00.000Z`<br>`2018-01-01T01:00:00.000+09:00` |
| DateTimeOffset | `{Th}:{Tm}:{Ts}\.{Tf}`                    | `00:00:15.123` |
| Text | `\"[^\"]*\"` | `"abcdef"` |
| Regex | `\/[^/]*\/` | `/02.*03/` |

- **Regex type can be compared with Text type and regular expression.**
- Ellipses represent the following.
  - `TY = ([0-9]{4})`
  - `TM = ([0][0-9]|1[0-2])`
  - `TD = ([0-2][0-9]|3[0-1])`
  - `Th = ([0-1][0-9]|2[0-3])`
  - `Tm = ([0-5][0-9])`
  - `Ts = ([0-5][0-9])`
  - `Tf = ([0-9]{3})`

Below is an example of frequently used description.
| Filter expression | Meaning |
|:---|:---|
| `IsData && HexText == /02.*03/`| Data packets enclosed in 02 to 03 |
