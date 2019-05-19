using System;
using System.Dynamic;

namespace Isen.DotNet.Library.Models
{
    public class Historique : BaseModel<Historique>
      
    {
        public int Date_debut { get;set; }
        public int Date_fin { get;set; }
        public override int Id { get;set; }

        public override bool IsNew => Id <= 0;

        public override string Display => 
            $"[{this.GetType()}] Id={Id}|Name={Name}";

        public override string ToString() 
            => Display;

        

        public override dynamic ToDynamic()
        {
            dynamic response = new ExpandoObject();
            response.id = Id;
            response.fetch = DateTime.Now;
            return response;
        }
    }
}