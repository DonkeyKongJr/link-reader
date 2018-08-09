using System.Collections.Generic;

public interface IReader
{
    IEnumerable<string> GetLinksFromUrl(string url);
}