namespace Pulse.Users.Producers.Contracts
{
    public static class ContractStrings
    {
        public static string UserCreatedActionQueue => "queue:user-created-action";

        public static string UserDeletedActionQueue => "queue:user-deleted-action";
    }
}
