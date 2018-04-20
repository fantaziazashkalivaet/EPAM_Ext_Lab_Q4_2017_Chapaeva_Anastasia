namespace FileStorage.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using DAL.Interfaces;
    using DAL.Models;
    using Models;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private const int PageSize = 12;
        private IFileRepository fileRepository;
        private IUserRepository userRepository;
        private ITagRepository tagRepository;
        private IAccessRepository accessRepository;
        
        public AdminController(
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

        public ActionResult GetAllDocuments(int page = 1)
        {
            var model = new DocumentsViewModel();

            model.Documents = fileRepository.SearchDocuments(null, string.Empty)
                .OrderBy(d => d.DocumentID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            model.Tags = tagRepository.GetTags() as List<Tag>;
            model.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalItems = fileRepository.SearchDocuments(null, string.Empty).Count()
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

            model.Documents = fileRepository.SearchDocuments(tags, model.Title)
                .OrderBy(d => d.DocumentID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            model.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalItems = fileRepository.SearchDocuments(tags, model.Title).Count()
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

            return RedirectToAction("GetAllDocuments");
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

            return RedirectToAction("GetAllDocuments");
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

                if (userInfo.UserID != fileRepository.GetHolder(model.DocumentID))
                {
                    accessRepository.ChangePartialAccessToUser(userInfo.UserID, model.DocumentID);
                }
            }

            return RedirectToAction("GetAllDocuments");
        }
    }
}