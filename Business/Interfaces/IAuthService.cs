using Core.Entities.UserClaimModels;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Domain.EntitiesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDTO userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDTO userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
