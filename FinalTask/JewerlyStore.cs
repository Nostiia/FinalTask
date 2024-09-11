using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalTask
{
    [Serializable]
    public class JewerlyStore
    {
        [XmlAttribute]
        public string mainAdress { get; set; }
        public List<string> adresses { get; set; }
        [XmlAttribute]
        public int numberOfJewerly { get; set; }
        [XmlArray("Catalog")]
        [XmlArrayItem("Jewerly")]
        public List<Jewerly> jewerlys { get; set; }
        public JewerlyStore() { }
        public JewerlyStore(string adresses, int numberOfJewerly, List<Jewerly> jewerlys)
        {
            mainAdress = adresses;
            //this.adresses = adresses;
            this.numberOfJewerly = numberOfJewerly;
            this.jewerlys = jewerlys;
        }
        public override string ToString()
        {
            string str = $"Adress- {mainAdress} \n Number of beauty- {numberOfJewerly} \n List: \n";
            foreach (var item in jewerlys)
            {
                str = str + item.ToString();
            }
            return str;
        }
    }
}
