using System;

public class ReaderFactory
{

    public IReader GetReader(string type)
    {
        if (type == "Html")
        {
            return new HtmlReader();
        }

        throw new NotSupportedException();
    }
}