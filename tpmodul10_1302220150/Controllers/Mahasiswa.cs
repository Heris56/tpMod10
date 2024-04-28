using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tpmodul10_1302220150.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerMahasiswa : Controller
    {
        public class Mahasiswa
        {
            public string Nama { get; set; }
            public string NIM { get; set; }
            
        }

        private static Mahasiswa[] mahasiswas = new Mahasiswa[]
        {
            new Mahasiswa { Nama = "Haikal Risnandar", NIM = "1302220150" },
            new Mahasiswa { Nama = "Dafa Raimi Suandi", NIM = "1302223156" },
            new Mahasiswa { Nama = "Darryl Frizzangelo Rambi", NIM = "1302223154"},
            new Mahasiswa { Nama = "Fersya Zufar Muhara", NIM = "1302223090"},
            new Mahasiswa { Nama = "Raphael Permana Barus", NIM = "1302220140"},
            new Mahasiswa { Nama = "Mahesa Athaya Zain", NIM = "1302220105"}
        };
        [HttpGet]
        public IActionResult GetMahasiswa()
        {
            return Ok(mahasiswas);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(mahasiswas[id]);
        }
        [HttpPost]
        public IActionResult CreateMahasiswa([FromBody] MahasiswaInputModel inputModel)
        {
            if (inputModel == null)
            {
                return BadRequest("Data mahasiswa tidak boleh kosong");
            }

            // Membuat objek Mahasiswa baru dari input model
            Mahasiswa newMahasiswa = new Mahasiswa
            {
                Nama = inputModel.Nama,
                NIM = inputModel.NIM
            };

            // Menambahkan mahasiswa baru ke dalam list
            Mahasiswa[] newMahasiswas = new Mahasiswa[mahasiswas.Length + 1];
            for (int i = 0; i < mahasiswas.Length; i++)
            {
                newMahasiswas[i] = mahasiswas[i];
            }
            newMahasiswas[mahasiswas.Length] = newMahasiswa;
            mahasiswas = newMahasiswas;

            // Return 201 Created response
            return CreatedAtAction(nameof(GetMahasiswa), newMahasiswa);
        }
        [HttpDelete("{index}")]
        public IActionResult DeleteMahasiswa(int index)
        {
            if (index < 0 || index >= mahasiswas.Length)
            {
                return NotFound("Indeks mahasiswa tidak valid");
            }

            // Hapus mahasiswa dari array berdasarkan indeks
            for (int i = index; i < mahasiswas.Length - 1; i++)
            {
                mahasiswas[i] = mahasiswas[i + 1];
            }
            Array.Resize(ref mahasiswas, mahasiswas.Length - 1);

            // Return 204 No Content response
            return NoContent();
        }
    }
    public class MahasiswaInputModel
    {
        public string Nama { get; set; }
        public string NIM { get; set; }
    }
}
