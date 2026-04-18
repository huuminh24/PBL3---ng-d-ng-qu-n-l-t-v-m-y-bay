using System;
using System.Security.Cryptography;
using System.Text;
using FlightTicketManager.DAL;
using FlightTicketManager.Models;

namespace FlightTicketManager.BLL
{
    public class AccountBLL
    {
        private AccountDAL _accountDAL;

        public AccountBLL()
        {
            _accountDAL = new AccountDAL();
        }

        // Mã hóa mật khẩu (Đơn giản bằng SHA256)
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Logic xử lý Đăng nhập
        public bool Login(string username, string password, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!";
                return false;
            }

            try
            {
                Account account = _accountDAL.GetAccountByUsername(username);

                if (account == null)
                {
                    errorMessage = "Tên đăng nhập không tồn tại!";
                    return false;
                }

                if (account.Status == "Inactive")
                {
                    errorMessage = "Tài khoản của bạn đã bị khóa!";
                    return false;
                }

                string hashedPassword = HashPassword(password);
                if (account.Password != hashedPassword && account.Password != password) // Chấp nhận cả plain text cũ để phòng trường hợp có sẵn data
                {
                    errorMessage = "Mật khẩu không chính xác!";
                    return false;
                }

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi kết nối cơ sở dữ liệu: " + ex.Message;
                return false;
            }
        }

        // Logic xử lý Đăng ký
        public bool Register(string fullName, string username, string password, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Vui lòng nhập đầy đủ thông tin (Họ tên, Tên đăng nhập, Mật khẩu)!";
                return false;
            }

            // Kiểm tra Họ tên: Chỉ chứa chữ cái và khoảng trắng
            if (!System.Text.RegularExpressions.Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
            {
                errorMessage = "Họ tên chỉ được chứa chữ cái và khoảng trắng!";
                return false;
            }

            // Kiểm tra chữ cái đầu viết hoa
            string[] words = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (!char.IsUpper(word[0]))
                {
                    errorMessage = "Chữ cái đầu của mỗi từ trong Họ tên phải được viết hoa!";
                    return false;
                }
            }

            // Kiểm tra Mật khẩu: ít nhất 8 ký tự, 1 hoa, 1 đặc biệt
            if (password.Length < 8)
            {
                errorMessage = "Mật khẩu phải có ít nhất 8 ký tự!";
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]"))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất 1 ký tự viết hoa!";
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất 1 ký tự đặc biệt!";
                return false;
            }

            try
            {
                // Kiểm tra trùng lặp
                Account existingAccount = _accountDAL.GetAccountByUsername(username);
                if (existingAccount != null)
                {
                    errorMessage = "Tên đăng nhập này đã có người sử dụng. Vui lòng chọn tên khác!";
                    return false;
                }

                // Tạo mới Account
                Account newAccount = new Account
                {
                    Username = username,
                    Password = HashPassword(password),
                    RoleId = 2, // Mặc định 2: Customer (Giả sử 1 là Admin, 2 là Customer)
                    Status = "Active"
                };

                // Tạo profile
                Profile newProfile = new Profile
                {
                    FullName = fullName,
                    // Có thể bổ sung Email, Phone nếu form mở rộng sau này
                };

                // Thực hiện lưu qua DAL
                bool isSuccess = _accountDAL.AddAccountWithProfile(newAccount, newProfile);
                if (isSuccess)
                {
                    errorMessage = string.Empty;
                    return true;
                }
                else
                {
                    errorMessage = "Lỗi khi lưu dữ liệu vào hệ thống. Vui lòng thử lại!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi kết nối cơ sở dữ liệu: " + ex.Message;
                return false;
            }
        }
    }
}
