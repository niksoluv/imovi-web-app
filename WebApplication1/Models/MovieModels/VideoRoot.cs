using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imovi.Models.MovieModels {
    public class VideoRoot {
        public int id { get; set; }
        public List<VideoResult> results {get;set;}
    }
}
