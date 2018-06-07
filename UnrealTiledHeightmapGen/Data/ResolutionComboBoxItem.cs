namespace UnrealTiledHeightmapGen.Data
{
    public class ResolutionComboBoxItem
    {
        public string Resolution { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return Resolution;
        }
    }
}
