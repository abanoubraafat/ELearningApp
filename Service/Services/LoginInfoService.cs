//using ELearning_App.Domain.Entities;
//using ELearning_App.Repository.IRepositories;
//using ELearning_App.Repository.Repositories;
//using ELearning_App.Service.IServices;
//using Microsoft.EntityFrameworkCore;

//namespace ELearning_App.Service.Services
//{
//    public class LoginInfoService : ILoginInfoService
//    {

//        private ILoginInfoRepository iLoginInfoRepository { get; }

//        public LoginInfoService(ILoginInfoRepository _iLoginInfoRepository)
//        {
//            iLoginInfoRepository = _iLoginInfoRepository;
//        }

//        public async Task<LoginInfo> AddLoginInfo(LoginInfo loginInfo)
//        {
//            return await iLoginInfoRepository.Add(loginInfo);
//        }

//        public async Task<LoginInfo> UpdateLoginInfo(LoginInfo c)
//        {
//            return await iLoginInfoRepository.Update(c);
//        }

//        public async Task<LoginInfo> DeleteLoginInfo(int id)
//        {
//            return await iLoginInfoRepository.Delete(id);
//        }

//        public async Task<IEnumerable<LoginInfo>> GetAllLoginInfo()
//        {
//            return await iLoginInfoRepository.GetAll().ToListAsync();
//        }

//        public async Task<LoginInfo> GetLoginInfoById(int id)
//        {
//            return await iLoginInfoRepository.GetById(id);
//        }
//        public async Task<LoginInfo> Login(string email, string password)
//        {
//            return await iLoginInfoRepository.GetAll()
//                .FirstOrDefaultAsync(u => u.Password == password && u.EmailAddress == email);
//        }

//        public async Task<LoginInfo> GetByIdWithToDoLists(int id)
//        {
//            return await iLoginInfoRepository.GetByIdWithToDoLists(id).Select(t => new LoginInfo
//            {
//                EmailAddress = t.EmailAddress,
//                Password = t.Password,
//                ToDoLists = t.ToDoLists
//            }).FirstAsync();
//        }
//    }
//}
        

