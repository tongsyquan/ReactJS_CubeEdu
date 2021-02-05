using MyWeb.DAO;
using MyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MyWeb.API.Controller
{
    public class TeacherController : ApiController
    {
        //
        // GET: /Teacher/

        //public object GetList()
        //{
        //    BaseDAO dao = new BaseDAO();
        //    var lst = dao.Model.Teachers.Where(f => f.IsDelete == false).ToList();
        //    return lst;
        //}     

        public object Search(string info, string BranchID)
        {
            BaseDAO dao = new BaseDAO();
            //string sql = string.Format("exec [Customer_Search] N'{0}'", info);
            string sql = string.Format("exec [dbo].[Teacher_Search] N'{0}', N'{1}'", info, BranchID);
            var lst = dao.GetDataTable(sql);
            return lst;
        }

        public object GetClassTimeTables(string  id)
        {
            BaseDAO dao = new BaseDAO();
            string sql = string.Format("exec [dbo].[Teacher_Schedule] N'{0}'", id);
            var lst = dao.GetDataTable(sql);
            return lst;
        }



        [HttpPost]
        public object DeleteTeacher(int id)
        {
            BaseDAO dao = new BaseDAO();
            var myTeacher = dao.Model.Teachers.FirstOrDefault(f => f.ID == id);
            if (myTeacher != null)
            {
                myTeacher.IsDelete = true;

                //luu xuong DB
                dao.Model.SaveChanges();
            }
            var lst = dao.Model.Teachers.Where(f => f.IsDelete == false).ToList();
            return lst;
        }

        public object Update(Teacher item)
        {
            BaseDAO dao = new BaseDAO();
            var myTeacher = dao.Model.Teachers.FirstOrDefault(f => f.ID == item.ID);
            if (myTeacher != null)
            {
                myTeacher.Name = item.Name;
                myTeacher.Email = item.Email;
                myTeacher.Phone = item.Phone;
                myTeacher.Gender = item.Gender;
                myTeacher.Birthday = item.Birthday;
                myTeacher.PlaceOfBirth = item.PlaceOfBirth;
                myTeacher.Address = item.Address;
                //myTeacher.CubeID = item.CubeID;
                myTeacher.Images = item.Images;
                myTeacher.BeginDate = item.BeginDate;
                myTeacher.EndDate = item.EndDate;
                myTeacher.WorkPermits = item.WorkPermits;
                myTeacher.BranchID = item.BranchID;
           
               

                //luu xuong DB
                try
                {
                    dao.Model.SaveChanges();
                }
                catch (DbEntityValidationException)
                {
                    return false;
                }
            }
            else
            {
                item.CreateBy = "Admin";
                item.CreateTime = DateTime.Now;
                item.IsDelete = false;

                dao.Model.Teachers.Add(item);
                try
                {
                    dao.Model.SaveChanges();
                }
                catch (DbEntityValidationException)
                {
                    return false;
                }
            }
            return true;
        }

     

        public object GetListBranch()
        {
            BaseDAO dao = new BaseDAO();
            var lstBranch = dao.Model.Branches.ToList();
            return lstBranch;
        }

        public object GetListProvince()
        {
            BaseDAO dao = new BaseDAO();
            var lstProvince = dao.Model.Provinces.ToList();
            return lstProvince;
        }

        public object GetListDistrict(string ID)
        {
            BaseDAO dao = new BaseDAO();
            var lstDistrict = dao.Model.Districts.Where(f => f.ProvinceID == ID).ToList();
            return lstDistrict;
        }

        public object GetListStatusTeacher()
        {
            BaseDAO dao = new BaseDAO();
            var lstStatusTeacher = dao.Model.StatusTeachers.ToList();
            return lstStatusTeacher;
        }

        public object GetSingleTeacher(int id)
        {
            BaseDAO dao = new BaseDAO();
            var myTeacher = dao.Model.Teachers.FirstOrDefault(f => f.ID == id);
            return myTeacher;
        }
        //[HttpPost]
        //public object GetClassTimeTables(string ClassID)
        //{
        //    BaseDAO dao = new BaseDAO();
        //    var lst = dao.Model.ClassTimeTables.Where(f => f.ClassID == ClassID).ToList();

        //    return lst;
        //}

        private static readonly string[] VietnameseSigns = new string[]
       {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
       };

        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }


        public void Test()
        {
            BaseDAO dao = new BaseDAO();
            var lstProv = dao.Model.Provinces.ToList();
            foreach (var item in lstProv)
            {
                string NameKoDAU = RemoveSign4VietnameseString(item.Name);
                var id = item.ID; ///AG

                string sql = string.Format("Update JTEXPRESS_WARD set Trust_MaTinhThanh='{0}' where KHONG_DAU='{1}'", id, NameKoDAU);
                dao.Model.Database.ExecuteSqlCommand(sql);

            }

        }


    }
}