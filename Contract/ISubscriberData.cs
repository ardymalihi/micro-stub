﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract
{
    public interface ISubscriberData
    {
        List<Subscriber> GetSubscribers();
    }
}