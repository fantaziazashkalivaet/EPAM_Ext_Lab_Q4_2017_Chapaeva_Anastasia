namespace FileStorage.DAL.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface ITagRepository
    {
        Tag GetTag(int tagID);

        int GetTagID(string tagName);

        IEnumerable<Tag> GetTags();
    }
}
