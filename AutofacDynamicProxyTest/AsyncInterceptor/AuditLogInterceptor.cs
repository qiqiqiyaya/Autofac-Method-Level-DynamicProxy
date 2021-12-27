using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDynamicProxyTest.AsyncInterceptor
{
    public class AuditLogInterceptor : AbpInterceptorBase
    {
        public override void InterceptSynchronous(IInvocation invocation)
        {
            Console.WriteLine("同步方法日志");
            invocation.Proceed();
        }

        protected override async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            Console.WriteLine("异步方法 无返回值");
            var proceedInfo = invocation.CaptureProceedInfo();
            var method = GetMethodInfo(invocation);

            proceedInfo.Invoke();
            var task = (Task)invocation.ReturnValue;
            await task;
        }

        protected override async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            Console.WriteLine("异步方法 有返回值");
            var proceedInfo = invocation.CaptureProceedInfo();
            var method = GetMethodInfo(invocation);

            proceedInfo.Invoke();
            var taskResult = (Task<TResult>)invocation.ReturnValue;
            return await taskResult;
        }

        private static MethodInfo GetMethodInfo(IInvocation invocation)
        {
            MethodInfo method;
            try
            {
                method = invocation.MethodInvocationTarget;
            }
            catch
            {
                method = invocation.GetConcreteMethod();
            }

            return method;
        }
    }
}
