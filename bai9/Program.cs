using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bai9
{
    public class HinhChuNhat
    {
        // Hai trường private lưu giá trị thực tế.
        // Bên ngoài không truy cập trực tiếp được.
        private double chieuDai;
        private double chieuRong;

        // Thuộc tính dùng để đọc và thay đổi chiều dài.
        public double ChieuDai
        {
            // get chạy khi đọc giá trị, ví dụ: hcn.ChieuDai.
            get
            {
                return chieuDai;
            }

            // set chạy khi gán giá trị, ví dụ: hcn.ChieuDai = 5.
            set
            {
                // value chính là giá trị đang được gán.
                // Ngoài số không dương, loại cả NaN và vô cực.
                if (value <= 0 ||
                    double.IsNaN(value) ||
                    double.IsInfinity(value))
                {
                    throw new ArgumentException(
                        "Chiều dài phải là số dương hữu hạn.");
                }

                // Chỉ lưu giá trị khi hợp lệ.
                chieuDai = value;
            }
        }

        public double ChieuRong
        {
            get
            {
                return chieuRong;
            }

            set
            {
                if (value <= 0 ||
                    double.IsNaN(value) ||
                    double.IsInfinity(value))
                {
                    throw new ArgumentException(
                        "Chiều rộng phải là số dương hữu hạn.");
                }

                chieuRong = value;
            }
        }

        // Constructor khởi tạo hai cạnh.
        public HinhChuNhat(double chieuDai, double chieuRong)
        {
            // Gán qua thuộc tính để chạy phần kiểm tra trong set.
            // Không gán thẳng vào trường private.
            ChieuDai = chieuDai;
            ChieuRong = chieuRong;
        }

        public double TinhChuVi()
        {
            // Chu vi = 2 × (chiều dài + chiều rộng).
            return 2 * (ChieuDai + ChieuRong);
        }

        public double TinhDienTich()
        {
            // Diện tích = chiều dài × chiều rộng.
            return ChieuDai * ChieuRong;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Console.Write("Nhập chiều dài: ");
                double dai = double.Parse(Console.ReadLine() ?? "");

                Console.Write("Nhập chiều rộng: ");
                double rong = double.Parse(Console.ReadLine() ?? "");

                HinhChuNhat hcn = new HinhChuNhat(dai, rong);

                Console.WriteLine("Chu vi: {0:N2}", hcn.TinhChuVi());
                Console.WriteLine("Diện tích: {0:N2}", hcn.TinhDienTich());
            }
            catch (FormatException)
            {
                Console.WriteLine("Lỗi: Bạn phải nhập bằng số.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Lỗi: Giá trị nhập quá lớn.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
    }
}    

