namespace App.Test.Console
{
    using App.Common.Event;
    using App.Common.Helpers;
    using App.Order.Event;
    using System;
    using System.Collections.Generic;
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IEnumerable<Type> types = AssemblyHelper.GetTypes<IEventHandler<OnCustomerDetailChanged>>("App.EventHandler.Impl.dll");
                //App.Common.Helpers.AssemblyHelper.ExecuteTasks<IEventHandler<OnCustomerDetailChanged>, OnCustomerDetailChanged>(eventType);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            finally {
                System.Console.Read();
            }
        }
    }
}
