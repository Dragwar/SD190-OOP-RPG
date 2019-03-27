using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace OOP_RPG.SaveSystemRepo
{
    public class FileSaveRepo
    {
        public JsonSerializerSettings SerializerSettings { get; set; }
        public string SaveFileFullPath { get => $"{SaveFileDirectory}{SaveFileFullName}"; }
        public string SaveFileDirectory { get => $@"\OOP_RPG\saves\"; }
        public const string SaveFileFullName = "OOP_RPG_SAVE.json";
        public const string SaveFileName = "OOP_RPG_SAVE";
        public const string SaveFileExtension = ".json";
        public Hero HeroState { get; set; }

        public FileSaveRepo(Hero initialHeroState)
        {
            HeroState = initialHeroState;
            SerializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
            };

            InitializeSaveDirectory();
        }
        public void OverwriteHeroState(Hero heroState) => HeroState = heroState;

        public bool DoesSaveFileExist() => Directory.Exists(SaveFileDirectory) && File.Exists($"{SaveFileFullPath}");

        private void InitializeSaveDirectory()
        {
            if (!Directory.Exists(SaveFileDirectory))
            {
                Directory.CreateDirectory(SaveFileDirectory);
            }

            if (!File.Exists($"{SaveFileFullPath}"))
            {
                FileStream stream = File.Create($"{SaveFileFullPath}");
                stream.Close();
            }
        }

        public bool ResetSaveFile()
        {
            try
            {
                File.WriteAllLines($"{SaveFileFullPath}", new string[] { "" });
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool LoadStateFromFile()
        {
            try
            {
                string json = string.Join("\n", File.ReadAllLines($"{SaveFileFullPath}"));
                Hero hero = JsonConvert.DeserializeObject<Hero>(json);
                // BUG: creates duplicate achievements
                hero.ManageAchievements.AllAchievements = hero.ManageAchievements.AllAchievements
                    .GroupBy(achievement => achievement.EnumTitle)
                    .Select(achievement => achievement.First())
                    .ToList();
                HeroState = hero;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool SaveStateToFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(HeroState, typeof(Hero), SerializerSettings);
                File.WriteAllText($"{SaveFileFullPath}", json);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
