using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mvc.Models
{

    [MetadataType(typeof(KhachHangmetata))]
    public partial class Khachhang
    {
        internal sealed class KhachHangmetata
        {
            public int MaKH { get; set; }
            [Display(Name = "Họ Tên")]
            [Required(ErrorMessage = "{0} Không ĐƯợc Để Trống!.")]
            public string HoTen { get; set; }

            [Display(Name = "Tài Khoản")]
            //[Range(6,20,ErrorMessage ="{0} Phải Nằm Trong Khoản {1} đến {2} Kí Tự.")]
            [Required(ErrorMessage = "{0} Không ĐƯợc Để Trống!.")]
            public string TaiKhoan { get; set; }

            [Display(Name = "Mật Khẩu")]
            [Required(ErrorMessage = "{0} Không ĐƯợc Để Trống!.")]
            [DataType(DataType.Password)]
            //[StringLength(10,ErrorMessage ="Mật Khẩu Không Quá 10 Kí TỰ.")]
            public string MatKhau { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "{0} Không ĐƯợc Để Trống!.")]
            [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)+$", ErrorMessage = "{0} Không Hợp Lệ.")]
            //[DisplayFormat(DataFormatString = @"\w + ([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
            public string Email { get; set; }

            [Display(Name = "Địa Chỉ")]
            [Required(ErrorMessage = "{0} Không ĐƯợc Để Trống!.")]
            public string DiaChi { get; set; }
            [Display(Name = "ĐIện Thoại")]
            [Required(ErrorMessage = "{0} Không ĐƯợc Để Trống!.")]
            public string DienThoai { get; set; }
            [Display(Name = "Giới Tính")]
            [Required(ErrorMessage = "{0} Không ĐƯợc Để Trống!.")]
            public string GioiTinh { get; set; }
            [Display(Name = "Ngày Sinh")]
            [DataType(DataType.Date)]
            [Required(ErrorMessage = "{0} Không ĐƯợc Để Trống!.")]

            public Nullable<System.DateTime> NgaySinh
            {
                get; set;
            }
        }
    }
}