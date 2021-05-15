using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imovi.Models.MovieModels {
    public class Root {
        public string created_by { get; set; }
        public string description { get; set; }
        public int favorite_count { get; set; }
        public string id { get; set; }
        public List<Movie> items { get; set; }


        public int page { get; set; }
        public List<Movie> results { get; set; }
    }
}
