namespace FileStorage.DAL.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        public int TagID { get; set; }

        [Display(Name = "Формат")]
        public string TagName { get; set; }

        public string Type { get; set; }
    }
}