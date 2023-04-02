using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class MembersIds
    {
        [Key]
        public string MemberId { get; set; } = string.Empty;
        public int GoalId { get; set; }
    }
}
