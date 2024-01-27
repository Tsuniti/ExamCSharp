namespace ExamCSharp;

public class DBManagment
{
    ApplicationContext db = new ApplicationContext();

    public void AddUser(string login, string password)
    {
        using (db)
        {
            if (db.Users.Any(user => user.Login == login))
            {
                Console.WriteLine("User with this login already exist");
                return;
            }

            db.Users.Add(new User { Login = login, Password = password });
        }
    }
    public void AddCharacterByUserId(int userId, string nickname, int level)
    {
        using (db)
        {
            if (!db.Users.Any(user => user.Id == userId))
            {
                Console.WriteLine("UserId not found");
                return;
            }
            if (db.Characters.Any(character => character.NickName == nickname))
            {
                Console.WriteLine("Character with this nickname already exist");
                return;
            }
            db.Characters.Add(new Character { NickName = nickname, Level = level, UserId = userId });
        }
    }
    public void AddCharacterByLogin(string login, string nickname, int level)
    {
        using (db)
        {
            var user = db.Users.FirstOrDefault(user => user.Login == login);
            if (user == null)
            {
                Console.WriteLine("User with this login not found");
                return;
            }
            if (db.Characters.Any(character => character.NickName == nickname))
            {
                Console.WriteLine("Character with this nickname already exist");
                return;
            }
            db.Characters.Add(new Character { NickName = nickname, Level = level, UserId = user.Id });

        }
    }

    public void PrintAllInfo()
    {
        using (db)
        {
            var query = from user in db.Users
                        join character in db.Characters on user.Id equals character.UserId
                        select new
                        {
                            UserId = user.Id,
                            UserLogin = user.Login,
                            CharacterId = character.Id,
                            CharacterName = character.NickName,
                            CharacterLevel = character.Level
                        };

            Console.WriteLine("GameDB info:");
            foreach (var result in query)
            {
                Console.WriteLine($"[{result.UserId}]{result.UserLogin} - [{result.CharacterId}]{result.CharacterName} ({result.CharacterLevel})lvl");
            }
        }
    }
    public void PrintInfoByLogin(string login)
    {
        using (db)
        {
            var query = from user in db.Users
                        where user.Login == login
                        join character in db.Characters on user.Id equals character.UserId
                        select new
                        {
                            UserId = user.Id,
                            UserLogin = user.Login,
                            CharacterId = character.Id,
                            CharacterName = character.NickName,
                            CharacterLevel = character.Level
                        };
            Console.WriteLine("GameDB info:");
            foreach (var result in query)
            {
                Console.WriteLine($"[{result.UserId}]{result.UserLogin} - [{result.CharacterId}]{result.CharacterName} ({result.CharacterLevel})lvl");
            }

        }
  
    }
    public void PrintInfoByCharacterName(string characterName)
    {
        using (db)
        {

            var character = db.Characters.FirstOrDefault(character => character.NickName == characterName);
            if (character == null)
            {
                Console.WriteLine("Nickname not found");
                return;
            }

            var user = db.Users.FirstOrDefault(user => user.Id == character.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

                Console.WriteLine($"[{user.Id}]{user.Login} - [{character.Id}]{character.NickName} ({character.Level})lvl");

        }
    }
}
