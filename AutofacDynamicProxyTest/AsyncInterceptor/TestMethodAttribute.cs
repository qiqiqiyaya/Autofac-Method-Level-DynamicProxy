using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDynamicProxyTest.AsyncInterceptor
{
    public class TestMethodAttribute
    {
        [UnitOfWorkAttribute]
        public async Task Tets()
        {
            await Task.Delay(3000);
        }
    }
}
