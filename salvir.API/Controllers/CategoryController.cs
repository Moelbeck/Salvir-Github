﻿using deprosa.ViewModel;
using deprosa.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using deprosa.WebApi.Services;
using deprosa.Common;
using deprosa.service;

namespace WebService.Api.Controllers
{

    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private readonly CategoryWebService _categoryService;
        private readonly ProductTypeWebService _productService;
        private readonly LogService _log;
        public CategoryController()
        {
            _categoryService = new CategoryWebService();
            _productService = new ProductTypeWebService();
            _log = new LogService();
        }

        /// <summary>
        /// Gets all categories - Get
        /// </summary>
        [HttpGet, Route("allmain")]
        public IHttpActionResult GetAllMainCategories()
        {

            var categories = _categoryService.GetMainCategories();
            if (categories.Any())
            {
                return Ok(categories);
            }
            return NotFound();
        }

        /// <summary>
        /// Gets main category by id - Get
        /// </summary>
        [HttpGet]
        public IHttpActionResult GetMainCategory(int id)
        {
            var category = _categoryService.GetMainCategory(id);
            if (category != null)
            {
                int userid;
                if (int.TryParse(Thread.CurrentPrincipal.Identity.Name, out userid) && userid > 0)
                {
                    _log.LogCategory(userid,id,0);
                }
                return Ok(category);
            }
            return NotFound();
        }

        /// <summary>
        /// Gets sub categories for main id - Get
        /// </summary>
        [HttpGet, Route("{mainid}/sub")]
        public IHttpActionResult GetSubCategoriesForMain(int mainid)
        {
            var sub = _categoryService.GetSubCategoriesForMain(mainid);
            if (sub != null)
            {
                return Ok(sub);
            }
            return NotFound();
        }

        [HttpGet, Route("categorystructure")]
        public IHttpActionResult GetCategoryStructure()
        {
            try
            {
                var allproducttypes = _productService.GetAllProductTypes();
                var categorystructure = new CategoryStructureRequest();
                foreach (var type in allproducttypes)
                {
                    if(!categorystructure.MainCategories.Any(e=>e.ID == type.SubCategory.MainCategory.ID))
                    {
                        categorystructure.MainCategories.Add(type.SubCategory.MainCategory);
                    }
                    if(!categorystructure.SubCategories.Any(e => e.ID == type.SubCategory.ID))
                    {
                        categorystructure.SubCategories.Add(type.SubCategory);
                    }
                }
                categorystructure.SubCategories = categorystructure.SubCategories.Distinct().ToList();
                categorystructure.MainCategories = categorystructure.MainCategories.Distinct().ToList();
                categorystructure.ProductTypes = allproducttypes;
                return Ok(categorystructure);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        #region ProductTypes
        /// <summary>
        /// Get product types for sub category - Get
        /// </summary>
        [HttpGet, Route("sub/{subcategory}/producttypes")]
        public IHttpActionResult GetProductTypesForSubCategory(int subcategory)
        {
            var producttypes = _productService.GetProductTypesForSubCategory(subcategory);
            if (producttypes != null)
            {
                return Ok(producttypes);
            }
            return NotFound();
        }

        /// <summary>
        /// Get product type by id - Get
        /// </summary>
        [HttpGet, Route("sub/{id}/producttype")]
        public IHttpActionResult GetProductType(int id)
        {
            var producttypes = _productService.GetProdyctType(id);
            if (producttypes != null)
            {
                return Ok(producttypes);
            }
            return NotFound();
        }
        #endregion
    }
}
