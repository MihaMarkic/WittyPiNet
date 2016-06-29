using System;
using NUnit.Framework;

namespace Righthand.WittyPi.Test
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class PiAttribute: CategoryAttribute
    {
    }
}
