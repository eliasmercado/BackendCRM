using CRM.DTOs.Producto;
using CRM.Helpers;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Producto
{
    public class ProductoService
    {
        private CrmDbContext _context;

        public ProductoService(CrmDbContext context) {
            _context = context;
        }

        public List<ProductoDTO> ObtenerListaProdcutos()
        {
            List<ProductoDTO> listaProductos = (from producto in _context.Productos
                                       
                                          select new ProductoDTO()
                                          {
                                              IdProducto = producto.IdProducto,
                                              Descripcion = producto.Descripcion,
                                              Precio  = producto.Precio,
                                              IdMarca = producto.IdMarca,
                                              IdCategoria = producto.IdCategoria,
                                              IdMoneda = producto.IdMoneda
                                          }).ToList();

            return listaProductos;
        }

        public ProductoDTO ObtenerPRoductoById(int id)
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

        public string ModificarProducto(int id, ProductoDTO prdModificado)
        {
            if (id != prdModificado.IdProducto)
            {
                throw new ApiException("Identificador de Producto no válido");
            }

            Models.Producto producto = _context.Productos.Find(id);

            if (producto == null)
                throw new ApiException("El producto no existe.");

            producto.IdProducto = prdModificado.IdProducto;
            producto.Descripcion = prdModificado.Descripcion;
            producto.Precio = prdModificado.Precio;
            producto.IdMarca = prdModificado.IdMarca;
            producto.IdCategoria = prdModificado.IdCategoria;
            producto.IdMoneda = prdModificado.IdMoneda;

            _context.SaveChanges();

            return "El Producto modificó correctamente.";
        }

        public string CrearProducto(ProductoDTO prodNuevo)
        {
            Models.Producto producto = new()
            {
                IdProducto = prodNuevo.IdProducto,
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

  

    }
}
