﻿using BookStore.Catalog.ReadModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Catalog.Persistence.ReadModels
{
    public class ReadModelProductDao : IReadModelProductDao
    {
        private readonly IReadModelContext _context;

        public ReadModelProductDao(IReadModelContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public async Task<IEnumerable<IReadModelProduct>> FindAsync()
        {
            var query = from x in _context.Products
                        select x;
            var products = await query.ToArrayAsync();

            return products;
        }
    }
}