﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanSach_MyClass.Models
{
    public class GioHang
    {
         db_myclassEntities db = new db_myclassEntities();
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien { get { return dDonGia * iSoLuong; }  }
        //taoj gior hangf dung ham tao
        public GioHang(int masach) 
        {
            iMaSach = masach;
            Sach book = db.Sachs.Single(s => s.MaSach == iMaSach);
            sTenSach = book.TenSach;
            sAnhBia = book.AnhBia;
            dDonGia =double.Parse(book.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}