namespace App.Common.UITest.Suite
{
    using System.Xml.Serialization;

    [System.Serializable()]
    [XmlType("testcases")]
    public class TestCaseCollectionRef
    {
        [XmlAttribute("path")]
        public string Path { get; set; }
    }
}