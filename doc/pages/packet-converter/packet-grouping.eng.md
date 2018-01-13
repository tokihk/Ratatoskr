# Packet Join /Split

Add `[Menu Bar]->[View]->[Add converter]- [Grouping]`.

You can combine and split packets according to the rules.

* **Data patterns match**

  Join packets until it finds data matching the described pattern.<br>
  Enter a pattern in the same format as the edit send mode.

* **Data size**

  When passing through the converter, it becomes a packet of the specified size and is output.

* **Receive timeout**

  Packets are combined until the packet input interval to the converter reaches the specified time or longer.

* **Interval**

  Packets are output at specified intervals regardless of the packet input interval to the converter.<br>
  Packets entered into the converter before being output are combined.
