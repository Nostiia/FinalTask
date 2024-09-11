using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalTask
{
    [Serializable]
    public class Jewerly : IComparable<Jewerly>
    {
        [XmlIgnore]
        private int id;
        [XmlIgnore]
        public static int lastId = 1;
        [XmlAttribute]
        public int Id { get { return id; } }
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public string metal { get; set; }
        [XmlAttribute]
        public double masa { get; set; }
        [XmlAttribute]
        public double price { get; set; }
        public Jewerly()
        { id = lastId++; }
        public Jewerly(string name, string metal, double masa, double price)
        {
            id = lastId++;
            this.name = name;
            this.metal = metal;
            this.masa = masa;
            this.price = price;
        }
        public override string ToString()
        {
            return $"Id : {Id} \n Name: {name},\n Metal: {metal}, Mass: {masa},\n Price: {price}. \n";
        }
        public int CompareTo(Jewerly other)
        {
            return this.name.CompareTo(other.name);
        }
    }
}
