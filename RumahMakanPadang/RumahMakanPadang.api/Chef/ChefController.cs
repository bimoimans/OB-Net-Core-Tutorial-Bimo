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
using RumahMakanPadang.api.Chef.DTO;
using RumahMakanPadang.api.Masakan.DTO;

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
                m.CreateMap<Model.Chef, ChefDTO>();
                m.CreateMap<ChefDTO, Model.Chef>();
                m.CreateMap<Model.Masakan, MasakanDTO>();

                m.CreateMap<ChefWithMasakanDTO, Model.Chef>();
                m.CreateMap<Model.Chef, ChefWithMasakanDTO>();
            });

            _mapper = config.CreateMapper();

            _chefService ??= new ChefService(uow);//, configuration);

        }

        /// <summary>
        /// Create Chef entry 
        /// </summary>
        /// <param name="chefDto">Chef data.</param>
        /// <response code="200">Request ok.</response>
        /// <response code="400">Request failed because of an exception.</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> CreateAsync([FromBody] ChefDTO chefDto)
        {
            try
            {
                Model.Chef chef_model = _mapper.Map<Model.Chef>(chefDto);
                await _chefService.CreateChefAsync(chef_model);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new BadRequestResult();
            }

        }

        /// <summary>
        /// Get all Chef
        /// </summary>
        /// <response code="200">Request ok.</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Model.Chef>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> GetAllAsync()
        {
            List<Model.Chef> result = await _chefService.GetAllChefAsync();
            List<ChefWithMasakanDTO> mappedResult = _mapper.Map<List<ChefWithMasakanDTO>>(result);
            return new OkObjectResult(mappedResult);
        }

        /// <summary>
        /// Get Chef by nama
        /// </summary>
        /// <param name="nama">user Model.</param>
        /// <response code="200">Request ok.</response>
        /// <response code="405">Request not found.</response>
        [HttpGet]
        [Route("queryNama")]
        [ProducesResponseType(typeof(ChefWithMasakanDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> GetByNamaAsync([FromQuery(Name = "nama")] string nama)
        {
            Model.Chef result = await _chefService.GetChefByNamaAsync(nama);
            if (result != null)
            {
                ChefWithMasakanDTO mappedResult = _mapper.Map<ChefWithMasakanDTO>(result);
                return new OkObjectResult(mappedResult);
            }
            return new NotFoundResult();
        }

        /// <summary>
        /// Get Chef by Ktp
        /// </summary>
        /// <param name="noKTP">No. KTP of the Chef</param>
        /// <response code="200">Request ok.</response>
        /// <response code="405">Request not found.</response>
        [HttpGet]
        [Route("queryKTP")]
        [ProducesResponseType(typeof(ChefWithMasakanDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> GetByKTPAsync([FromQuery(Name = "noKTP")] string noKTP)
        {
            Model.Chef result = await _chefService.GetChefByKTPAsync(noKTP);
            if (result != null)
            {
                ChefWithMasakanDTO mappedResult = _mapper.Map<ChefWithMasakanDTO>(result);
                return new OkObjectResult(mappedResult);
            }
            return new NotFoundResult();
        }

        /// <summary>
        /// Delete Chef
        /// </summary>
        /// <param name="nama">Nama Chef</param>
        /// <response code="200">Request ok.</response>
        [HttpDelete]
        [Route("{nama}")]
        [ProducesResponseType(typeof(Model.Chef), 200)]
        public async Task<ActionResult> DeleteAsync([FromRoute] string nama)
        {
            await _chefService.DeleteChefAsync(nama);
            return new OkResult();
        }

    }
}
