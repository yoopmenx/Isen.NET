using Isen.DotNet.Library.Models;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Isen.DotNet.Web.Controllers
{
    public class JoueurController : BaseController<Joueur, IJoueurRepository>
    {
        public JoueurController(IJoueurRepository repository) : base(repository)
        {
        }
    }
}