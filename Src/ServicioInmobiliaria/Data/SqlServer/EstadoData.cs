﻿using Inmobiliaria.Entities;
using Inmobiliaria.Entities.Interfaces;
using Inmobilliaria.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria.Data.SqlServer
{
    public class EstadoData : IDatosRead<Estado>
    {
        private readonly InmobiliariaContext _context;

        public EstadoData(DbConfig conn)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseSqlServer(conn.ConnectionString);
            _context =  new InmobiliariaContext(optionsBuilder.Options);
        }
       
        public List<Estado> Obtener(Expression<Func<Estado, bool>>? filtro = null)
        {
            IQueryable<Estado> ofertas = _context.Estado;
            if (filtro != null)
                ofertas=  ofertas.Where(filtro);
            return ofertas.ToList();
        }

        public Estado? Obtener(int id)
        {
            return _context?.Estado?.FirstOrDefault(x=>x.Id == id);
        }
    }
}
