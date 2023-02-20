namespace UGeoDB
{
    public struct DbFile
    {
        public string Path { get; set; }
        public bool IsRemote { get; set; }

        public DbFile(string path, bool isRemote)
        {
            this.Path = path;
            this.IsRemote = isRemote;
        }
    }
}