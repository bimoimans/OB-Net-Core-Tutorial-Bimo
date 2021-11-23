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
using Microsoft.Extensions.Configuration;

namespace RumahMakanPadang.api.Chef
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefController : ControllerBase
    {
        private readonly ChefService _chefService;
        private readonly IMapper _mapper;
        private readonly ILogger<ChefController> _logger;

        public ChefController(ILogger<ChefController> logger, IUnitOfWork uow, IConfiguration configuration)
        {
            _logger = logger;
            MapperConfiguration config = new MapperConfiguration(m =>
            {
                m.CreateMap<Model.Masakan, Model.Masakan>();
            });

            _mapper = config.CreateMapper();

            _chefService ??= new ChefService(uow, configuration);

        }

        ///// <summary>
        ///// Get all Masakan
        ///// </summary>
        ///// <response code="200">Request ok.</response>
        //[HttpGet]
        //[Route("")]
        //[ProducesResponseType(typeof(List<Model.Masakan>), 200)]
        //[ProducesResponseType(typeof(string), 400)]
        //public ActionResult GetAll()
        //{
        //    List<Model.Masakan> result = _masakanService.GetAllMasakan();
        //    //List<MasakanWithAuthorDTO> mappedResult = _mapper.Map<List<MasakanWithAuthorDTO>>(result);
        //    return new OkObjectResult(result);
        //}

        /// <summary>
        /// Delete Book
        /// </summary>
        /// <param name="nama">Nama masakan</param>
        /// <response code="200">Request ok.</response>
        [HttpDelete]
        [Route("{nama}")]
        [ProducesResponseType(typeof(Model.Masakan), 200)]
        public async Task<ActionResult> DeleteAsync([FromRoute] string nama)
        {
            await _chefService.DeleteChefAsync(nama);
            return new OkResult();
        }

        /// <summary>
        /// Get all Masakan
        /// </summary>
        /// <response code="200">Request ok.</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Model.Masakan>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> GetAllAsync()
        {
            List<Model.Chef> result = await _chefService.GetAllChefAsync();
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
        public async Task<ActionResult> GetByNamaAsync([FromRoute] string nama)
        {
            Model.Chef result = await _chefService.GetChefByNamaAsync(nama);
            if (result != null)
            {
                //BookWithAuthorDTO mappedResult = _mapper.Map<BookWithAuthorDTO>(result);
                return new OkObjectResult(result);
            }
            return new NotFoundResult();
        }

        /// <summary>
        /// Create masakan entry 
        /// </summary>
        /// <param name="masakanDto">masakan data.</param>
        /// <response code="200">Request ok.</response>
        /// <response code="400">Request failed because of an exception.</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> CreateAsync([FromBody] Model.Chef chef)
        {
            try
            {
                Model.Chef chef_model = _mapper.Map<Model.Chef>(chef);
                await _chefService.CreateChefAsync(chef_model);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new BadRequestResult();
            }

        }

    }
}
