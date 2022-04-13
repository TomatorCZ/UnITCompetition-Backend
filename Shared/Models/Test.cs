using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public enum ResultTestEnum { FAIL, PASS };
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int TestId { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
        public string TestClass { get; set; }
        public bool Retest { get; set; }
        public int TestSequenceNumber { get; set; }
        public int TestGroupSequenceNumber { get; set; }
        public ResultTestEnum Result { get; set; }
        public Operation Operations { get; set; } //is JSON
        public Dictionary<string, string> Config { get; set; } //is JSON
        public List<string> ErrorInfo { get; set; } //is JSON
    }
}
