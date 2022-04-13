using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public enum SecurityCheckEnum { PASS, FAIL };
    public class Head
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Code { get; set; }
        public string Product_SFCode { get; set; }
        public string Product_Family { get; set; }
        public string Product_SN { get; set; }
        public string Product_SFSN { get; set; }
        public string Product_SFIdString { get; set; }
        public string Product_HwVersion { get; set; }
        public ResultTestEnum Result_Value { get; set; }
        public string Result_FailTestName { get; set; }
        public string Result_FailGroupName { get; set; }
        public string UploadState { get; set; }
        public float TestTotalTime { get; set; }
        public SecurityCheckEnum SecurityCheck { get; set; }
        public string TesterInfo { get; set; }
        public string UserName { get; set; }
        public string DmmInfo { get; set; }
        public DateTime TimeStamp { get; set; }
        public string IniSecurity { get; set; }
        public List<Tuple<string, string>> ComponentVersions { get; set; }
    }
}
