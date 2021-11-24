using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RumahMakanPadang.dal.Models;
using RumahMakanPadang.dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = RumahMakanPadang.dal.Models;

namespace RumahMakanPadang.bll
{
    public class MasakanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        //private readonly IMessageSenderFactory _msgSernderFactory;

        public MasakanService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            //_msgSernderFactory = msgSernderFactory;
        }

        public async Task<List<Masakan>> GetAllMasakanAsync()
        {
            return await _unitOfWork.MasakanRepository.GetAll().ToListAsync();
        }

        public async Task<Masakan> GetMasakanByNamaAsync(string nama)
        {
            return await _unitOfWork.MasakanRepository
                .GetAll()
                .Include(b => b.Chef)
                .FirstOrDefaultAsync(b => b.Nama.ToLower() == nama.ToLower());
        }

        public async Task CreateMasakanAsync(Masakan masakan)
        {
            bool isExist = _unitOfWork.MasakanRepository.GetAll().Where(x => x.Nama.ToLower() == masakan.Nama.ToLower()).Any();
            bool isChefExist = _unitOfWork.ChefRepository.GetAll().Where(x => x.KTP == masakan.ChefKTP).Any();
            if (!isExist && isChefExist)
            {
                _unitOfWork.MasakanRepository.Add(masakan);
                await _unitOfWork.SaveAsync();

                //await SendMasakanToEventHub(masakan);
            }
            else
            {
                throw new Exception($"Masakan with {masakan.Nama} already exist");
            }
        }

        public async Task DeleteMasakanAsync(string nama)
        {
            _unitOfWork.MasakanRepository.Delete(x => x.Nama.ToLower() == nama.ToLower());
            await _unitOfWork.SaveAsync();
        }
    }
}
