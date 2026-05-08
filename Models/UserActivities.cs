//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace SeniorCenterWebApp.Models
//{
//    public class UserActivities
//    {
//        //[Key]
//        //public int Id { get; set; }

//        //[Required]
//        //public string UserId { get; set; } // Identity или Supabase

//        //[Required]
//        //public int ActivityId { get; set; }

//        //[Required]
//        //public string PreferredDay { get; set; }

//        //[Required]
//        //public string PreferredTime { get; set; }

//        //public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

//        //[ForeignKey("ActivityId")]
//        //public Activity Activity { get; set; }

//        [Key]
//        public int Id { get; set; }

//        [Required]
//        public string UserId { get; set; } // Обычно для связки с Identity

//        [Required]
//        public int ActivityId { get; set; }

//        [Required]
//        public string PreferredDay { get; set; }

//        [Required]
//        public string PreferredTime { get; set; }

//        [Required]
//        public int UserId1 { get; set; } // Внутренний Id пользователя, как в таблице Users
//    }
//}
