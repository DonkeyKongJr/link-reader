namespace LinkReader.Handler
{
    public class UrlProtocolHandler : IHandler<string, string>
    {
        public string Handle(string value)
        {
            if (!value.StartsWith("http"))
            {
                value.Insert(0, "https");
            }

            return value;
        }
    }
}