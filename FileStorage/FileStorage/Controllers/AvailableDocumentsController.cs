namespace FileStorage.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using DAL.Interfaces;
    using DAL.Models;
    using Models;

    [Authorize]
    public class AvailableDocumentsController : Controller
    {
        private const int PageSize = 12;
        private IFileRepository fileRepository;
        private IUserRepository userRepository;
        private ITagRepository tagRepository;
        private IAccessRepository accessRepository;

        public AvailableDocumentsController(
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

        public ActionResult GetAvailableDocuments(int page = 1)
        {
            var model = new DocumentsViewModel();
            model.Documents = fileRepository.SearchAvailableDocuments(null, string.Empty, GetUserId())
                .OrderBy(d => d.DocumentID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            model.Tags = tagRepository.GetTags() as List<Tag>;
            model.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalItems = fileRepository
                    .SearchAvailableDocuments(null, string.Empty, GetUserId()).Count()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult GetAvailableDocuments(DocumentsViewModel model, int page = 1)
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

            model.Documents = fileRepository.SearchAvailableDocuments(tags, model.Title, userId)
                .OrderBy(d => d.DocumentID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            model.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalItems = fileRepository.SearchAvailableDocuments(tags, model.Title, userId).Count()
            };
            model.Tags = tagRepository.GetTags() as List<Tag>;

            return View(model);
        }

        public ActionResult Details(int id)
        {
            Document d = fileRepository.GetDocument(id);
            if (d != null)
            {
                return PartialView(d);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public FileResult GetFile(string hash)
        {
            var document = fileRepository.GetDocument(hash);

            return File(document.Doc, document.Tag.Type, document.Title + document.Tag.TagName);
        }

        private int GetUserId()
        {
            return userRepository.SearchUserByLogin(User.Identity.Name).UserID;
        }
    }
}