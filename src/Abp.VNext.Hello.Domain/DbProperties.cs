namespace Abp.VNext.Hello
{
    public static class DbProperties
    {
        public static string DbTablePrefix { get; set; } = "";

        public static string DbSchema { get; set; } = "Basic";

        public const string ConnectionStringName = "Default";
    }
}
