namespace HttpLogic.HttpContentParsers;

public interface IHttpContentConverter
{
    HttpContent ConvertToHttpContent(object value);
    Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent);
}