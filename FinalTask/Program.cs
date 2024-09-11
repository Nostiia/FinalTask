using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using System.Net.NetworkInformation;
using System;



namespace FinalTask
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                string path = @"C:\Stores.txt";
                List<JewerlyStore> jewerlyStores = new List<JewerlyStore>();
                jewerlyStores = Read(path);
                //foreach (var item in jewerlyStores)
                //{
                //    Console.WriteLine(item);
                //}
                nameOfMetals(jewerlyStores);
                Sortby10(jewerlyStores);
                Serialization(jewerlyStores);
                Deseriazation();
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("Errors.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"An Error in Main: {ex.Message}");
                }
            }
        }
        public static List<JewerlyStore> Read(string path)
        {
            List<JewerlyStore> stores = new List<JewerlyStore>();
            try
            {
                using (StreamReader reader = new StreamReader(path, System.Text.Encoding.Default))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(';');
                        List<Jewerly> Jewerlys = new List<Jewerly>();
                        if (values.Length >= 3)
                        {

                            for (int k = 2; k < values.Length; k++)
                            {

                                string listJewerlys = values[k];
                                string[] arrayJewerlys = listJewerlys.Split(",");
                                if (arrayJewerlys.Length == 4)
                                {
                                    Jewerly jewerly = new Jewerly(arrayJewerlys[0], arrayJewerlys[1], int.Parse(arrayJewerlys[2]), int.Parse(arrayJewerlys[3]));
                                    if (jewerly != null)
                                    {
                                        Jewerlys.Add(jewerly);
                                    }
                                }
                            }
                            JewerlyStore store = new JewerlyStore(values[0], int.Parse(values[1]), Jewerlys);

                            if (store != null)
                            {
                                stores.Add(store);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("Errors.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"An Error in Read method: {ex.Message}");
                }
            }
            return stores;
        }
        public static void nameOfMetals(List<JewerlyStore> jewerlyStores)
        {
            try
            {
                List<string> metals = new List<string>();
                foreach (var item in jewerlyStores)
                {
                    foreach (var jew in item.jewerlys)
                    {
                        metals.Add(jew.metal);
                    }
                }
                metals.Sort();
                for (int i = 1; i < metals.Count; i++)
                {
                    if (metals[i] == metals[i - 1])
                    {
                        metals.RemoveAt(i);
                        i--;
                    }
                }
                for (int j = 0; j < metals.Count; j++)
                {
                    int count = 0;
                    foreach (var item in jewerlyStores)
                    {
                        foreach (var jew in item.jewerlys)
                        {
                            if (jew.metal == metals[j])
                            {
                                count++;
                            }
                        }
                    }
                    using (StreamWriter sw = new StreamWriter("Counts.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(metals[j] + "- " + count);
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("Errors.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"An Error in nameOfMetals method: {ex.Message}");
                }
            }
        }
        public static void Sortby10(List<JewerlyStore> jewerlyStores)
        {
            try
            {
                foreach (var item in jewerlyStores)
                {
                    if (item.jewerlys.Count >= 10)
                    {
                        item.jewerlys.Sort();
                        using (StreamWriter sw = new StreamWriter("SortlyList.txt", true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(item.mainAdress);
                            foreach (var jew in item.jewerlys)
                            {
                                sw.WriteLine(jew);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("Errors.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"An Error in Sortby10: {ex.Message}");
                }
            }
        }
        public static void Serialization(List<JewerlyStore> stores)
        {
            try
            {
                XmlSerializer xmlser = new XmlSerializer(typeof(List<JewerlyStore>));
                using (Stream serialStream = new FileStream("stores.xml", FileMode.Create))
                {
                    xmlser.Serialize(serialStream, stores);
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("Errors.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"An Error in Serialization: {ex.Message}");
                }
            }
        }
        public static void Deseriazation()
        {
            try
            {
                Stream serialStream = new FileStream("stores.xml", FileMode.Open);
                XmlSerializer xmlser = new XmlSerializer(typeof(List<JewerlyStore>));
                List<JewerlyStore> jew = xmlser.Deserialize(serialStream) as List<JewerlyStore>;
                if (jew != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("XML: Deserialized");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foreach (var item in jew)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("Errors.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"An Error in Deseriazation: {ex.Message}");
                }
            }
        }
    }
}
