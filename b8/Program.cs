using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b8
{
    public class NhanVien
    {
        // Các thuộc tính lưu thông tin của một nhân viên.
        public string MaNV { get; private set; }
        public string HoTen { get; private set; }
        public decimal LuongCoBan { get; private set; }
        public decimal HeSoLuong { get; private set; }
        public decimal PhuCap { get; private set; }

        // Constructor nhận thông tin để khởi tạo nhân viên.
        public NhanVien(
            string maNV,
            string hoTen,
            decimal luongCoBan,
            decimal heSoLuong,
            decimal phuCap)
        {
            // Nếu mã hoặc họ tên trống thì không chấp nhận.
            if (string.IsNullOrWhiteSpace(maNV) ||
                string.IsNullOrWhiteSpace(hoTen))
            {
                throw new ArgumentException(
                    "Mã và họ tên không được trống.");
            }

            // Lương cơ bản và phụ cấp không âm.
            // Hệ số lương phải lớn hơn 0.
            if (luongCoBan < 0 || heSoLuong <= 0 || phuCap < 0)
            {
                throw new ArgumentException(
                    "Lương cơ bản, phụ cấp phải không âm; hệ số phải dương.");
            }

            MaNV = maNV.Trim();
            HoTen = hoTen.Trim();
            LuongCoBan = luongCoBan;
            HeSoLuong = heSoLuong;
            PhuCap = phuCap;
        }

        // Tính lương theo công thức của bài.
        public decimal TinhLuong()
        {
            return LuongCoBan * HeSoLuong + PhuCap;
        }

        // In một nhân viên thành một dòng trong bảng lương.
        public void InDongLuong()
        {
            // -8, -24: căn trái trong vùng rộng 8 hoặc 24 ký tự.
            // 15, 10: căn phải trong vùng có độ rộng tương ứng.
            // N0: không hiển thị phần thập phân.
            // N2: hiển thị 2 chữ số sau dấu thập phân.
            Console.WriteLine(
                "{0,-8}{1,-24}{2,15:N0}{3,10:N2}{4,15:N0}{5,15:N0}",
                MaNV, HoTen, LuongCoBan, HeSoLuong, PhuCap, TinhLuong());
        }
    }
        internal class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;

            NhanVien[] danhSach =
            {
            new NhanVien(
                "NV01", "Nguyễn Văn An", 5000000m, 2.0m, 500000m),

            new NhanVien(
                "NV02", "Trần Thị Bình", 5000000m, 2.5m, 700000m),

            new NhanVien(
                "NV03", "Lê Văn Cường", 5000000m, 3.0m, 1000000m)
        };

            Console.WriteLine("BẢNG LƯƠNG NHÂN VIÊN — Đơn vị: đồng\n");

            Console.WriteLine(
                "{0,-8}{1,-24}{2,15}{3,10}{4,15}{5,15}",
                "Mã NV", "Họ tên", "Lương cơ bản",
                "Hệ số", "Phụ cấp", "Tổng lương");
            // Khởi tạo biến cộng dồn tổng lương.
            decimal tongLuong = 0;
            // foreach lần lượt lấy từng nhân viên trong danhSach.
            foreach (NhanVien nv in danhSach)
            {
                // In dòng lương của nhân viên hiện tại.
                nv.InDongLuong();
                // Cộng lương nhân viên hiện tại vào tổng lương.
                tongLuong += nv.TinhLuong();
            }

            Console.WriteLine("\nTổng tiền lương: {0:N0} đồng", tongLuong);
        
    }
    }
}
