// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using Fitness.Api.Dtos;
using System.Collections.Generic;

namespace Fitness.Api.Rest
{
    public class UserFoodArrayResource : IRestResource
    {
        private List<UserFoodDto> _dto;
        public object Data
        {
            get { return _dto; }
            set { _dto = (List<UserFoodDto>)value; }
        }
    }
}
