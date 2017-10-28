using AutoMapper;
using Forecasting.App.Services.Models.ForecastingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.VM.Models
{
    public class TourGameResultObservable : TourGameResultBaseObservable
    {
        public int TournamentTeam1Points { get; set; }
        public int TournamentTeam2Points { get; set; }
    }

    public class TourGameResultBaseObservable:ObservableModelWithId<long>
    {
        public long TournamentTeam1Id { get; set; }
        public long TournamentTeam2Id { get; set; }

    }

    public class PlayerTourForecast : TourGameResultBaseObservable
    {
        public string TournamentTeam1Name { get; set; }
        public string TournamentTeam2Name { get; set; }

        public int? TournamentTeam1Points { get; set; }
        public int? TournamentTeam2Points { get; set; }
        public int TourGameResultId { get; set; }

        private bool _forecastEnabled;
        public bool ForecastEnabled
        {
            get
            {
                return _forecastEnabled;
            }
            set
            {
                Set(() => ForecastEnabled, ref _forecastEnabled, value);
            }
        }
    }

    internal class TourGameResultObservableMappingProfile : Profile
    {
        public TourGameResultObservableMappingProfile()
        {
            CreateMap<TourGameResult, TourGameResultObservable>();
            CreateMap<TourGameResult, PlayerTourForecast>().ForMember(dst => dst.TournamentTeam1Points, opt => opt.Ignore())
                .ForMember(dst => dst.TournamentTeam2Points, opt => opt.Ignore())
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.TourGameResultId, opt => opt.MapFrom(x=>x.Id));
            CreateMap<PlayerTourForecast, TourGameForecast>().ForMember(dst=>dst.Team1Points, opt=>opt.MapFrom(x=>x.TournamentTeam1Points))
                .ForMember(dst => dst.Team2Points, opt => opt.MapFrom(x => x.TournamentTeam2Points))
                .AfterMap((map,dst,cntx)=>
                {
                    if (cntx.Options.Items.ContainsKey("PlayerId"))
                        dst.PlayerId = (long)cntx.Options.Items["PlayerId"];
                });
            CreateMap<TourGameResultObservable, TourGameResult>();
        }
    }
}
