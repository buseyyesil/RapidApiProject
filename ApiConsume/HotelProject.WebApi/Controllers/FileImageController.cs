using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HotelProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileImageController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm]IFormFile file)
        {
            var fileName=Guid.NewGuid()+Path.GetExtension(file.FileName);//benzersiz dosya adı oluşturur
            var path=Path.Combine(Directory.GetCurrentDirectory(), "images/"+fileName); // dosyanın kayıt edileceği yol
            var stream = new FileStream(path, FileMode.Create); //dosya akışı oluşturur yüklenen dosyayı belirlenen yola asenkron olarak kopyalar.
            await file.CopyToAsync(stream);
            return Created("", file);
        }
    }
}



 /* Ahmet bir sosyal medya uygulamasında profil fotoğrafı değiştirmek istiyor
Telefonundan bir fotoğraf seçiyor
Mobil uygulama → API'ye POST isteği atar
API resmi sunucuya kaydeder
Ahmet'in profili artık yeni fotoğrafı gösterir* */