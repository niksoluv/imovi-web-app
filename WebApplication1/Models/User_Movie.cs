using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imovi.Models {
    public class User_Movie {
        public int Id { get; set; }
        public int movie_id { get; set; }
        public User user { get; set; }
    }
}
