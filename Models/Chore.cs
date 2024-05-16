namespace Choreganizer_webapp.Models
{
    public class Chore
    {
        public int Id { get; set; }
        public string ChoreName { get; set; }
        public DateTime Date { get; set; }
        public byte Finished { get; set; }

    }
}
