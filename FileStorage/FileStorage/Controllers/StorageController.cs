namespace FileStorage.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using DAL.Interfaces;
    using DAL.Models;
    using Models;

    public class StorageController : Controller
    {
        private const int PageSize = 12;
        private IFileRepository fileRepository;
        private IUserRepository userRepository;
        private ITagRepository tagRepository;
        private IAccessRepository accessRepository;

        public StorageController(
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

        public ActionResult GetPublicDocuments(int page = 1)
        {
            var model = new DocumentsViewModel();
            model.Documents = fileRepository.SearchPublicDocuments(null, string.Empty)
                .OrderBy(d => d.DocumentID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            model.Tags = tagRepository.GetTags() as List<Tag>;
            model.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalItems = fileRepository.SearchPublicDocuments(null, string.Empty).Count()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult GetPublicDocuments(DocumentsViewModel model, int page = 1)
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

            model.Documents = fileRepository.SearchPublicDocuments(tags, model.Title)
                .OrderBy(d => d.DocumentID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            model.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalItems = fileRepository.SearchPublicDocuments(tags, model.Title).Count()
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

        public FileResult GetFile(string hash = null)
        {
            var document = fileRepository.GetDocument(hash);
            if (hash == null)
            {
                return null;
            }
            
            return File(document.Doc, document.Tag.Type, document.Title + document.Tag.TagName);
        }
    }
}