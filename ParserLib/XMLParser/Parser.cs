
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ParserLib.XMLParser
{
    public class Parser
    {
        public Parser() {}

        public List<Tuple<Head, List<Tuple<Group, List<Test>>>>> ParseFiles(string dirPath)
        {
            var result = new List<Tuple<Head, List<Tuple<Group, List<Test>>>>>();

            foreach (var filePath in Directory.GetFiles(dirPath))
            {
                if (filePath.EndsWith(".xml"))
                {
                    result.Add(ParseFile(filePath));
                }
            }
           
            return result;
        }

        public Tuple<Head, List<Tuple<Group, List<Test>>>> ParseFile(string path)
        {
            XElement document = XElement.Load(path);

            var head = document.Element("head");
            if (head == null)
                throw new Exception("XML has no head"); 

            Head headResult = ProcessHead(head);

            var result2 = new List<Tuple<Group, List<Test>>>();
            foreach (var group in document.Element("test_set").Elements("group"))
            {
                var t = Tuple.Create(ProcessGroup(group), new List<Test>());

                foreach (var test in group.Elements("test"))
                {
                    t.Item2.Add(ProcessTest(test));
                }
                result2.Add(t);
            }

            return Tuple.Create(headResult, result2);
        }

        public Head ProcessHead(XElement head) 
        {
            Head result = new Head();

            //product
            var product = head.Element("product");
            result.Product_Name = product.Attribute("name").Value;
            result.Product_Code = product.Attribute("code").Value;
            result.Product_SFCode = product.Attribute("sf-code").Value;
            result.Product_Family = product.Attribute("family").Value;
            result.Product_SN = product.Attribute("sn").Value;
            result.Product_SFSN = product.Attribute("sf-sn").Value;
            result.Product_SFIdString = product.Attribute("sf-id-string").Value;
            result.Product_HwVersion = product.Attribute("hw-version").Value;

            //result
            var resultTag = head.Element("result");
            result.Result_Value = resultTag.Attribute("value").Value == "FAIL" ? ResultTestEnum.FAIL : ResultTestEnum.PASS;
            result.Result_FailTestName = resultTag.Attribute("fail-test-name").Value;
            result.Result_FailGroupName = resultTag.Attribute("fail-group-name").Value;
            //upload-state
            result.UploadState = head.Element("upload-state").Attribute("value").Value;
            //test-total-time
            result.TestTotalTime = float.Parse(head.Element("test-total-time").Attribute("value").Value);
            //security-check
            result.SecurityCheck = head.Element("security-check").Attribute("value").Value == "PASS" ? SecurityCheckEnum.PASS : SecurityCheckEnum.FAIL;
            //tester-info
            result.TesterInfo = head.Element("tester-info").Attribute("value").Value;
            //user-name 
            result.UserName = head.Element("user-name").Attribute("value").Value;
            //dmm-info
            result.DmmInfo = head.Element("dmm-info").Attribute("value").Value;
            //timestamp
            result.TimeStamp = DateTime.Parse(head.Element("timestamp").Attribute("value").Value);
            //ini-security
            result.IniSecurity= head.Element("ini-security").Attribute("value").Value;
            //component-versions
            result.ComponentVersions = new List<Tuple<string, string>>();
            foreach (var version in head.Element("component-versions").Elements("version"))
            {
                result.ComponentVersions.Add(Tuple.Create(version.Attribute("name").Value, version.Attribute("version-number").Value));
            }

            return result;
        }

        public Test ProcessTest(XElement test)
        {
            var result = new Test();

            //title
            result.Title = test.Attribute("title").Value;
            //checked
            result.Checked = bool.Parse(test.Attribute("checked").Value);
            //test-class
            result.TestClass = test.Attribute("test-class").Value;
            //test-id
            result.TestId = int.Parse(test.Attribute("test-id").Value);
            //retest
            result.Retest = bool.Parse(test.Attribute("retest").Value);
            //test-sequence-number
            result.TestSequenceNumber = int.Parse(test.Attribute("test-sequence-number").Value);
            //test-group-sequence-number
            result.TestGroupSequenceNumber = int.Parse(test.Attribute("test-group-sequence-number").Value);
            //result
            result.Result = test.Attribute("result").Value == "Pass" ? ResultTestEnum.PASS : ResultTestEnum.FAIL;
            //config
            result.Config = new Dictionary<string, string>();
            foreach (var value in test.Element("config").Elements("value"))
            {
                result.Config.Add(value.Attribute("name").Value, value.Value);
            }
            //operations
            result.Operations = new Operation();
            //info
            result.Operations.Info = new List<OperationInfo>();
            foreach (var inf in test.Element("operations").Elements("info"))
            {
                var info = new OperationInfo();

                info.Text = inf.Attribute("text").Value;
                info.Extended = bool.Parse(inf.Attribute("extended-info").Value);

                result.Operations.Info.Add(info);
            }
            //checks
            result.Operations.Checks = new List<OperationCheck>();
            foreach (var inf in test.Element("operations").Elements("check"))
            {
                var check = new OperationCheck();

                check.Name = inf.Attribute("name").Value;
                check.Value = inf.Attribute("value")?.Value;
                check.ValueType = inf.Attribute("value-type")?.Value;
                check.Expected = inf.Attribute("expected")?.Value;
                check.ExpectedType = inf.Attribute("expected-type")?.Value;
                check.ExpectedLow = inf.Attribute("expected-low")?.Value;
                check.ExpectedHigh = inf.Attribute("expected-high")?.Value;
                check.ExpectedInfo = inf.Attribute("extended-info")?.Value;
                check.Result = inf.Attribute("result")?.Value == "OK" ? ResultCheckEnum.OK : ResultCheckEnum.ERROR;
                check.Side = inf.Attribute("side")?.Value == "product" ? SideEnum.product : SideEnum.unittester;

                result.Operations.Checks.Add(check);
            }

            result.ErrorInfo = new List<string>();
            var err = test.Element("error-info");
            if (err != null)
            {
                foreach (var inf in err.Elements("error"))
                {
                    result.ErrorInfo.Add(inf.Value);
                }
            }

            return result;
        }

        public Group ProcessGroup(XElement group)
        {
            var result = new Group();
            //title
            result.Title = group.Attribute("title").Value;
            //title
            result.GroupSequenceNumber = int.Parse(group.Attribute("group-sequence-number").Value);
            //title
            result.Retest = bool.Parse(group.Attribute("retest").Value);
            //title
            result.ResultTest = group.Attribute("result").Value == "PASS" ? ResultTestEnum.PASS : ResultTestEnum.FAIL;
            //title
            result.GroupId = int.Parse(group.Attribute("group-id").Value);


            return result;
        }
    }
}
