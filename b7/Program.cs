using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b7
{
    public class TaiKhoanNganHang
    {
        public string SoTaiKhoan { get; private set; }
        public string ChuTaiKhoan { get; private set; }

        // Bên ngoài chỉ được xem số dư.
        // Muốn thay đổi số dư phải thông qua nạp, rút hoặc chuyển tiền.
        public decimal SoDu { get; private set; }

        // soDu = 0 là tham số mặc định.
        // Nếu không truyền số dư ban đầu thì tài khoản có số dư bằng 0.
        public TaiKhoanNganHang(
            string soTaiKhoan,
            string chuTaiKhoan,
            decimal soDu = 0)
        {
            if (string.IsNullOrWhiteSpace(soTaiKhoan))
            {
                throw new ArgumentException(
                    "Số tài khoản không được trống.");
            }

            if (string.IsNullOrWhiteSpace(chuTaiKhoan))
            {
                throw new ArgumentException(
                    "Tên chủ tài khoản không được trống.");
            }

            // Không cho tạo tài khoản có số dư âm.
            if (soDu < 0)
            {
                throw new ArgumentException(
                    "Số dư ban đầu không được âm.");
            }

            SoTaiKhoan = soTaiKhoan.Trim();
            ChuTaiKhoan = chuTaiKhoan.Trim();
            SoDu = soDu;
        }

        // bool: trả về true nếu thành công, false nếu thất bại.
        public bool NapTien(decimal soTien)
        {
            // Tiền nạp phải lớn hơn 0.
            // Điều kiện thứ hai tránh vượt giới hạn của kiểu decimal.
            if (soTien <= 0 || soTien > decimal.MaxValue - SoDu)
            {
                return false;
            }

            // Tương đương: SoDu = SoDu + soTien;
            SoDu += soTien;

            return true;
        }

        public bool RutTien(decimal soTien)
        {
            // Không cho rút số tiền không dương hoặc vượt quá số dư.
            // || có nghĩa là "hoặc".
            if (soTien <= 0 || soTien > SoDu)
            {
                return false;
            }

            // Chỉ trừ tiền sau khi đã kiểm tra đủ số dư.
            SoDu -= soTien;

            return true;
        }

        public bool ChuyenTien(TaiKhoanNganHang nguoiNhan, decimal soTien)
        {
            // null nghĩa là chưa có đối tượng tài khoản nhận.
            if (nguoiNhan == null)
            {
                return false;
            }

            // Không cho chuyển vào cùng số tài khoản.
            if (nguoiNhan.SoTaiKhoan == SoTaiKhoan)
            {
                return false;
            }

            // Kiểm tra số tiền chuyển và số dư người gửi.
            if (soTien <= 0 || soTien > SoDu)
            {
                return false;
            }

            // Kiểm tra giới hạn số dư người nhận trước khi chuyển.
            if (soTien > decimal.MaxValue - nguoiNhan.SoDu)
            {
                return false;
            }

            // Khi mọi điều kiện đều hợp lệ:
            // trừ tiền người gửi và cộng tiền người nhận.
            SoDu -= soTien;
            nguoiNhan.SoDu += soTien;

            return true;
        }

        public void HienThi()
        {
            Console.WriteLine(
                "{0} - {1} - Số dư: {2:N0} đồng",
                SoTaiKhoan, ChuTaiKhoan, SoDu);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            TaiKhoanNganHang tk1 = new TaiKhoanNganHang(
                "1001", "Nguyễn Văn An", 1000000m);

            TaiKhoanNganHang tk2 = new TaiKhoanNganHang(
                "1002", "Trần Thị Bình", 500000m);

            Console.WriteLine("--- TRƯỚC GIAO DỊCH ---");
            tk1.HienThi();
            tk2.HienThi();

            Console.WriteLine(
                "\nTK1 nạp 200.000 đồng: " +
                (tk1.NapTien(200000m) ? "Thành công" : "Thất bại"));

            Console.WriteLine(
                "TK1 rút 300.000 đồng: " +
                (tk1.RutTien(300000m) ? "Thành công" : "Thất bại"));

            Console.WriteLine(
                "TK1 chuyển 400.000 đồng cho TK2: " +
                (tk1.ChuyenTien(tk2, 400000m) ? "Thành công" : "Thất bại"));

            // TK1 còn 500.000 đồng nên không thể rút 1.000.000 đồng.
            Console.WriteLine(
                "TK1 rút 1.000.000 đồng: " +
                (tk1.RutTien(1000000m) ? "Thành công" : "Không đủ số dư"));

            Console.WriteLine("\n--- SAU GIAO DỊCH ---");
            tk1.HienThi();
            tk2.HienThi();
        }
    }
}
