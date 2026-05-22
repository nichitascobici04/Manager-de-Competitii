using Microsoft.AspNetCore.Cors;

namespace Manager_de_Competitii.Models.AppConfig
{
    public sealed class AppConfig
    {
        public AppConfig()
        {
            // Inițializare implicită a configurației
            AppName = "Competition Manager";
            MaximumParticipants = 16;
            EnableLogging = true;
            DefaultTournamentType = "roundrobin";
        }
        public string DefaultTournamentType { get; set; } = "roundrobin";

        private static AppConfig _instance;
        public static AppConfig CreateInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppConfig();
                }
                return _instance;
            }
        }
        public string AppName { get; set; }
        public int MaximumParticipants { get; set; }
        public bool EnableLogging { get; set; }

    }
}
