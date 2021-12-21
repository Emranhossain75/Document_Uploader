using ClassLibrary1.DAL;
using ClassLibrary1.Model;
using DocumentUploader.Helper;
using DocumentUploader.web.data.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DocumentUploader.Controllers
{
    public class DocumentUploaderController : Controller
    {
        private DocumentAccess dbContext;

        public DocumentUploaderController()
        {
            dbContext = new DocumentAccess(HelperMethod.GetConnectionString());
        }
        // GET: DocumentUploader
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Uploader uploader)
        {
            string fileName = Path.GetFileNameWithoutExtension(uploader.ImageFile.FileName);
            string extension = Path.GetExtension(uploader.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            uploader.Document = "~/ImageAndPDF/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/ImageAndPDF/"), fileName);
            uploader.ImageFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                dbContext.AddDocumnet(uploader);
                return View();
            }

            return View(uploader);

        }

        public ActionResult GetDocumentList()
        {
            return View(dbContext.GetDocument());
        }


        
        public ActionResult FileUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FileUpload(FileDetailsModel fileDetailsModel, HttpPostedFileBase files)
        {

            //String FileExt = Path.GetExtension(files.FileName).ToUpper();

            //if (FileExt == ".PDF")
            //{
            //    Stream str = files.InputStream;
            //    BinaryReader Br = new BinaryReader(str);
            //    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

            //    FileDetailsModel Fd = new FileDetailsModel();
            //    Fd.FileName = files.FileName;
            //    Fd.FileContent = FileDet;
            fileDetailsModel.FileContent = new byte[files.ContentLength];
            files.InputStream.Read(fileDetailsModel.FileContent, 0, files.ContentLength);
                if (ModelState.IsValid)
                {
                    dbContext.AddDocumnetFile(fileDetailsModel);
                    return View();
                }
                return RedirectToAction("FileUpload");
            //}
            //else
            //{

            //    ViewBag.FileStatus = "Invalid file format.";
            //    return View();

            //}

        }
    
    }
}