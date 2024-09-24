using Application.Constans;
using Application.Interfaces;
using Core.Entities.UserClaimModels;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Domain.EntitiesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private ITokenHelper _tokenHelper;
        private IUserService _userService;

        public AuthService(IUserService userService, ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }

        public IDataResult<User> Register(UserForRegisterDTO userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDTO userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                ErrorDataResult<User> error = new ErrorDataResult<User>(Messages.UserNotFound);
                var jsonError = JsonSerializer.Serialize(error);
                return new ErrorDataResult<User>(jsonError);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                ErrorDataResult<User> error = new ErrorDataResult<User>(Messages.PasswordError);
                var jsonError = JsonSerializer.Serialize(error);
                return new ErrorDataResult<User>(jsonError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                
                ErrorResult error = new ErrorResult(Messages.UserAlreadyExists);
                var jsonError = JsonSerializer.Serialize(error);
                return new ErrorResult(jsonError);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
