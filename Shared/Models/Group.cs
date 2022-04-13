namespace Shared.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int HeadId { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
        public int GroupId { get; set; }
        public int GroupSequenceNumber { get; set; }
        public bool Retest { get; set; }
        public ResultTestEnum ResultTest { get; set; }
    }
}
