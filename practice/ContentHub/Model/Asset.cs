namespace ContentHub.Model
{
    internal class Asset
    {
        public long EntityId { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public long MasterFile { get; set; }
        public string DuplicateFileMapperId { get; set; }
    }
}
