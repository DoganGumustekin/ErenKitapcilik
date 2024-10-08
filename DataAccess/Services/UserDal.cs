﻿using Core.Entities.UserClaimModels;
using Core.EntityRepositoryBase.EfRepository;
using Infrastructure.DBContext;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserDal : EfEntityRepositoryBase<User, DBConnection>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new DBConnection())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
    }
}
