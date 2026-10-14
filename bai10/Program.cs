using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bai10
{
    public class SinhVien
    {
        // Các thuộc tính mô tả một sinh viên.
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public double DiemTB { get; set; }

        // Constructor nhận dữ liệu để tạo đối tượng sinh viên.
        public SinhVien(
            string maSV,
            string hoTen,
            DateTime ngaySinh,
            double diemTB)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            DiemTB = diemTB;
        }

        // In thông tin của một sinh viên.
        public void HienThi()
        {
            // dd/MM/yyyy: định dạng ngày/tháng/năm.
            // F2: hiển thị điểm với 2 chữ số sau dấu thập phân.
            Console.WriteLine(
                "{0,-12}{1,-30}{2,-15:dd/MM/yyyy}{3,8:F2}",
                MaSV, HoTen, NgaySinh, DiemTB);
        }
    }
    internal class Program
    {
        static List<SinhVien> danhSach = new List<SinhVien>();

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n===== QUẢN LÝ SINH VIÊN =====");
                Console.WriteLine("1. Thêm sinh viên");
                Console.WriteLine("2. Sửa sinh viên");
                Console.WriteLine("3. Xóa sinh viên");
                Console.WriteLine("4. Tìm kiếm sinh viên");
                Console.WriteLine("5. Sắp xếp danh sách");
                Console.WriteLine("6. Thống kê");
                Console.WriteLine("7. Hiển thị danh sách");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");

                string luaChon = Console.ReadLine() ?? "0";

                switch (luaChon)
                {
                    case "1":
                        ThemSinhVien();
                        break;

                    case "2":
                        SuaSinhVien();
                        break;

                    case "3":
                        XoaSinhVien();
                        break;

                    case "4":
                        TimKiemSinhVien();
                        break;

                    case "5":
                        SapXepDanhSach();
                        break;

                    case "6":
                        ThongKe();
                        break;

                    case "7":
                        InDanhSach(danhSach);
                        break;

                    case "0":
                        Console.WriteLine("Đã thoát chương trình.");
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }

        // Nhập chuỗi và không cho để trống.
        static string NhapChuoi(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string giaTri = (Console.ReadLine() ?? "").Trim();

                if (giaTri.Length > 0)
                    return giaTri;

                Console.WriteLine("Không được để trống. Hãy nhập lại.");
            }
        }

        // Nhập ngày sinh đúng định dạng và không ở tương lai.
        static DateTime NhapNgaySinh()
        {
            while (true)
            {
                Console.Write("Ngày sinh (dd/MM/yyyy): ");
                string chuoi = Console.ReadLine() ?? "";

                DateTime ngaySinh;

                bool hopLe = DateTime.TryParseExact(
                    chuoi.Trim(),
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out ngaySinh);

                if (hopLe && ngaySinh <= DateTime.Today)
                    return ngaySinh;

                Console.WriteLine(
                    "Ngày sinh không hợp lệ. Ví dụ: 24/02/2006.");
            }
        }

        // Cho phép nhập điểm bằng dấu chấm hoặc dấu phẩy.
        static double NhapDiem()
        {
            while (true)
            {
                Console.Write("Điểm trung bình (0–10): ");

                string chuoi = (Console.ReadLine() ?? "")
                    .Trim()
                    .Replace(',', '.');

                double diem;

                bool hopLe = double.TryParse(
                    chuoi,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out diem);

                if (hopLe && diem >= 0 && diem <= 10)
                    return diem;

                Console.WriteLine("Điểm phải là số từ 0 đến 10.");
            }
        }

        // 1. Thêm sinh viên.
        static void ThemSinhVien()
        {
            Console.WriteLine("\n--- THÊM SINH VIÊN ---");

            string ma = NhapChuoi("Mã sinh viên: ");

            bool trungMa = danhSach.Exists(sv =>
                sv.MaSV.Equals(ma, StringComparison.OrdinalIgnoreCase));

            if (trungMa)
            {
                Console.WriteLine("Mã sinh viên đã tồn tại.");
                return;
            }

            string hoTen = NhapChuoi("Họ tên: ");
            DateTime ngaySinh = NhapNgaySinh();
            double diem = NhapDiem();

            SinhVien svMoi = new SinhVien(ma, hoTen, ngaySinh, diem);

            danhSach.Add(svMoi);

            Console.WriteLine("Thêm sinh viên thành công.");
        }

        // 2. Sửa thông tin, giữ nguyên mã sinh viên.
        static void SuaSinhVien()
        {
            string ma = NhapChuoi("Nhập mã sinh viên cần sửa: ");

            var sv = danhSach.Find(x =>
                x.MaSV.Equals(ma, StringComparison.OrdinalIgnoreCase));

            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            Console.WriteLine("Nhập thông tin mới, giữ nguyên mã sinh viên.");

            sv.HoTen = NhapChuoi("Họ tên mới: ");
            sv.NgaySinh = NhapNgaySinh();
            sv.DiemTB = NhapDiem();

            Console.WriteLine("Cập nhật thành công.");
        }

        // 3. Xóa sinh viên theo mã.
        static void XoaSinhVien()
        {
            string ma = NhapChuoi("Nhập mã sinh viên cần xóa: ");

            var sv = danhSach.Find(x =>
                x.MaSV.Equals(ma, StringComparison.OrdinalIgnoreCase));

            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            danhSach.Remove(sv);

            Console.WriteLine("Xóa sinh viên thành công.");
        }

        // 4. Tìm theo một phần mã hoặc họ tên.
        static void TimKiemSinhVien()
        {
            string tuKhoa = NhapChuoi("Nhập mã hoặc họ tên cần tìm: ");

            List<SinhVien> ketQua = danhSach.FindAll(sv =>
                sv.MaSV.IndexOf(
                    tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0
                ||
                sv.HoTen.IndexOf(
                    tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0);

            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không tìm thấy sinh viên phù hợp.");
                return;
            }

            Console.WriteLine("\n--- KẾT QUẢ TÌM KIẾM ---");
            InDanhSach(ketQua);
        }

        // 5. Sắp xếp theo điểm hoặc họ tên.
        static void SapXepDanhSach()
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách đang trống.");
                return;
            }

            Console.WriteLine("1. Điểm trung bình tăng dần");
            Console.WriteLine("2. Điểm trung bình giảm dần");
            Console.WriteLine("3. Họ tên A–Z");
            Console.Write("Chọn cách sắp xếp: ");

            string luaChon = Console.ReadLine() ?? "";

            switch (luaChon)
            {
                case "1":
                    danhSach.Sort((a, b) => a.DiemTB.CompareTo(b.DiemTB));
                    break;

                case "2":
                    danhSach.Sort((a, b) => b.DiemTB.CompareTo(a.DiemTB));
                    break;

                case "3":
                    danhSach.Sort((a, b) =>
                        string.Compare(
                            a.HoTen,
                            b.HoTen,
                            StringComparison.CurrentCultureIgnoreCase));
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    return;
            }

            Console.WriteLine("\n--- DANH SÁCH SAU SẮP XẾP ---");
            InDanhSach(danhSach);
        }

        // 6. Thống kê danh sách sinh viên.
        static void ThongKe()
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách đang trống.");
                return;
            }

            Console.WriteLine("\n--- THỐNG KÊ ---");
            Console.WriteLine("Tổng số sinh viên: " + danhSach.Count);

            Console.WriteLine(
                "Trung bình điểm của danh sách: {0:F2}",
                danhSach.Average(sv => sv.DiemTB));

            Console.WriteLine(
                "Điểm cao nhất: {0:F2}",
                danhSach.Max(sv => sv.DiemTB));

            Console.WriteLine(
                "Điểm thấp nhất: {0:F2}",
                danhSach.Min(sv => sv.DiemTB));

            Console.WriteLine(
                "Số sinh viên có điểm từ 5 trở lên: " +
                danhSach.Count(sv => sv.DiemTB >= 5));

            Console.WriteLine(
                "Số sinh viên có điểm dưới 5: " +
                danhSach.Count(sv => sv.DiemTB < 5));
        }

        // 7. In danh sách dưới dạng bảng.
        static void InDanhSach(List<SinhVien> ds)
        {
            if (ds.Count == 0)
            {
                Console.WriteLine("Danh sách đang trống.");
                return;
            }

            Console.WriteLine(
                "{0,-12}{1,-30}{2,-15}{3,8}",
                "Mã SV", "Họ tên", "Ngày sinh", "Điểm TB");

            foreach (SinhVien sv in ds)
            {
                sv.HienThi();
            }
        }
    }
}

