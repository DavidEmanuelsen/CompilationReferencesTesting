using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Runtime.Loader;

namespace CompilationReferencesTesting.Controllers
{
    public class SecondaryController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly RazorViewEngineOptions _razorViewEngineOptions;

        public SecondaryController(
            IOptions<RazorViewEngineOptions> razorViewEngineOptions, 
            IHostingEnvironment hostingEnvironment)
        {
            _razorViewEngineOptions = razorViewEngineOptions.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View(GetViewModel());
        }

        private dynamic GetViewModel()
        {
            Assembly modelProjectAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(_hostingEnvironment.ContentRootPath + @"/Assemblies/ModelProject.dll");
            _razorViewEngineOptions.AdditionalCompilationReferences.Add(MetadataReference.CreateFromFile(modelProjectAssembly.Location));
            
            return modelProjectAssembly.CreateInstance("ModelProject.ViewModel");
        }
    }
}
