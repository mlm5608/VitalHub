namespace WebAPI.Utils.Mail
{
    public class EmailSettings
    {

        //email do remetente
        public string? Email { get; set; }

        //host do servidor SMTP
        public string? Password { get; set; }

        //nome exibido do remetente
        public string? Host { get; set;}

        //porta do servidor SMTP
        public string? Displayname { get; set;}

        public int Port {  get; set;}

    }
}
