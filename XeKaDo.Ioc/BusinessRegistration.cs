﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Abstraction.BusinessLogic;
using XeKaDo.BusinessLogic;

namespace XeKaDo.Ioc
{
    public static class BusinessRegistration
    {
        public static IServiceCollection BusinessRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ICategoriaEventoService, CategoriaEventoService>();

            return services;
        }
    }
}
