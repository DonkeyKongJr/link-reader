namespace LinkReader.Handler
{
    public interface IHandler<in TIn, out TOut>
    where TIn : struct
    where TOut : struct
    {
        TOut Handle(TIn value);
    }
}