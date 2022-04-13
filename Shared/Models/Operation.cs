namespace Shared.Models
{
    public class Operation
    {
        public IList<OperationInfo> Info { get; set; }
        public IList<OperationCheck> Checks { get; set;}
    }
}
