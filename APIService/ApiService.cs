using Model;
using System.Diagnostics;
using System.Net.Http.Json;

namespace APIService
{
    public interface IApiService
    {
        //League
		public Task<LeagueList> GetLeagues();
		public Task<int> InsertLeague(League league);
		public Task<int> UpdateLeague(League league);
		public Task<int> DeleteLeague(League league);
		//MatchSum
		public Task<MatchSumList> GetMatchSums();
		public Task<int> InsertMatchSum(MatchSum matchsum);
		public Task<int> UpdateMatchSum(MatchSum matchsum);
		public Task<int> DeleteMatchSum(MatchSum matchsum);
		//Offences
		public Task<OffencesList> GetOffencess();
		public Task<int> InsertOffences(Offences offences);
		public Task<int> UpdateOffences(Offences offences);
		public Task<int> DeleteOffences(Offences offences);
		//Player
		public Task<PlayerList> GetPlayers();
		public Task<int> InsertPlayer(Player player);
		public Task<int> UpdatePlayer(Player player);
		public Task<int> DeletePlayer(Player player);
		//SpecialTeams
		public Task<SpecialTeamsList> GetSpecialTeamss();
		public Task<int> InsertSpecialTeams(SpecialTeams specialteams);
		public Task<int> UpdateSpecialTeams(SpecialTeams specialteams);
		public Task<int> DeleteSpecialTeams(SpecialTeams specialteams);
		//Sport
		public Task<SportList> GetSports();
		public Task<int> InsertSport(Sport sport);
		public Task<int> UpdateSport(Sport sport);
		public Task<int> DeleteSport(Sport sport);
		//Team
		public Task<TeamList> GetTeams();
		public Task<int> InsertTeam(Team team);
		public Task<int> UpdateTeam(Team team);
		public Task<int> DeleteTeam(Team team);
        //Users
        public Task<UserList> GetUser();
        public Task<int> InsertUser(User user);
        public Task<int> UpdateUser(User user);
        public Task<int> DeleteUser(User user);

    }
    public class ApiService : IApiService
    {
        HttpClient client;
        string uri;
        public ApiService()
        {
            client = new HttpClient();
            uri="http://localhost:5299/api/Ex/";
        }
        public async Task<LeagueList> GetLeagues()
        {
            LeagueList lst = null;
            try
            {
                lst = await client.GetFromJsonAsync<LeagueList>(uri+"GetLeagues");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return lst;
        }
        public async Task<int> InsertLeague(League league)
        {
            var x = await client.PostAsJsonAsync<League>(uri+"InsertLeague",league);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateLeague(League league)
        {
            var x = await client.PutAsJsonAsync<League>(uri+"UpdateLeague",league);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeleteLeague(League league)
        {
            var x = await client.DeleteAsync(uri+"DeleteLeague/"+league.Id);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
		public async Task<MatchSumList> GetMatchSums()
        {
            MatchSumList lst = null;
            try
            {
                lst = await client.GetFromJsonAsync<MatchSumList>(uri+"GetMatchSums");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return lst;
        }
        public async Task<int> InsertMatchSum(MatchSum matchsum)
        {
            var x = await client.PostAsJsonAsync<MatchSum>(uri+"InsertMatchSum",matchsum);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateMatchSum(MatchSum matchsum)
        {
            var x = await client.PutAsJsonAsync<MatchSum>(uri+"UpdateMatchSum",matchsum);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeleteMatchSum(MatchSum matchsum)
        {
            var x = await client.DeleteAsync(uri+"DeleteMatchSum/"+matchsum.Id);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
		public async Task<OffencesList> GetOffencess()
        {
            OffencesList lst = null;
            try
            {
                lst = await client.GetFromJsonAsync<OffencesList>(uri+"GetOffencess");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return lst;
        }
        public async Task<int> InsertOffences(Offences offences)
        {
            var x = await client.PostAsJsonAsync<Offences>(uri+"InsertOffences",offences);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateOffences(Offences offences)
        {
            var x = await client.PutAsJsonAsync<Offences>(uri+"UpdateOffences",offences);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeleteOffences(Offences offences)
        {
            var x = await client.DeleteAsync(uri+"DeleteOffences/"+offences.Id);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
		public async Task<PlayerList> GetPlayers()
        {
            PlayerList lst = null;
            try
            {
                lst = await client.GetFromJsonAsync<PlayerList>(uri+"GetPlayers");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return lst;
        }
        public async Task<int> InsertPlayer(Player player)
        {
            var x = await client.PostAsJsonAsync<Player>(uri+"InsertPlayer",player);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdatePlayer(Player player)
        {
            var x = await client.PutAsJsonAsync<Player>(uri+"UpdatePlayer",player);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeletePlayer(Player player)
        {
            var x = await client.DeleteAsync(uri+"DeletePlayer/"+player.Id);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
		public async Task<SpecialTeamsList> GetSpecialTeamss()
        {
            SpecialTeamsList lst = null;
            try
            {
                lst = await client.GetFromJsonAsync<SpecialTeamsList>(uri+"GetSpecialTeamss");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return lst;
        }
        public async Task<int> InsertSpecialTeams(SpecialTeams specialteams)
        {
            var x = await client.PostAsJsonAsync<SpecialTeams>(uri+"InsertSpecialTeams",specialteams);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateSpecialTeams(SpecialTeams specialteams)
        {
            var x = await client.PutAsJsonAsync<SpecialTeams>(uri+"UpdateSpecialTeams",specialteams);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeleteSpecialTeams(SpecialTeams specialteams)
        {
            var x = await client.DeleteAsync(uri+"DeleteSpecialTeams/"+specialteams.Id);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
		public async Task<SportList> GetSports()
        {
            SportList lst = null;
            try
            {
                lst = await client.GetFromJsonAsync<SportList>(uri+"GetSports");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return lst;
        }
        public async Task<int> InsertSport(Sport sport)
        {
            var x = await client.PostAsJsonAsync<Sport>(uri+"InsertSport",sport);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateSport(Sport sport)
        {
            var x = await client.PutAsJsonAsync<Sport>(uri+"UpdateSport",sport);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeleteSport(Sport sport)
        {
            var x = await client.DeleteAsync(uri+"DeleteSport/"+sport.Id);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
		public async Task<TeamList> GetTeams()
        {
            TeamList lst = null;
            try
            {
                lst = await client.GetFromJsonAsync<TeamList>(uri+"GetTeams");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return lst;
        }
        public async Task<int> InsertTeam(Team team)
        {
            var x = await client.PostAsJsonAsync<Team>(uri+"InsertTeam",team);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateTeam(Team team)
        {
            var x = await client.PutAsJsonAsync<Team>(uri+"UpdateTeam",team);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeleteTeam(Team team)
        {
            var x = await client.DeleteAsync(uri+"DeleteTeam/"+team.Id);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<UserList> GetUser()
        {
            UserList lst = null;
            try
            {
                lst = await client.GetFromJsonAsync<UserList>(uri + "GetUser");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return lst;
        }
        public async Task<int> InsertUser(User user)
        {
            var x = await client.PostAsJsonAsync<User>(uri + "InsertUser", user);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateUser(User user)
        {
            var x = await client.PutAsJsonAsync<User>(uri + "UpdateUser", user);
            return x.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeleteUser(User user)
        {
            var x = await client.DeleteAsync(uri + "DeleteUser/" + user.Id);
            return x.IsSuccessStatusCode ? 1 : 0;
        }


    }
}
