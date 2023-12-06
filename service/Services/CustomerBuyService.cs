using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service.Services;

public class CustomerBuyService
{
    private readonly CustomerBuyRepository _customerBuyRepository;

    public CustomerBuyService(CustomerBuyRepository customerBuyRepository)
    {
        _customerBuyRepository = customerBuyRepository;
    }

    public IEnumerable<CustomerBuyModel> GetAllCustomerBuy()
    {
        return _customerBuyRepository.GetAllCustomerBuy();
    }

    public CustomerBuyModel CreateCustomerBuy(CustomerBuyModel customerBuy)
    {
        return _customerBuyRepository.CreateCustomerBuy(customerBuy);
    }
    
    public void deleteCustomerBuy(int customer_buy_id)
    {
        _customerBuyRepository.DeleteCustomerBuy(customer_buy_id);
    }
}