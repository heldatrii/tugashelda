using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tugas1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<Siswa> namaSiswa = new List<Siswa>();
            namaSiswa.Add(new Siswa("Helda",56,67,78));
            namaSiswa.Add(new Siswa("Bagas",56,87,34));
            namaSiswa.Add(new Siswa("Berlin",56,76,55));
            
            
            while (true)
            {
                try
                {
                    Console.WriteLine("----Menu Pilihan----");
                    Console.WriteLine("1. Tambah Data Siswa");
                    Console.WriteLine("2. Tampil Data Siswa");
                    Console.WriteLine("3. Nilai Total Siswa");
                    Console.WriteLine("4. Hapus Data Siswa");
                 }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e.Message}");
                    Console.ReadLine();
                     throw;
                }
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option) 
                {
                    case 1:
                        TambahDataSiswa(namaSiswa);
                        break;

                    case 2:
                        TampilDataSiswa(namaSiswa); break;
                    case 3:
                        TampilNilaiTotal(namaSiswa); break;

                    case 4:
                        HapusData(namaSiswa); break;

                }
            }
        }

        private static void HapusData(List<Siswa> namaSiswa)
        {
            int pilihdata = Convert.ToInt32(Console.ReadLine());
            namaSiswa.RemoveAt(pilihdata);
            Console.WriteLine("Hapus Data Berhasil");
        }

        private static void TampilNilaiTotal(List<Siswa> namaSiswa)
        {
            Console.WriteLine("Nilai Total Siswa");
            Console.WriteLine("==================");

            foreach (Siswa s in namaSiswa)
            {
                //Console.WriteLine($" siswa : {s.Nama}");
                double nTotal = (s.NUts * 0.3) + (s.NUas * 0.3) + (s.NTugas * 0.4);
                Console.WriteLine($"Nilai total siswa {s.Nama}= {nTotal}");
            }
        }

        private static void TampilDataSiswa(List<Siswa> namaSiswa)
        {
            Console.WriteLine("Tampilan Data");
            Console.WriteLine("Nilai Siswa");

            foreach (Siswa s in namaSiswa)
            {
                Console.WriteLine($"Nama : {s.Nama}");
                Console.WriteLine($"Nilai UTS : {s.NUts}");
                Console.WriteLine($"Nilai UAS : {s.NUas}");
                Console.WriteLine($"Nilai Tugas : {s.NTugas}");
            }
        }

        private static void TambahDataSiswa(List<Siswa> namaSiswa)
        {
            try
            {
                Console.WriteLine("Nama Siswa : ");
                string nama = Console.ReadLine();
                Console.WriteLine("Nilai UTS : ");
                int nUts = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Nilai UAS : ");
                int nUas = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Nilai Tugas : ");
                int nTugas = Convert.ToInt32(Console.ReadLine());
                namaSiswa.Add(new Siswa(nama, nUas, nUts, nTugas));

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                throw;
            }
            Console.WriteLine("=====================");
            Console.WriteLine("Tambah data berhasil");
            Console.WriteLine("=====================");
        }
    }
}
