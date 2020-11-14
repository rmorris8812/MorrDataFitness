using Fitness.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Api.Docker.Rest
{
    public class UserResource : IRestResource
    {
        private UserDto _dto;
        public object Data
        {
            get { return _dto; }
            set { _dto = (UserDto)value; }
        }
    }
}

