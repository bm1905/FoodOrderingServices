﻿namespace Catalog.API.Application.Configurations
{
    public class RedisCacheSettings
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
    }
}
