﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
	public class Rooms
	{
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Type { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Images { get; set; }
        [Required]
        public int RoomNo { get; set; }
        [Required]
        public int IdHotel { get; set; }

    }
}

