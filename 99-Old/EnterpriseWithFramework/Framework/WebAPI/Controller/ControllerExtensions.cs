﻿/*
  This file is part of CNCLib - A library for stepper motors.

  Copyright (c) Herbert Aitenbichler

  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
  and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

namespace Framework.WebAPI.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Logic.Abstraction;

    public static class ControllerExtensions
    {
        public static string GetCurrentUri(this Controller controller)
        {
            if (controller.Request == null)
            {
                // unit test => no Request available 
                return "dummy";
            }

            return $"{controller.Request.Scheme}://{controller.Request.Host}{controller.Request.Path}{controller.Request.QueryString}";
        }

        public static string GetCurrentUri(this Controller controller, string removeTrailing)
        {
            if (controller.Request == null)
            {
                // unit test => no Request available 
                return "dummy";
            }

            string totalUri = controller.GetCurrentUri();

            int filterIdx = totalUri.LastIndexOf('?');
            if (filterIdx > 0)
            {
                totalUri = totalUri.Substring(0, filterIdx - 1);
            }

            return totalUri.Substring(0, totalUri.Length - removeTrailing.Length);
        }

        public static async Task<ActionResult<T>> NotFoundOrOk<T>(this Controller controller, T obj)
        {
            if (obj == null)
            {
                await Task.CompletedTask; // avoid CS1998
                return controller.NotFound();
            }

            return controller.Ok(obj);
        }

        #region Get/GetAll

        public static async Task<ActionResult<T>> Get<T, TKey>(this Controller controller, IGetManager<T, TKey> manager, TKey id) where T : class where TKey : IComparable
        {
            var dto = await manager.Get(id);
            if (dto == null)
            {
                return controller.NotFound();
            }

            return controller.Ok(dto);
        }

        public static async Task<ActionResult<IEnumerable<T>>> GetAll<T, TKey>(this Controller controller, IGetManager<T, TKey> manager) where T : class where TKey : IComparable
        {
            var dtos = await manager.GetAll();
            if (dtos == null)
            {
                return controller.NotFound();
            }

            return controller.Ok(dtos);
        }

        #endregion

        #region Add

        public static async Task<ActionResult<T>> Add<T, TKey>(this Controller controller, ICRUDManager<T, TKey> manager, T value) where T : class where TKey : IComparable
        {
            TKey   newId  = await manager.Add(value);
            string newUri = controller.GetCurrentUri() + "/" + newId;
            return controller.Created(newUri, await manager.Get(newId));
        }

        public static async Task<IEnumerable<UriAndValue<T>>> AddIntern<T, TKey>(this Controller controller, ICRUDManager<T, TKey> manager, IEnumerable<T> values)
            where T : class where TKey : IComparable
        {
            var newIds     = await manager.Add(values);
            var newObjects = await manager.Get(newIds);

            string uri     = controller.GetCurrentUri("/bulk");
            var    newUris = newIds.Select(id => uri + "/" + id);
            var    results = newIds.Select((id, idx) => new UriAndValue<T>() { Uri = uri + "/" + id, Value = newObjects.ElementAt(idx) });
            return results;
        }

        public static async Task<ActionResult<IEnumerable<UriAndValue<T>>>> Add<T, TKey>(this Controller controller, ICRUDManager<T, TKey> manager, IEnumerable<T> values)
            where T : class where TKey : IComparable
        {
            return controller.Ok(await AddIntern(controller, manager, values));
        }

        public static async Task<ActionResult<UrisAndValues<T>>> Add2<T, TKey>(this Controller controller, ICRUDManager<T, TKey> manager, IEnumerable<T> values)
            where T : class where TKey : IComparable
        {
            return controller.Ok((await AddIntern(controller, manager, values)).ToUrisAndValues());
        }

        public static async Task<ActionResult<T>> AddNoGet<T, TKey>(this Controller controller, ICRUDManager<T, TKey> manager, T value, Action<T, TKey> setIdFunc)
            where T : class where TKey : IComparable
        {
            TKey   newId  = await manager.Add(value);
            string newUri = controller.GetCurrentUri() + "/" + newId;
            setIdFunc(value, newId);
            return controller.Created(newUri, value);
        }

        public static async Task<IEnumerable<UriAndValue<T>>> AddNoGetIntern<T, TKey>(
            this Controller       controller,
            ICRUDManager<T, TKey> manager,
            IEnumerable<T>        values,
            Action<T, TKey>       setIdFunc)
            where T : class where TKey : IComparable
        {
            IEnumerable<TKey> newIds = await manager.Add(values);

            Func<T, TKey, T> mySetFunc = (v, k) =>
            {
                setIdFunc(v, k);
                return v;
            };

            string uri     = controller.GetCurrentUri("/bulk");
            var    newUris = newIds.Select(id => uri + "/" + id);
            var    results = newIds.Select((id, idx) => new UriAndValue<T>() { Uri = uri + "/" + id, Value = mySetFunc(values.ElementAt(idx), id) });
            return results;
        }

        public static async Task<ActionResult<IEnumerable<UriAndValue<T>>>> AddNoGet<T, TKey>(
            this Controller       controller,
            ICRUDManager<T, TKey> manager,
            IEnumerable<T>        values,
            Action<T, TKey>       setIdFunc)
            where T : class where TKey : IComparable
        {
            return controller.Ok(await AddNoGetIntern(controller, manager, values, setIdFunc));
        }

        public static async Task<ActionResult<UrisAndValues<T>>> Add2NoGet<T, TKey>(this Controller controller, ICRUDManager<T, TKey> manager, IEnumerable<T> values, Action<T, TKey> setIdFunc)
            where T : class where TKey : IComparable
        {
            return controller.Ok((await AddNoGetIntern(controller, manager, values, setIdFunc)).ToUrisAndValues());
        }

        #endregion

        #region Update

        public static async Task<ActionResult> Update<T, TKey>(this Controller controller, ICRUDManager<T, TKey> manager, TKey idFromUri, TKey idFromValue, T value)
            where T : class where TKey : IComparable
        {
            if (idFromUri.CompareTo(idFromValue) != 0)
            {
                return controller.BadRequest("Mismatch between id and dto.Id");
            }

            await manager.Update(value);
            return controller.NoContent();
        }

        public static async Task<ActionResult> Update<T, TKey>(this Controller controller, ICRUDManager<T, TKey> manager, IEnumerable<T> values) where T : class where TKey : IComparable
        {
            await manager.Update(values);
            return controller.NoContent();
        }

        #endregion

        #region Delete

        public static async Task<ActionResult> Delete<T, TKey>(this Controller controller, ICRUDManager<T, TKey> manager, TKey id) where T : class where TKey : IComparable
        {
            await manager.Delete(id);
            return controller.NoContent();
        }

        public static async Task<ActionResult> Delete<T, TKey>(this Controller controller, ICRUDManager<T, TKey> manager, IEnumerable<TKey> ids) where T : class where TKey : IComparable
        {
            await manager.Delete(ids);
            return controller.NoContent();
        }

        #endregion
    }
}