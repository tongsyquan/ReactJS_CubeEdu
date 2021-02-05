using MyWeb.DAO;
using MyWeb.Models;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http;

namespace MyWeb.API.Controller
{
    public class ClassManagementController : ApiController
    {
        //
        // GET: /ClassManagement/

        [HttpPost]
        public object GetBranchID()
        {
            BaseDAO dao = new BaseDAO();
            var lst = dao.Model.Branches.Where(f => f.IsDelete == false).ToList();
            return lst;
        }

        [HttpPost]
        public object GetSubjectID()
        {
            BaseDAO dao = new BaseDAO();
            var lst = dao.Model.Subjects.Where(f => f.IsDelete == false).ToList();
            return lst;
        }

        [HttpPost]
        public object GetClassRoomID()
        {
            BaseDAO dao = new BaseDAO();
            var lst = dao.Model.ClassRooms.Where(f => f.IsDelete == false).ToList();
            return lst;
        }

        [HttpPost]
        public object GetTeacher()
        {
            BaseDAO dao = new BaseDAO();
            var lst = dao.Model.ClassRooms.Where(f => f.IsDelete == false).ToList();
            return lst;
        }

        [HttpPost]
        public object GetStatusClassID()
        {
            BaseDAO dao = new BaseDAO();
            var lst = dao.Model.StatusClasses.Where(f => f.IsDelete == false).ToList();
            return lst;
        }

        [HttpPost]
        public object GetClassTimeTables(string id)
        {
            BaseDAO dao = new BaseDAO();
            string sql = string.Format("exec [dbo].[Class_TimeTable] N'{0}'", id);
            var lst = dao.GetDataTable(sql);
            return lst;
        }

        [HttpPost]
        public object DeleteClass(string id)
        {
            BaseDAO dao = new BaseDAO();
            var myClass = dao.Model.Classes.FirstOrDefault(f => f.ID == id);
            if (myClass != null)
            {
                myClass.IsDelete = true;
                myClass.DeleteBy = "quan";

                //luu xuong DB
                dao.Model.SaveChanges();
            }
            var lst = dao.Model.Classes.Where(f => f.IsDelete == false).ToList();
            return lst;
        }

        [HttpPost]
        public object UpdateClass(Class item)
        {
            BaseDAO dao = new BaseDAO();
            var myClass = dao.Model.Classes.FirstOrDefault(f => f.ID == item.ID);
            if (myClass != null)
            {
                myClass.Name = item.Name;
                myClass.BranchID = item.BranchID;
                myClass.SubjectID = item.SubjectID;
                myClass.ClassRoomID = item.ClassRoomID;

                myClass.StatusClassID = item.StatusClassID;
                myClass.Fee = item.Fee;
                myClass.FeeType = item.FeeType;
                myClass.MinStudent = item.MinStudent;
                myClass.MaxStudent = item.MaxStudent;
                myClass.BeginDate = item.BeginDate;
                myClass.NumberOfLesson = item.NumberOfLesson;
                myClass.EndDate = item.EndDate;
                myClass.BeginTime = item.BeginTime;
                myClass.EndTime = item.EndTime;
                myClass.T8 = item.T8;
                myClass.T7 = item.T7;
                myClass.T6 = item.T6;
                myClass.T5 = item.T5;
                myClass.T4 = item.T4;
                myClass.T3 = item.T3;
                myClass.T2 = item.T2;

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
                item.IsDelete = false;

                dao.Model.Classes.Add(item);
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

        public object UpdateClassTimeTables(ClassTimeTable item)
        {
            BaseDAO dao = new BaseDAO();
            var timeTable = new ClassTimeTable();
            timeTable.ClassID = item.ClassID;
            timeTable.Dates = item.Dates;
            dao.Model.ClassTimeTables.Add(timeTable);
            try
            {
                dao.Model.SaveChanges();
            }
            catch (DbEntityValidationException)
            {
                return false;
            }
            return true;
        }

        public object Search(string info)
        {
            BaseDAO dao = new BaseDAO();
            string sql = string.Format("exec [Class_Search] N'{0}'", info);
            var lst = dao.GetDataTable(sql);
            return lst;
        }

        public object GetSingleClass(string id)
        {
            BaseDAO dao = new BaseDAO();
            var myClass = dao.Model.Classes.FirstOrDefault(f => f.ID == id);
            return myClass;
        }
    }
}