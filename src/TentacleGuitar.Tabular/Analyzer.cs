using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace TentacleGuitar.Tabular
{
    public static class Analyzer
    {
        public static Tabular ParseMusicXml(string xml)
        {
            var ret = new Tabular();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            // 获取每分钟节拍数
            ret.BPM = Convert.ToInt32(xmlDoc.GetElementsByTagName("per-minute").Item(0).Value.ToString());

            // 获取调弦信息
            var tuning = xmlDoc.GetElementsByTagName("staff-tuning");
            foreach (XmlNode x in tuning)
                ret.Staff.Add(new Staff { TuningStep = x.FirstChild.Value.ToString(), TuningOctave = Convert.ToInt32(x.LastChild.Value.ToString()) });

            // 获取全部小节
            var measures = xmlDoc.GetElementsByTagName("measure");
            foreach(XmlNode x in measures)
            {
                int beats, beatType; // 定义拍号信息
                int timePerBeat; // 定义每拍占用毫秒数

                // 判断小节是否变奏
                if (x.ChildNodes.Cast<XmlNode>().Where(y => y.Name == "attributes").Count() > 0 && x.ChildNodes.Cast<XmlNode>().First(y => y.Name == "attributes").Cast<XmlNode>().Where(y => y.Name == "time").Count() > 0)
                {
                    var time = x.ChildNodes.Cast<XmlNode>().First(y => y.Name == "attributes").Cast<XmlNode>().Where(y => y.Name == "time").First();
                    beats = Convert.ToInt32(time.FirstChild.Value.ToString());
                    beatType = Convert.ToInt32(time.LastChild.Value.ToString());
                    timePerBeat = 60 * 1000 / ret.BPM;
                }
            }
        }
    }
}
