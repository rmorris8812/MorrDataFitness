// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using Fitness.Api.Dtos;

namespace Fitness.Api.Rest
{
    public class DashboardResource : IRestResource
    {
        private DashboardDto _dto;
        public object Data
        {
            get { return _dto; }
            set { _dto = (DashboardDto)value; }
        }
    }
}
