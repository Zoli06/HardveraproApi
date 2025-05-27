using AngleSharp.Dom;

namespace HardveraproApi.Parsers;

public interface IParser<out T>
{
    public T Parse(IElement input);
}