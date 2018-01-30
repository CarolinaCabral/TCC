using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;
using CoffeeExperience.Domain.Interfaces.Services;
using CoffeeExperience.Domain.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.Domain.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
            : base(unitOfWork.ProductRepository)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<Product> GetAllByLot(string LotCode, bool isAsNoTracking)
        {
            try
            {
                if (string.IsNullOrEmpty(LotCode))
                {
                    throw new Exception("Código inválido");
                }
                else
                {
                    Lot lot = null;
                    if (isAsNoTracking)
                    {
                        lot = unitOfWork.LotRepository.GetByCodeAsNoTracking(LotCode);
                    }
                    else
                    {
                        lot = unitOfWork.LotRepository.GetByCode(LotCode);
                    }

                    if (lot == null)
                    {
                        throw new Exception("Lote não encontrado");
                    }
                    else
                    {
                        List<Product> produto = null;

                        if (isAsNoTracking)
                        {
                            produto = unitOfWork.ProductRepository.GetManyAsNoTracking(p => p.LotId == lot.Id && p.Status == EnmStatus.Enabled).ToList();
                        }
                        else
                        {
                            produto = unitOfWork.ProductRepository.GetMany(p => p.LotId == lot.Id && p.Status == EnmStatus.Enabled).ToList();

                        }
                        if (produto == null || produto.Count() == 0)
                        {
                            throw new Exception("Não existe produto cadastrado neste lote");
                        }
                        return produto;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Response Save(Product product)
        {
            if (product.Id > 0)
            {
                if (string.IsNullOrEmpty(product.Name) || product.Weight == 0)
                {
                    return new Response(true, "Campos inválidos");
                }
                else
                {
                    unitOfWork.ProductRepository.Update(product);
                }
            }
            else
            {
                Product productExists = unitOfWork.ProductRepository.Get(p => p.Name == product.Name && p.LotId == product.LotId && p.Status == EnmStatus.Enabled);

                if (productExists == null)
                {
                    unitOfWork.ProductRepository.Add(product);
                }
                else
                {
                    return new Response(true, "Produto já cadastrado");
                }

            }
            unitOfWork.Commit();
            return new Response(false);
        }

        public override Product GetById(int id)
        {
            if (id < 0)
            {
                throw new Exception("Código inválido");
            }
            else if (base.GetById(id) == null)
            {
                throw new Exception("Produto não encontrado");
            }
            else
            {
                return base.GetById(id);
            }
        }

        public Response Exclude(int ProductId)
        {
            try
            {
                Product product = unitOfWork.ProductRepository.Get(x => x.Id.Equals(ProductId));

                if (product == null)
                {
                    return new Response(false, "Produto não encontrado!");
                }
                else
                {

                    if (product.Status != EnmStatus.Deleted)
                    {
                        product.Delete();
                        Update(product);
                        unitOfWork.Commit();
                        return new Response(true);
                    }
                    else
                    {
                        return new Response(false, "Este produto já foi excluído!");
                    }
                }

            }
            catch (Exception e)
            {

                return new Response(false, e.Message);
            }

        }
    }
}
