class CheckUserInfo
{
    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.All(char.IsLetter);
    }

    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email && char.IsLetter(email[0]);
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        return !string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
    }

}
