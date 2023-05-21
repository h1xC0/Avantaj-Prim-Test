namespace Constants
{
    public class ViewInfo : IViewInfo
    {
        private string _identifier;
        public string Identifier => _identifier;

        private string _path;
        public string Path => _path;
        
        public ViewInfo(string identifier, string path)
        {
            _path = path;
            _identifier = identifier;
        }
    }
}