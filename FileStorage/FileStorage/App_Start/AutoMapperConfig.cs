namespace FileStorage.App_Start
{
    using AutoMapper;
    using FileStorage.DAL.Models;
    using FileStorage.Models;

    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserBasicInfo>());
        }
    }
}