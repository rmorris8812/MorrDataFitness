// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using Fitness.Api.Dtos;

namespace Fitness.Api.Rest
{
    public class GoalResource : IRestResource
    {
        private GoalDto _dto;
        public object Data
        {
            get { return _dto; }
            set { _dto = (GoalDto)value; }
        }
    }
}
