using System;
using System.Collections.Generic;
using System.Text;

namespace GameAdver
{
    public class Picture
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid GameId { get; set; }
        public virtual Game Game { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
