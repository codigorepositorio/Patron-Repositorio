﻿using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository Owner { get; }
        IAccountRepository Account { get; }
        IArticuloRepository Articulo { get; }
        ICategoriaRepository Categoria { get; }
        IPersonaRepository Persona { get; }
        IItemRepository Item { get; }
        ICustomerRepository Customer { get; }
        IOrderRepository Order { get; }
        IOrderItemsRepository OrderItems { get; }
        IVentaRepository Venta { get; }
        IDetalleVentaRepository DetalleVenta { get; }
        Task SaveAsync();
    }
}
