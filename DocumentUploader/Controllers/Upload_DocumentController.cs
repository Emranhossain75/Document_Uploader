using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1.DAL;
using ClassLibrary1.Model;
using Dapper;

namespace DocumentUploader.Controllers
{
    public class Upload_DocumentController : Controller
    {
        public ActionResult FileUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FileUpload(EmpModel emp, HttpPostedFileBase files)
        {

            String FileExt = Path.GetExtension(files.FileName).ToUpper();

            if (FileExt == ".PDF" || FileExt==".PNG" || FileExt==".JPG")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                FileDetailsModel Fd = new  FileDetailsModel();
                Fd.FileName = files.FileName;
                Fd.FileContent = FileDet;
                Fd.RelatedRecordId = emp.RelatedRecordId;
                Fd.RelatedTo = emp.RelatedTo;
                Fd.Note = emp.Note;
                Fd.CreatedBy = emp.CreatedBy;
                SaveFileDetails(Fd);
                return RedirectToAction("FileDetailsTest");
            }
            else
            {

                ViewBag.FileStatus = "Invalid file format.";
                return View();

            }

        }

        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            List<FileDetailsModel> ObjFiles = GetFileList();

            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();

            return File(FileById.FileContent, "application/pdf", FileById.FileName);

        }

        #region View Uploaded files  
        [HttpGet]
        public PartialViewResult FileDetails()
        {
            List<FileDetailsModel> DetList = GetFileList();

            return PartialView("FileDetails", DetList);
        }

        [HttpGet]
        public ActionResult FileDetailsTest()
        {
            List<FileDetailsModel> DetList = GetFileList();

            return View(DetList);
        }
        private List<FileDetailsModel> GetFileList()
        {
            List<FileDetailsModel> DetList = new List<FileDetailsModel>();

            DbConnection();
            con.Open();
            DetList = SqlMapper.Query<FileDetailsModel>(con, "GetFileDetails", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList;
        }

        #endregion

        #region Database related operations  
        private void SaveFileDetails(FileDetailsModel objDet)
        {

            DynamicParameters Parm = new DynamicParameters();
            Parm.Add("@FileName", objDet.FileName);
            Parm.Add("@FileContent", objDet.FileContent);
            Parm.Add("@RelatedTo", objDet.RelatedTo);
            Parm.Add("@RelatedRecordId", objDet.RelatedRecordId);
            Parm.Add("@CreatedBy", objDet.CreatedBy);
            Parm.Add("@Note", objDet.Note);
            Parm.Add("@CreateDate", objDet.CreateDate);

            DbConnection();
            con.Open();
            con.Execute("AddFileDetails", Parm, commandType: System.Data.CommandType.StoredProcedure);
            con.Close();
        }
        #endregion

        #region Database connection  

        private SqlConnection con;
        private string constr;
        private void DbConnection()
        {
            constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);

        }
        #endregion
    }
}