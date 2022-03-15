﻿using CRM.DTOs.Producto;
using CRM.Helpers;
using CRM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.ProductoService
{
    public class ProductoService
    {
        private CrmDbContext _context;

        public ProductoService(CrmDbContext context)
        {
            _context = context;
        }

        public List<ProductoDTO> ObtenerListaProductos()
        {
            List<ProductoDTO> listaProductos = (from producto in _context.Productos
                                                select new ProductoDTO()
                                                {
                                                    IdProducto = producto.IdProducto,
                                                    Descripcion = producto.Descripcion,
                                                    Precio = producto.Precio,
                                                    IdMarca = producto.IdMarca,
                                                    IdCategoria = producto.IdCategoria,
                                                    IdMoneda = producto.IdMoneda
                                                }).ToList();

            return listaProductos;
        }

        public ProductoDTO ObtenerProductoById(int id)
        {
            ProductoDTO productoDto = (from producto in _context.Productos
                                       where producto.IdProducto == id
                                       select new ProductoDTO()
                                       {
                                           IdProducto = producto.IdProducto,
                                           Descripcion = producto.Descripcion,
                                           Precio = producto.Precio,
                                           IdMarca = producto.IdMarca,
                                           IdCategoria = producto.IdCategoria,
                                           IdMoneda = producto.IdMoneda
                                       }).FirstOrDefault();

            return productoDto;
        }

        public List<MonedaDTO> ObtenerListaMonedas()
        {
            List<MonedaDTO> listaMonedas = (from moneda in _context.Moneda
                                                select new MonedaDTO()
                                                {
                                                    IdMoneda= moneda.IdMoneda,
                                                    Moneda = moneda.Descripcion,
                                                    Codigo = moneda.Codigo,
                                                }).ToList();

            return listaMonedas;
        }

        public string ModificarProducto(int id, ProductoDTO prdModificado)
        {
            if (id != prdModificado.IdProducto)
            {
                throw new ApiException("Identificador de Producto no válido");
            }

            Producto producto = _context.Productos.Find(id);

            if (producto == null)
                throw new ApiException("El producto no existe.");

            producto.Descripcion = prdModificado.Descripcion;
            producto.Precio = prdModificado.Precio;
            producto.IdMarca = prdModificado.IdMarca;
            producto.IdCategoria = prdModificado.IdCategoria;
            producto.IdMoneda = prdModificado.IdMoneda;

            _context.SaveChanges();

            return "El producto se modificó correctamente.";
        }

        public string CrearProducto(ProductoDTO prodNuevo)
        {
            Producto producto = new()
            {
                Descripcion = prodNuevo.Descripcion,
                Precio = prodNuevo.Precio,
                IdMarca = prodNuevo.IdMarca,
                IdCategoria = prodNuevo.IdCategoria,
                IdMoneda = prodNuevo.IdMoneda
            };
            _context.Productos.Add(producto);
            _context.SaveChanges();

            return "El producto se agregó correctamente.";
        }

        public string EliminarProducto(int id)
        {
            Producto producto = _context.Productos.Find(id);

            if (producto == null)
                throw new ApiException("El producto no existe.");

            _context.Remove(producto);
            _context.SaveChanges();

            return "El producto se eliminó correctamente.";
        }
    }
}
