using AutoMapper;
using FriedChickenStore.Model.DTOs;
using FriedChickenStore.Model.Entity;
using FriedChickenStore.Repository;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Xml;

namespace FriedChickenStore.Service
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper) 
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProductDto> ListAll()
        {
            IEnumerable<Product> products = _productRepository.GetAll();
            IEnumerable<ProductDto> productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productDtos;
        }

        public ProductDto ListById(int id)
        {
            Product product = _productRepository.GetById(id);
            if(product != null)
            {
                ProductDto productDto = _mapper.Map<ProductDto>(product);
                return productDto;
            }
            return null;       
        }

        public bool Add(ProductDto dto)
        {
            Product product = _mapper.Map<Product>(dto);

            if (product != null)
            {
                _productRepository.Add(product);
                return true;
            }

            return false;
        }

        public bool Update(int id, ProductDto newDto)
        {
            DateTime nowDate = DateTime.Now;
            Product oldEntity = _productRepository.GetById(id);

            if (oldEntity != null)
            {
                Product newEntity = _mapper.Map(newDto, oldEntity);
                newEntity.LastUpdateTimes = nowDate;
                _productRepository.Update(newEntity);
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            var exist = _productRepository.GetById(id);

            if (exist != null)
            {
                _productRepository.DeleteById(id);
                return true;
            }

            return false;
        }

        public bool UpdateAny(int id, ProductDto newDto)
        {
            DateTime nowDate = DateTime.Now;
            Product oldEntity = _productRepository.GetById(id);
            oldEntity = _mapper.Map(newDto, oldEntity);
            if (oldEntity != null)
            {
                oldEntity.LastUpdateTimes = nowDate;
                _productRepository.Update(oldEntity);
                return true;
            }
            return false;
        }

    }

}
