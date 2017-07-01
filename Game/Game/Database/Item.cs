using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Game
{
    public class Item
    {
      [PrimaryKey, AutoIncrement]
      public int ID { get; set; }
      public string Name { get; set; }
      public int Score { get; set; }
      public DateTime TimeStamp { get; set; }

      public Item()
      {
      }

      public override string ToString()
      {
        return "Name: " + Name + " |  Score: " + Score;
      }
  }
}
