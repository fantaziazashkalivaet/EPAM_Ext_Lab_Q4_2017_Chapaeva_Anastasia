﻿namespace FileStorage.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using DAL.Interfaces;
    using DAL.Models;
    using Models;

    [Authorize]
    public class UserDocumentsController : Controller
    {
        private const int PageSize = 12;
        private IFileRepository fileRepository;
        private IUserRepository userRepository;
        private ITagRepository tagRepository;
        private IAccessRepository accessRepository;

        public UserDocumentsController(
            IFileRepository fileRepositoryParam, 
            IUserRepository userRepositoryParam,
            ITagRepository tagRepositoryParam,
            IAccessRepository accessRepositoryParam)
        {
            fileRepository = fileRepositoryParam;
            userRepository = userRepositoryParam;
            tagRepository = tagRepositoryParam;
            accessRepository = accessRepositoryParam;
        }

        public ActionResult GetUserDocuments(int page = 1)
        {
            var userId = GetUserId(); 

            var model = new DocumentsViewModel();
            model.Documents = fileRepository.SearchUsersDocuments(null, string.Empty, userId)
                .OrderBy(d => d.DocumentID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            model.Tags = tagRepository.GetTags() as List<Tag>;
            model.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalItems = fileRepository.SearchUsersDocuments(null, string.Empty, userId).Count()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult GetUserDocuments(DocumentsViewModel model, int page = 1)
        {
            var tags = new List<Tag>();

            if (model.Filter != null)
            {
                foreach (var tagName in model.Filter)
                {
                    tags.Add(new Tag());
                    tags.Last().TagID = tagRepository.GetTagID(tagName);
                }
            }

            var userId = GetUserId();

            model.Documents = fileRepository.SearchUsersDocuments(tags, model.Title, userId)
                .OrderBy(d => d.DocumentID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            model.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalItems = fileRepository.SearchUsersDocuments(tags, model.Title, userId).Count()
            };
            model.Tags = tagRepository.GetTags() as List<Tag>;

            return View(model);
        }

        public FileResult GetFile(string hash)
        {
            var document = fileRepository.GetDocument(hash);

            return File(document.Doc, document.Tag.Type, document.Title + document.Tag.TagName);
        }

        public ActionResult RemoveFile(int id, string title)
        {
            return PartialView(new RemoveDocViewModel() { DocumentID = id, Title = title });
        }

        [HttpPost]
        public ActionResult RemoveFile(RemoveDocViewModel doc)
        {
            fileRepository.DeleteDocument(
                doc.DocumentID, 
                fileRepository.GetDocument(doc.DocumentID).AccessID);

            return RedirectToAction("GetUserDocuments");
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Document doc, HttpPostedFileBase uploadDoc)
        {
            if (ModelState.IsValid && uploadDoc != null)
            {
                byte[] docData = null;

                using (var binaryReader = new BinaryReader(uploadDoc.InputStream))
                {
                    docData = binaryReader.ReadBytes(uploadDoc.ContentLength);
                }

                doc.Doc = docData;
                doc.Date = DateTime.Now;
                doc.Title = uploadDoc.FileName.Remove(uploadDoc.FileName.LastIndexOf("."));
                doc.UserID = userRepository.SearchUserByLogin(User.Identity.Name).UserID;
                doc.AccessID = DocumentAccess.Public;
                doc.Tag = new Tag();
                doc.Tag.TagName = Path.GetExtension(uploadDoc.FileName);
                doc.Tag.TagID = tagRepository.GetTagID(doc.Tag.TagName);

                if (doc.Tag.TagID != 0)
                {
                    fileRepository.CreateDocument(doc);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Файлы этого типа не доступны для загрузки в хранилище");
                    return PartialView();
                }

                return RedirectToAction("GetUserDocuments");
            }

            ModelState.AddModelError(string.Empty, "Что-то пошло не так");
            return PartialView();
        }

        public ActionResult ChangeDocAccess(int docId, DocumentAccess oldIdAccess)
        {
            var model = new AccessViewModel()
            {
                DocumentID = docId,
                OldAccess = oldIdAccess,
                NewAccess = oldIdAccess,
                ListAccess = accessRepository.GetAccess()
            };

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ChangeDocAccess(AccessViewModel model)
        {
            if (model.NewAccess != model.OldAccess)
            {
                accessRepository.ChangeAccess(model.DocumentID, model.NewAccess, model.OldAccess);
            }

            return RedirectToAction("GetUserDocuments");
        }

        public ActionResult ChangeUserAccess(int docId)
        {
            var model = new UsersAccessControlViewModel()
            {
                DocumentID = docId,
                Users = Mapper.Map<IEnumerable<User>, IEnumerable<UserBasicInfo>>(accessRepository.UsersWithAccess(docId))
            };

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ChangeUserAccess(UsersAccessControlViewModel model)
        {
            model.Users = Mapper.Map<IEnumerable<User>, IEnumerable<UserBasicInfo>>(accessRepository.UsersWithAccess(model.DocumentID));
            if (!userRepository.CheckUser(model.ChangeAccessToUser) || string.IsNullOrEmpty(model.ChangeAccessToUser))
            {
                ModelState.AddModelError(string.Empty, "Пользователя с таким логином не существует");
            }
            else
            {
                var user = userRepository.SearchUserByLogin(model.ChangeAccessToUser);
                var userInfo = new UserBasicInfo()
                {
                    UserID = user.UserID,
                    Login = user.Login
                };

                if (userInfo.UserID != GetUserId())
                {
                    accessRepository.ChangePartialAccessToUser(userInfo.UserID, model.DocumentID);
                }
            }

            return RedirectToAction("GetUserDocuments");
        }

        private int GetUserId()
        {
            return userRepository.SearchUserByLogin(User.Identity.Name).UserID;
        }
    }
}