using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCenterWebApp.Models
{
    public class UserMeropr
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int MeroprId { get; set; }
        public Meropr Meropr { get; set; }

        public DateTime RegisteredAt { get; set; }
    }
}
