﻿namespace Spark.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}