using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace СпортКлик.Сервис
{
    public class PageableData<T> where T : class
    {
        protected static int ItemPerPageDefault = 20;

        public IEnumerable<T> Список { get; set; }

        public int PageNo { get; set; }

        public int CountPage { get; set; }

        public int ItemPerPage { get; set; }

        public IEnumerable<List<T>> Группа { get; private set; }

        public PageableData(IQueryable<T> queryableSet, int page, int itemPerPage = 0)
        {
            if (itemPerPage == 0)
            {
                itemPerPage = ItemPerPageDefault;
            }
            ItemPerPage = itemPerPage;

            PageNo = page;
            var count = queryableSet.Count();

            CountPage = (int)decimal.Remainder(count, itemPerPage) == 0 ? count / itemPerPage : count / itemPerPage + 1;
            Список = queryableSet.Skip((PageNo - 1) * itemPerPage).Take(itemPerPage);

            int j = 0;
            //Групируем по колонкам
            Группа = Список.GroupBy(i => { return j++ / Константы.КоличествоКартинокВСтроке; }).Select(i => i.ToList()); 
        }
    }
}