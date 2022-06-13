namespace LandonApi.Models
{
    public class RootResponse : PagedCollection<Room>
    {
        public Link Info { get; set; }

        public Link Rooms { get; set; }
    }
}
