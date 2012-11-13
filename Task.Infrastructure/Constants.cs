using System;

namespace Task.Infrastructure
{
    /// <summary>
    /// Global constants of application
    /// </summary>
    public class Constants
    {
        public const double MIN_VALUE_FACTOR_GIRL = 17;
        public const double MAX_VALUE_FACTOR_GIRL = 22;
        public const double ONE_METR_IN_SM = 100;

        public const int LENGTH_TRUNCATE_NEWS_DESCRIPTION = 450;

        public static readonly TimeSpan TIME_OF_NEWS = new TimeSpan(7, 0, 0, 0);

        public const int NEWS_PAGER_LINKS_PER_PAGE = 10;
        public const int GIRLS_PAGER_LINKS_PER_PAGE = 10;
        public const int PAGER_NUMBER_OF_VISIBLE_LINKS = 2;

        public const string ROLE_ADMIN = "admin";
        public const string ROLE_USER = "user";
    }
}
