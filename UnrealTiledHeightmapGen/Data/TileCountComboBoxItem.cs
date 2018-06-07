namespace UnrealTiledHeightmapGen.Data
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
