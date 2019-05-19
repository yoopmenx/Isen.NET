using System;

namespace Isen.DotNet.Library
{
    /// <summary>
    /// Classe de test
    /// </summary>
    public class Hello
    {
        /// <summary>
        /// Nom de la joueurne (accesseur implicite - auto-property)
        /// </summary>
        /// <value></value>
        public string Name { get; set; } = "World";

        /// <summary>
        /// L'heure du point de vue du runtime de cette classe
        /// </summary>
        public DateTime ServerTime => DateTime.Now; // syntaxe 'expression body'
 
        /// <summary>
        /// Constructeur avec param
        /// </summary>
        /// <param name="name">Nom de la joueurne</param>
        public Hello(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Saluer le client de la classe
        /// </summary>
        /// <returns></returns>
        public string Greet()
        {
            var serverTimeFormatted = 
                ServerTime.ToShortTimeString();
            var message = $"Hello, {Name} [{serverTimeFormatted}]"; 
            return message;
        }
    }
}