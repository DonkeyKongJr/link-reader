namespace LinkReader.Handler
{
    public interface IHandler<in TIn, out TOut>
    where TIn : class
    where TOut : class
    {
        TOut Handle(TIn value);
    }
}