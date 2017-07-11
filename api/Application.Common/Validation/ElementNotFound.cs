namespace App.Common.Validation
{
    using System;

    public class ElementNotFound : Exception
    {
        public string[] Params { get; set; }
        public ElementNotFound(params string[] args)
        {
            this.Params = args;
        }
    }
}