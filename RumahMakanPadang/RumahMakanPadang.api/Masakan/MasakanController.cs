using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RumahMakanPadang.bll;
using Model = RumahMakanPadang.dal.Models;
using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using RumahMakanPadang.dal.Repositories;

namespace RumahMakanPadang.api.Masakan
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasakanController : ControllerBase
    {
        private readonly MasakanService _masakanService;
        private readonly IMapper _mapper;
        private readonly ILogger<MasakanController> _logger;

        public MasakanController(ILogger<MasakanController> logger)
        {
            _logger = logger;
            MapperConfiguration config = new MapperConfiguration(m =>
            {
                m.CreateMap<Model.Masakan, Model.Masakan>();
            });

            _mapper = config.CreateMapper();

            _masakanService ??= new MasakanService();

        }



        /// <summary>
        /// Get all Masakan
        /// </summary>
        /// <response code="200">Request ok.</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Model.Masakan>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult GetAll()
        {
            List<Model.Masakan> result = _masakanService.GetAllMasakan();
            //List<MasakanWithAuthorDTO> mappedResult = _mapper.Map<List<MasakanWithAuthorDTO>>(result);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Get masakan by nama
        /// </summary>
        /// <param name="nama">user Model.</param>
        /// <response code="200">Request ok.</response>
        /// <response code="405">Request not found.</response>
        [HttpGet]
        [Route("{nama}")]
        [ProducesResponseType(typeof(Model.Masakan), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult GetByNama([FromRoute] string nama)
        {
            Model.Masakan result = _masakanService.GetMasakanByNama(nama);
            if (result != null)
            {
                Model.Masakan mappedResult = _mapper.Map<Model.Masakan>(result);
                return new OkObjectResult(result);
            }
            return new NotFoundResult();
        }


        /// <summary>
        /// Create masakan entry 
        /// </summary>
        /// <param name="masakan">masakan data.</param>
        /// <response code="200">Request ok.</response>
        /// <response code="400">Request failed because of an exception.</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult Create([FromBody] Model.Masakan masakan)
        {
            try
            {
                Model.Masakan new_masakan = _mapper.Map<Model.Masakan>(masakan);
                _masakanService.CreateMasakan(masakan);
                //Console.WriteLine("Obj created");
                _masakanService.GetAllMasakan();
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new BadRequestResult();
            }

        }

        ///// <summary>
        ///// Delete Book
        ///// </summary>
        ///// <param name="nama">Nama masakan</param>
        ///// <response code="200">Request ok.</response>
        //[HttpDelete]
        //[Route("{isbn}")]
        //[ProducesResponseType(typeof(BookDTO), 200)]
        //public ActionResult Delete([FromRoute] string nama)
        //{
        //    _masakanService.DeleteMasakan(nama);
        //    return new OkResult();
        //}

        /// <summary>
        /// Delete Book
        /// </summary>
        /// <param name="nama">Nama masakan</param>
        /// <response code="200">Request ok.</response>
        [HttpDelete]
        [Route("{nama}")]
        [ProducesResponseType(typeof(Model.Masakan), 200)]
        public ActionResult Delete([FromRoute] string nama)
        {
            _masakanService.DeleteMasakan(nama);
            return new OkResult();
        }
        ///// <summary>
        ///// Get all Masakan
        ///// </summary>
        ///// <response code="200">Request ok.</response>
        //[HttpGet]
        //[Route("")]
        //[ProducesResponseType(typeof(List<Model.Masakan>), 200)]
        //[ProducesResponseType(typeof(string), 400)]
        //public async Task<ActionResult> GetAllAsync()
        //{
        //    List<Model.Masakan> result = await _masakanService.GetAllMasakanAsync();
        //    //List<MasakanWithAuthorDTO> mappedResult = _mapper.Map<List<MasakanWithAuthorDTO>>(result);
        //    return new OkObjectResult(mappedResult);
        //}

        ///// <summary>
        ///// Get masakan by nama
        ///// </summary>
        ///// <param name="nama">user Model.</param>
        ///// <response code="200">Request ok.</response>
        ///// <response code="405">Request not found.</response>
        //[HttpGet]
        //[Route("{isbn}")]
        //[ProducesResponseType(typeof(Model.Masakan), 200)]
        //[ProducesResponseType(typeof(string), 400)]
        //public async Task<ActionResult> GetByTitleAsync([FromRoute] string nama)
        //{
        //    Model.Masakan result = await _masakanService.GetMasakanByNamaAsync(isbn);
        //    if (result != null)
        //    {
        //        BookWithAuthorDTO mappedResult = _mapper.Map<BookWithAuthorDTO>(result);
        //        return new OkObjectResult(mappedResult);
        //    }
        //    return new NotFoundResult();
        //}

        ///// <summary>
        ///// Create masakan entry 
        ///// </summary>
        ///// <param name="masakanDto">masakan data.</param>
        ///// <response code="200">Request ok.</response>
        ///// <response code="400">Request failed because of an exception.</response>
        //[HttpPost]
        //[Route("")]
        //[ProducesResponseType(typeof(string), 200)]
        //[ProducesResponseType(typeof(string), 400)]
        //public async Task<ActionResult> CreateAsync([FromBody] BookWithAuthorDTO bookDto)
        //{
        //    try
        //    {
        //        Model.Masakan book = _mapper.Map<Model.Masakan>(bookDto);
        //        await _masakanService.CreateMasakanAsync(book);
        //        return new OkResult();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.ToString());
        //        return new BadRequestResult();
        //    }

        //}

    }
}
