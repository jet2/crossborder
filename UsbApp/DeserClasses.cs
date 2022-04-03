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
        [JsonProperty("u")] public string userguid { get; set; }
        [JsonProperty("k")] public string card { get; set; }
        [JsonProperty("g")] public int isGuardian { get; set; }
        [JsonProperty("j")] public string jobDescription { get; set; }
    }

    public class perimeterOperation
    {
        [JsonProperty("operid")] public int operid { get; set; }
        [JsonProperty("operdesc")] public string operdesc { get; set; }
        [JsonProperty("operhide")] public int operhide { get; set; }
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
        [JsonProperty("isOut")] public int operCode { get; set; }
        [JsonProperty("kppId")] public string kppId { get; set; }
        [JsonProperty("tabnom")] public long tabnom { get; set; }
        [JsonProperty("isManual")] public int isManual { get; set; }
        [JsonProperty("isDelivered")] public int isDelivered { get; set; }
        [JsonProperty("description")] public string description { get; set; }
        [JsonProperty("toDelete")] public int toDelete { get; set; }
    }

    internal class Passage1bit
    {
        [JsonProperty("id")] public string bit1_id { get; set; }
        [JsonProperty("system")] public string bit1_system { get; set; }
        [JsonProperty("timestamp")] public long bit1_timestampUTC { get; set; }
        [JsonProperty("lat")] public double bit1_lat { get; set; }
        [JsonProperty("lon")] public double bit1_lon { get; set; }
        [JsonProperty("card")] public string bit1_card { get; set; }
        [JsonProperty("reader_id")] public int bit1_reader_id { get; set; }
        [JsonProperty("description")] public string bit1_comment { get; set; }
        [JsonProperty("personnel_number")] public string bit1_tabnom { get; set; }
        [JsonProperty("type")] public string bit1_opercode { get; set; }
    }

}
