using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BaseEntity
    {
        private int id;
        public int Id { get => id; set => id = value; }
    }
    public class League : BaseEntity
    {

        private string leagueName;
        private Sport sportID;

        public string LeagueName { get => leagueName; set => leagueName = value; }
        public Sport SportID { get => sportID; set => sportID = value; }
    }
    public class LeagueList : List<League>
    {
        public LeagueList() { }
        public LeagueList(IEnumerable<League> list) : base(list) { }
        public LeagueList(IEnumerable<BaseEntity> list) : base(list.Cast<League>().ToList()) { }

    }
    public class MatchSum : BaseEntity
    {
        private DateTime matchDate;
        private League leagueID;
        private Team winnerTeam;
        private int matchTime;


        public DateTime MatchDate { get => matchDate; set => matchDate = value; }
        public int MatchTime { get => matchTime; set => matchTime = value; }
        public League LeagueID { get => leagueID; set => leagueID = value; }
        public Team WinnerTeam { get => winnerTeam; set => winnerTeam = value; }
    }
    public class MatchSumList : List<MatchSum>
    {

        public MatchSumList() { }
        public MatchSumList(IEnumerable<MatchSum> list) : base(list) { }
        public MatchSumList(IEnumerable<BaseEntity> list) : base(list.Cast<MatchSum>().ToList()) { }

    }
    public class Offences : BaseEntity
    {
        private Team tid;
        private string offenceName;
        private int offenceLevel;

        public Team Tid { get => tid; set => tid = value; }
        public string OffenceName { get => offenceName; set => offenceName = value; }
        public int OffenceLevel { get => offenceLevel; set => offenceLevel = value; }
    }
    public class OffencesList : List<Offences>
    {
        public OffencesList() { }
        public OffencesList(IEnumerable<Offences> list) : base(list) { }
        public OffencesList(IEnumerable<BaseEntity> list) : base(list.Cast<Offences>().ToList()) { }
    }
    public class Player : BaseEntity
    {
        private string playerName;
        private Team teamID;

        public string PlayerName { get => playerName; set => playerName = value; }
        public Team TeamID { get => teamID; set => teamID = value; }
    }
    public class PlayerList : List<Player>
    {
        public PlayerList() { }
        public PlayerList(IEnumerable<Player> list) : base(list) { }
        public PlayerList(IEnumerable<BaseEntity> list) : base(list.Cast<Player>().ToList()) { }
    }
    public class SpecialTeams : Team
    {
        private DateTime foundedDate;
        private int totalTrophies;
        private int totalWins;
        private int goldenBalls;
        private int totalYearPlayers;

        public DateTime FoundedDate { get => foundedDate; set => foundedDate = value; }
        public int TotalTrophies { get => totalTrophies; set => totalTrophies = value; }
        public int TotalWins { get => totalWins; set => totalWins = value; }
        public int GoldenBalls { get => goldenBalls; set => goldenBalls = value; }
        public int TotalYearPlayers { get => totalYearPlayers; set => totalYearPlayers = value; }
    }
    public class SpecialTeamsList : List<SpecialTeams>
    {
        public SpecialTeamsList() { }
        public SpecialTeamsList(IEnumerable<SpecialTeams> list) : base(list) { }
        public SpecialTeamsList(IEnumerable<BaseEntity> list) : base(list.Cast<SpecialTeams>().ToList()) { }
    }
    public class Sport : BaseEntity
    {
        private string sportName;
        private string sportDescription;

        public string SportName { get => sportName; set => sportName = value; }
        public string SportDescription { get => sportDescription; set => sportDescription = value; }
    }
    public class SportList : List<Sport>
    {
        public SportList() { }
        public SportList(IEnumerable<Sport> list) : base(list) { }
        public SportList(IEnumerable<BaseEntity> list) : base(list.Cast<Sport>().ToList()) { }

    }
    public class Team : BaseEntity
    {
        private string teamName;
        private League leagueID;
        private string teamColor;

        public string TeamName { get => teamName; set => teamName = value; }
        public League LeagueID { get => leagueID; set => leagueID = value; }
        public string TeamColor { get => teamColor; set => teamColor = value; }
    }
    public class TeamList : List<Team>
    {
        public TeamList() { }
        public TeamList(IEnumerable<Team> list) : base(list) { }
        public TeamList(IEnumerable<BaseEntity> list) : base(list.Cast<Team>().ToList()) { }
    }

    public class User : BaseEntity
    {
        private string userName;
        private string pass;
        private bool permission;

        public string UserName { get => userName; set => userName = value; }
        public string Pass { get => pass; set => pass = value; }
        public bool Permission { get => permission; set => permission = value; }
    }
    public class UserList : List<User>
    {
        public UserList() { }
        public UserList(IEnumerable<User> list) : base(list) { }
        public UserList(IEnumerable<BaseEntity> list) : base(list.Cast<User>().ToList()) { }
    }




}
