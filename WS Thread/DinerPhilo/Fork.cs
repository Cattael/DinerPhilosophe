using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace WS_Thread.DinerPhilo
{
    public class Fork
    {
        public bool IsUsed { get; set; }
        public int Id { get; set; }
        public Fork(int Id)
        {
            this.Id = Id;
        }
    }
}
