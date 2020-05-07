namespace Abp.VNext.Hello
{
    public static class HelloPermissions
    {
        public const string GroupName = "Hello";


        public static class StateProvince
        {
            public const string Default = GroupName + ".StateProvince";

            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Search = Default + ".Search";
            public const string View = Default + ".View";
        }
        public static class Country
        {
            public const string Default = GroupName + ".Country";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Search = Default + ".Search";
            public const string View = Default + ".View";
        }

        public static class City
        {
            public const string Default = GroupName + ".City";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Search = Default + ".Search";
            public const string View = Default + ".View";
        }
    }


}