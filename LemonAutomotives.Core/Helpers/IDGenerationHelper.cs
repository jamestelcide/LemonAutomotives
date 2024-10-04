
namespace LemonAutomotives.Core.Helpers
{
    public static class IDGenerationHelper
    {
        public static string GenerateSalespersonID(string firstName, string lastName)
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 100000);

            string newUsername = $"{firstName[0]}{lastName}{randomNumber}";
            return newUsername;
        }
    }
}
