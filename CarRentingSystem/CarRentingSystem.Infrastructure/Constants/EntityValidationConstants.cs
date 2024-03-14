using static CarRentingSystem.Infrastructure.Constants.GeneralApplicationConstants;

namespace CarRentingSystem.Infrastructure.Constants
{
	public static class EntityValidationConstants
	{
        public static class Category
        {
            public const int NameMinLength = 2;

            public const int NameMaxLength = 50;

        }

        public static class Car
        {
            public const int ColorMinLength = 2;

            public const int ColorMaxLength = 50;

            public const int YearMinValue = 1900;

            public const int YearMaxValue = ReleaseYear;

            public const int DescriptionMinLength = 10;

            public const int DescriptionMaxLength = 500;

            public const int FuelMinValue = 1;

            public const int FuelMaxValue = 3;

            public const int GearMinValue = 1;

            public const int GearMaxValue = 2;

        }

        public static class Brand
        {
            public const int NameMinLength = 2;

            public const int NameMaxLength = 50;

            public const int ModelMinLength = 2;

            public const int ModelMaxLength = 50;

        }

        public static class Dealer
        {
            public const int PhoneNumberMinLength = 7;

            public const int PhoneNumberMaxLength = 15;

        }
    }
}

