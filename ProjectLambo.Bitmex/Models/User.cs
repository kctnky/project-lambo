namespace ProjectLambo.Bitmex.Models
{
    /// <summary>
    /// Class definition to store account information of authenticated account.
    /// </summary>
    public class User
    {
        public string UserId { get; internal set; }

        public string Username { get; internal set; }

        public string Email { get; internal set; }
    }
}
