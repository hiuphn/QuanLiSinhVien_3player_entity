create database QuanLySV

use QuanLySV

create table Lop(

MaLop char(3) primary key, 
TenLop nvarchar(40) not null
)

create table SinhVien(

MaSV char (6) primary key,

HoTenSV nvarchar(40),

NgaySinh DateTime not null,

MaLop char(3) not null foreign key (MaLop) references Lop (MaLop)
)


insert into Lop (MaLop, TenLop) values ('1',N'21DTHE3')

insert into Lop (MaLop, TenLop) values ('2',N'21DTHE5')


insert into Sinhvien(MaSV,HoTenSV, NgaySinh,MaLop)values ('2',N'Nguy?n Vân Anh','2993/10/18', '1')

insert into Sinhvien(MaSV,HoTenSV,NgaySinh,MaLop) values ('3',N'Lê V?n Nam','2003/11/11','2') 
insert into Sinhvien(MaSV,HoTenSV,NgaySinh,MaLop) values ('4',N'Bùi Lâm Sang','2883/12/18','2')
insert into Sinhvien(MaSV,HoTenSV,NgaySinh,MaLop) values ('1',N'Ph?m Hi?u','2883/12/17','1')