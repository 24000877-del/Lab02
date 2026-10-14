using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b6
{
    public class SanPham
    {
        // public get: bên ngoài lớp được đọc thông tin.
        // private set: chỉ bên trong lớp mới được thay đổi thông tin.
        // Nhờ vậy, bên ngoài không thể tự gán đơn giá hoặc số lượng âm.
        public string MaSP { get; private set; }
        public string TenSP { get; private set; }

        // decimal phù hợp để lưu và tính toán tiền.
        public decimal DonGia { get; private set; }

        // Số lượng sản phẩm là số nguyên.
        public int SoLuong { get; private set; }

        // Constructor: tự động chạy khi tạo đối tượng bằng new SanPham(...).
        // Constructor có tên trùng tên lớp và không có kiểu trả về.
        public SanPham(string maSP, string tenSP, decimal donGia, int soLuong)
        {
            // Kiểm tra mã có null, rỗng hoặc chỉ chứa khoảng trắng không.
            if (string.IsNullOrWhiteSpace(maSP))
            {
                // Ném ngoại lệ để báo dữ liệu không hợp lệ.
                throw new ArgumentException(
                    "Mã sản phẩm không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(tenSP))
            {
                throw new ArgumentException(
                    "Tên sản phẩm không được để trống.");
            }

            // Đơn giá bằng 0 được chấp nhận, nhưng không được âm.
            if (donGia < 0)
            {
                throw new ArgumentException(
                    "Đơn giá không được âm.");
            }

            if (soLuong < 0)
            {
                throw new ArgumentException(
                    "Số lượng không được âm.");
            }

            // Dữ liệu hợp lệ mới được gán vào các thuộc tính.
            // Trim() bỏ khoảng trắng ở đầu và cuối chuỗi.
            MaSP = maSP.Trim();
            TenSP = tenSP.Trim();
            DonGia = donGia;
            SoLuong = soLuong;
        }

        // Phương thức trả về thành tiền của sản phẩm.
        public decimal TinhThanhTien()
        {
            return DonGia * SoLuong;
        }

        // void: phương thức chỉ thực hiện công việc, không trả về giá trị.
        public void HienThi()
        {
            Console.WriteLine("\n--- THÔNG TIN SẢN PHẨM ---");
            Console.WriteLine("Mã sản phẩm: " + MaSP);
            Console.WriteLine("Tên sản phẩm: " + TenSP);

            // N0: định dạng số có phân nhóm hàng nghìn,
            // không hiển thị chữ số sau dấu thập phân.
            Console.WriteLine("Đơn giá: {0:N0} đồng", DonGia);

            Console.WriteLine("Số lượng: " + SoLuong);

            // Gọi phương thức tính thành tiền rồi đưa kết quả ra màn hình.
            Console.WriteLine(
                "Thành tiền: {0:N0} đồng", TinhThanhTien());
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Console.Write("Nhập mã sản phẩm: ");
                string maSP = Console.ReadLine() ?? "";

                Console.Write("Nhập tên sản phẩm: ");
                string tenSP = Console.ReadLine() ?? "";

                Console.Write("Nhập đơn giá: ");
                decimal donGia = decimal.Parse(Console.ReadLine() ?? "");

                Console.Write("Nhập số lượng: ");
                int soLuong = int.Parse(Console.ReadLine() ?? "");

                SanPham sp = new SanPham(maSP, tenSP, donGia, soLuong);

                sp.HienThi();
            }
            catch (FormatException)
            {
                Console.WriteLine("Lỗi: Đơn giá và số lượng phải nhập bằng số.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Lỗi: Giá trị nhập hoặc thành tiền quá lớn.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

        }
    }
}
