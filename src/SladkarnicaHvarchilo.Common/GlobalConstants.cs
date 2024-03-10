namespace SladkarnicaHvarchilo.Common
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class GlobalConstants
    {
        public const string SystemName = "SladkarnicaHvarchilo";

        public const string AdministratorRoleName = "Administrator";

        public const string AdministrationSettingsDateTimeFormat = "dd.MM.yyyyг., HH:mm ч.";

        public static class NutritionInfoConstants
        {
            public const double MacrosMinValue = 0.0;
            public const double MacrosMaxValue = 100.0;

            public const int EnergyMaxLenght = 20;
        }

        public static class PastryValidationConstants
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

            public const double PriceMinValue = 0.01;
            public const double PriceMaxValue = 1000.00d;

            public const double PiecesMinValue = 1;
            public const double PiecesMaxValue = 100;
        }

        public static class OrderCriteria
        {
            public const string PriceAscending = "Цена възходящо";

            public const string PriceDescending = "Цена низходящо";

            public const string PiecesAscending = "Брой парчета възходящо";

            public const string PiecesDescending = "Брой парчета низходящо";

            public const string Recent = "Най-нови";

            public const string Name = "Име";

            public static readonly string[] AllOrderCriteria = new ReadOnlyCollection<string>(new List<string>
                                                               {
                                                                   PriceAscending,
                                                                   PriceDescending,
                                                                   PiecesAscending,
                                                                   PiecesDescending,
                                                                   Recent,
                                                                   Name,
                                                               })
                                                               .ToArray();
        }

        public static class UserMessage
        {
            public const string InvalidInputData = "Невалидни Данни";

            public const string CakeAlreadyExist = "Торта със същото име вече съществува";

            public const string SuccessfullyAddedNewCake = "Успешно добавихте нова торта. Резултат:";

            public const string SuccessfullyEditedCake = "Успешно редактирахте торта. Резултат:";

            public const string InvalidImageFile = "Невалиден или повреден файл";

            public const string CakeDoesNotExist = "Не е намерена торта с въведенте данни";

            public const string NoChangesHaveBeenMade = "Не са направени никакво промени. Моля редактирайтел";

            public const string SuccessfullyDeletedCake = "Успешно премахнахте тортата ";
        }
    }
}
