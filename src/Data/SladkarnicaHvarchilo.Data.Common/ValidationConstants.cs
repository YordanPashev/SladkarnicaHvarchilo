namespace SladkarnicaHvarchilo.Data.Common
{
    public static class ValidationConstants
    {
        public class CakeValidationConstants
        {
            public const int NameMinLength = 1;
            public const int NameMaxLength = 50;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1500;

            public const int IngredientsMinLength = 10;
            public const int IngredientsMaxLength = 1000;

            public const int ImageFileDirectoryPathMinLength = 5;
            public const int ImageFileDirectoryPathMaxLength = 260;

            public const int AllergensMinLength = 5;
            public const int AllergensMaxLength = 500;
        }
    }
}
