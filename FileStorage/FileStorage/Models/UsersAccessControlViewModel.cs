namespace FileStorage.Models
{
    using System.Collections.Generic;

    public class UsersAccessControlViewModel
    {
        public IEnumerable<UserBasicInfo> Users { get; set; }

        public string ChangeAccessToUser { get; set; }

        public int DocumentID { get; set; }
    }
}