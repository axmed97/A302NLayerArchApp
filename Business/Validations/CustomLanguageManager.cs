using FluentValidation.Resources;


namespace Business.Validations
{
    public class CustomLanguageManager : LanguageManager
    {
        public CustomLanguageManager()
        {
            AddTranslation("az", "FirstnameIsRequired", "Ad boş ola bilməz!");
            AddTranslation("ru-RU", "FirstnameIsRequired", "Имя не может быть пустым!");
            AddTranslation("en-US", "FirstnameIsRequired", "First name can't be empty!");
        }
    }
}
