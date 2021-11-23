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
    public class ChefService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        //private readonly IMessageSenderFactory _msgSernderFactory;

        public ChefService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            //_msgSernderFactory = msgSernderFactory;
            //_masakans = new List<Masakan>();
        }

        public async Task<List<Chef>> GetAllChefAsync()
        {
            return await _unitOfWork.ChefRepository.GetAll().ToListAsync();
        }

        public async Task<Chef> GetChefByNamaAsync(string nama)
        {
            return await _unitOfWork.ChefRepository
                .GetAll()
                //.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Nama.ToLower() == nama.ToLower());
        }

        public async Task<Chef> GetChefByKTPAsync(string ktp)
        {
            return await _unitOfWork.ChefRepository
                .GetAll()
                //.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.KTP == ktp);
        }

        public async Task CreateChefAsync(Chef chef)
        {
            bool isExist = _unitOfWork.ChefRepository.GetAll().Where(x => x.KTP == chef.KTP).Any();
            //bool isAuthorExist = _unitOfWork.AuthorRepository.GetAll().Where(x => x.Id == masakan.AuthorId).Any();
            if (!isExist)// && isAuthorExist)
            {
                _unitOfWork.ChefRepository.Add(chef);
                await _unitOfWork.SaveAsync();

                //await SendMasakanToEventHub(masakan);
            }
            else
            {
                throw new Exception($"Chef with {chef.Nama} already exist");
            }
        }

        public async Task DeleteChefAsync(string nama)
        {
            _unitOfWork.ChefRepository.Delete(x => x.Nama.ToLower() == nama.ToLower());
            await _unitOfWork.SaveAsync();
        }
    }
}
