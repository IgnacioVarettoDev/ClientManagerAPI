using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ClientManagerDAO.Exceptions
{
    public static class ExceptionHandler
    {
        public static bool IsUniqueViolationException(DbUpdateException ex)
        {
            if (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19)
            {
                return true;
            }
            return false;
        }
    }
}
