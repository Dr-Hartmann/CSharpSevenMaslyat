//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using System.Linq;

//namespace MVPv4.Controllers
//{
//    /// <summary>
//    /// Системный контроллер для служебных endpoint-ов.
//    /// </summary>
//    [ApiController]
//    [Route("[controller]/[action]")]
//    public class SystemController : ControllerBase
//    {
//        private readonly IApiDescriptionGroupCollectionProvider _apiProvider;

//        public SystemController(IApiDescriptionGroupCollectionProvider apiProvider)
//        {
//            _apiProvider = apiProvider;
//        }

//        /// <summary>
//        /// Возвращает список всех доступных API (маршрутов, методов и описаний).
//        /// Только для пользователей с ролью Admin.
//        /// </summary>
//        [HttpGet]
//        [Authorize(Roles = "Admin")] // Доступ только для администраторов
//        public IActionResult ApiList()
//        {
//            // Получаем все описания API из провайдера
//            var apis = _apiProvider.ApiDescriptionGroups.Items
//                .SelectMany(group => group.Items)
//                .Select(desc => new
//                {
//                    Method = desc.HttpMethod,
//                    Route = "/" + desc.RelativePath,
//                    desc.ActionDescriptor.DisplayName,
//                    Parameters = desc.ParameterDescriptions.Select(p => new 
//                    {
//                        ParameterName = p.Name,
//                        ParameterType = p.Type.Name
//                    })
//                })
//                .ToList();

//            return Ok(apis);
//        }
//    }
//} 