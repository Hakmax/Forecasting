using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forecasting.App.Services.Models.ForecastingModels;
using Forecasting.App.Data.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Forecasting.App.Services.Implementations
{
    internal class TournamentTeamService : ITournamentTeamService
    {
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly Lazy<ITournamentTeamDataService> _tournamentTeamDataService;

        public TournamentTeamService(Lazy<IUnitOfWork> unitOfWork, Lazy<ITournamentTeamDataService> tournamentTeamDataService)
        {
            _unitOfWork = unitOfWork;
            _tournamentTeamDataService = tournamentTeamDataService;
        }
        public TournamentTeam Create(TournamentTeam team)
        {
            var newTeam = Mapper.Map<Entities.Forecast.TournamentTeam>(team);
            _tournamentTeamDataService.Value.Create(newTeam);
            _unitOfWork.Value.Save();
            return Mapper.Map<TournamentTeam>(newTeam);
        }

        public bool Delete(int id)
        {
            var result = false;
            var dbTeam = _tournamentTeamDataService.Value.Get(id);
            if(dbTeam!=null)
            {
                _tournamentTeamDataService.Value.Delete(dbTeam);
                _unitOfWork.Value.Save();
                result = true;
            }
            return result;
        }

        public IList<TournamentTeam> GetList(long tournamentId)
        {
            var items = _tournamentTeamDataService.Value.Query().Where(x=>x.TournamentId==tournamentId).OrderBy(x => x.Name).ProjectTo<TournamentTeam>().ToList();
            return items;
        }

        public TournamentTeam Update(TournamentTeam team)
        {
            var dbItem = _tournamentTeamDataService.Value.Get(team.Id);
            if(dbItem!=null)
            {
                Mapper.Map(team, dbItem);
                _tournamentTeamDataService.Value.Update(dbItem);
                _unitOfWork.Value.Save();
            }
            return team;
        }

       

    }
}
