namespace App.Common.UITest.Suite
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable()]
    [XmlType("testcase")]
    public class TestCaseData
    {
        [XmlAttribute("key")]
        public string Key { get; set; }
        [XmlAnyAttribute]
        public XmlAttribute[] Attributes { get; set; }
        [XmlIgnore]
        public IList<TestDataKeyNamePair> Values
        {
            get
            {
                IList<TestDataKeyNamePair> attrs = new List<TestDataKeyNamePair>();
                if (this.Attributes == null || this.Attributes.Length == 0) { return attrs; }
                foreach (XmlAttribute attr in this.Attributes)
                {
                    attrs.Add(new TestDataKeyNamePair(attr));
                }

                return attrs;
            }
        }
    }
}