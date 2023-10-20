namespace ClientManager.Constant
{
    public class Message
    {
        public class MessageError
        {
            public const string BadRequest = "getAllClient";
            public const string NotFound = "getOneClientById";
            public const string Ok = "getSearchClient";
            public const string CreateNew = "createNewClient";
            public const string UpdateOne = "updateOneClient";
            public const string DeleteOneById = "deleteClientOneById";
        }
    }
}
