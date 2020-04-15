using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyStore.BLL
{
    class userBLL
    {
        /* This is a BLL = Bussiess Logic Layer [It is process of development architecture of Software] Part in which I have used Logic of User table of database to this get set option
         It makes easier to handle out and deffrenciate which data types have been used we can see there is int infront of id and string infornt first_name*/
         /* Each table columns should be defined*/

        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string user_type { get; set; }
        public DateTime added_date { get; set; }
        public int added_by { get; set; }

        //------------------------------------------------------------------------------------------------------------------------------------
    }
}
