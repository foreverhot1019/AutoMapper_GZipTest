using AutoMapper;
using System;

namespace webApiTest
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }


    public class Source
    {
        public string _StrVal { get; set; }

        public int _IntVal { get; set; }

        public DateTime _dtValue { get; set; }

        public bool _tfValue { get; set; }
        public Decimal? _expValue { get; set; }
    }
    public class Destination
    {
        public string StrVal { get; set; }

        public int IntVal { get; set; }

        public DateTime dtValue { get; set; }

        public bool tfValue { get; set; }

        public Decimal? ExpValue { get; set; }
    }
    public class MyAutoMapperProfile : Profile
    {
        public MyAutoMapperProfile()
        {
            ClearPrefixes();
            RecognizePrefixes("_");
            var mapper = CreateMap<Source, Destination>().ForMember(x => x.ExpValue, src => src.Ignore())
               .ReverseMap();
            //SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            //DestinationMemberNamingConvention = new PascalCaseNamingConvention();    //将CreateMap 等等放在这里
        }
    }

    public class MyMapprt<TSource, TDestination>
    {
        protected readonly IMapper _Mapper;
        public MyMapprt(IMapper mapper)
        {
            _Mapper = mapper;
        }
        public TSource ToSource(TDestination entity)
        {
            return _Mapper.Map<TSource>(entity);
        }
        public TDestination ToDestination(TSource model)
        {
            return _Mapper.Map<TDestination>(model);
        }
    }

    //public static class MyAutoMapper
    //{
    //    static MyAutoMapper()
    //    {
    //        _Mapper = new MapperConfiguration(cfg =>
    //        {
    //            cfg.AddProfile<MyAutoMapperProfile>();
    //            //cfg.CreateMap<Source, Destination>();
    //           //.ReverseMap();
    //        }).CreateMapper();
    //    }

    //    internal static IMapper _Mapper { get; }

    //    /// <summary>
    //    /// Maps an entity to a model.
    //    /// </summary>
    //    /// <param name="entity">The entity.</param>
    //    /// <returns></returns>
    //    public static Source ToSource(this Destination entity)
    //    {
    //        return _Mapper.Map<Source>(entity);
    //    }

    //    /// <summary>
    //    /// Maps a model to an entity.
    //    /// </summary>
    //    /// <param name="model">The model.</param>
    //    /// <returns></returns>
    //    public static Destination ToDestination(this Source model)
    //    {
    //        return _Mapper.Map<Destination>(model);
    //    }
    //}
}
