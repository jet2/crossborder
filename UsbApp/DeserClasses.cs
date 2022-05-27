using Newtonsoft.Json;
using SQLite;
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


    public class Val
    {
        [JsonProperty("str")] public string str { get; set; }
    }

    public class ValCnt
    {
        [JsonProperty("cnt")] public int cnt { get; set; }
    }

    // select w.second_name||'@'||w.first_name||'@'||w.last_name as fio,
    // w.asup_guid as userguid,
    // p.name as job,
    // p.personnel_number as tabnom,
    // d.number as card
    public class PrettyWorker
    {
        [JsonProperty("fio")] public string fio { get; set; }
        [JsonProperty("userguid")] public string userguid { get; set; }
        [JsonProperty("job")] public string job { get; set; }
        [JsonProperty("tabnom")] public string tabnom { get; set; }
        [JsonProperty("card")] public string card { get; set; }
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
    
    public class WorkerPersonHierarhy
    {
        public int id_person { get; set; }
        public int id_position { get; set; }
        public int id_card { get; set; }
    }


    public class PerimeterOperation
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
    
    internal class Passage: PassageBase {
        [PrimaryKey, AutoIncrement] public long passageID { get; set; }
    }

    internal class PassageMem : PassageBase
    {
        [JsonProperty("passageID")] public long passageID { get; set; }
    }

    

    internal class PassageBase
    {
        [JsonProperty("rowID")] public string rowID { get; set; }
        [JsonProperty("timestampUTC")] public double timestampUTC { get; set; }
        [JsonProperty("card")] public string card { get; set; }
        [JsonProperty("operCode")] public int operCode { get; set; }
        [JsonProperty("kppId")] public string kppId { get; set; }
        [JsonProperty("tabnom")] public string tabnom { get; set; }
        [JsonProperty("userguid")] public string userguid { get; set; }
        [JsonProperty("isManual")] public int isManual { get; set; }
        [JsonProperty("isDelivered")] public int isDelivered { get; set; }
        [JsonProperty("isChecked")] public int isChecked { get; set; }
        [JsonProperty("description")] public string description { get; set; }
        [JsonProperty("toDelete")] public int toDelete { get; set; }
    }

    internal class PassageFIO : Passage
    {
        [JsonProperty("fio")] public string fio { get; set; }
    }

    public class ShortPassage
    {
        public string card { get; set; }
        public string tabnom { get; set; }
        public int control_point_type_id { get; set; }
        public int timestampUTC { get; set; }
        public string description { get; set; }
        public string userguid { get; set; }
        public int isManual { get; set; }
        public int toDelete { get; set; }
        public int isDelivered { get; set; }
    };

    internal class Passage1bitExt
    {
        [JsonProperty("id")] public string bit1_id { get; set; }
        [JsonProperty("system")] public string bit1_system { get; set; }
        [JsonProperty("timestamp")] public long bit1_timestampUTC { get; set; }
        [JsonProperty("card_number")] public string bit1_card_number { get; set; }
        //[JsonProperty("card_guid")] public string bit1_card_guid { get; set; }
        //[JsonProperty("position_guid")] public string bit1_position_guid { get; set; }
        [JsonProperty("individual_guid")] public string bit1_individual_guid { get; set; }
        [JsonProperty("reader_id")] public int bit1_reader_id { get; set; }
        [JsonProperty("description")] public string bit1_comment { get; set; }
        //[JsonProperty("personnel_number")] public string bit1_tabnom { get; set; }
        [JsonProperty("type")] public string bit1_opercode { get; set; }
        [JsonProperty("control_point_type_id")] public int bit1_control_point_type_id { get; set; }
        [JsonProperty("timezone_seconds")] public int timezone_seconds { get; set; }

    }


    internal class Passage1bit
    {
        [JsonProperty("id")] public string bit1_id { get; set; }
        [JsonProperty("system")] public string bit1_system { get; set; }
        [JsonProperty("timestamp")] public long bit1_timestampUTC { get; set; }
        [JsonProperty("lat")] public double bit1_lat { get; set; }
        [JsonProperty("lon")] public double bit1_lon { get; set; }
        [JsonProperty("card")] public string bit1_card { get; set; }
        [JsonProperty("reader_id")] public string bit1_reader_id { get; set; }
        [JsonProperty("description")] public string bit1_comment { get; set; }
        [JsonProperty("personnel_number")] public string bit1_tabnom { get; set; }
        [JsonProperty("type")] public string bit1_opercode { get; set; }
    }

    public class Card
    {
        public int ownerid { get; set; }
        public int id { get; set; }
        public string number { get; set; }
    }

    public class Position
    {
        public int ownerid { get; set; }
        public int id { get; set; }
        public bool active { get; set; }
        public string name { get; set; }
        public string personnel_number { get; set; }
        //public List<Card> cards { get; set; }
    }

    public class PositionX : Position
    {
        public List<Card> cards { get; set; }
    }

    public class WorkerPersonPure
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string last_name { get; set; }
        public string asup_guid { get; set; }
    }

    public class WorkerPersonX : WorkerPersonPure
    {
        public List<PositionX> positions { get; set; }

    }

    public class AllPersons
    {
        public List<WorkerPersonX> data { get; set; }
    }

    public class ControlPoint
    {
        public int id;
        public string title;
    }
    public class AppControlPoints
    {
        public List<ControlPoint> data { get; set; }
    }
}
