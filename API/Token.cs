namespace API
{
    public class Token
    {
        public string Key { get; set; }
        public int TimeToAccessExpires { get; set; }
        public int TimeToRefreshExpires { get; set; }

        public Token()
        {
            Key = string.Empty;
        }

        public override string ToString()
        {
            return $"Key: {Key} | TimeToAccessExpires: {TimeToAccessExpires} | TimeToRefreshExpires: {TimeToRefreshExpires} ";
        }
    }
}
