﻿namespace LandonApi.Models
{
    public class RoomsResponse : PagedCollection<Room>
    {
        public Link Openings { get; set; }
    }
}
