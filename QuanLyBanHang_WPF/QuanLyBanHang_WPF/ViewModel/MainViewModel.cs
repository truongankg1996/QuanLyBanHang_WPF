using QuanLyBanHang_WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyBanHang_WPF.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        #region Thuộc tính khác
        public ICommand ThoatCommand { get; set; }
        public ICommand KhachHangCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            ThoatCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            },
            (p) =>
            {
                p.Hide();
                DangNhapWindow window = new DangNhapWindow();
                window.ShowDialog();
                p.Close();
            });

            KhachHangCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            },
            (p) =>
            {
                KhachHangWindow window = new KhachHangWindow();
                window.ShowDialog();
            });
        }
    }
}
