using System.Text.RegularExpressions;

namespace API_WebApplication.Helpers
{
    public class CheckPhoneNumber
    {
        public string ValidateAndFormatPhoneNumber(string? phoneNumber)
        {
            // Kiểm tra xem số điện thoại có đúng định dạng không
            var regex = new Regex(@"^\+?(\d{10,12})$");
            var match = regex.Match(phoneNumber);

            if (!match.Success)
            {
                return ""; // Trả về một chuỗi rỗng khi số điện thoại không hợp lệ
            }

            var phone = match.Groups[1].Value;

            if (phone.Length == 10 || phone.Length == 11)
            {
                return "+84" + phone.Substring(phone.Length - 9);
            }

            return ""; 
        }
    }
}
