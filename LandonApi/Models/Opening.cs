﻿using System;

namespace LandonApi.Models
{
    public class Opening
    {
        public Link Room { get; set; }

        public DateTimeOffset StartAt { get; set; }

        public DateTimeOffset EndAt { get; set; }

        public decimal Rate { get; set; }
    }
}
