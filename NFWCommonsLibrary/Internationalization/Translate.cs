using System.Windows;

namespace NFWCommonsLibrary.Internationalization
{
    public class Translate
    {
        public static string ResString(string key)
        {
            try
            {
                return Application.Current.FindResource(key).ToString();
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                return ex.Message;
            }
        }
    }
}
