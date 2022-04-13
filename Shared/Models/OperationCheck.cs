namespace Shared.Models
{
    public enum ResultCheckEnum { OK, ERROR };
    public enum SideEnum { unittester, product};

    public class OperationCheck
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string Expected { get; set; }
        public string ExpectedType { get; set; }
        public string ExpectedLow { get; set; }
        public string ExpectedHigh { get; set; }
        public string ExpectedInfo { get; set; }
        public ResultCheckEnum Result { get; set; }
        public SideEnum Side { get; set; }
    }
}
