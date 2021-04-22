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
                Console.WriteLine("----Menu Pilihan----");
                Console.WriteLine("1. Tambah Data Siswa");
                Console.WriteLine("2. Tampil Data Siswa");
                Console.WriteLine("3. Nilai Total Siswa");
                Console.WriteLine("4. Hapus Data Siswa");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option) 
                {
                    case 1:
                        Console.WriteLine("Nama Siswa : ");
                        string nama = Console.ReadLine();

                        Console.WriteLine("Nilai UTS : ");
                        int nuts = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine($"Nilai UAS : ");
                        int nuas = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Nilai Tugas : ");
                        int ntugas = Convert.ToInt32(Console.ReadLine());
                        namaSiswa.Add(new Siswa(nama, nuas, nuts, ntugas));
                        Console.WriteLine("Tambah data berhasil");
                      break;

                    case 2:
                        Console.WriteLine("Tampilan Data");
                        Console.WriteLine("Nilai Siswa");
                                              
                        foreach (Siswa s in namaSiswa)
                        { 
                            Console.WriteLine($"Nama : {s.Nama}");
                            Console.WriteLine($"Nilai UTS : {s.Nuts}");
                            Console.WriteLine($"Nilai UAS : {s.Nuas}");
                            Console.WriteLine($"Nilai Tugas : {s.Ntugas}");
                        }
                          break;
                    case 3:
                        Console.WriteLine("Nilai Total Siswa");
                        Console.WriteLine("==================");
                        foreach (Siswa s in namaSiswa)
                        {
                            //Console.WriteLine($" siswa : {s.Nama}");
                            double Ntotal = (s.Nuts * 0.3) + (s.Nuas * 0.3) + (s.Ntugas * 0.4);
                            Console.WriteLine($"Nilai total siswa {s.Nama}= {Ntotal}");
                        } break;

                    case 4:
                        int pilihdata = Convert.ToInt32(Console.ReadLine());
                        namaSiswa.RemoveAt(pilihdata);
                        Console.WriteLine("Hapus Data Berhasil");
                     break;

                }
            }
        }
        
    }
}
