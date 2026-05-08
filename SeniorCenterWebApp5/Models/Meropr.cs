using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeniorCenterWebApp.Models
{
    public class Meropr
    {
      

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public string District { get; set; }

        // Может быть NULL, если указано день недели вместо конкретной даты
        public DateTime? EventDate { get; set; }

        public string? DayOfWeek { get; set; }

        public TimeSpan? EventTime { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int MaxParticipants { get; set; }

        // Сразу задаём текущую дату/время при создании
        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Связь с таблицей UserMeroprs (многие ко многим)
        public List<UserMeropr> UserMeroprs { get; set; } = new List<UserMeropr>();

   
    }
}
