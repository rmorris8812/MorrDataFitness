// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************

namespace Fitness.Api.Docker.Rest
{
    public class StringResource : IRestResource
    {
        private string _data;
        public object Data { get => _data; set { _data = (string) value; } }
    }
    public class LongResource : IRestResource
    {
        private long _data;
        public object Data { get => _data; set { _data = (long)value; } }
    }
    public class BoolResource : IRestResource
    {
        private bool _data;
        public object Data { get => _data; set { _data = (bool)value; } }
    }
}
