using System.Collections.Generic;

public interface IReader
{
    IEnumerable<string> getLinksFromUrl(string url);
}