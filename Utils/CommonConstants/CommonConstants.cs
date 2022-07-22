namespace ProductApi.Utils.CommonConstants
{
    public class CommonConstants
    {
        public static class UserRoles
        {
            public const string Admin = "Admin";
            public const string User = "User";
            
        }

        public static class CustomStatusCode
        {
            public const int ProductNotFound = -1;
            public const int ProductNameDuplicated = 0;
        }
    }
}
