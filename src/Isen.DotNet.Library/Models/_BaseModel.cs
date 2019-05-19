using System;
using System.Dynamic;

namespace Isen.DotNet.Library.Models
{
    public abstract class BaseModel<T>
        where T : BaseModel<T>
    {
        public virtual int Id { get;set; }
        public virtual string Name { get;set; }

        public virtual bool IsNew => Id <= 0;

        public virtual string Display => 
            $"[{this.GetType()}] Id={Id}|Name={Name}";

        public override string ToString() 
            => Display;

        public virtual void Map(T copy)
        {
            Name = copy.Name;
        }

        public virtual dynamic ToDynamic()
        {
            dynamic response = new ExpandoObject();
            response.id = Id;
            response.name = Name;
            response.fetch = DateTime.Now;
            return response;
        }
    }
}