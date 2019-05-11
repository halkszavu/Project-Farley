namespace WebApi.Entities
{
    public class PersonMeeting
    {
        public int ID { get; set; }

        public int PersonID { get; set; }
        public Person Person { get; set; }

        public int MeetingID { get; set; }
        public Meeting Meeting { get; set; }
    }
}
