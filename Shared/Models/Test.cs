﻿namespace Shared.Models
{
    public enum ResultTestEnum { FAIL, PASS};
    public class Test
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
        public string TestClass { get; set; }
        public bool Retest { get; set; }
        public int TestSequenceNumber { get; set; }
        public int TestGroupSequenceNumber { get; set; }
        public ResultTestEnum Result { get; set; }
        public Operation Operations { get; set; }
        public Dictionary<string, object> Config { get; set; }
        public IList<string> ErrorInfo { get; set; }
    }
}
