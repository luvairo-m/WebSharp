using System.Web;

namespace HttpLogic.HttpContentParsers;

public class XWwwFormUrlEncodedConverter : IHttpContentConverter
{
    public HttpContent ConvertToHttpContent(object value)
    {
        if (value is IEnumerable<KeyValuePair<string, string>> list)
            return new FormUrlEncodedContent(list);

        throw new Exception($"Bad value for {nameof(FormUrlEncodedContent)}");
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        var contentString = await httpContent.ReadAsStringAsync();
        var formData = HttpUtility.ParseQueryString(contentString);

        var result = Activator.CreateInstance<TOutput>();
        var resultType = typeof(TOutput);

        foreach (var key in formData.AllKeys)
        {
            var propertyInfo = resultType.GetProperty(key);

            if (propertyInfo == null || !propertyInfo.CanWrite)
                continue;

            var value = formData[key];
            propertyInfo.SetValue(result, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }

        return result;
    }
}