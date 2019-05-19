using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Isen.DotNet.Library.Models
{
    public class Club : BaseModel<Club>
    {
        public override int Id { get;set; }
        public override string Name { get;set; }
        public string ZipCode { get;set; }
        public string Logo { get;set; }
        // Relation réciproque de
        // Joueur.BornIn (Club)
        public float Latitude { get;set; }
        public float Longitude { get;set; }
        public List<Joueur> JoueurCollection { get; set; } =
            new List<Joueur>();

        public List<Historique> HistoriqueCollection { get; set; } =
            new List<Historique>();

        [NotMapped]
        public override string Display => 
            $"{base.Display}|ZipCode={ZipCode}";

        public override void Map(Club copy)
        {
            base.Map(copy);
            ZipCode = copy.ZipCode;
        }

        public override dynamic ToDynamic()
        {
            var baseDynamic = base.ToDynamic();
            baseDynamic.nb = JoueurCollection?.Count;
            return baseDynamic;
        }
    }
}