using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class SinhvienService
    {
        public static List<SinhVien> GetAll()
        {
            StudentModel context = new StudentModel();
            return context.SinhViens.ToList();
        }
        public static List<SinhVien> GetAllHasNoMajor()
        {
            StudentModel context = new StudentModel();
            return context.SinhViens.Where(p => p.MaLop == null).ToList();

        }

        public static List<SinhVien> GetAllHasNoMajor(string malop)
        {
            StudentModel context = new StudentModel();
            return context.SinhViens.Where(p => p.MaLop == malop).ToList();
        }

        public static SinhVien FindByID(string studentID)
        {
            StudentModel context = new StudentModel();
            return context.SinhViens.FirstOrDefault(p => p.MaSV == studentID);
        }

        public static void InsertUpdate(SinhVien student)
        {
            StudentModel context = new StudentModel();
            context.SinhViens.AddOrUpdate(student);
            context.SaveChanges();
        }
        public  static void Delete(SinhVien student)
        {
            StudentModel context = new StudentModel();
            context.SinhViens.Remove(student);
            context.SaveChanges();
        }
    }
}
