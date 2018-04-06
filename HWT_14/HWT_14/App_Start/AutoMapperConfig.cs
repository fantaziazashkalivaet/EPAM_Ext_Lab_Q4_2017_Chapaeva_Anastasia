namespace HWT_14.App_Start
{
    using AutoMapper;
    using DAL.Model;
    using Models;

    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SummaryOfOrder, SummaryOfOrderViewModel>());
        }
    }
}