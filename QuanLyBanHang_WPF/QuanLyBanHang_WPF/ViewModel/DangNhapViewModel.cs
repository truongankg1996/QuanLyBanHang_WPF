using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyBanHang_WPF.ViewModel
{
    public class DangNhapViewModel:BaseViewModel
    {
        #region Thuộc tính Binding
        private string _TenDangNhap;
        public string TenDangNhap { get => _TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }

        public string _MatKhau;
        public string MatKhau { get => _MatKhau; set { _MatKhau = value; OnPropertyChanged(); } }
        #endregion

        #region  Thuộc tính khác
        public bool IsLogin { get; set; }

        public ICommand DangNhapCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        #endregion

        public DangNhapViewModel()
        {
            IsLogin = false;
            TenDangNhap = "";
            MatKhau = "";
            DangNhapCommand = new RelayCommand<Window>((p) =>
            {
                return p == null ? false : true;
            },
            (p) =>
            {
                DangNhap(p);
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) =>
            {
                return p == null ? false : true;
            }, (p) =>
            {
                MatKhau = p.Password;
            });
            CloseCommand = new RelayCommand<Window>((p) =>
             {
                 return p == null ? false : true;
             }, (p) =>
             {
                 MessageBoxResult result = MessageBox.Show("Bạn có thực sụ muốn thoát không!", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                 if (result == MessageBoxResult.Yes)
                 {
                     p.Close();
                 }

             });
        }

        #region Hàm hổ trợ
        void DangNhap(Window p)
        {
            //string matKhauMaHoa = MD5Hash(Base64Encode(MatKhau));
            //var taiKhoan = DataProvider.Ins.DB.TaiKhoan.Where(x => x.TenDangNhap_TK == TenDangNhap && x.MatKhau_TK == matKhauMaHoa).SingleOrDefault();

            //if (taiKhoan != null)
            //{
            //    IsLogin = true;
            //}
            if (TenDangNhap =="admin"&& MatKhau == "123456")
            {
                IsLogin = true;
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (IsLogin)
            {
                p.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
                p.Close();
            }
        }
        //Mã hóa mật khẩu
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        #endregion
    }
}
