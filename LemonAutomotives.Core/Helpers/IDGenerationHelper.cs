
namespace LemonAutomotives.Core.Helpers
{
    public static class IDGenerationHelper
    {
        public static string GenerateSalespersonID(string firstName, string lastName)
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 100000);

            string newUsername = $"{firstName[0]}{lastName}{randomNumber}";
            return newUsername.ToUpper();
        }

        public static string GenerateProductID(string manufacturer, string model, string year)
        {
            string newUsername = $"{manufacturer}-{model}-{year}";
            return newUsername.ToUpper();
        }

        public static string GenerateCustomerID(string firstName, string lastName)
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 100000);

            string newUsername = $"CU-{firstName}{lastName}-{randomNumber}";
            return newUsername.ToUpper();
        }
    }
}
