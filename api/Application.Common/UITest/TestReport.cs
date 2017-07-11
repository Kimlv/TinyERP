namespace App.Common.UITest
{
    using System.Xml.Serialization;

    [XmlType("report")]
    public class TestReport : ITestReport
    {
        [XmlAttribute("type")]
        public TestReportType Type { get; set; }
    }
}