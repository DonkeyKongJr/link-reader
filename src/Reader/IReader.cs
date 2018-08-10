using System.Collections.Generic;

public interface IReader
{
    IEnumerable<string> GetLinksFromText(string text);
}