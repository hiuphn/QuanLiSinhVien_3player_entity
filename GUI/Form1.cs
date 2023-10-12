using BUS;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle(dgv);
                var listLop = MaLopService.GetAll();
                var listStudent = SinhvienService.GetAll();
                FillFacultyCombobox(listLop);
                BindGrid(listStudent);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void setGridViewStyle(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void FillFacultyCombobox(List<Lop> listLop)
        {
            this.cbLopHoc.DataSource = listLop;
            this.cbLopHoc.DisplayMember = "TenLop";
            this.cbLopHoc.ValueMember = "MaLop";
        }

        private void BindGrid(List<SinhVien> listStudents)
        {
            dgv.Rows.Clear();
            foreach (var item in listStudents)
            {
                int index = dgv.Rows.Add();
                dgv.Rows[index].Cells[0].Value = item.MaSV;
                dgv.Rows[index].Cells[1].Value = item.HoTenSV;
                if (item.Lop != null)
                {
                    
                    dgv.Rows[index].Cells[2].Value = item.NgaySinh;
                    dgv.Rows[index].Cells[3].Value = item.Lop.TenLop;

                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            StudentModel studentModel = new StudentModel();
            Lop selectedFacultyObj = studentModel.Lops.FirstOrDefault(f => f.TenLop == cbLopHoc.Text);
            try 
            { 
                
                SinhVien std = new SinhVien() { MaSV = txtMSSV.Text, HoTenSV = txtTen.Text, NgaySinh = dtNgaysinh.Value, MaLop = selectedFacultyObj.MaLop};

                if (SinhvienService.FindByID(txtMSSV.Text) == null)
                {
                    SinhvienService.InsertUpdate(std);
                    throw new Exception("Thêm mới thành công !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var listStudent = SinhvienService.GetAll();
            BindGrid(listStudent);
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            StudentModel context = new StudentModel();
            Lop selectedFacultyObj = context.Lops.FirstOrDefault(f => f.TenLop == cbLopHoc.Text);

                DialogResult dl = MessageBox.Show("Bạn có muốn xóa nhân viên", "", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                { 
                    SinhVien dbXoa = context.SinhViens.FirstOrDefault(nv => nv.MaSV == txtMSSV.Text);
                    if (dbXoa != null)
                    {
                        context.SinhViens.Remove(dbXoa);
                        context.SaveChanges();

                        List<SinhVien> listNewNhanvien = context.SinhViens.ToList();
                        dgv.DataSource = null;
                        BindGrid(listNewNhanvien);
                        MessageBox.Show("Xóa nhân viên thành công");
                    } 
                }
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            StudentModel studentModel = new StudentModel();
            Lop selectedFacultyObj = studentModel.Lops.FirstOrDefault(f => f.TenLop == cbLopHoc.Text);
            try
            {

                SinhVien std = new SinhVien() { MaSV = txtMSSV.Text, HoTenSV = txtTen.Text, NgaySinh = dtNgaysinh.Value, MaLop = selectedFacultyObj.MaLop };

                if (SinhvienService.FindByID(txtMSSV.Text) != null)
                {
                    SinhvienService.InsertUpdate(std);
                    throw new Exception("Cập nhật thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var listStudent = SinhvienService.GetAll();
            BindGrid(listStudent);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn thoát chương trình khong", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dl == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            //List<Sinhvien> sinhviens = new List<Sinhvien>();
            string findName = txtTimkiem.Text;
            findName = RemoveDiacritics(findName);
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string name = dgv.Rows[i].Cells[1].Value.ToString();


                name = RemoveDiacritics(name);


                bool contains = name.IndexOf(findName, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    dgv.Rows[i].Visible = true;
                }
                else
                {
                    dgv.Rows[i].Visible = false;
                }
            }
        }
        private string RemoveDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private void dgv_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            txtMSSV.Text = dgv.Rows[index].Cells[0].Value.ToString();
            txtTen.Text = dgv.Rows[index].Cells[1].Value.ToString();
            cbLopHoc.Text = dgv.Rows[index].Cells[3].Value.ToString();
            dtNgaysinh.Text = dgv.Rows[index].Cells[2].Value.ToString();
        }
    }
}
