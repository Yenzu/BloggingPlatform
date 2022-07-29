using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.DataService.Models
{
    public class Post
    {
        [Key]
        public int postId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Publication Date")]
        [DataType(DataType.Date)]
        public DateTime publication_date { get; set; }

        [Display(Name = "User")]
        public string user { get; set; }

    }
}