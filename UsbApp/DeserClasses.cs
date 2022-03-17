using Newtonsoft.Json;
using System.Collections.Generic;

namespace kppApp
{
    class Passage
    {
        public double timestampUTC;
        public string card;
        public int isOut;
        public int isManual;
        public string kppID;
    }

    internal class tsUpdated
    {
        [JsonProperty("timestampUTC")] public double timestampUTC { get; set; }
    }

    internal class WokerPerson
    {
        [JsonProperty("f")] public string fio { get; set; }
        [JsonProperty("t")] public int tabnom { get; set; }
        [JsonProperty("j")] public string job { get; set; }
        [JsonProperty("k")] public string kard { get; set; }
        [JsonProperty("g")] public int isGuardian { get; set; }
    }
}
