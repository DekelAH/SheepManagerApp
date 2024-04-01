namespace Assets.Scripts.Infrastructure.Providers
{
    public static class SceneNameProvider
    {
        #region Consts

        private const string MAIN_MENU_SCREEN_NAME = "Main Menu";
        private const string HERD_SCREEN_NAME = "Herd Data";
        private const string SHEEP_DATA_SCREEN_NAME = "Sheep Data";
        private const string ADD_SHEEP_SCREEN_NAME = "Add Sheep";
        private const string EDIT_SHEEP_SCREEN_NAME = "Edit Sheep";
        private const string MACHES_SCREEN_NAME = "Matches";
        private const string VACCINES_SCREEN_NAME = "Vaccines";

        #endregion

        #region Properties

        public static string GetMainMenuName => MAIN_MENU_SCREEN_NAME;
        public static string GetHerdScreenName => HERD_SCREEN_NAME;
        public static string GetSheepDataScreenName => SHEEP_DATA_SCREEN_NAME;
        public static string GetAddSheepScreenName => ADD_SHEEP_SCREEN_NAME;
        public static string GetEditSheepScreenName => EDIT_SHEEP_SCREEN_NAME;
        public static string GetMatchesScreenName => MACHES_SCREEN_NAME;
        public static string GetVaccinesScreenName => VACCINES_SCREEN_NAME;

        #endregion
    }
}
