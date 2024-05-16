using Microsoft.AspNetCore.Mvc;
using ViewModel;
using Model;
using System.Runtime.InteropServices;
       
namespace TestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExController : ControllerBase
    {
        // LEAGUES
        [HttpGet]
        [ActionName("GetLeagues")]
        public LeagueList GetLeagues()
        {
            return new LeagueDB().SelectAll();
        }
        [HttpPost]
        public int InsertLeague([FromBody] League league)
        {
            LeagueDB db = new LeagueDB();
            db.Insert(league);
            return db.SaveChanges();
        }
        [HttpPut]
        public int UpdateLeague([FromBody] League league)
        {
            LeagueDB db = new LeagueDB();
            db.Update(league);
            return db.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteLeague(int id)
        {
            LeagueDB db = new LeagueDB();
            db.Delete(LeagueDB.SelectByID(id));
            return db.SaveChanges();
        }
        // MATCHSUMS
		[HttpGet]
        [ActionName("GetMatchSums")]
        public MatchSumList GetMatchSums()
        {
            return new MatchSumDB().SelectAll();
        }
        [HttpPost]
        public int InsertMatchSum([FromBody] MatchSum matchsum)
        {
            MatchSumDB db = new MatchSumDB();
            db.Insert(matchsum);
            return db.SaveChanges();
        }
        [HttpPut]
        public int UpdateMatchSum([FromBody] MatchSum matchsum)
        {
            MatchSumDB db = new MatchSumDB();
            db.Update(matchsum);
            return db.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteMatchSum(int id)
        {
            MatchSumDB db = new MatchSumDB();
            db.Delete(MatchSumDB.SelectByID(id));
            return db.SaveChanges();
        }
        // OFFENCESS
		[HttpGet]
        [ActionName("GetOffencess")]
        public OffencesList GetOffencess()
        {
            return new OffencesDB().SelectAll();
        }
        [HttpPost]
        public int InsertOffences([FromBody] Offences offences)
        {
            OffencesDB db = new OffencesDB();
            db.Insert(offences);
            return db.SaveChanges();
        }
        [HttpPut]
        public int UpdateOffences([FromBody] Offences offences)
        {
            OffencesDB db = new OffencesDB();
            db.Update(offences);
            return db.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteOffences(int id)
        {
            OffencesDB db = new OffencesDB();
            db.Delete(OffencesDB.SelectByID(id));
            return db.SaveChanges();
        }
        //PLAYERS
		[HttpGet]
        [ActionName("GetPlayers")]
        public PlayerList GetPlayers()
        {
            return new PlayerDB().SelectAll();
        }
        [HttpPost]
        public int InsertPlayer([FromBody] Player player)
        {
            PlayerDB db = new PlayerDB();
            db.Insert(player);
            return db.SaveChanges();
        }
        [HttpPut]
        public int UpdatePlayer([FromBody] Player player)
        {
            PlayerDB db = new PlayerDB();
            db.Update(player);
            return db.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeletePlayer(int id)
        {
            PlayerDB db = new PlayerDB();
            db.Delete(PlayerDB.SelectByID(id));
            return db.SaveChanges();
        }
        // SPECIALTEAMS
		[HttpGet]
        [ActionName("GetSpecialTeamss")]
        public SpecialTeamsList GetSpecialTeamss()
        {
            return new SpecialTeamsDB().SelectAll();
        }
        [HttpPost]
        public int InsertSpecialTeams([FromBody] SpecialTeams specialteams)
        {
            SpecialTeamsDB db = new SpecialTeamsDB();
            db.Insert(specialteams);
            return db.SaveChanges();
        }
        [HttpPut]
        public int UpdateSpecialTeams([FromBody] SpecialTeams specialteams)
        {
            SpecialTeamsDB db = new SpecialTeamsDB();
            db.Update(specialteams);
            return db.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteSpecialTeams(int id)
        {
            SpecialTeamsDB db = new SpecialTeamsDB();
            db.Delete(SpecialTeamsDB.SelectByID(id));
            return db.SaveChanges();
        }
        // SPORTS
		[HttpGet]
        [ActionName("GetSports")]
        public SportList GetSports()
        {
            return new SportDB().SelectAll();
        }
        [HttpPost]
        public int InsertSport([FromBody] Sport sport)
        {
            SportDB db = new SportDB();
            db.Insert(sport);
            return db.SaveChanges();
        }
        [HttpPut]
        public int UpdateSport([FromBody] Sport sport)
        {
            SportDB db = new SportDB();
            db.Update(sport);
            return db.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteSport(int id)
        {
            SportDB db = new SportDB();
            db.Delete(SportDB.SelectByID(id));
            return db.SaveChanges();
        }
        // TEAMS
		[HttpGet]
        [ActionName("GetTeams")]
        public TeamList GetTeams()
        {
            return new TeamDB().SelectAll();
        }
        [HttpPost]
        public int InsertTeam([FromBody] Team team)
        {
            TeamDB db = new TeamDB();
            db.Insert(team);
            return db.SaveChanges();
        }
        [HttpPut]
        public int UpdateTeam([FromBody] Team team)
        {
            TeamDB db = new TeamDB();
            db.Update(team);
            return db.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteTeam(int id)
        {
            TeamDB db = new TeamDB();
            db.Delete(TeamDB.SelectByID(id));
            return db.SaveChanges();
        }
        // Users
        [HttpGet]
        [ActionName("GetUser")]
        public UserList GetUser()
        {
            return new UserDB().SelectAll();
        }
        [HttpPost]
        public int InsertUser([FromBody] User user)
        {
            UserDB db = new UserDB();
            db.Insert(user);
            return db.SaveChanges();
        }
        [HttpPut]
        public int UpdateUser([FromBody] User user)
        {
            UserDB db = new UserDB();
            db.Update(user);
            return db.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteUser(int id)
        {
            UserDB db = new UserDB();
            db.Delete(UserDB.SelectByID(id));
            return db.SaveChanges();
        }

    }
}
