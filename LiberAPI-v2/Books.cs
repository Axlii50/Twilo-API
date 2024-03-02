using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Libre_API
{
    [XmlRoot("books")]
    public class Books
    {
        [XmlElement("book")]
        public Book[] book { get; set; }
    }
}
