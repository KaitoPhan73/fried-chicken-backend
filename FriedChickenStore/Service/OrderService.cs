using AutoMapper;
using FriedChickenStore.CoreHelper;
using FriedChickenStore.Model.DTOs;
using FriedChickenStore.Model.Entity;
using FriedChickenStore.Repository;

namespace FriedChickenStore.Service
{
    public class OrderService 
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository,IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public IEnumerable<OrderDto> ListAll()
        {
            IEnumerable<Order> Orders = _orderRepository.GetAll();
            foreach (var item in Orders)
            {
                IEnumerable<OrderDetail> orderDetail = _orderDetailRepository.GetAllByOrderId(item.Id);
                double totalPrice = 0.0;
                foreach (var item1 in orderDetail)
                {
                    totalPrice += item1.price;
                }
                item.price = totalPrice;
                _orderRepository.Update(item);
            }
            
            IEnumerable<OrderDto> OrderDtos = _mapper.Map<IEnumerable<OrderDto>>(Orders);
            return OrderDtos;
        }

        public IEnumerable<OrderDto> ListAllByOrderStatus(String orderStatus)
        {
            IEnumerable<Order> Orders = _orderRepository.GetAllByOrderStatus(orderStatus);
            foreach (var item in Orders)
            {
                IEnumerable<OrderDetail> orderDetail = _orderDetailRepository.GetAllByOrderId(item.Id);
                double totalPrice = 0.0;
                foreach (var item1 in orderDetail)
                {
                    totalPrice += item1.price;
                }
                item.price = totalPrice;
                _orderRepository.Update(item);
            }

            IEnumerable<OrderDto> OrderDtos = _mapper.Map<IEnumerable<OrderDto>>(Orders);
            return OrderDtos;
        }

        public OrderDto ListById(int id)
        {
            Order order = _orderRepository.GetById(id);
            if (order != null)
            {
                IEnumerable<OrderDetail> orderDetail = _orderDetailRepository.GetAllByOrderId(id);
                double totalPrice = 0.0;
                foreach (var item1 in orderDetail)
                {
                    totalPrice += item1.price;
                }
                order.price = totalPrice;
                _orderRepository.Update(order);
                OrderDto orderDto = _mapper.Map<OrderDto>(order);
                return orderDto;
            }
            return null;
        }

        public bool Add(OrderDto dto)
        {
            dto.OrderStatus = OrderStatus.CONFIRM.ToString();
            Order order = _mapper.Map<Order>(dto);

            if (order != null)
            {
                _orderRepository.Add(order);
                return true;
            }

            return false;
        }

        public bool Update(int id, OrderDto newDto)
        {
            DateTime nowDate = DateTime.Now;
            Order oldEntity = _orderRepository.GetById(id);

            if (oldEntity != null)
            {
                newDto.UserId = oldEntity.UserId;
                Order newEntity = _mapper.Map(newDto, oldEntity);
                newEntity.LastUpdateTimes = nowDate;
                _orderRepository.Update(newEntity);
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            var exist = _orderRepository.GetById(id);

            if (exist != null)
            {
                _orderRepository.DeleteById(id);
                return true;
            }

            return false;
        }

        public bool UpdateAny(int id, OrderDto newDto)
        {
            DateTime nowDate = DateTime.Now;
            Order oldEntity = _orderRepository.GetById(id);
            newDto.UserId = oldEntity.UserId;
            oldEntity = _mapper.Map(newDto, oldEntity);
            if (oldEntity != null)
            {
                oldEntity.LastUpdateTimes = nowDate;
                _orderRepository.Update(oldEntity);
                return true;
            }
            return false;
        }
    }
}
