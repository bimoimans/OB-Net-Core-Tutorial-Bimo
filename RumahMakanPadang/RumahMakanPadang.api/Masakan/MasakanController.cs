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
using RumahMakanPadang.api.Masakan.DTO;

namespace RumahMakanPadang.api.Masakan
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasakanController : ControllerBase
    {
        private readonly MasakanService _masakanService;
        private readonly IMapper _mapper;
        private readonly ILogger<MasakanController> _logger;

        public MasakanController(ILogger<MasakanController> logger, IUnitOfWork uow, IConfiguration configuration)
        {
            _logger = logger;
            MapperConfiguration config = new MapperConfiguration(m =>
            {
                m.CreateMap<MasakanDTO, Model.Masakan>();
                m.CreateMap<Model.Masakan, MasakanDTO>();
                m.CreateMap<MasakanWithChefDTO, Model.Masakan>()
                    .ForMember(s => s.ChefKTP, d => d.MapFrom(t => t.Chef.KTP)) //???
                    .ForMember(s => s.Chef, a => a.Ignore());
                m.CreateMap<Model.Masakan, MasakanWithChefDTO>();
            });

            _mapper = config.CreateMapper();

            _masakanService ??= new MasakanService(uow, configuration);
            
        }

        /// <summary>
        /// Create masakan entry 
        /// </summary>
        /// <param name="masakanDTO">masakan data.</param>
        /// <response code="200">Request ok.</response>
        /// <response code="400">Request failed because of an exception.</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> CreateAsync([FromBody] MasakanWithChefDTO masakanDTO)
        {
            try
            {
                Model.Masakan masakan_model = _mapper.Map<Model.Masakan>(masakanDTO);
                await _masakanService.CreateMasakanAsync(masakan_model);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new BadRequestResult();
            }

        }

        /// <summary>
        /// Get all Masakan
        /// </summary>
        /// <response code="200">Request ok.</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<MasakanWithChefDTO>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> GetAllAsync()
        {
            List<Model.Masakan> result = await _masakanService.GetAllMasakanAsync();
            List<MasakanWithChefDTO> mappedResult = _mapper.Map<List<MasakanWithChefDTO>>(result);
            return new OkObjectResult(mappedResult);
        }

        /// <summary>
        /// Get masakan by nama
        /// </summary>
        /// <param name="nama">user Model.</param>
        /// <response code="200">Request ok.</response>
        /// <response code="405">Request not found.</response>
        [HttpGet]
        [Route("{nama}")]
        [ProducesResponseType(typeof(MasakanWithChefDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> GetByNamaAsync([FromRoute] string nama)
        {
            Model.Masakan result = await _masakanService.GetMasakanByNamaAsync(nama);
            if (result != null)
            {
                MasakanWithChefDTO mappedResult = _mapper.Map<MasakanWithChefDTO>(result);
                return new OkObjectResult(mappedResult);
            }
            return new NotFoundResult();
        }

        /// <summary>
        /// Delete Masakan
        /// </summary>
        /// <param name="nama">Nama masakan</param>
        /// <response code="200">Request ok.</response>
        [HttpDelete]
        [Route("{nama}")]
        [ProducesResponseType(typeof(MasakanWithChefDTO), 200)]
        public async Task<ActionResult> DeleteAsync([FromRoute] string nama)
        {
            await _masakanService.DeleteMasakanAsync(nama);
            return new OkResult();
        }

    }
}
