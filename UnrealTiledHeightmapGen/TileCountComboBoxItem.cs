using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnrealTiledHeightmapGen
{
    public class TileCountComboBoxItem
    {
        public string Value { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
