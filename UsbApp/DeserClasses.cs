using Newtonsoft.Json;
using System.Collections.Generic;

namespace kppApp
{
    /*
    class Passage
    {
        public double timestampUTC;
        public string card;
        public int isOut;
        public int isManual;
        public string kppID;
    }
    */
    internal class tsUpdated
    {
        [JsonProperty("timestampUTC")] public double timestampUTC { get; set; }
    }

    public class WorkerPerson
    {
        [JsonProperty("f")] public string fio { get; set; }
        [JsonProperty("t")] public long tabnom { get; set; }
        [JsonProperty("j")] public string job { get; set; }
        [JsonProperty("k")] public string card { get; set; }
        [JsonProperty("g")] public int isGuardian { get; set; }
    }

    /*
    timestampUTC REAL NOT NULL,
    card TEXT NOT NULL,
    isOut INTEGER DEFAULT 0 NOT NULL,
    kppId TEXT NOT NULL, 
    tabnom INTEGER DEFAULT 0 NOT NULL,
    isManual INTEGER DEFAULT 0 NOT NULL,
    */
    internal class Passage
    {
        [JsonProperty("passageID")] public long passageID { get; set; }
        [JsonProperty("timestampUTC")] public double timestampUTC { get; set; }
        [JsonProperty("card")] public string card { get; set; }
        [JsonProperty("isOut")] public int isOut { get; set; }
        [JsonProperty("kppId")] public string kppId { get; set; }
        [JsonProperty("tabnom")] public long tabnom { get; set; }
        [JsonProperty("isManual")] public int isManual { get; set; }
    }
}
