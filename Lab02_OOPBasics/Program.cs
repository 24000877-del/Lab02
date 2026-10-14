using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02_OOPBasics
{
    public class SinhVien
    {
        private string _hoTen;
        private double _diemTB;
        public string MaSV { get; set; }
        public DateTime NgaySinh { get; set; }
        public string HoTen
        {
            get { return _hoTen; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Họ tên không được rỗng.");
                _hoTen = value.Trim();
            }
        }
        public double DiemTB
        {
            get { return _diemTB; }
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentOutOfRangeException("DiemTB", "Điểm phải từ 0 đến 10.");
                _diemTB = value;
            }
        }
        public SinhVien()
        {
            MaSV = "Chưa có";
            HoTen = "Chưa có";
            NgaySinh = DateTime.Today;
            DiemTB = 0;
        }
        public SinhVien(string maSV, string hoTen, DateTime ngaySinh, double diemTB)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            DiemTB = diemTB;
        }
        public int TinhTuoi()
        {
            int tuoi = DateTime.Today.Year - NgaySinh.Year;
            if (NgaySinh.Date > DateTime.Today.AddYears(-tuoi)) tuoi--;
            return tuoi;
        }
        public string XepLoai()
        {
            if (DiemTB >= 9) return "Xuất sắc";
            if (DiemTB >= 8) return "Giỏi";
            if (DiemTB >= 6.5) return "Khá";
            if (DiemTB >= 5) return "Trung bình";
            return "Yếu";
        }
        public void HienThi()
        {
            Console.WriteLine("{0} - {1} - {2:dd/MM/yyyy} ({3} tuổi) - Điểm: {4:N2} - Xếp loại: {5}",
                MaSV, HoTen, NgaySinh, TinhTuoi(), DiemTB, XepLoai());
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                // Giúp hiển thị tiếng Việt có dấu trên Console
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;

                // --- BÀI 5: Tạo danh sách và thêm 5 dữ liệu mẫu ---
                List<SinhVien> ds = new List<SinhVien>();
                ds.Add(new SinhVien("SV01", "Nguyễn An", new DateTime(2005, 5, 12), 8.2));
                ds.Add(new SinhVien("SV02", "Trần Bình", new DateTime(2005, 9, 3), 6.8));
                ds.Add(new SinhVien("SV03", "Lê Cường", new DateTime(2004, 11, 20), 9.1));
                ds.Add(new SinhVien("SV04", "Phạm Dung", new DateTime(2006, 2, 15), 5.5));
                ds.Add(new SinhVien("SV05", "Hoàng Em", new DateTime(2005, 7, 8), 4.0));

                // In danh sách sinh viên
                Console.WriteLine("=== DANH SÁCH SINH VIÊN ===");
                foreach (SinhVien sv in ds)
                {
                    sv.HienThi();
                }

                // Tìm kiếm theo mã sinh viên (không phân biệt chữ hoa/thường)
                Console.Write("\nNhập mã SV cần tìm: ");
                string ma = Console.ReadLine();

                SinhVien ketQua = ds.Find(x => x.MaSV.Equals(ma, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine(ketQua == null ? "Không tìm thấy" : "Tìm thấy: " + ketQua.HoTen);
            }
        }
    }
}
