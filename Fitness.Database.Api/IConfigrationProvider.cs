/*---------------------------------------------------------------------------
 * IConfigrationProvider.cs
 *---------------------------------------------------------------------------
 * Copyright (C) MorrData LLS 2020. All rights reserved.
 *---------------------------------------------------------------------------
 */

namespace Fitness.Database.Api
{
    public interface IConfigrationProvider
    {
        string GetValue(string key);
    }
}
