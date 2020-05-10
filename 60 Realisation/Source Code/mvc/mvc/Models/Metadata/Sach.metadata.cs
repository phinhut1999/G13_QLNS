using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace mvc.Models
{
	[MetadataType(typeof(MetadataSach))]
	public partial class Sach
	{
		internal sealed class MetadataSach{

            [Display(Name = "Mã Sách")]
            public int MaSach { get; set; }
            [Display(Name = "Tên Sách")]
            public string TenSach { get; set; }
            [Display(Name = "Giá Bán")]
            public Nullable<decimal> GiaBan { get; set; }
            [Display(Name = "Mô Tả")]
            public string MoTa { get; set; }
            [Display(Name = "Ảnh Bìa")]
            public string AnhBia { get; set; }
            [Display(Name = "Ngày Cập Nhạt")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
            [Display(Name = "Số Lượng Tồn")]
            public Nullable<int> SoLuongTon { get; set; }
            [Display(Name = "Nhà Xuất Bản")]
            public Nullable<int> MaNXB { get; set; }
            [Display(Name = " CHủ Đề")]
            public Nullable<int> MaChuDe { get; set; }
            [Display(Name = "Mới")]
            public Nullable<int> Moi { get; set; }
        }
    }
}