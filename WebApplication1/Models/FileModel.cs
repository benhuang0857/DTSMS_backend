using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTSMS.Models
{
    public class FileModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MemberId { get; set; }
        public string? FileName { get; set; }
        public long Size { get; set; }
        public long Checksum { get; set; }
        public string? Stage { get; set; }
        public string? Status { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
