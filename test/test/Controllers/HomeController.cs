using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using test.DAL;
using test.DAL.Models;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private IFileRepository fileRepository;

        public HomeController(IFileRepository repositoryParam)
        {
            fileRepository = repositoryParam;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Storage()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult MyDocuments()
        {
            ViewBag.Message = "Your documents page.";
            ViewBag.Docs = GetDocuments();

            return View();
        }

        [Authorize]
        public ActionResult Create()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Document doc, HttpPostedFileBase uploadDoc)
        {
            if (ModelState.IsValid && uploadDoc != null)
            {
                byte[] docData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadDoc.InputStream))
                {
                    docData = binaryReader.ReadBytes(uploadDoc.ContentLength);
                }
                // установка массива байтов
                doc.Doc = docData;
                doc.Date = DateTime.Now;
                doc.Title = uploadDoc.FileName;
                doc.UserID = 2;
                doc.AccessID = DocumentAccess.Full;
                doc.TagID = 1;

                fileRepository.CreateDocument(doc);
                //AddDB(doc);

                //db.Pictures.Add(pic);
                //db.SaveChanges();

                return RedirectToAction("MyDocuments");
            }

            return View();
        }

        public FileResult GetFile(int id)
        {
            string file_type = "application/jpg";

            var document = fileRepository.GetDocument(id);

            return File(document.Doc, file_type, document.Title);
        }

        public ActionResult RemoveFile(int id)
        {
            fileRepository.DeleteDocument(id, fileRepository.GetDocument(id).AccessID);

            return RedirectToAction("MyDocuments");
        }

        private List<Document> GetDocuments()
        {
            var docs = fileRepository.GetDocuments() as List<Document>;

            return docs;
        }
    }
}