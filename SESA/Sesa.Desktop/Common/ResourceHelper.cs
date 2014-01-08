namespace Sesa.Desktop.Common
{
    public static class ResourceHelper
    {
        public static T GetResource<T>(object value)
        {

            return value != null ? (T)App.Current.TryFindResource(value) : default(T);
        }
        public static string GetResource(string value)
        {
            var res = GetResource<string>(value);
            if (res == null)
                return value;
            return res;
        }
        public static object GetResource(object value)
        {
            return GetResource<object>(value);
        }

    }
}
