using System;

public static class ReaderFactory
{

    public static IReader GetReader(string type)
    {
        if (type == "Html")
        {
            return new HtmlReader();
        }

        throw new NotSupportedException();
    }
}