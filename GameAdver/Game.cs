using System;
using System.Collections.Generic;
using System.Text;

namespace GameAdver
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainImage { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public double Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Developer { get; set; }
        public double Rating { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
