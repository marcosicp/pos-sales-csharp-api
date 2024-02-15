using Microsoft.AspNetCore.Mvc;
using DieteticaPuchiApi.Interfaces;

namespace DieteticaPuchiApi.Controllers
{
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        private readonly IUsuariosRepository _noteRepository;

        public SystemController(IUsuariosRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // Call an initialization - api/system/init
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting == "init")
            {
                //_noteRepository.DeleteAllUsuarios();
                //var name = _noteRepository.CreateIndex();

                //_noteRepository.AddUsuario(new Usuarios()
                //{
                //    Id = "1",
                //    Body = "Test note 1",
                //    UpdatedOn = DateTime.Now,
                //    UserId = 1,
                //    HeaderImage = new UsuariosImage
                //    {
                //        ImageSize = 10,
                //        Url = "http://localhost/image1.png",
                //        ThumbnailUrl = "http://localhost/image1_small.png"
                //    }
                //});

                //_noteRepository.AddUsuario(new Usuarios()
                //{
                //    Id = "2",
                //    Body = "Test note 2",
                //    UpdatedOn = DateTime.Now,
                //    UserId = 1,
                //    HeaderImage = new UsuariosImage
                //    {
                //        ImageSize = 13,
                //        Url = "http://localhost/image2.png",
                //        ThumbnailUrl = "http://localhost/image2_small.png"
                //    }
                //});

                //_noteRepository.AddUsuarios(new Usuarios()
                //{
                //    Id = "3",
                //    Body = "Test note 3",
                //    UpdatedOn = DateTime.Now,
                //    UserId = 1,
                //    HeaderImage = new UsuariosImage
                //    {
                //        ImageSize = 14,
                //        Url = "http://localhost/image3.png",
                //        ThumbnailUrl = "http://localhost/image3_small.png"
                //    }
                //});

                //_noteRepository.AddUsuarios(new Usuarios()
                //{
                //    Id = "4",
                //    Body = "Test note 4",
                //    UpdatedOn = DateTime.Now,
                //    UserId = 1,
                //    HeaderImage = new UsuariosImage
                //    {
                //        ImageSize = 15,
                //        Url = "http://localhost/image4.png",
                //        ThumbnailUrl = "http://localhost/image4_small.png"
                //    }
                //});

                return "Database UsuariossDb was created, and collection 'Usuarioss' was filled with 4 sample items";
            }

            return "Unknown";
        }
    }
}
