﻿using Microsoft.Extensions.DependencyInjection;
using System;
using WorldStatistics.Services;

namespace WorldStatistics.Configuration
{
    public static class HttpClientConfig
    {
        public static void ConfigureHttpClient(this IServiceCollection services, ApplicationSettings appSettings)
        {
            //TO DO: need to think of how to handle failures and error responces 'AddTransientHttpErrorPolicy'
            services.AddHttpClient<IApiClientService, ApiClientService>(c =>
            {
                c.BaseAddress = new Uri(appSettings.WorldBankApiBaseUrl);
            });

        }
    }
}
