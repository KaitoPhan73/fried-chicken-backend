using AutoMapper;
using FriedChickenStore.Model.DTOs;
using FriedChickenStore.Model.Entity;
using FriedChickenStore.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FriedChickenStore.Service
{
    public class UserService 
    {
        private readonly IConfiguration _config;

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }
        public IEnumerable<UserDto> ListAll()
        {
            IEnumerable<User> users = _userRepository.GetAll();
            IEnumerable<UserDto> userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }

        public UserDto ListById(int id)
        {
            User user = _userRepository.GetById(id);
            if (user != null)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }
            return null;
        }
        public ResponseLoginDto Add(UserDto dto)
        {
            bool existUserName = _userRepository.IsUserNameExists(dto.UserName);
            bool existEmail = _userRepository.IsEmailExists(dto.Email);

            if (!existUserName && !existEmail)
            {
                User user = _mapper.Map<User>(dto);
                _userRepository.Add(user);

                // Tạo token và trả về ResponseLoginDto
                var tokenDto = GenerateToken(user);
                return tokenDto;
            }

            return null; 
        }



        public bool Update(int id, UserDto newDto)
        {

            DateTime nowDate = DateTime.Now;
            User oldEntity = _userRepository.GetById(id);

            if (oldEntity != null)
            {
                User newEntity = _mapper.Map(newDto, oldEntity);
                newEntity.LastUpdateTimes = nowDate;
                _userRepository.Update(oldEntity);
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            var exist = _userRepository.GetById(id);

            if (exist != null)
            {
                _userRepository.DeleteById(id);
                return true;
            }

            return false;
        }


        public bool UpdateAny(int id, UserDto newDto)
        {
            DateTime nowDate = DateTime.Now;
            User oldEntity = _userRepository.GetById(id); 
            oldEntity = _mapper.Map(newDto, oldEntity);
            if (oldEntity != null)
            {
                oldEntity.LastUpdateTimes = nowDate;
                _userRepository.Update(oldEntity);
                return true;
            }
            return false;
        }

        public ResponseLoginDto Login(LoginDto loginDto)
        {
            User user = _userRepository.GetUserByUserNameAndPassword(loginDto.UserName, loginDto.Password);
            if (user != null)
            {
                var tokenDto = GenerateToken(user);
                return tokenDto;
            }

            return null;
        }


        //Tạo token
        private ResponseLoginDto GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
        new Claim("userName", user.UserName),
        new Claim("role",  user.Role) 
    };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var responseLoginDto = new ResponseLoginDto
            {
                Token = tokenString,
                Username = user.UserName,
                Role = user.Role // Đặt vai trò cho người dùng mới đăng ký
            };

            return responseLoginDto;
        }

    }
}
