using System;
using System.Linq;
using System.Security;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace AnyListen.Settings
{
    public class PasswordEntry
    {
        public string Id { get; set; }
        [XmlIgnore, JsonIgnore]
        public string Field1 { get; set; }
        [XmlIgnore, JsonIgnore]
        public string Field2 { get; set; }

        [XmlElement(ElementName = "Field1")]
        public string Field12Serialize
        {
            get { return EncryptString(Field1); }
            set { Fiel