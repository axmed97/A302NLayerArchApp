namespace Core.Entities.Concrete
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string RefreshToken { get; set; }
    }
}
