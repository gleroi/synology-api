using System;
using System.Threading.Tasks;
namespace Synology.Api.Http
{
    public interface IHttpGateway
    {
        /// <summary>
        /// Call an api method <paramref name="apiPath"/> using parameters 
        /// defined in <paramref name="query"/>
        /// </summary>
        /// <param name="apiPath">path.cgi?api&method&version</param>
        /// <param name="query">expected parameters for method</param>
        /// <returns></returns>
        Task<string> Get(string apiPath, string query);

        /// <summary>
        /// Call an api method <paramref name="apiPath"/> using parameters 
        /// defined in <paramref name="query"/>
        /// </summary>
        /// <param name="apiPath">path.cgi?api&method&version</param>
        /// <param name="query">expected parameters for method</param>
        /// <param name="filepath">"file" parameter for the method</param>
        /// <returns></returns>
        Task<string> PostFile(string apiPath, string query, string filepath);
    }
}
