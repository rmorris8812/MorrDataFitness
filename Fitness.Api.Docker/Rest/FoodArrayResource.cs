// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************

using Fitness.Api.Dtos;
using System.Collections.Generic;

namespace Fitness.Api.Rest
{
    public class FoodArrayResource : IRestResource
    {
        private List<FoodDto> _dto;
        public object Data
        {
            get { return _dto; }
            set { _dto = (List<FoodDto>)value; }
        }
    }
}
