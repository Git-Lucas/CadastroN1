using AutoMapper;
using Cadastro.Domain.Entities;
using Cadastro.Domain.Interfaces;
using Cadastro.Interfaces;
using Cadastro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Services
{
    public class ProductViewModelService : IProductViewModelService
    {
        private readonly IProductRepository _productRepository;
        private readonly IClientViewModelService _clientViewModelService;
        private readonly IMapper _mapper;

        public ProductViewModelService(IProductRepository productRepository, IClientViewModelService clientViewModelService, IMapper mapper)
        {
            _productRepository = productRepository;
            _clientViewModelService = clientViewModelService;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public ProductViewModel Get(int id)
        {
            var entity = _productRepository.Get(id);
            if (entity == null)
                return null;
            else
                entity.Client = _mapper.Map<Client>(_clientViewModelService.Get(entity.IdClient));

            return _mapper.Map<ProductViewModel>(entity);
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            var list = _productRepository.GetAll();
            if (list == null)
                return new ProductViewModel[] { };
            else
            {
                foreach (var product in list)
                {
                    product.Client = _mapper.Map<Client>(_clientViewModelService.Get(product.IdClient));
                }
            }

            return _mapper.Map<IEnumerable<ProductViewModel>>(list);
        }

        public IEnumerable<ClientViewModel> GetAllActiveClients()
        {
            var list = _clientViewModelService.GetAll();
            var listActiveClients = list.Where(x => x.Ative == true)
                                        .OrderBy(x => x.Name);

            if (listActiveClients == null)
                return new ClientViewModel[] { };

            return listActiveClients;
        }

        public void Insert(ProductViewModel viewModel)
        {
            var entity = _mapper.Map<Product>(viewModel);

            _productRepository.Insert(entity);
        }

        public void Update(ProductViewModel viewModel)
        {
            var entity = _mapper.Map<Product>(viewModel);

            _productRepository.Update(entity);
        }
    }
}
