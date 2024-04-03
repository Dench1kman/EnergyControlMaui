

namespace EnergyControlMaui.Utilities
{
    public static class PathDB
    {
        public static string GetPath(string nameDb) 
        {
            string pathDbSqlite = string.Empty;

            pathDbSqlite = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            pathDbSqlite = Path.Combine(pathDbSqlite, nameDb);

            return pathDbSqlite;
        }
    }
}
