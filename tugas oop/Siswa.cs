using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tugas1
{
    class Siswa
    {
        public string Nama { get; set; }
        public int Nuts { get; set; }
        public int Nuas { get; set; }
        public int Ntugas { get; set; }


        public Siswa(string nama, int nuas, int nuts, int ntugas) 
        {
            Nama = nama;
            Nuts = nuts;
            Nuas = nuas;
            Ntugas = ntugas;                     
        }

      }
}
