namespace ProductApi.Utils.CommonConstants
{
    public class CommonConstants
    {
        public static string ConnectionStringName = "ProductDatabase";


        public static class UserRoles
        {
            public const string Admin = "Admin";
            public const string User = "User";
            
        }

        public static class StatusType
        {
            public const string BadRequest = "400 Bad Request";
            public const string Success = "200 Success";
            public const string InternalServerError = "500 Internal Server Error";

        }

        public static class CustomErrorMessage
        {
            public const string ProductNotExist = "Product is not exist !!!";
            public const string NameDuplicated = "Product Name duplicated !!!";
            public const string InvalidId = "Product ID invalid !!!";
        }

        public static class CustomStatusCode
        {
            public const int ProductNotFound = -1;
            public const int ProductNameDuplicated = 0;
        }
    }
}
