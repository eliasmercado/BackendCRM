using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CRM.Models
{
    public partial class CrmDbContext : DbContext
    {
        public CrmDbContext()
        {
        }

        public CrmDbContext(DbContextOptions<CrmDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActividadEconomica> ActividadEconomicas { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<Comunicacion> Comunicacions { get; set; }
        public virtual DbSet<Contacto> Contactos { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<DetalleOportunidad> DetalleOportunidads { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<EstadoActividad> EstadoActividads { get; set; }
        public virtual DbSet<EstadoCivil> EstadoCivils { get; set; }
        public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }
        public virtual DbSet<Etapa> Etapas { get; set; }
        public virtual DbSet<Fase> Fases { get; set; }
        public virtual DbSet<Fuente> Fuentes { get; set; }
        public virtual DbSet<FuenteTicket> FuenteTickets { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<MedioComunicacion> MedioComunicacions { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Monedum> Moneda { get; set; }
        public virtual DbSet<Oportunidad> Oportunidads { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<PerfilPermiso> PerfilPermisos { get; set; }
        public virtual DbSet<Pipeline> Pipelines { get; set; }
        public virtual DbSet<Prioridad> Prioridads { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Recordatorio> Recordatorios { get; set; }
        public virtual DbSet<Sucursal> Sucursals { get; set; }
        public virtual DbSet<Tarea> Tareas { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public virtual DbSet<TipoPedido> TipoPedidos { get; set; }
        public virtual DbSet<TipoTarea> TipoTareas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<ActividadEconomica>(entity =>
            {
                entity.HasKey(e => e.IdActividadEconomica)
                    .HasName("ActividadEconomica_pk");

                entity.ToTable("ActividadEconomica");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("Categoria_pk");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .HasDefaultValue(true);

                entity.HasOne(d => d.IdCategoriaPadreNavigation)
                    .WithMany(p => p.InverseIdCategoriaPadreNavigation)
                    .HasForeignKey(d => d.IdCategoriaPadre)
                    .HasConstraintName("Categoria_Categoria_fk");
            });

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.IdCiudad)
                    .HasName("Ciudad_pk");

                entity.ToTable("Ciudad");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Ciudads)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Departamento_Ciudad_fk");
            });

            modelBuilder.Entity<Comunicacion>(entity =>
            {
                entity.HasKey(e => e.IdComunicacion)
                    .HasName("Comunicacion_pk");

                entity.ToTable("Comunicacion");

                entity.Property(e => e.Observacion).HasMaxLength(200);

                entity.Property(e => e.Referencia).HasMaxLength(200);

                entity.Property(e => e.MotivoComunicacion).HasMaxLength(50);

                entity.HasOne(d => d.IdContactoNavigation)
                    .WithMany(p => p.Comunicacions)
                    .HasForeignKey(d => d.IdContacto)
                    .HasConstraintName("Contacto_Llamada_fk");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Comunicacions)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("Empresa_Llamada_fk");

                entity.HasOne(d => d.IdMedioComunicacionNavigation)
                    .WithMany(p => p.Comunicacions)
                    .HasForeignKey(d => d.IdMedioComunicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Medio_Comunicacion_FK");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Comunicacions)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comunicacion_FK");
            });

            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.IdContacto)
                    .HasName("Contacto_pk");

                entity.ToTable("Contacto");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CorreoLaboral).HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DireccionLaboral).HasMaxLength(50);

                entity.Property(e => e.Documento)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.NombreEmpresa).HasMaxLength(50);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TelefonoLaboral).HasMaxLength(50);

                entity.Property(e => e.Estado).HasDefaultValue(true);

                entity.Property(e => e.UltimoContacto).HasColumnType("datetime");

                entity.HasOne(d => d.IdActividadEconomicaNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.IdActividadEconomica)
                    .HasConstraintName("ActividadEconomica_Contacto_fk");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ciudad_Contacto_fk");

                entity.HasOne(d => d.IdEstadoCivilNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.IdEstadoCivil)
                    .HasConstraintName("EstadoCivil_Contacto_fk");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Contacto_FK");

                entity.HasOne(d => d.IdTipoDocumentoNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.IdTipoDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TipoDocumento_Contacto_fk");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("Departamento_pk");

                entity.ToTable("Departamento");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DetalleOportunidad>(entity =>
            {
                entity.HasKey(e => e.IdDetalleOportunidad)
                    .HasName("DetalleOportunidad_pk");

                entity.ToTable("DetalleOportunidad");

                entity.HasOne(d => d.IdOportunidadNavigation)
                    .WithMany(p => p.DetalleOportunidads)
                    .HasForeignKey(d => d.IdOportunidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Oportunidad_DetalleOportunidad_fk");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleOportunidads)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Producto_DetalleOportunidad_fk");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("Empresa_pk");

                entity.ToTable("Empresa");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CelularRepresentante)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NombreRepresentante)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ruc)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Estado).HasDefaultValue(true);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UltimoContacto).HasColumnType("datetime");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ciudad_Empresa_fk");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Empresa_FK");
            });

            modelBuilder.Entity<EstadoActividad>(entity =>
            {
                entity.HasKey(e => e.IdEstadoActividad)
                    .HasName("EstadoActividad_pk");

                entity.ToTable("EstadoActividad");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EstadoCivil>(entity =>
            {
                entity.HasKey(e => e.IdEstadoCivil)
                    .HasName("EstadoCivil_pk");

                entity.ToTable("EstadoCivil");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EstadoPedido>(entity =>
            {
                entity.HasKey(e => e.IdEstadoPedido)
                    .HasName("EstadoPedido_pk");

                entity.ToTable("EstadoPedido");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Etapa>(entity =>
            {
                entity.HasKey(e => e.IdEtapa)
                    .HasName("Etapa_pk");

                entity.ToTable("Etapa");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Fase>(entity =>
            {
                entity.HasKey(e => e.IdFase)
                    .HasName("Fase_pk");

                entity.ToTable("Fase");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Fuente>(entity =>
            {
                entity.HasKey(e => e.IdFuente)
                    .HasName("Fuente_pk");

                entity.ToTable("Fuente");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FuenteTicket>(entity =>
            {
                entity.HasKey(e => e.IdFuenteTicket)
                    .HasName("FuenteTicket__pk");

                entity.ToTable("FuenteTicket");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca)
                    .HasName("Marca_pk");

                entity.ToTable("Marca");

                entity.Property(e => e.Estado)
                    .HasDefaultValue(true);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MedioComunicacion>(entity =>
            {
                entity.HasKey(e => e.IdMedioComunicacion)
                    .HasName("MedioComunicacion_pk");

                entity.ToTable("MedioComunicacion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK__Menu__4D7EA8E15160C76C");

                entity.ToTable("Menu");

                entity.Property(e => e.Descripcion).HasMaxLength(100);

                entity.Property(e => e.MenuUrl)
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdMenuPadreNavigation)
                    .WithMany(p => p.InverseIdMenuPadreNavigation)
                    .HasForeignKey(d => d.IdMenuPadre)
                    .HasConstraintName("Menu_FK");

                entity.Property(e => e.Icono)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Monedum>(entity =>
            {
                entity.HasKey(e => e.IdMoneda)
                    .HasName("Moneda_pk");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Oportunidad>(entity =>
            {
                entity.HasKey(e => e.IdOportunidad)
                    .HasName("Oportunidad_pk");

                entity.ToTable("Oportunidad");

                entity.Property(e => e.FechaCierre).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Observacion).HasMaxLength(200);

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdContactoNavigation)
                    .WithMany(p => p.Oportunidads)
                    .HasForeignKey(d => d.IdContacto)
                    .HasConstraintName("Contacto_Oportunidad_fk");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Oportunidads)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("Empresa_Oportunidad_fk");

                entity.HasOne(d => d.IdEtapaNavigation)
                    .WithMany(p => p.Oportunidads)
                    .HasForeignKey(d => d.IdEtapa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Etapa_Oportunidad_fk");

                entity.HasOne(d => d.IdFuenteNavigation)
                    .WithMany(p => p.Oportunidads)
                    .HasForeignKey(d => d.IdFuente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fuente_Oportunidad_fk");

                entity.HasOne(d => d.IdPrioridadNavigation)
                    .WithMany(p => p.Oportunidads)
                    .HasForeignKey(d => d.IdPrioridad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Prioridad_Oportunidad_fk");

                entity.HasOne(d => d.IdPropietarioNavigation)
                    .WithMany(p => p.Oportunidads)
                    .HasForeignKey(d => d.IdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Oportunidad_FK");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Oportunidads)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sucursal_Oportunidad_fk");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido)
                    .HasName("Pedido_pk");

                entity.ToTable("Pedido");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FechaCierre).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.NumeroPedido)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RazonSocial).HasMaxLength(50);

                entity.Property(e => e.RucFacturacion).HasMaxLength(50);

                entity.Property(e => e.UltimaActualizacion).HasColumnType("datetime");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ciudad_Pedido_fk");

                entity.HasOne(d => d.IdEstadoPedidoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdEstadoPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EstadoPedido_Pedido_fk");

                entity.HasOne(d => d.IdFaseNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdFase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fase_Pedido_fk");

                entity.HasOne(d => d.IdOportunidadNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdOportunidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Oportunidad_Pedido_fk");

                entity.HasOne(d => d.IdTipoPedidoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdTipoPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TipoPedido_Pedido_fk");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil)
                    .HasName("PK__Perfil__C7BD5CC15E4CBCFB");

                entity.ToTable("Perfil");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<PerfilPermiso>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PerfilPermiso");

                entity.Property(e => e.IdPerfilPermiso).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PerfilPermiso_FK_1");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PerfilPermiso_FK");
            });

            modelBuilder.Entity<Pipeline>(entity =>
            {
                entity.HasKey(e => e.IdPipeline)
                    .HasName("Pipeline_pk");

                entity.ToTable("Pipeline");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Prioridad>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad)
                    .HasName("Prioridad_pk");

                entity.ToTable("Prioridad");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("Producto_pk");

                entity.ToTable("Producto");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Categoria_Producto_fk");

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Marca_Producto_fk");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Moneda_Producto_fk");
            });

            modelBuilder.Entity<Recordatorio>(entity =>
            {
                entity.HasKey(e => e.IdRecordatorio)
                    .HasName("Recordatorio_pk");

                entity.ToTable("Recordatorio");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("Sucursal_pk");

                entity.ToTable("Sucursal");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Sucursals)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ciudad_Sucursal_fk");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => e.IdTarea)
                    .HasName("Tarea_pk");

                entity.ToTable("Tarea");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FechaCierre).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdContactoNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdContacto)
                    .HasConstraintName("Contacto_Tarea_fk");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("Empresa_Tarea_fk");

                entity.HasOne(d => d.IdEstadoActividadNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdEstadoActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Estado_Tarea_fk");

                entity.HasOne(d => d.IdOportunidadNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdOportunidad)
                    .HasConstraintName("Oportunidad_Tarea_fk");

                entity.HasOne(d => d.IdRecordatorioNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdRecordatorio)
                    .HasConstraintName("Recordatorio_Tarea_fk");

                entity.HasOne(d => d.IdTareaAsociadaNavigation)
                    .WithMany(p => p.InverseIdTareaAsociadaNavigation)
                    .HasForeignKey(d => d.IdTareaAsociada)
                    .HasConstraintName("Tarea_Tarea_fk");

                entity.HasOne(d => d.IdTicketNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdTicket)
                    .HasConstraintName("Ticket_Tarea_fk");

                entity.HasOne(d => d.IdTipoTareaNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdTipoTarea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TipoTarea_Tarea_fk");

                entity.HasOne(d => d.IdUsuarioResponsableNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdUsuarioResponsable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Tarea_FK");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket)
                    .HasName("Ticket_pk");

                entity.ToTable("Ticket");

                entity.Property(e => e.Decripcion)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FechaCierre).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdContactoNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdContacto)
                    .HasConstraintName("Contacto_Ticket_fk");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("Empresa_Ticket_fk");

                entity.HasOne(d => d.IdEstadoActividadNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdEstadoActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Estado_Ticket_fk");

                entity.HasOne(d => d.IdFuenteTicketNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdFuenteTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FuenteTicket__Ticket_fk");

                entity.HasOne(d => d.IdPipelineNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdPipeline)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pipeline_Ticket_fk");

                entity.HasOne(d => d.IdPrioridadNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdPrioridad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Prioridad_Ticket_fk");

                entity.HasOne(d => d.IdUsuarioResponsableNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdUsuarioResponsable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ticket_FK");
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(e => e.IdTipoDocumento)
                    .HasName("TipoDocumento_pk");

                entity.ToTable("TipoDocumento");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoPedido>(entity =>
            {
                entity.HasKey(e => e.IdTipoPedido)
                    .HasName("TipoPedido_pk");

                entity.ToTable("TipoPedido");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoTarea>(entity =>
            {
                entity.HasKey(e => e.IdTipoTarea)
                    .HasName("TipoTarea_pk");

                entity.ToTable("TipoTarea");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF975104F4FF");

                entity.ToTable("Usuario");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Usuario_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
