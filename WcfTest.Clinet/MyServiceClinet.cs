﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfTest.Contracts;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet
{
    public class MyServiceClinet : ClientBase<IMyService>, IMyService
    {
        public Task<int> GetAgeAsync()
        {
            return Channel.GetAgeAsync();
        }
    }
}