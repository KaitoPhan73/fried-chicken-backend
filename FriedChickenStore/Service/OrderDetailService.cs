using AutoMapper;
using FriedChickenStore.Model.DTOs;
using FriedChickenStore.Model.Entity;
using FriedChickenStore.Repository;
using System.Collections.Generic;

namespace FriedChickenStore.Service
{
    public class OrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper
            , IOrderRepository orderRepository
            , IProductRepository productRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<OrderDetailDto> ListAll()
        {
            IEnumerable<OrderDetail> orderDetails = _orderDetailRepository.GetAll();
            foreach (var item in orderDetails)
            {
                Product existProduct = _productRepository.GetById(item.ProductId);
                Order existOrder = _orderRepository.GetById(item.OrderId);
                item.Product = existProduct;
                item.Order = existOrder;
            }
            IEnumerable<OrderDetailDto> orderDetailDtos = _mapper.Map<IEnumerable<OrderDetailDto>>(orderDetails);
            return orderDetailDtos;
        }

        public OrderDetailDto ListById(int id)
        {       
            OrderDetail orderDetail = _orderDetailRepository.GetById(id);
            if (orderDetail != null)
            {
                Product existProduct = _productRepository.GetById(orderDetail.ProductId);
                Order existOrder = _orderRepository.GetById(orderDetail.OrderId);
                orderDetail.Product = existProduct;
                orderDetail.Order = existOrder;
                OrderDetailDto orderDetailDto = _mapper.Map<OrderDetailDto>(orderDetail);

                return orderDetailDto;
            }
            return null;
        }

        public bool Add(OrderDetailDto dto)
        {
            Product existProduct = _productRepository.GetById(dto.ProductId);
            Order existOrder = _orderRepository.GetById(dto.OrderId);

            if (existProduct != null && existOrder != null)
            {
                dto.price = existProduct.Price * dto.Quantity;
                OrderDetail orderDetail = _mapper.Map<OrderDetail>(dto);
                _orderDetailRepository.Add(orderDetail);
                return true;
            }
            else
            {
                return false; // Trả về false nếu không tìm thấy Product hoặc Order
            }
        }

        public bool Update(int id, OrderDetailDto newDto)
        {
            DateTime nowDate = DateTime.Now;
            OrderDetail oldEntity = _orderDetailRepository.GetById(id);
            Product product = _productRepository.GetById(oldEntity.ProductId);
            double totalPrice = newDto.Quantity * product.Price;

            if (oldEntity != null)
            {
                newDto.price = totalPrice;
                newDto.OrderId = oldEntity.OrderId;
                newDto.ProductId = oldEntity.ProductId;
                OrderDetail newEntity = _mapper.Map(newDto, oldEntity);
                newEntity.LastUpdateTimes = nowDate;
                _orderDetailRepository.Update(newEntity);
                return true;
            }

            return false; 
        }

        public bool Delete(int id)
        {
            var exist = _orderDetailRepository.GetById(id);

            if (exist != null)
            {
                _orderDetailRepository.DeleteById(id);
                return true;
            }

            return false; 
        }

        public bool UpdateAny(int id, OrderDetailDto newDto)
        {
            
            DateTime nowDate = DateTime.Now;
            OrderDetail oldEntity = _orderDetailRepository.GetById(id);
            Product product = _productRepository.GetById(oldEntity.ProductId);
            double totalPrice = newDto.Quantity * product.Price;
            newDto.price = totalPrice;
            newDto.OrderId = oldEntity.OrderId;
            newDto.ProductId = oldEntity.ProductId;

            oldEntity = _mapper.Map(newDto, oldEntity);
            if (oldEntity != null)
            {
                oldEntity.LastUpdateTimes = nowDate;
                _orderDetailRepository.Update(oldEntity);
                return true;
            }
            return false;
        }

    }
}
