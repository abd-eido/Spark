using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spark.Models;
using Spark.Repository;


namespace Spark.Controllers
{
    public class KeywordController : Controller
    {
        private readonly IKeywordRepository _keywordRepository = null;

        public KeywordController(IKeywordRepository keywordRepository)
        {
            _keywordRepository = keywordRepository;
        }

        [Route("all-keywords")]
        public async Task<ViewResult> GetAllKeywords()
        {
            var data = await _keywordRepository.GetAllKeywords();

            return View(data);
        }

        [Authorize]
        public async Task<ViewResult> AddNewKeyword(bool isSuccess = false, int keywordId = 0)
        {
            var model = new KeywordModel();
            ViewBag.IsSuccess = isSuccess;
            ViewBag.KeywordId = keywordId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewKeyword(KeywordModel keywordModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _keywordRepository.AddNewKeyword(keywordModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewKeyword), new { isSuccess = true, keywordId = id });
                }
            }

            return View();
        }
        
    }
}
