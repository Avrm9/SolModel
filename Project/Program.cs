using Model;
using System.Runtime.CompilerServices;
using ViewModel;
using APIService;
namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            //             LeagueDB
            LeagueDB LeagueDB = new LeagueDB();
            League T = new League();
            /*
            //     LeagueDB check ADD
            T.LeagueName = "NewLeagueName";
            T.SportID = SportDB.SelectByID(4);
            LeagueDB.Insert(T);
            
            
            //      LeagueDB check UPDATE
            League Update = LeagueDB.SelectByID(5);
            Update.LeagueName = "newH";
            LeagueDB.Update(Update);
            
            //      LeagueDB check DELETE 
            League delete = LeagueDB.SelectByID(4);
            LeagueDB.Delete(delete);
            

            LeagueDB.SaveChanges();
           
          foreach (var d in LeagueDB.SelectAll())
          {
              Console.WriteLine(d.Id + ", " + d.LeagueName);

          }
           */

            /*
            //             MatchSumDB
            MatchSumDB MatchSumDB = new MatchSumDB();
            MatchSum T = new MatchSum();
            /*
            //     MatchSumDB check ADD
            T.LeagueID = LeagueDB.SelectByID(5);
            T.MatchDate = DateTime.Now.Date;
            T.MatchTime = 2;
            T.WinnerTeam = TeamDB.SelectByID(5);
            MatchSumDB.Insert(T);
            */
            /*
            //      MatchSumDB check UPDATE
            MatchSum Update = MatchSumDB.SelectByID(8);
            Update.MatchTime = 222222;
            MatchSumDB.Update(Update);
            
            //      MatchSumDB check DELETE 
            MatchSum delete = MatchSumDB.SelectByID(8);
            MatchSumDB.Delete(delete);

            
            MatchSumDB.SaveChanges();
            foreach (var d in MatchSumDB.SelectAll())
            {
              Console.WriteLine(d.Id + ", " + d.WinnerTeam);

            }
            */
            /*
            //             OffencesDB
            OffencesDB OffencesDB = new OffencesDB();
            Offences T = new Offences();

            
            //     OffencesDB check ADD
            T.OffenceName = "CHECK";
            T.OffenceLevel = 1;
            T.Tid = TeamDB.SelectByID(5);
            OffencesDB.Insert(T);
            
            
            //      OffencesDB check UPDATE
            Offences Update = OffencesDB.SelectByID(3);
            Update.OffenceName = "222222";
            OffencesDB.Update(Update);
            /*
            //      Offences check DELETE 
            Offences delete = OffencesDB.SelectByID(6);
            OffencesDB.Delete(delete);

            
            OffencesDB.SaveChanges();
            foreach (var d in OffencesDB.SelectAll())
            {
                Console.WriteLine(d.Id + ", " + d.OffenceName);

            }
            */
            /*
            //             PlayerDB
            PlayerDB PlayerDB = new PlayerDB();
            Player T = new Player();

            
            //     PlayerDB check ADD
            T.PlayerName = "AVRAM";
            T.TeamID = TeamDB.SelectByID(5);
            PlayerDB.Insert(T);
            
            
            //      PlayerDB check UPDATE
            Player Update = PlayerDB.SelectByID(6);
            Update.PlayerName = "222222";
            PlayerDB.Update(Update);
            /*
            //      Player check DELETE 
            Player delete = PlayerDB.SelectByID(6);
            PlayerDB.Delete(delete);

            
            PlayerDB.SaveChanges();
            foreach (var d in PlayerDB.SelectAll())
            {
                Console.WriteLine(d.Id + ", " + d.PlayerName);

            }
            */

            /*
            //             SpecialTeamsDB
            SpecialTeamsDB SpecialTeamsDB = new SpecialTeamsDB();
            SpecialTeams T = new SpecialTeams();

            
            //     SpecialTeamsDB check ADD
            T.Id = 7;
            T.FoundedDate = DateTime.Now;
            T.TotalTrophies = 0;
            T.TotalWins = 0;
            T.GoldenBalls = 0;
            T.TotalYearPlayers = 0;
            SpecialTeamsDB.Insert(T);

            
            //      SpecialTeamsDB check UPDATE
            SpecialTeams Update = SpecialTeamsDB.SelectByID(6);
            Update.TotalWins = 222222;
            SpecialTeamsDB.Update(Update);
            
            //      SpecialTeams check DELETE 
            SpecialTeams delete = SpecialTeamsDB.SelectByID(6);
            SpecialTeamsDB.Delete(delete);

            
            SpecialTeamsDB.SaveChanges();
            foreach (var d in SpecialTeamsDB.SelectAll())
            {
                Console.WriteLine(d.Id + ", " + d.TotalWins);

            }
            */

            /*
            //             SportDB
            SportDB SportDB = new SportDB();
            Sport T = new Sport();

            
            //     SportDB check ADD
            T.SportName = "AAAAAAAA";
            T.SportDescription = "Description";
            SportDB.Insert(T);
            
            
            //      SportDB check UPDATE
            Sport Update = SportDB.SelectByID(6);
            Update.SportName = "Aasdhsd";
            SportDB.Update(Update);
            
            //      SportDB check DELETE 
            Sport delete = SportDB.SelectByID(6);
            SportDB.Delete(delete);

            
            SportDB.SaveChanges();
            foreach (var d in SportDB.SelectAll())
            {
                Console.WriteLine(d.Id + ", " + d.SportName);

            }
            */
            /*
            //             TeamDB
            TeamDB TeamDB = new TeamDB();
            Team T = new Team();

            
            //     TeamDB check ADD
            T.TeamName = "AAAAAAAA";
            T.TeamColor = "COLOR";
            T.LeagueID = LeagueDB.SelectByID(5);
            TeamDB.Insert(T);
            

            //      TeamDB check UPDATE
            Team Update = TeamDB.SelectByID(6);
            Update.TeamName = "Aasdhsd";
            TeamDB.Update(Update);
            
            //      TeamDB check DELETE 
            Team delete = TeamDB.SelectByID(6);
            TeamDB.Delete(delete);

            
            TeamDB.SaveChanges();
            foreach (var d in TeamDB.SelectAll())
            {
                Console.WriteLine(d.Id + ", " + d.TeamName);

            }
            */

            /*
            //             TeamDB
            UserDB UserDB = new UserDB();
            User U = new User();

            
            //     TeamDB check ADD
            U.UserName = "AAAAAAAA";
            U.Pass = "COLOR";
            U.Permission = true;
            UserDB.Insert(U);
            
            
            
            //      TeamDB check UPDATE
            User Update = UserDB.SelectByID(5);
            Update.UserName = "NEW";
            UserDB.Update(Update);
            
            //      TeamDB check DELETE 
            User delete = UserDB.SelectByID(4);
            UserDB.Delete(delete);
            
            
            UserDB.SaveChanges();
            foreach (var d in UserDB.SelectAll())
            {
                Console.WriteLine(d.Id + ", " + d.UserName);

            }
           */



        }


    }
}