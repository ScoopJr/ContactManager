using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ContactManager
{
    public class Contact
    {
        [XmlElement(ElementName ="ID")]
        public string ID { get; set; }

        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "LastName")]
        public string LastName { get; set; }

        [XmlElement(ElementName = "Mobile")]
        public string Mobile { get; set; }

        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
    }
}


