// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************

namespace Fitness.Database.Api
{
    public interface IConfigrationProvider
    {
        string GetValue(string key);
    }
}
