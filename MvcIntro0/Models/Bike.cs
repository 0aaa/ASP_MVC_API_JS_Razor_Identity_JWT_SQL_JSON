﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MvcIntro0.Models
{
    public class Bike
    {
        public int? BikeId { get; set; }


        public byte[] Image64 { get; set; }


        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "Wrong length", MinimumLength = 2)]
        public string Line { get; set; }


        [StringLength(100, ErrorMessage="Wrong length", MinimumLength = 2)]
        public string Model { get; set; }


        [StringLength(100, ErrorMessage = "Wrong length", MinimumLength = 2)]
        public string Frame { get; set; }


        [StringLength(100, ErrorMessage = "Wrong length", MinimumLength = 2)]
        public string Fork { get; set; }


        [StringLength(100, ErrorMessage = "Wrong length", MinimumLength = 2)]
        public string Shifter { get; set; }


        [StringLength(100, ErrorMessage = "Wrong length", MinimumLength = 2)]
        public string Brake { get; set; }


        [Required(ErrorMessage ="Required")]
        [Range(1000, 100000, ErrorMessage ="Wrong value")]
        public int Cost { get; set; }
    }
}
