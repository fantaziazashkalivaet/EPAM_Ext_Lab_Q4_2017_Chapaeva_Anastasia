namespace FileStorage.DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Document
    {
        public int DocumentID { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        public byte[] Doc { get; set; }

        public int UserID { get; set; }

        [Display(Name = "Автор")]
        public string UserName { get; set; }

        [Display(Name = "Доступ")]
        public DocumentAccess AccessID { get; set; }

        public Tag Tag { get; set; }

        [Display(Name = "Дата загрузки")]
        public DateTime Date { get; set; }

        public string Hash { get; set; }
    }
}