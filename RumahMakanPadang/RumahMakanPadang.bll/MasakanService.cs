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
{using Model = RumahMakanPadang.dal.Models;
    public class MasakanService
    {
        private readonly List<Masakan> _masakans;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        //private readonly IMessageSenderFactory _msgSernderFactory;

        public MasakanService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            //_msgSernderFactory = msgSernderFactory;
            //_masakans = new List<Masakan>();
        }

        //public List<Masakan> GetAllMasakan()
        //{
        //    _masakans.ForEach(Console.WriteLine);
        //    return _masakans.ToList();
        //}

        public async Task<List<Masakan>> GetAllMasakanAsync()
        {
            return await _unitOfWork.MasakanRepository.GetAll().ToListAsync();
        }

        //public Masakan GetMasakanByNama(string nama)
        //{
        //    return _masakans.FirstOrDefault(b => b.Nama.ToLower() == nama.ToLower());
        //}

        public async Task<Masakan> GetMasakanByNamaAsync(string nama)
        {
            return await _unitOfWork.MasakanRepository
                .GetAll()
                //.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Nama.ToLower() == nama.ToLower());
        }

        //public void CreateMasakan(Masakan masakan)
        //{
        //    //Console.Write("Kentang");
        //    bool isExist = _masakans.Where(x => x.Nama.ToLower() == masakan.Nama.ToLower()).Any();
        //    if (!isExist)
        //    {
        //        _masakans.Add(masakan);
        //        Console.WriteLine(_masakans[0].Nama);
        //    }
        //    else
        //    {
        //        throw new Exception($"Masakan with {masakan.Nama} already exist");
        //    }
        //}

        public async Task CreateMasakanAsync(Masakan masakan)
        {
            bool isExist = _unitOfWork.MasakanRepository.GetAll().Where(x => x.Nama.ToLower() == masakan.Nama.ToLower()).Any();
            //bool isAuthorExist = _unitOfWork.AuthorRepository.GetAll().Where(x => x.Id == masakan.AuthorId).Any();
            if (!isExist)// && isAuthorExist)
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
            //Masakan masakan = GetMasakanByNama(nama);
            //_masakans.Remove(masakan);
            _unitOfWork.MasakanRepository.Delete(x => x.Nama.ToLower() == nama.ToLower());
            await _unitOfWork.SaveAsync();
        }
    }
}
