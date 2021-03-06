using System;

namespace LinkReader.Reader
{
    public static class ReaderFactory
    {

        public static IReader GetReader(string type)
        {
            if (type?.ToUpper() == "HTML")
            {
                return new HtmlReader();
            }

            throw new NotSupportedException();
        }
    }
}