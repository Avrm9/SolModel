using Model;
using System.Data.OleDb;

namespace ViewModel
{
    public abstract class BaseEntityDB
    {
        protected static string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location + "/../../../../../ViewModel/FinalProjectSport.accdb");
        protected OleDbConnection connection;
        protected OleDbCommand command;
        protected OleDbDataReader reader;

        protected static List<ChangeEntity> inserted = new List<ChangeEntity>();
        protected static List<ChangeEntity> updated = new List<ChangeEntity>();
        protected static List<ChangeEntity> deleted = new List<ChangeEntity>();
        protected abstract void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd);
        protected abstract void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd);
        protected abstract void CreateDeleteSQL(BaseEntity entity, OleDbCommand cmd);
        public virtual void Insert(BaseEntity entity)
        {
            BaseEntity req = this.NewEntity();
            if (entity != null && entity.GetType() == req.GetType())
            {
                inserted.Add(new ChangeEntity(entity, CreateInsertSQL));
            }
        }
        public virtual void Update(BaseEntity entity)
        {
            BaseEntity req = NewEntity();
            if (entity != null && entity.GetType() == req.GetType())
            {
                updated.Add(new ChangeEntity(entity, CreateUpdateSQL));
            }
        }
        public virtual void Delete(BaseEntity entity)
        {
            BaseEntity req = NewEntity();
            if (entity != null && entity.GetType() == req.GetType())
            {
                updated.Add(new ChangeEntity(entity, CreateDeleteSQL));
            }
        }

        protected abstract BaseEntity NewEntity();
        public BaseEntityDB()
        {
            connection = new OleDbConnection(ConnectionString);
            command = new OleDbCommand();
            command.Connection = connection;
        }
        protected virtual BaseEntity CreateModel(BaseEntity entity)
        {
            entity.Id = (int)reader["id"];
            return entity;
        }
        protected List<BaseEntity> Select()
        {
            List<BaseEntity> List = new List<BaseEntity>();
            try
            {
                command.Connection = connection;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    List.Add(CreateModel(entity));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL: " + command.CommandText);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return List;
        }
        protected async Task<List<BaseEntity>> SelectAsync(string SqlStr)
        {
            List<BaseEntity> list = new List<BaseEntity>();
            try
            {
                command.CommandText = SqlStr;
                command.Connection = connection;
                connection.Open();
                reader = (OleDbDataReader)await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    list.Add(CreateModel(NewEntity()));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL: " + command.CommandText);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return list;
        }
        public async Task<int> SaveChangesAsync()
        {
            int records = 0;
            try
            {
                command.Connection = connection;
                connection.Open();
                foreach (var item in inserted)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records += await command.ExecuteNonQueryAsync();
                    command.CommandText = "Select @@Identity";
                    item.Entity.Id = (int)command.ExecuteScalarAsync().Result;
                }
                foreach (var item in updated)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records += await command.ExecuteNonQueryAsync();
                }
                foreach (var item in deleted)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records += await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL: " + command.CommandText);
            }
            finally
            {
                inserted.Clear();
                updated.Clear();
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return records;
        }
        public int SaveChanges()
        {
            int records = 0;
            OleDbTransaction trans = null;
            try
            {
                command.Connection = connection;
                connection.Open();
                trans = connection.BeginTransaction();
                command.Transaction = trans;
                foreach (var item in inserted)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records += command.ExecuteNonQuery();
                    command.CommandText = "Select @@Identity";
                    item.Entity.Id = (int)command.ExecuteScalar();
                }
                foreach (var item in updated)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records += command.ExecuteNonQuery();
                }
                foreach (var item in deleted)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records +=  command.ExecuteNonQuery();
                }
                trans.Commit();

            }
            catch (Exception e)
            {
                if (trans != null)
                    trans.Rollback();
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL: " + command.CommandText);
            }
            finally
            {
                inserted.Clear();
                updated.Clear();
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return records;
        }
    }
    public delegate void CreateSql(BaseEntity entity, OleDbCommand cmd);
    public class ChangeEntity   //Create the changeEntity for better use in the same namespace
    {
        public BaseEntity Entity { get; set; }
        public CreateSql CreateSql { get; set; }
        public ChangeEntity(BaseEntity entity, CreateSql createSql)
        {
            Entity = entity;
            CreateSql = createSql;
        }

    }

    public class LeagueDB : BaseEntityDB
    {
        private static LeagueList lst = null;
        public LeagueDB() : base() { }
        public static League SelectByID(int num)
        {
            if (lst == null)
                lst = new LeagueDB().SelectAll();
            return lst.Find(x => x.Id == num);
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            League student = entity as League;
            student.LeagueName = reader["LeagueName"].ToString(); ;
            student.SportID = SportDB.SelectByID((int)reader["SportID"]);
            base.CreateModel(entity);
            return student;
        }
        protected override BaseEntity NewEntity()
        {
            return new League();
        }
        public LeagueList SelectAll()
        {
            command.CommandText = $"SELECT * FROM league";
            LeagueList lList = new LeagueList(base.Select());
            return lList;
        }

        public override void Insert(BaseEntity entity)
        {
            if (entity as League != null)
            {
                inserted.Add(new ChangeEntity(entity, CreateInsertSQL));
            }
        }

        public override void Update(BaseEntity entity)
        {
            if (entity as League != null)
            {
                updated.Add(new ChangeEntity(entity, CreateUpdateSQL));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            if (entity as League != null)
            {
                updated.Add(new ChangeEntity(entity, CreateDeleteSQL));
            }
        }

        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            League stu = entity as League;
            if (stu != null)
            {
                cmd.CommandText = "INSERT INTO league (LeagueName, SportID) VALUES (@LeagueName, @SportID)";
                cmd.Parameters.Add(new OleDbParameter("@LeagueName", stu.LeagueName));
                cmd.Parameters.Add(new OleDbParameter("@SportID", stu.SportID.Id));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            League stu = entity as League;
            if (stu != null)
            {
                cmd.CommandText = "UPDATE league SET LeagueName = @LeagueName, SportID = @SportID WHERE id=@Id";
                cmd.Parameters.Add(new OleDbParameter("@LeagueName", stu.LeagueName));
                cmd.Parameters.Add(new OleDbParameter("@SportID", stu.SportID.Id));
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }

        protected override void CreateDeleteSQL(BaseEntity entity, OleDbCommand cmd)
        {
            League stu = entity as League;
            if (stu != null)
            {
                cmd.CommandText = "DELETE FROM league WHERE id = @ID";
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }
    }

    public class MatchSumDB : BaseEntityDB
    {
        private static MatchSumList lst = null;
        public MatchSumDB() : base() { }
        public static MatchSum SelectByID(int num)
        {
            if (lst == null)
                lst = new MatchSumDB().SelectAll();
            return lst.Find(x => x.Id == num);
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            MatchSum student = entity as MatchSum;
            student.MatchDate = DateTime.Parse(reader["MatchDate"].ToString());
            student.WinnerTeam = TeamDB.SelectByID((int)reader["WinnerTeam"]);
            student.LeagueID = LeagueDB.SelectByID((int)reader["LeagueID"]);
            student.MatchTime = (int)reader["MatchTime"];
            base.CreateModel(entity);
            return student;
        }
        protected override BaseEntity NewEntity()
        {
            return new MatchSum();
        }
        public MatchSumList SelectAll()
        {
            command.CommandText = $"SELECT * FROM matchsum";
            MatchSumList mList = new MatchSumList(base.Select());
            return mList;
        }

        public override void Insert(BaseEntity entity)
        {
            if (entity as MatchSum != null)
            {
                inserted.Add(new ChangeEntity(entity, CreateInsertSQL));
            }
        }

        public override void Update(BaseEntity entity)
        {
            if (entity as MatchSum != null)
            {
                updated.Add(new ChangeEntity(entity, CreateUpdateSQL));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            if (entity as MatchSum != null)
            {
                deleted.Add(new ChangeEntity(entity, CreateDeleteSQL));
            }
        }

        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            MatchSum stu = entity as MatchSum;
            if (stu != null)
            {
                cmd.CommandText = "INSERT INTO matchsum (MatchDate, WinnerTeam, LeagueID, MatchTime) VALUES (@MatchDate, @WinnerTeam, @LeagueID, @MatchTime)";
                cmd.Parameters.Add(new OleDbParameter("@MatchDate", stu.MatchDate));
                cmd.Parameters.Add(new OleDbParameter("@WinnerTeam", stu.WinnerTeam.Id));
                cmd.Parameters.Add(new OleDbParameter("@LeagueID", stu.LeagueID.Id));
                cmd.Parameters.Add(new OleDbParameter("@MatchTime", stu.MatchTime));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            MatchSum stu = entity as MatchSum;
            if (stu != null)
            {
                cmd.CommandText = "UPDATE matchsum SET MatchDate = @MatchDate, WinnerTeam = @WinnerTeam, LeagueID = @LeagueID, MatchTime = @MatchTime WHERE id=@Id";
                cmd.Parameters.Add(new OleDbParameter("@MatchDate", stu.MatchDate));
                cmd.Parameters.Add(new OleDbParameter("@WinnerTeam", stu.WinnerTeam.Id));
                cmd.Parameters.Add(new OleDbParameter("@LeagueID", stu.LeagueID.Id));
                cmd.Parameters.Add(new OleDbParameter("@MatchTime", stu.MatchTime));
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }


        protected override void CreateDeleteSQL(BaseEntity entity, OleDbCommand cmd)
        {
            MatchSum stu = entity as MatchSum;
            if (stu != null)
            {
                cmd.CommandText = $"DELETE FROM matchsum WHERE id = @ID";
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }
    }

    public class OffencesDB : BaseEntityDB
    {
        private static OffencesList lst = null;
        public OffencesDB() : base() { }
        public static Offences SelectByID(int num)
        {
            if (lst == null)
                lst = new OffencesDB().SelectAll();
            return lst.Find(x => x.Id == num);
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Offences student = entity as Offences;
            student.Tid = TeamDB.SelectByID((int)reader["Tid"]);
            student.OffenceName = reader["OffenceName"].ToString(); ;
            student.OffenceLevel = (int)reader["OffenceLevel"];
            base.CreateModel(entity);
            return student;
        }
        protected override BaseEntity NewEntity()
        {
            return new Offences();
        }
        public OffencesList SelectAll()
        {
            command.CommandText = $"SELECT * FROM offences";
            OffencesList oList = new OffencesList(base.Select());
            return oList;
        }

        public override void Insert(BaseEntity entity)
        {
            if (entity as Offences != null)
            {
                inserted.Add(new ChangeEntity(entity, CreateInsertSQL));
            }
        }

        public override void Update(BaseEntity entity)
        {
            if (entity as Offences != null)
            {
                updated.Add(new ChangeEntity(entity, CreateUpdateSQL));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            if (entity as Offences != null)
            {
                deleted.Add(new ChangeEntity(entity, CreateDeleteSQL));
            }
        }

        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Offences stu = entity as Offences;
            if (stu != null)
            {
                cmd.CommandText = "INSERT INTO offences (Tid, OffenceName, OffenceLevel) VALUES (@Tid, @OffenceName, @OffenceLevel)";
                cmd.Parameters.Add(new OleDbParameter("@Tid", stu.Tid.Id));
                cmd.Parameters.Add(new OleDbParameter("@OffenceName", stu.OffenceName));
                cmd.Parameters.Add(new OleDbParameter("@OffenceLevel", stu.OffenceLevel));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Offences stu = entity as Offences;
            if (stu != null)
            {
                cmd.CommandText = "UPDATE offences SET Tid = @Tid, OffenceName = @OffenceName, OffenceLevel = @OffenceLevel WHERE id=@Id";
                cmd.Parameters.Add(new OleDbParameter("@Tid", stu.Tid.Id));
                cmd.Parameters.Add(new OleDbParameter("@OffenceName", stu.OffenceName));
                cmd.Parameters.Add(new OleDbParameter("@OffenceLevel", stu.OffenceLevel));
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }

        protected override void CreateDeleteSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Offences stu = entity as Offences;
            if (stu != null)
            {
                cmd.CommandText = "DELETE FROM offences WHERE id = @ID";
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }
    }

    public class PlayerDB : BaseEntityDB
    {
        private static PlayerList lst = null;
        public PlayerDB() : base() { }
        public static Player SelectByID(int num)
        {
            if (lst == null)
                lst = new PlayerDB().SelectAll();
            return lst.Find(x => x.Id == num);
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Player student = entity as Player;
            student.PlayerName = reader["PlayerName"].ToString(); ;
            student.TeamID = TeamDB.SelectByID((int)reader["TeamID"]);
            base.CreateModel(entity);
            return student;
        }
        protected override BaseEntity NewEntity()
        {
            return new Player();
        }
        public PlayerList SelectAll()
        {
            command.CommandText = $"SELECT * FROM player";
            PlayerList sList = new PlayerList(base.Select());
            return sList;
        }

        public override void Insert(BaseEntity entity)
        {
            if (entity as Player != null)
            {
                inserted.Add(new ChangeEntity(entity, CreateInsertSQL));
            }
        }

        public override void Update(BaseEntity entity)
        {
            if (entity as Player != null)
            {
                updated.Add(new ChangeEntity(entity, CreateUpdateSQL));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            if (entity as Player != null)
            {
                updated.Add(new ChangeEntity(entity, CreateDeleteSQL));
            }
        }

        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Player stu = entity as Player;
            if (stu != null)
            {
                cmd.CommandText = "INSERT INTO player (PlayerName, TeamID) VALUES (@PlayerName, @TeamID)";
                cmd.Parameters.Add(new OleDbParameter("@PlayerName", stu.PlayerName));
                cmd.Parameters.Add(new OleDbParameter("@TeamID", stu.TeamID.Id));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Player stu = entity as Player;
            if (stu != null)
            {
                cmd.CommandText = "UPDATE player SET PlayerName = @PlayerName, TeamID = @TeamID WHERE id=@Id";
                cmd.Parameters.Add(new OleDbParameter("@PlayerName", stu.PlayerName));
                cmd.Parameters.Add(new OleDbParameter("@TeamID", stu.TeamID.Id));
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }

        protected override void CreateDeleteSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Player stu = entity as Player;
            if (stu != null)
            {
                cmd.CommandText = "DELETE FROM player WHERE id = @ID";
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }
    }

    public class SpecialTeamsDB : TeamDB
    {
        private static SpecialTeamsList lst = null;
        public SpecialTeamsDB() : base() { }
        public static SpecialTeams SelectByID(int num)
        {
            if (lst == null)
                lst = new SpecialTeamsDB().SelectAll();
            return lst.Find(x => x.Id == num);
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            SpecialTeams student = entity as SpecialTeams;
            student.FoundedDate = DateTime.Parse(reader["FoundedDate"].ToString());
            student.TotalTrophies = (int)reader["TotalTrophies"];
            student.TotalWins = (int)reader["TotalWins"];
            student.GoldenBalls = (int)reader["GoldenBalls"];
            student.TotalYearPlayers = (int)reader["TotalYearPlayers"];
            base.CreateModel(entity);
            return student;
        }
        protected override BaseEntity NewEntity()
        {
            return new SpecialTeams();
        }
        public SpecialTeamsList SelectAll()
        {
            command.CommandText = $"SELECT team.id, team.TeamName, team.LeagueID, team.TeamColor, specialteams.id AS Expr1, specialteams.FoundedDate, specialteams.TotalTrophies, specialteams.TotalWins, specialteams.GoldenBalls, specialteams.TotalYearPlayers FROM (team INNER JOIN specialteams ON team.id = specialteams.id)";
            SpecialTeamsList sList = new SpecialTeamsList(base.Select());
            return sList;
        }
        public void InsertCombined(BaseEntity entity)
        {
            if (entity as SpecialTeams != null)
            {
                inserted.Add(new ChangeEntity(entity, base.CreateInsertSQL));
                inserted.Add(new ChangeEntity(entity, CreateInsertSQL));
            }
        }
        public void UpdateCombined(BaseEntity entity)
        {
            if (entity as SpecialTeams != null)
            {
                updated.Add(new ChangeEntity(entity, base.CreateUpdateSQL));
                updated.Add(new ChangeEntity(entity, CreateUpdateSQL));
            }
        }
        public void DeleteCombined(BaseEntity entity)
        {
            if (entity as SpecialTeams != null)
            {
                updated.Add(new ChangeEntity(entity, base.CreateDeleteSQL));
                updated.Add(new ChangeEntity(entity, CreateDeleteSQL));
            }
        }
        public override void Insert(BaseEntity entity)
        {
            if (entity as SpecialTeams != null)
            {
                inserted.Add(new ChangeEntity(entity, CreateInsertSQL));
            }
        }

        public override void Update(BaseEntity entity)
        {
            if (entity as SpecialTeams != null)
            {
                updated.Add(new ChangeEntity(entity, CreateUpdateSQL));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            if (entity as SpecialTeams != null)
            {
                updated.Add(new ChangeEntity(entity, CreateDeleteSQL));
            }
        }

        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            SpecialTeams stu = entity as SpecialTeams;
            if (stu != null)
            {
                cmd.CommandText = "INSERT INTO specialteams (id, FoundedDate, TotalTrophies, TotalWins, GoldenBalls, TotalYearPlayers) VALUES (@ID, @FoundedDate, @TotalTrophies, @TotalWins, @GoldenBalls, @TotalYearPlayers)";
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
                cmd.Parameters.Add(new OleDbParameter("@FoundedDate", OleDbType.Date)).Value = stu.FoundedDate;
                cmd.Parameters.Add(new OleDbParameter("@TotalTrophies", stu.TotalTrophies));
                cmd.Parameters.Add(new OleDbParameter("@TotalWins", stu.TotalWins));
                cmd.Parameters.Add(new OleDbParameter("@GoldenBalls", stu.GoldenBalls));
                cmd.Parameters.Add(new OleDbParameter("@TotalYearPlayers", stu.TotalYearPlayers));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            SpecialTeams stu = entity as SpecialTeams;
            if (stu != null)
            {
                cmd.CommandText = "UPDATE specialteams SET FoundedDate = @FoundedDate, TotalTrophies = @TotalTrophies, TotalWins = @TotalWins, GoldenBalls = @GoldenBalls, TotalYearPlayers = @TotalYearPlayers WHERE id=@Id";
                cmd.Parameters.Add(new OleDbParameter("@FoundedDate", stu.FoundedDate));
                cmd.Parameters.Add(new OleDbParameter("@TotalTrophies", stu.TotalTrophies));
                cmd.Parameters.Add(new OleDbParameter("@TotalWins", stu.TotalWins));
                cmd.Parameters.Add(new OleDbParameter("@GoldenBalls", stu.GoldenBalls));
                cmd.Parameters.Add(new OleDbParameter("@TotalYearPlayers", stu.TotalYearPlayers));
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }

        protected override void CreateDeleteSQL(BaseEntity entity, OleDbCommand cmd)
        {
            SpecialTeams stu = entity as SpecialTeams;
            if (stu != null)
            {
                cmd.CommandText = "DELETE FROM specialteams WHERE id = @ID";
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }
    }

    public class SportDB : BaseEntityDB
    {
        private static SportList lst = null;
        public SportDB() : base() { }
        public static Sport SelectByID(int num)
        {
            if (lst == null)
                lst = new SportDB().SelectAll();
            return lst.Find(x => x.Id == num);
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Sport student = entity as Sport;
            student.SportName = reader["SportName"].ToString(); ;
            student.SportDescription = reader["SportDescription"].ToString(); ;
            base.CreateModel(entity);
            return student;
        }
        protected override BaseEntity NewEntity()
        {
            return new Sport();
        }
        public SportList SelectAll()
        {
            command.CommandText = $"SELECT * FROM sport";
            SportList sList = new SportList(base.Select());
            return sList;
        }

        public override void Insert(BaseEntity entity)
        {
            if (entity as Sport != null)
            {
                inserted.Add(new ChangeEntity(entity, CreateInsertSQL));
            }
        }

        public override void Update(BaseEntity entity)
        {
            if (entity as Sport != null)
            {
                updated.Add(new ChangeEntity(entity, CreateUpdateSQL));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            if (entity as Sport != null)
            {
                updated.Add(new ChangeEntity(entity, CreateDeleteSQL));
            }
        }

        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Sport stu = entity as Sport;
            if (stu != null)
            {
                cmd.CommandText = "INSERT INTO sport (SportName, SportDescription) VALUES (@SportName, @SportDescription)";
                cmd.Parameters.Add(new OleDbParameter("@SportName", stu.SportName));
                cmd.Parameters.Add(new OleDbParameter("@SportDescription", stu.SportDescription));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Sport stu = entity as Sport;
            if (stu != null)
            {
                cmd.CommandText = "UPDATE sport SET SportName = @SportName, SportDescription = @SportDescription WHERE id=@Id";
                cmd.Parameters.Add(new OleDbParameter("@SportName", stu.SportName));
                cmd.Parameters.Add(new OleDbParameter("@SportDescription", stu.SportDescription));
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }

        protected override void CreateDeleteSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Sport stu = entity as Sport;
            if (stu != null)
            {
                cmd.CommandText = "DELETE FROM sport WHERE id = @ID";
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }
    }

    public class TeamDB : BaseEntityDB
    {
        private static TeamList lst = null;
        public TeamDB() : base() { }
        public static Team SelectByID(int num)
        {
            if (lst == null)
                lst = new TeamDB().SelectAll();
            return lst.Find(x => x.Id == num);
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Team student = entity as Team;
            student.TeamName = reader["TeamName"].ToString(); ;
            student.LeagueID = LeagueDB.SelectByID((int)reader["LeagueID"]);
            student.TeamColor = reader["TeamColor"].ToString(); ;
            base.CreateModel(entity);
            return student;
        }
        protected override BaseEntity NewEntity()
        {
            return new Team();
        }
        public TeamList SelectAll()
        {
            command.CommandText = $"SELECT * FROM team";
            TeamList tList = new TeamList(base.Select());
            return tList;
        }

        public override void Insert(BaseEntity entity)
        {
            if (entity as Team != null)
            {
                inserted.Add(new ChangeEntity(entity, CreateInsertSQL));
            }
        }

        public override void Update(BaseEntity entity)
        {
            if (entity as Team != null)
            {
                updated.Add(new ChangeEntity(entity, CreateUpdateSQL));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            if (entity as Team != null)
            {
                updated.Add(new ChangeEntity(entity, CreateDeleteSQL));
            }
        }

        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Team stu = entity as Team;
            if (stu != null)
            {
                cmd.CommandText = "INSERT INTO team (TeamName, LeagueID, TeamColor) VALUES (@TeamName, @LeagueID, @TeamColor)";
                cmd.Parameters.Add(new OleDbParameter("@TeamName", stu.TeamName));
                cmd.Parameters.Add(new OleDbParameter("@LeagueID", stu.LeagueID.Id));
                cmd.Parameters.Add(new OleDbParameter("@TeamColor", stu.TeamColor));
            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Team stu = entity as Team;
            if (stu != null)
            {
                cmd.CommandText = "UPDATE team SET TeamName = @TeamName, LeagueID = @LeagueID, TeamColor = @TeamColor WHERE id=@Id";
                cmd.Parameters.Add(new OleDbParameter("@TeamName", stu.TeamName));
                cmd.Parameters.Add(new OleDbParameter("@LeagueID", stu.LeagueID.Id));
                cmd.Parameters.Add(new OleDbParameter("@TeamColor", stu.TeamColor));
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }

        protected override void CreateDeleteSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Team stu = entity as Team;
            if (stu != null)
            {
                cmd.CommandText = "DELETE FROM team WHERE id = @ID";
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }
    }


    public class UserDB : BaseEntityDB
    {
        private static UserList lst = null;
        public UserDB() : base() { }
        public static User SelectByID(int num)
        {
            if (lst == null)
                lst = new UserDB().SelectAll();
            return lst.Find(x => x.Id == num);
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            User student = entity as User;
            student.UserName = reader["UserName"].ToString(); ;
            student.Pass = reader["Pass"].ToString(); ;
            student.Permission = bool.Parse(reader["Permission"].ToString());
            base.CreateModel(entity);
            return student;
        }
        protected override BaseEntity NewEntity()
        {
            return new User();
        }
        public UserList SelectAll()
        {
            command.CommandText = $"SELECT * FROM users";
            UserList uList = new UserList(base.Select());
            return uList;
        }

        public override void Insert(BaseEntity entity)
        {
            if (entity as User != null)
            {
                inserted.Add(new ChangeEntity(entity, CreateInsertSQL));
            }
        }

        public override void Update(BaseEntity entity)
        {
            if (entity as User != null)
            {
                updated.Add(new ChangeEntity(entity, CreateUpdateSQL));
            }
        }

        public override void Delete(BaseEntity entity)
        {
            if (entity as User != null)
            {
                updated.Add(new ChangeEntity(entity, CreateDeleteSQL));
            }
        }

        protected override void CreateInsertSQL(BaseEntity entity, OleDbCommand cmd)
        {
            User stu = entity as User;
            if (stu != null)
            {
                cmd.CommandText = "INSERT INTO users (UserName, Pass, Permission) VALUES (@UserName, @Pass, @Permission)";
                cmd.Parameters.Add(new OleDbParameter("@UserName", stu.UserName));
                cmd.Parameters.Add(new OleDbParameter("@Pass", stu.Pass));
                cmd.Parameters.Add(new OleDbParameter("@Permission", stu.Permission));

            }
        }

        protected override void CreateUpdateSQL(BaseEntity entity, OleDbCommand cmd)
        {
            User stu = entity as User;
            if (stu != null)
            {
                cmd.CommandText = "UPDATE users SET UserName = @UserName, Pass = @Pass, Permission = @Permission  WHERE id=@Id";
                cmd.Parameters.Add(new OleDbParameter("@UserName", stu.UserName));
                cmd.Parameters.Add(new OleDbParameter("@Pass", stu.Pass));
                cmd.Parameters.Add(new OleDbParameter("@Permission", stu.Permission));
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }

        protected override void CreateDeleteSQL(BaseEntity entity, OleDbCommand cmd)
        {
            User stu = entity as User;
            if (stu != null)
            {
                cmd.CommandText = "DELETE FROM users WHERE id = @ID";
                cmd.Parameters.Add(new OleDbParameter("@ID", stu.Id));
            }
        }
    }

}