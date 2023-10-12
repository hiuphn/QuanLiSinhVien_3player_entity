using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class MaLopService
    {
        public static List<Lop> GetAll()
        {
            StudentModel context = new StudentModel();
            return context.Lops.ToList();
        }
    }
}
