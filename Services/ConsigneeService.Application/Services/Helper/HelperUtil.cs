namespace ConsigneeService.Application.Services.Helper;
public static class HelperUtil
{
    private static readonly Random random = new Random();
    public static string GenerateCode(string type, string? previousCode = null)
    {

        int nextNumber = 1;
        if (!string.IsNullOrEmpty(previousCode))
        {
            // Extract the numeric part from the previous code
            string numericPart = previousCode.Substring(previousCode.IndexOf('-') + 1, 8);
            int previousNumber = int.Parse(numericPart);

            // Increment the previous number by 1
            nextNumber = previousNumber + 1;
        }
        // Format the next number with leading zeros
        string nextNumberFormatted = nextNumber.ToString("D8");

        // Get the current year and month
        int year = DateTime.Now.Year;

        // Create the reference ID with the prefix, year, month, and the next number
        string referenceId = $"{type}-{nextNumberFormatted}-{year}";

        // Example format: OT-2023-07-00000003
        return referenceId;
    }

    public static string GenerateCode(string type)
    {
        // Generate a random number with 8 digits
        int randomNumber = new Random().Next(10000000, 99999999);
        // Get the current year and month
        int year = DateTime.Now.Year;
        // Create the reference ID with the prefix, year, month, and random number
        string referenceId = $"{type}-{year}-{randomNumber}";
        //OT-2023-07-87654321 format
        return referenceId;
    }

   

        public static string GenerateVerificationCode(int codeLength=5)
        {
            const int minDigit = 0;
            const int maxDigit = 9;

            string code = string.Empty;
            for (int i = 0; i < codeLength; i++)
            {
                int digit = random.Next(minDigit, maxDigit + 1);
                code += digit;
            }

            return code;
        }
    public static string NormalizePhoneNumber(string phoneNumber)
    {
      
        if (phoneNumber.StartsWith("+251"))
        {
            return phoneNumber;
        }
        else if (phoneNumber.StartsWith("0") || char.IsDigit(phoneNumber[0]))
        {        
            return "+251" + phoneNumber.TrimStart('0');
        }
        else
        {
            return phoneNumber;
        }
    }
}
