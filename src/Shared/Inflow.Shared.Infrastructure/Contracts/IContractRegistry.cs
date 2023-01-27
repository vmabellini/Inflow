﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Contracts
{
    public interface IContractRegistry
    {
        IContractRegistry Register<T>() where T : class;

        IContractRegistry RegisterPath(string path);

        IContractRegistry RegisterPath<TRequest, TResponse>(string path)
            where TRequest : class where TResponse : class;

        IContractRegistry RegisterPathWithRequest<TRequest>(string path)
            where TRequest : class;

        IContractRegistry RegisterPathWithResponse<TResponse>(string path)
            where TResponse : class;

        void Validate(IEnumerable<Assembly> assemblies);
    }
}
